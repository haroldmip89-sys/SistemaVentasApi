namespace SistemaVentasAPI.DTOs.Categoria
{
    public class UpdateCategoriaRequestDTO
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? ColorHex { get; set; }
    }
}
