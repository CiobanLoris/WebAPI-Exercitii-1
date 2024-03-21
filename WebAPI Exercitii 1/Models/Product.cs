namespace WebAPI_Exercitii_1.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int[] ratings { get; set; }

        public DateTime CreatedON { get; set; }
    }
}
