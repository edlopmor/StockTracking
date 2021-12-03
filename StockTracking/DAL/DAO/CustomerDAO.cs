using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL.DAO;
using StockTracking.DAL;
using StockTracking.DAL.DTO;

namespace StockTracking.DAL.DAO
{
    class CustomerDAO : StockContext, IDAO<CUSTOMER, CustomerDetailDTO>
    {
        public bool Delete(CUSTOMER entity)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMERS.First(x => x.IdCustomer == entity.IdCustomer);
                customer.isDeleted = true;
                customer.deletedDate = DateTime.Today;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(CUSTOMER entity)
        {
            try
            {
                db.CUSTOMERS.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CustomerDetailDTO> Select()
        {
            List<CustomerDetailDTO> customers = new List<CustomerDetailDTO>();
            var list = db.CUSTOMERS.Where(x => x.isDeleted == false);
            foreach (var item in list)
            {
                CustomerDetailDTO dto = new CustomerDetailDTO();
                dto.ID = item.IdCustomer;
                dto.CustomerName = item.customerName;
                customers.Add(dto);
            }
            return customers;
        }

        public bool Update(CUSTOMER entity)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMERS.First(x => x.IdCustomer == entity.IdCustomer);
                customer.customerName = entity.customerName;
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
