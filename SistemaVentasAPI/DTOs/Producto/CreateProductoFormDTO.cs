namespace SistemaVentasAPI.DTOs.Producto
{
    public class CreateProductoFormDTO
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public IFormFile? Imagen { get; set; }

        public int IdMarca { get; set; }
        public decimal PrecioVenta { get; set; }
        public List<int> Categorias { get; set; } = new();
    }
}
