using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.BLL;
using StockTracking.DAL;
using StockTracking.DAL.DTO;

namespace StockTracking.DAL.DAO
{
    public class ProductDAO : StockContext, IDAO<PRODUCT, ProductDetailDTO>
    {
        public bool Delete(PRODUCT entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PRODUCT entity)
        {
            try
            {
                db.PRODUCTS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ProductDetailDTO> Select()
        {
            try
            {
                List<ProductDetailDTO> products = new List<ProductDetailDTO>();
                var list = (from p in db.PRODUCTS
                            join c in db.CATEGORYS on p.categoryId equals c.IdCategory
                            select new
                            {
                                productName = p.productName,
                                categoryName = c.categoryName,
                                stockAmount = p.stockAmount,
                                price = p.price,
                                productID = p.IdProduct,
                                categoryID = c.IdCategory
                            }).OrderBy(x => x.productName).ToList();
                foreach (var item in list)
                {
                    ProductDetailDTO dto = new ProductDetailDTO();
                    dto.ID = item.productID;
                    dto.ProductName = item.productName;
                    dto.CategoryName = item.categoryName;
                    dto.StockAmount = item.stockAmount;                       
                    dto.Price = item.price;
                    dto.CategoryID = item.categoryID;
                    products.Add(dto);
                }
                return products;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update(PRODUCT entity)
        {
            try
            {                
                PRODUCT product = db.PRODUCTS.First(x => x.IdProduct == entity.IdProduct);
                //Añadir Stock
                if (entity.categoryId == 0)
                {
                    product.stockAmount = entity.stockAmount;
                   

                }
                else //Actualizacion de producto 
                {
                    product.productName = entity.productName;
                    product.price = entity.price;
                    product.categoryId = entity.categoryId;

                }
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
