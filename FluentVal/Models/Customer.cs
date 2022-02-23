﻿namespace FluentVal.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int Age { get; set; }
        public DateTime? BirthDay{ get; set; }


        // ICollection Yerine IList  kulllanma sebebi
        // Customer.Adress[1] şeklinde indexır kullanmak
        public IList<Adress> Adresses { get; set; }=new List<Adress>();
    }

}
