
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication1.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //For making Every new comment generate id by itself
        public int CommentId { get; set; }  //Pk
        public string CommentString { get; set; }
        public int AnimalId { get; set; } //Fk
        public  Animal animal { get; set; } //Fk Refernce
    }
}
