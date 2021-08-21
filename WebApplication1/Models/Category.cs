using System.Collections.Generic;


namespace WebApplication1.Models
{
    public class Category
    {
        public int CategoryId { get; set; } //Pk
        public string Name { get; set; }
        
        public List<Animal> Animals { get; set; } //refernce to Animals - one to many

    }
}
