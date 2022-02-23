namespace FluentVal.Models
{
    public class Adress
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
        public string Province { get; set; } = "";// il bilgisi
        public string PostCode { get; set; } = "";



        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = new();
    }
}
