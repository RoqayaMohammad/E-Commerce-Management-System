namespace E_Commerce_Management_System.Modules.ProductsModule.Dtos
{
    public class UpdateProductDto:CreateProductDto
    {
        public bool IsActive { get; set; } = true;

    }
}
