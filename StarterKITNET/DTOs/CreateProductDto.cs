namespace StarterKITNET.DTOs
{
    public class CreateProductDto
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int? Stock { get; set; }
    }
}
