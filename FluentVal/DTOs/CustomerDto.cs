namespace FluentVal.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Isim { get; set; } = "";
        public string EPosta{ get; set; } = "";
        public int Yas{ get; set; }

        public string FullName { get; set; }

        // önce class ismini sonra property ismini yazarsak otomatik dönüştürme işlemi yapar.
        public string Number { get; set; }
        public DateTime ValidDate { get; set; }
    }

}
