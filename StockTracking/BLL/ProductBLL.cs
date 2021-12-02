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
        SalesDAO salesDAO = new SalesDAO();

        public bool Delete(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.IdProduct = entity.ID;
            dao.Delete(product);

            //Al eliminar un producto debemos eliminar una venta en la que este ese producto 
            SALE sale = new SALE();
            sale.product_Id = entity.ID;
            salesDAO.Delete(sale);

            return true; 

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
