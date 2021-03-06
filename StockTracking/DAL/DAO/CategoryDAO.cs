using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Añadimos la libreria para tener acceso al interface 
using StockTracking.DAL.DTO;
namespace StockTracking.DAL.DAO
{
    public class CategoryDAO :StockContext, IDAO<CATEGORY, CategoryDetailDTO>
    {
        public bool Delete(CATEGORY entity)
        {
            CATEGORY category = db.CATEGORYS.First(x => x.IdCategory == entity.IdCategory);
            category.isDeleted = true;
            category.deletedDate = DateTime.Today;
            db.SaveChanges();
            return true; 
        }

        public bool GetBack(int ID)
        {
            try
            {
                CATEGORY category = db.CATEGORYS.First(x => x.IdCategory == ID);
                category.isDeleted = false;
                category.deletedDate = null;
                db.SaveChanges();

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Insert(CATEGORY entity)
        {
            try
            {
                db.CATEGORYS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CategoryDetailDTO> Select()
        {
            List<CategoryDetailDTO> categories = new List<CategoryDetailDTO>();
            var list = db.CATEGORYS.Where(x => x.isDeleted==false);
            foreach (var item in list)
            {
                CategoryDetailDTO dto = new CategoryDetailDTO();
                dto.ID = item.IdCategory;
                dto.CategoryName = item.categoryName;
                categories.Add(dto);
            }
            return categories;
        }
        //Recuperar las categorias borradas
        public List<CategoryDetailDTO> Select(bool isDeleted)
        {
            List<CategoryDetailDTO> categories = new List<CategoryDetailDTO>();
            var list = db.CATEGORYS.Where(x => x.isDeleted == isDeleted);
            foreach (var item in list)
            {
                CategoryDetailDTO dto = new CategoryDetailDTO();
                dto.ID = item.IdCategory;
                dto.CategoryName = item.categoryName;
                categories.Add(dto);
            }
            return categories;
        }

        public bool Update(CATEGORY entity)
        {
            try
            {
                CATEGORY category = db.CATEGORYS.First(x => x.IdCategory == entity.IdCategory);
                category.categoryName = entity.categoryName;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
