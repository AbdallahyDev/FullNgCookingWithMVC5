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
        public IQueryable<Ingredient> getIngsByCategory(int idCategory) { 
            var category = FindById(idCategory, "Category") as Category ; 
            var ingredintsList = _cntx.Ingredients
                      .Where(ing => ing.Category == category.Name).OrderBy(ing => ing.Name);
            return ingredintsList ; 
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
        public IQueryable<Object> GetAll<T>(T entity)
        {
            Type type = entity.GetType();
            IQueryable<Object> results = null;
            if (type.Equals(typeof(Recette)))
            {
                results = _cntx.Recettes.OrderBy(c => c.Id);

            }
            else if (type.Equals(typeof(Comment)))
            {
                results = _cntx.Comments.OrderBy(c => c.Id);
            }
            else if (type.Equals(typeof(Ingredient)))
            {
                results = _cntx.Ingredients.OrderBy(c => c.Id);
            }
            else if (type.Equals(typeof(Category)))
            {
                results = _cntx.Categories.OrderBy(c => c.Id);
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