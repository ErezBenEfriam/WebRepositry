using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ZooContext : DbContext
    {
        public ZooContext(DbContextOptions<ZooContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(x => x.Animals);
            modelBuilder.Entity<Animal>().HasMany(x => x.Comments);

            #region discription
            string discriptForShore = "shorebirds are birds of the order Charadriiformes commonly found alonfg shorelines and mudflats that wade in order to forage for food (such as insects or crustaceans) in the mud or sand";
            string discripForDog = "The domestic dog (Canis familiaris or Canis lupus familiaris)is a domesticated descendant of the wolf. The dog derived from an ancient, extinct wolf, and the modern grey wolf is the dog's nearest living relative.";
            string discripFopigeon = "The domestic pigeon (Columba livia domestica) is a pigeon subspecies that was derived from the rock dove (also called the rock pigeon).";
            string discripForKingcobra = "The king cobra (Ophiophagus hannah) is a large elapid endemic to forests from India through Southeast Asia. It is the world's longest venomous snake Adult king cobras are 3.18 to 4 m  long on average.";
            string discripForpythone = "The Pythonidae, commonly known as pythons, are a family of nonvenomous snakes found in Africa, Asia, and Australia. Among its members are some of the largest snakes in the world.";
            string dicForCat = "The cat  is a domestic species of small carnivorous mammal. It is the only domesticated species in the family Felidae and is often referred to as the domestic cat to distinguish it from the wild members of the family.";
            string discriptionForEagle = "Eagle is the common name for many large birds of prey of the family Accipitridae. Eagles belong to several groups of genera, some of which are closely related. Most of the 60 species of eagle are from Eurasia and Africa.";
            #endregion
            #region ImageUrl
            string picForshorebird = @"https://files.dnr.state.mn.us/mcvmagazine/issues/2019/mar-apr/img//shorebirds/shorebirds02.jpg";
            string picForsDog = @"https://leaderreaderjournal.com/wp-content/uploads/2021/01/dog.jpg";
            string picForspigeon = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQZR6Mjs59dCEoroTHU9O-XzFpCOoVCO9B3WvGUUkEfnAvcSQSI30SKjnmEGjxvI2SFCwJlvD0tO-d8AQ&usqp=CAU";
            string picForsKingcobra = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUk_ZSWNcsQ1nBlVeIBopJPCZR3estFC8-vXj2bC_TR4np8O3NSmnDYJip7PQnVXQUle8pyh5uhOiMpw&usqp=CAU";
            string picForspython = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Python_molurus_molurus_2.jpg/220px-Python_molurus_molurus_2.jpg";
            string PicForCat = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRn87Ff2bKwFhYygHGTqF5_8pjaPyV6lTUyyQ&usqp=CAU";
            string PicForEagle = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ1V_mn2tNaE8q3A40BzEcYFmCRnfJYgS70iQ&usqp=CAU";

            #endregion

            modelBuilder.Entity<Category>().HasData(
                new { CategoryId = 1, Name = "mammels" },
                new { CategoryId = 2, Name = "birds"},
                new { CategoryId = 3, Name = "reptiles" }
            );
            modelBuilder.Entity<Animal>().HasData(
                new { AnimalId = 1, Name = "Dog", PictureName = picForsDog, Description = discripForDog, CategoryId=1 },
                new { AnimalId = 2, Name = "Pigeon", PictureName = picForspigeon, Description = discripFopigeon, CategoryId = 2},
                new { AnimalId = 3, Name = "ShoreBirds", PictureName = picForshorebird, Description = discriptForShore, CategoryId = 2},
                new { AnimalId = 4, Name = "King cobra", PictureName = picForsKingcobra, Description = discripForKingcobra, CategoryId = 3},
                new { AnimalId = 5, Name = "Python", PictureName = picForspython, Description = discripForpythone, CategoryId = 3},
                new { AnimalId = 6, Name = "Cat", PictureName = PicForCat, Description = dicForCat, CategoryId = 1},
                new { AnimalId = 7, Name = "Eagle", PictureName = PicForEagle, Description = discriptionForEagle, CategoryId = 2}
            );
            modelBuilder.Entity<Comment>().HasData(
                new { CommentId = 1, CommentString = "firstcomment", AnimalId = 1, CategoryId = 1 },
                new { CommentId = 2, CommentString = "secondcomment", AnimalId = 1, CategoryId = 1 },
                new { CommentId = 3, CommentString = "thirdcomment", AnimalId = 2, CategoryId = 2 },
                new { CommentId = 4, CommentString = "forthcommentcomment", AnimalId = 2, CategoryId = 2 },
                new { CommentId = 5, CommentString = "fivecomment", AnimalId = 3, CategoryId = 2 },
                new { CommentId = 6, CommentString = "sixcomment", AnimalId = 3, CategoryId = 2 },
                new { CommentId = 7, CommentString = "Very cool, I like it", AnimalId = 3, CategoryId = 2 },
                new { CommentId = 8, CommentString = "Wow", AnimalId = 4, CategoryId = 3 },
                new { CommentId = 9, CommentString = "King Cobraaa", AnimalId = 4, CategoryId =3  },
                new { CommentId = 10, CommentString = "Amazing", AnimalId = 5, CategoryId = 3 },
                new { CommentId = 11, CommentString = "Cool Animal", AnimalId = 6, CategoryId = 1 },
                new { CommentId = 12, CommentString = "I like cats", AnimalId = 6, CategoryId = 1 },
                new { CommentId = 13, CommentString = "Most cool bird", AnimalId = 7, CategoryId = 2 },
                new { CommentId = 14, CommentString = "Amrican Eagle", AnimalId = 7, CategoryId = 2 },
                new { CommentId = 15, CommentString = "i love Dogs", AnimalId = 1, CategoryId = 1 },
                new { CommentId = 16, CommentString = "nice image", AnimalId = 1, CategoryId = 1 },
                new { CommentId = 17, CommentString = "Cute Dog!", AnimalId = 1, CategoryId = 1 }
            );
        }
    }
}
