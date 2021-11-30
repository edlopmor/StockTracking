using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;


namespace StockTracking.BLL
{
    class ProductBLL : IBLL<ProductDetailDTO, ProductDTO>
    {
        CategoryDAO categoryDao = new CategoryDAO();
        ProductDAO dao = new ProductDAO();
        public bool Delete(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(ProductDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.productName = entity.ProductName;
            product.categoryId = entity.CategoryID;
            product.price = entity.Price;
            return dao.Insert(product);
        }

        public ProductDTO select()
        {
            ProductDTO dto = new ProductDTO();
            dto.Categories = categoryDao.Select();
            dto.Products = dao.Select();
            return dto;

        }

        public bool Update(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.IdProduct = entity.ID;
            product.productName = entity.ProductName;
            product.price = entity.Price;
            product.stockAmount = entity.StockAmount;
            product.categoryId = entity.CategoryID;
            return dao.Update(product);

            
        }
    }
}
