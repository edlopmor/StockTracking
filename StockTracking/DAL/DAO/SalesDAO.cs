﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL;
using StockTracking.DAL.DTO;

namespace StockTracking.DAL.DAO
{
    public class SalesDAO : StockContext, IDAO<SALE, SalesDetailDto>
    {
        public bool Delete(SALE entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(SALE entity)
        {
            try
            {
                db.SALES.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SalesDetailDto> Select()
        {
            try
            {
                List<SalesDetailDto> sales = new List<SalesDetailDto>();
                var list = (from s in db.SALES
                            join p in db.PRODUCTS on s.product_Id equals p.IdProduct
                            join c in db.CUSTOMERS on s.customer_Id equals c.IdCustomer
                            join category in db.CATEGORYS on s.category_Id equals category.IdCategory
                            select new
                            {
                                productName = p.productName,
                                customerName = c.customerName,
                                categoryName = category.categoryName,
                                categoryId = s.category_Id,
                                salesPrice = s.productSalesPrice,
                                salesAmount = s.productSalesAmount,
                                salesDate = s.salesDate,
                                productId = p.IdProduct,
                                customerId = c.IdCustomer,
                                salesId = s.id,
                            }).OrderBy(x => x.salesDate).ToList();
                foreach (var item in list)
                {
                    SalesDetailDto dto = new SalesDetailDto();
                    dto.ProductName = item.productName;
                    dto.CustomerName = item.customerName;
                    dto.CategoryName = item.categoryName;
                    dto.CategoryID = Convert.ToInt32(item.categoryId);
                    dto.Price = Convert.ToInt32(item.salesPrice);
                    dto.SalesAmount = Convert.ToInt32(item.salesAmount);
                    dto.SalesDate = Convert.ToDateTime(item.salesDate);
                    dto.ProductID = Convert.ToInt32(item.productId);
                    dto.CustomerID = Convert.ToInt32(item.customerId);
                    dto.SalesID = Convert.ToInt32(item.salesId);
                    sales.Add(dto);

                }
                return sales;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update(SALE entity)
        {
            throw new NotImplementedException();
        }
    }
}
