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
    class SalesBLL : IBLL<SalesDetailDto, SalesDTO>
    {
        SalesDAO salesDAO = new SalesDAO();
        ProductDAO productDAO = new ProductDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        CustomerDAO customerDAO = new CustomerDAO();


        public bool Delete(SalesDetailDto entity)
        {
            SALE sale = new SALE();
            sale.id = entity.SalesID;
            salesDAO.Delete(sale);

            PRODUCT product = new PRODUCT();
            product.IdProduct = entity.ProductID;
            product.stockAmount = entity.StockAmount + entity.SalesAmount;
            productDAO.Update(product);

            return true;
        }

        public bool GetBack(SalesDetailDto entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SalesDetailDto entity)
        {
            SALE sale = new SALE();
            sale.category_Id = entity.CategoryID;
            sale.customer_Id = entity.CustomerID;
            sale.product_Id = entity.ProductID;
            sale.productSalesPrice = entity.Price;
            sale.productSalesAmount = entity.SalesAmount;
            sale.salesDate = entity.SalesDate;
            sale.isDeleted = false; 
            salesDAO.Insert(sale);

            //Actualizar el producto a la resta de la cantidad total - cantidad vendida
            PRODUCT product = new PRODUCT();
            product.IdProduct = entity.ProductID;
            product.categoryId = entity.CategoryID;
            int temp = entity.StockAmount - entity.SalesAmount;
            product.stockAmount = temp;

            productDAO.Update(product);
            return true;
        }

        public SalesDTO select()
        {
            SalesDTO dto = new SalesDTO();
            dto.Products = productDAO.Select();
            dto.Customers = customerDAO.Select();
            dto.Categories = categoryDAO.Select();
            dto.Sales = salesDAO.Select();

            return dto;

        }

        public bool Update(SalesDetailDto entity)
        {
            SALE sale = new SALE();
            sale.id = entity.SalesID;
            sale.productSalesAmount = entity.SalesAmount;
            salesDAO.Update(sale);

            PRODUCT product = new PRODUCT();
            product.IdProduct = entity.ProductID;
            product.stockAmount = entity.StockAmount;
            productDAO.Update(product);

            return true;
        }
    }
}
