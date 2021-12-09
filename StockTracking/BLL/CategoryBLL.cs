using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL.DTO;
using StockTracking.DAL;
using StockTracking.DAL.DAO;

namespace StockTracking.BLL
{
    public class CategoryBLL : IBLL<CategoryDetailDTO, CategoryDTO>
    {
        CategoryDAO dao = new CategoryDAO();
        ProductDAO productDAO = new ProductDAO();

        public bool Delete(CategoryDetailDTO entity)
        {
            CATEGORY category = new CATEGORY();
            category.IdCategory = entity.ID;
            dao.Delete(category);

            //Si eliminamos una categoria

            PRODUCT product = new PRODUCT();
            product.categoryId = entity.ID;
            productDAO.Delete(product);

            return true; 
        }

        public bool GetBack(CategoryDetailDTO entity)
        {
            return dao.GetBack(entity.ID);
        }

        public bool Insert(CategoryDetailDTO entity)
        {
            
            CATEGORY category = new CATEGORY();
            category.categoryName = entity.CategoryName;
            return dao.Insert(category);
        }

        public CategoryDTO select()
        {
            CategoryDTO dto = new CategoryDTO();
            dto.categories = dao.Select();
            return dto;
        }

        public bool Update(CategoryDetailDTO entity)
        {
            CATEGORY category = new CATEGORY();
            category.categoryName = entity.CategoryName;
            category.IdCategory = entity.ID;
            return dao.Update(category);
        }
    }
}
