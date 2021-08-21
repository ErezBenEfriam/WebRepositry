using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models
{
    public class Animal
    {
        public int AnimalId { get; set; } //PK

        [StringLength(40, ErrorMessage = "{0}  must be between {2} and {1} .", MinimumLength = 2)]//must be at length of 2-40
        [Required(ErrorMessage = "Please enter a name")]                                           //Regured Filed
        [RegularExpression("^[A-Z].+", ErrorMessage = "Name must start with capital letter")]      //must start with capitl letter
        public string Name { get; set; }

        [Display (Name= "image url")]                                                                         //how to display the filed at htmlcs
        [RegularExpression("^(http|https)://.+", ErrorMessage = "url must start with https://(or http...) ")] //pattern of photo
        [Required(ErrorMessage = "cannot be empty")]                                                         //Regured Filed
        public string PictureName { get; set; }

        [RegularExpression(".{20,300}", ErrorMessage = "Description must be between 20-300")]        //must be at length 20-300
        [Required(ErrorMessage = "Description can not be empty")]                                     //Regured Filed
        public string Description { get; set; }
       
        public int CategoryId { get; set; } //Fk Id
        public  Category category { get; set; }//Fk Refernce
        public List<Comment> Comments { get; set; } //Refernce of collection- one to mant


    }
}
