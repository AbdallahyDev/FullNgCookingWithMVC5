using FullNgCookingWithMVC5.Models;
using Model.Comments;
using Models.Communities;
using Models.Ctegories;
using Models.Ingredients;
using Models.Recettes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullNgCookingWithMVC5.Services
{
    //public class FilterConditions {
    //    public IQueryable query;
    //}

    //public static class LoadFilters {
    //    public static Dictionary<string, FilterConditions> LoadFindByIdCondition(NgCookingDbContext _cntx,int Id) {

    //        IQueryable query_singleUser =  Queryable.Select(
    //                                                          _cntx.Users,
    //                                                          cust => cust.Id == Id.ToString());



    //        Dictionary <string, FilterConditions> s = new Dictionary<string, FilterConditions>();
    //        s.Add("Communaute", new FilterConditions() { query = query_singleUser });
    //        //s.Add("Comment", new FilterConditions() { query = "_cntx.Comments.Single(x => x.Id == myid)" });

    //        return s;
    //       }
    //}
    public class NgCookingServices
    {
        private NgCookingDbContext _cntx;
        public NgCookingServices(NgCookingDbContext cntx)
        {
            _cntx = cntx;
        }
        public NgCookingServices()
        {

        }
        public Object FindById(int id, string tableName)
        {
            object res = null;
            switch (tableName)
            {
                case "Communaute":
                    res = _cntx.Users.Single(x => x.Id == id.ToString());
                    break;
                case "Comment":
                    res = _cntx.Comments.Single(x => x.Id == id);
                    break;
                case "Recette":
                    res = _cntx.Recettes.Single(x => x.Id.Equals(id));
                    break;
                case "Ingredient":
                    res = _cntx.Ingredients.Single(x => x.Id == id);
                    break;
                case "Category":
                    res = _cntx.Categories.Single(x => x.Id == id);
                    break;
                default:
                    break;
            }
            return res;
        }

        internal object OrderCommunities(string orderBy, List<NgCookingUser> communitiesList)
        {
            List<NgCookingUser> results = null;
            List<NgCookingUser> usersWithRecette;
            List<NgCookingUser> usersWithoutRecette;
            try
            {
                switch (orderBy)
                {
                    case "az":
                        results = communitiesList.OrderBy(x => x.FirstName).ToList();
                        break;
                    case "za":
                        results = communitiesList.OrderByDescending(x => x.FirstName).ToList();
                        break;
                    case "exp":
                        usersWithRecette = communitiesList.Where(u => getRecettesByUserId(u.Id).Count > 0).OrderBy(x => getRecettesByUserId(x.Id).Select(y => y.Comments.Sum(z => z.Mark) / getRecettesByUserId(x.Id).Count)).ToList();
                        usersWithoutRecette = communitiesList.Where(u => getRecettesByUserId(u.Id).Count == 0).ToList();
                        results = usersWithRecette.Concat(usersWithoutRecette).ToList();
                        break;
                    case "nov":
                        usersWithRecette = communitiesList.Where(u => getRecettesByUserId(u.Id).Count > 0).OrderByDescending(x => getRecettesByUserId(x.Id).Select(y => y.Comments.Sum(z => z.Mark) / getRecettesByUserId(x.Id).Count)).ToList();
                        usersWithoutRecette = communitiesList.Where(u => getRecettesByUserId(u.Id).Count == 0).ToList();
                        results = usersWithoutRecette.Concat(usersWithRecette).ToList();
                        break;
                    case "prod":
                        results = communitiesList.OrderByDescending(x => getRecettesByUserId(x.Id).Count).ToList();
                        break;
                    case "prod_desc":
                        results = communitiesList.OrderBy(x => getRecettesByUserId(x.Id).Count).ToList();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("le message d'erreur :" + e.Message);
            }
            return results;
        }
        public List<Recette> getRecettesByUserId(string userId)
        {
            if (userId == null)
            {
                return null;
            }
            var userRecettes = _cntx.Recettes.Where(x => x.CreatorId.Equals(userId));
            return userRecettes.ToList();

        }

        public List<Ingredient> getFilteredIngredients(string subName, int categorieId, float minCalorieValue, float maxCalorieValue)
        {
            List<Ingredient> res;
            var categorie = _cntx.Categories.Find(categorieId);
            if (subName != "" && categorie != null)
            {
                res = _cntx.Ingredients.Where(x => x.Name.ToLower().Contains(subName.ToLower()) && x.Calories < maxCalorieValue && x.Calories > minCalorieValue &&
                x.Category.ToLower().Contains(categorie.Name.ToLower())).ToList();
            }
            else
            {
                if (subName == "" && categorie == null)
                {

                    res = _cntx.Ingredients.Where(x => x.Calories < maxCalorieValue && x.Calories > minCalorieValue).ToList();
                }
                else
                {
                    if (subName != "")
                    {
                        res = _cntx.Ingredients.Where(x => x.Name.ToLower().Contains(subName.ToLower()) && x.Calories < maxCalorieValue && x.Calories > minCalorieValue).ToList();
                    }
                    else
                    {
                        res = _cntx.Ingredients.Where(x => x.Calories < maxCalorieValue && x.Calories > minCalorieValue &&
                         x.Category.ToLower().Contains(categorie.Name.ToLower())).ToList();
                    }
                }
            }
            return res;
        }

        public Object FindByName(string name, string tableName)
        {
            object res = null;
            switch (tableName)
            {
                case "Communaute":
                    res = _cntx.Users.Single(x => x.FirstName == name);
                    break;
                case "Comment":
                    res = _cntx.Comments.Single(x => x.Title == name);
                    break;
                case "Recette":
                    res = _cntx.Recettes.Single(x => x.Name == name);
                    break;
                case "Ingredient":
                    res = _cntx.Ingredients.Single(x => x.Name == name);
                    break;
                case "Category":
                    res = _cntx.Categories.Single(x => x.Name == name);
                    break;
                default:
                    break;
            }
            return res;

        }
        public List<Recette> getFilteredRecettes(string subName, string ingsName, float minCalorieValue, float maxCalorieValue, string orderBy)
        {
            List<Recette> res;
            string[] sd = ingsName.Split(';');
            if (subName != "" && ingsName != "")
            {
                res = _cntx.Recettes.Where(x => x.Name.ToLower().Contains(subName.ToLower()) && x.Calories < maxCalorieValue && x.Calories > minCalorieValue &&
                x.Ingredients.Select(ing => ing.Name.ToLower()).Contains(ingsName.ToLower())).ToList();
            }
            else
            {
                if (ingsName == "")
                {
                    res = _cntx.Recettes.Where(x => x.Name.ToLower().Contains(subName.ToLower()) && x.Calories < maxCalorieValue && x.Calories > minCalorieValue).ToList();
                }
                else
                {
                    res = _cntx.Recettes.Where(x => x.Calories < maxCalorieValue && x.Calories > minCalorieValue &&
                    x.Ingredients.Select(ing => ing.Name.ToLower()).Contains(ingsName.ToLower())).ToList();
                }
            }
            if (orderBy != "")
            {
                res = OrderRecettes(orderBy, res);
            }
            return res;
        }
        public IQueryable<Ingredient> getIngsByCategory(int idCategory)
        {
            var category = FindById(idCategory, "Category") as Category;
            var ingredintsList = _cntx.Ingredients
                      .Where(ing => ing.Category == category.Name).OrderBy(ing => ing.Name);
            return ingredintsList;
        }

        public List<Recette> OrderRecettes(string orderBy, List<Recette> recettes)
        {
            List<Recette> results = null;
            List<Recette> recettesWithComment;
            List<Recette> recettesWithoutComment;

            try
            {
                switch (orderBy)
                {
                    case "az":
                        results = recettes.OrderBy(x => x.Name).ToList();
                        break;
                    case "za":
                        results = recettes.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "new":
                        results = recettes.OrderByDescending(x => x.CreationDate).ToList();
                        break;
                    case "old":
                        results = recettes.OrderBy(x => x.CreationDate).ToList();
                        break;
                    case "best":
                        recettesWithComment = recettes.Where(y => y.Comments.Count > 0).OrderByDescending(x => (x.Comments.Sum(y => y.Mark) / x.Comments.Count)).ToList();
                        recettesWithoutComment = recettes.Where(y => y.Comments.Count == 0).ToList();
                        results = recettesWithComment.Concat(recettesWithoutComment).ToList();
                        break;
                    case "worst":
                        recettesWithComment = recettes.Where(y => y.Comments.Count > 0).OrderBy(x => (x.Comments.Sum(y => y.Mark) / x.Comments.Count)).ToList();
                        recettesWithoutComment = recettes.Where(y => y.Comments.Count == 0).ToList();
                        results = recettesWithoutComment.Concat(recettesWithComment).ToList();
                        break;
                    case "cal":
                        results = recettes.OrderByDescending(x => x.Calories).ToList();
                        break;
                    case "cal_desc":
                        results = recettes.OrderBy(x => x.Calories).ToList();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("le message d'erreur :" + e.Message);
                throw;
            }
            return results;
        }

        public string Add<T>(T entity)
        {
            Object t = entity;
            Type type = entity.GetType();
            if (type.Equals(typeof(Recette)))
            {
                _cntx.Recettes.Add((Recette)t);
            }
            else if (type.Equals(typeof(Comment)))
            {
                _cntx.Comments.Add((Comment)t);
            }
            else if (type.Equals(typeof(Ingredient)))
            {
                _cntx.Ingredients.Add((Ingredient)t);
            }
            else if (type.Equals(typeof(Category)))
            {
                _cntx.Categories.Add((Category)t);
            }
            var messRetour = (_cntx.SaveChanges() > 0) ? "entity added" : "adding entity failed";
            return messRetour;
        }

        public float getRecetteNote(int idRecette)
        {
            var recette = _cntx.Recettes.Find(idRecette);
            float recetteNote = 0;
            if (recette != null)
            {
                foreach (var item in recette.Comments)
                {
                    recetteNote += item.Mark;
                }
            }
            recetteNote = recetteNote != 0 ? (recetteNote / recette.Comments.Count) : 0;
            return recetteNote;
        }

        public IQueryable<Recette> GetNewRecettes()
        {
            var results = _cntx.Recettes.OrderByDescending(r => r.CreationDate);
            return results;
        }
        public IQueryable<Recette> GetBestRecettes()
        {

            var results = _cntx.Recettes.OrderBy(x => x.Comments.Sum(y => y.Mark));
            var i = results.Count();
            return results;
        }
        public IQueryable<Object> GetAll<T>(T entity)
        {
            Type type = entity.GetType();
            IQueryable<Object> results = null;
            if (type.Equals(typeof(Recette)))
            {
                results = _cntx.Recettes.OrderBy(c => c.Name);

            }
            else if (type.Equals(typeof(Comment)))
            {
                results = _cntx.Comments.OrderBy(c => c.Mark);
            }
            else if (type.Equals(typeof(Ingredient)))
            {
                results = _cntx.Ingredients.OrderBy(c => c.Name);
            }
            else if (type.Equals(typeof(Category)))
            {
                results = _cntx.Categories.OrderBy(c => c.Name);

            }
            return results;
        }
        public string Delete<T>(T entity)
        {
            Object t = entity;
            Type type = entity.GetType();
            if (type.Equals(typeof(Recette)))
            {
                _cntx.Recettes.Remove((Recette)t);
            }
            else if (type.Equals(typeof(Comment)))
            {
                _cntx.Comments.Remove((Comment)t);
            }
            else if (type.Equals(typeof(Ingredient)))
            {
                _cntx.Ingredients.Remove((Ingredient)t);
            }
            else if (type.Equals(typeof(Category)))
            {
                _cntx.Categories.Remove((Category)t);
            }
            _cntx.SaveChanges();
            var messRetour = (_cntx.SaveChanges() > 0) ? "entity added" : "adding entity failed";
            return messRetour;
        }
    }
}