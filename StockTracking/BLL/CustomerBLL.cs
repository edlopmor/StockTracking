using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL.DAO;
using StockTracking.DAL;
using StockTracking.DAL.DTO;

namespace StockTracking.BLL
{
    public class CustomerBLL : IBLL<CustomerDetailDTO, CustomerDTO>
    {
        CustomerDAO dao = new CustomerDAO();
        SalesDAO salesDAO = new SalesDAO();
        public bool Delete(CustomerDetailDTO entity)
        {
            CUSTOMER customer = new CUSTOMER();
            customer.IdCustomer = entity.ID;

            SALE sale = new SALE();
            sale.customer_Id = entity.ID;
            salesDAO.Delete(sale);

            return dao.Delete(customer);
        }

        public bool GetBack(CustomerDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(CustomerDetailDTO entity)
        {
            CUSTOMER customer = new CUSTOMER();
            customer.customerName = entity.CustomerName;
            return dao.Insert(customer);
        }

        public CustomerDTO select()
        {
            CustomerDTO dto = new CustomerDTO();
            dto.Customers = dao.Select();
            return dto; 
        }

        public bool Update(CustomerDetailDTO entity)
        {
            CUSTOMER customer = new CUSTOMER();
            customer.IdCustomer = entity.ID;
            customer.customerName = entity.CustomerName;

            return dao.Update(customer);
        }
    }
}
