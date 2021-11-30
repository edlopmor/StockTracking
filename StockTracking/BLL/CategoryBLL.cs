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
        public bool Delete(CategoryDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(CategoryDetailDTO entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
