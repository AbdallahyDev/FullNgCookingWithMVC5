using FullNgCookingWithMVC5.Models;
using Model.Comments;
using Models.Ctegories;
using Models.Ingredients;
using Models.Recettes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullNgCookingWithMVC5.Services
{
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
                    res = _cntx.Recettes.Single(x => x.Id.Equals(id.ToString()));
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
        public List<Recette> FilterRecetteByName(string subName)
        {
            List<Recette> res;      
            res = _cntx.Recettes.Where(x => x.Name.ToLower().Contains(subName.ToLower())).ToList();         
            return res;
        }
        public IQueryable<Ingredient> getIngsByCategory(int idCategory)
        {
            var category = FindById(idCategory, "Category") as Category;
            var ingredintsList = _cntx.Ingredients
                      .Where(ing => ing.Category == category.Name).OrderBy(ing => ing.Name);
            return ingredintsList;
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