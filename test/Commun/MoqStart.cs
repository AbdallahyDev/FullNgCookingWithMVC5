using Models.Recettes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Commun
{
    public class MoqStart
    {
       
        public static IQueryable<Recette> getRecetteData()
        {
            IQueryable<Recette> data;
            #region data generation 
            data = new List<Recette>
            {
                new Recette { Id=1,
                    Name = "aaaa",
                    Calories=452.25f,
                    Category = "",
                    Comments = null,
                    Ingredients = null,
                    CreationDate = DateTime.Now,
                    CreatorId = "gggg",
                    IsAvailable = true,
                    Picture = null,
                    Preparation = ""
                },
                new Recette { Id=2,
                    Name = "Tajine de poulet",
                    Calories=452.25f,
                    Category = "",
                    Comments = null,
                    Ingredients = null,
                    CreationDate = DateTime.Now,
                    CreatorId = "gggg",
                    IsAvailable = true,
                    Picture = null,
                    Preparation = ""
                },
                new Recette { Id=3,
                    Name = "BBB",
                    Calories=452.25f,
                    Category = "",
                    Comments = null,
                    Ingredients = null,
                    CreationDate = DateTime.Now,
                    CreatorId = "gggg",
                    IsAvailable = true,
                    Picture = null,
                    Preparation = ""
                },
            }.AsQueryable();
            #endregion
            return data;        
        }
    }
}
