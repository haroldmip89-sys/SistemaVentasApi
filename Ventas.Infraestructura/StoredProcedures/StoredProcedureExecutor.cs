using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Compras.DTOs;
using Ventas.Aplicacion.Features.Ventas.DTOs;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Modelos;
using Ventas.Infraestructura.Context;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ventas.Infraestructura.StoredProcedures
{
    public class StoredProcedureExecutor : IStoredProcedureExecutor
    {
        private readonly ApplicationDbContext _context;

        public StoredProcedureExecutor(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<DbConnection> GetConnection()
        {
            var conn = _context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();
            return conn;
        }

        public async Task<int> CrearVentaAsync(
            int idUsuario,
            string comprobante,
            string? tipoDocumento,
            string? numeroDocumento,
            string? clienteNombre,
            string? clienteEmail,
            List<DetalleVentaDTO> detalles)
                {
                    var conn = await GetConnection();
                    using var cmd = conn.CreateCommand();

                    cmd.CommandText = @"SELECT crear_venta(
                @p_id_usuario,
                @p_comprobante,
                @p_tipo_documento,
                @p_numero_documento,
                @p_cliente_nombre,
                @p_cliente_email,
                @p_detalles::jsonb
            )";
            cmd.CommandType = CommandType.Text;
            var detallesJson = JsonSerializer.Serialize(
                detalles.Select(d => new
                {
                    id_producto = d.IdProducto,
                    cantidad = d.Cantidad
                })
            );

            cmd.Parameters.Add(new NpgsqlParameter("p_id_usuario", idUsuario));
            cmd.Parameters.Add(new NpgsqlParameter("p_comprobante", comprobante));
            cmd.Parameters.Add(new NpgsqlParameter("p_tipo_documento", (object?)tipoDocumento ?? DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_numero_documento", (object?)numeroDocumento ?? DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_cliente_nombre", (object?)clienteNombre ?? DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_cliente_email", (object?)clienteEmail ?? DBNull.Value));

            
            cmd.Parameters.Add(new NpgsqlParameter("p_detalles", NpgsqlDbType.Jsonb)
            {
                Value = detallesJson
            });

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<int> CrearCompraAsync(
            int idProveedor,
            int idUsuario,
            List<DetalleCompraDTO> detalles)
                {
                    var conn = await GetConnection();
                    using var cmd = conn.CreateCommand();

                    cmd.CommandText = "SELECT crear_compra(@p_id_proveedor, @p_id_usuario, @p_detalles::jsonb)";

            cmd.CommandType = CommandType.Text;

            var detallesJson = System.Text.Json.JsonSerializer.Serialize(
                detalles.Select(d => new
                {
                    id_producto = d.IdProducto,
                    cantidad = d.Cantidad,
                    precio_compra = d.PrecioCompra
                })
            );

            cmd.Parameters.Add(new NpgsqlParameter("p_id_proveedor", idProveedor));
            cmd.Parameters.Add(new NpgsqlParameter("p_id_usuario", idUsuario));

            cmd.Parameters.Add(
                    new NpgsqlParameter(
                        "p_detalles",
                        NpgsqlTypes.NpgsqlDbType.Jsonb)
                    {
                        Value = detallesJson
                    });

            var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
        }

        public async Task RegistrarPagoAsync(int idVenta, string metodoPago, decimal monto)
        {
            var conn = await GetConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT registrar_pago(@p_id_venta, @p_metodo_pago, @p_monto)";

            cmd.Parameters.Add(new NpgsqlParameter("p_id_venta", idVenta));
            cmd.Parameters.Add(new NpgsqlParameter("p_metodo_pago", metodoPago));
            cmd.Parameters.Add(new NpgsqlParameter("p_monto", monto));

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task AnularVentaAsync(int idVenta)
        {
            var conn = await GetConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT anular_venta(@p_id_venta)";

            cmd.Parameters.Add(new NpgsqlParameter("p_id_venta", idVenta));

            await cmd.ExecuteNonQueryAsync();
        }
        public async Task EmitirComprobanteAsync(int idVenta, string? email)
        {
            var conn = await GetConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM emitir_comprobante(@p_id_venta, @p_email)";

            cmd.Parameters.Add(new NpgsqlParameter("p_id_venta", idVenta));
            cmd.Parameters.Add(new NpgsqlParameter("p_email", (object?)email ?? DBNull.Value));

            await cmd.ExecuteNonQueryAsync();
        }


        //public async Task<int> CrearCompraAsync(int idProveedor, int idUsuario, DataTable detalles)
        //{
        //    using var conn = await GetConnection();
        //    using var cmd = conn.CreateCommand();

        //    cmd.CommandText = "CrearCompra";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
        //    cmd.Parameters.Add(new SqlParameter("@IdUsuario", idUsuario));
        //    cmd.Parameters.Add(new SqlParameter("@Detalles", detalles)
        //    {
        //        SqlDbType = SqlDbType.Structured,
        //        TypeName = "TipoDetalleCompra"
        //    });

        //    var result = await cmd.ExecuteScalarAsync();
        //    return Convert.ToInt32(result);
        //}

        //public async Task<int> CrearVentaAsync(
        //    int idUsuario,
        //    string comprobante,
        //    string? tipoDocumento,
        //    string? numeroDocumento,
        //    string? clienteNombre,
        //    string? clienteEmail,
        //    DataTable detalles)
        //{
        //    using var conn = await GetConnection();
        //    using var cmd = conn.CreateCommand();

        //    cmd.CommandText = "CrearVenta";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add(new SqlParameter("@IdUsuario", idUsuario));
        //    cmd.Parameters.Add(new SqlParameter("@Comprobante", comprobante));
        //    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", (object?)tipoDocumento ?? DBNull.Value));
        //    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", (object?)numeroDocumento ?? DBNull.Value));
        //    cmd.Parameters.Add(new SqlParameter("@ClienteNombre", (object?)clienteNombre ?? DBNull.Value));
        //    cmd.Parameters.Add(new SqlParameter("@ClienteEmail", (object?)clienteEmail ?? DBNull.Value));

        //    cmd.Parameters.Add(new SqlParameter("@Detalles", detalles)
        //    {
        //        SqlDbType = SqlDbType.Structured,
        //        TypeName = "TipoDetalleVenta"
        //    });

        //    var result = await cmd.ExecuteScalarAsync();
        //    return Convert.ToInt32(result);
        //}

        //public async Task RegistrarPagoAsync(int idVenta, string metodoPago, decimal monto)
        //{
        //    using var conn = await GetConnection();
        //    using var cmd = conn.CreateCommand();

        //    cmd.CommandText = "RegistrarPago";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add(new SqlParameter("@IdVenta", idVenta));
        //    cmd.Parameters.Add(new SqlParameter("@MetodoPago", metodoPago));
        //    cmd.Parameters.Add(new SqlParameter("@Monto", monto));

        //    await cmd.ExecuteNonQueryAsync();
        //}

        //public async Task EmitirComprobanteAsync(int idVenta, string? email)
        //{
        //    var conn = await GetConnection();
        //    using var cmd = conn.CreateCommand();

        //    cmd.CommandText = "EmitirComprobante";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@IdVenta", idVenta));
        //    cmd.Parameters.Add(new SqlParameter("@EmailDestino", (object?)email ?? DBNull.Value));

        //    await cmd.ExecuteNonQueryAsync();

        ////Escenario A(Sin error): Llamas al SP->El SP hace su trabajo -> El using cierra la conexión->La ejecución termina y se envía la respuesta al usuario. (Aquí no notas nada porque ya no necesitas la conexión para nada más).

        ////Escenario B(Tu problema actual):

        ////Llamas al SP.

        ////El using destruye la conexión del contexto.

        ////Intentas usar el Repositorio(GetVentaByIdAsync).

        ////El Repositorio intenta usar el mismo contexto, pero este le dice: "Oye, alguien cerró y limpió mi ConnectionString, ya no sé a dónde conectarme". ¡BOOM! Error.
        //}

        //public async Task AnularVentaAsync(int idVenta)
        //{
        //    using var conn = await GetConnection();
        //    using var cmd = conn.CreateCommand();

        //    cmd.CommandText = "AnularVenta";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add(new SqlParameter("@IdVenta", idVenta));

        //    await cmd.ExecuteNonQueryAsync();
        //}
    }
}
