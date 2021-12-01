using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.BLL;
using StockTracking.DAL.DTO;

namespace StockTracking
{
    public partial class FrmCustomerList : Form
    {
        CustomerDTO dto = new CustomerDTO();
        CustomerBLL bll = new CustomerBLL();
        CustomerDetailDTO customerSeleccionado =  new CustomerDetailDTO();

        bool primeraCarga = true; 

        public FrmCustomerList()
        {
            InitializeComponent();
        }

     
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmCustomer frm = new FrmCustomer();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.select();
            dataGridViewCustomers.DataSource = dto.Customers; 
        }

        private void FrmCustomerList_Load(object sender, EventArgs e)
        {
            dto = bll.select();
            dataGridViewCustomers.DataSource = dto.Customers;
            dataGridViewCustomers.Columns[0].Visible = false;
            dataGridViewCustomers.Columns[1].HeaderText = "Nombre customers";

            primeraCarga = false; 
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> list = dto.Customers;
            list = list.Where(x => x.CustomerName.Contains(txtCustomerName.Text)).ToList();
        }

        private void dataGridViewCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!primeraCarga)
            {
                customerSeleccionado = new CustomerDetailDTO();
                customerSeleccionado.ID = Convert.ToInt32(dataGridViewCustomers.Rows[e.RowIndex].Cells[0].Value.ToString());
                customerSeleccionado.CustomerName = dataGridViewCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();               
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (customerSeleccionado.ID == 0)            
                MessageBox.Show("Por favor selecciona algun cliente");
            else
            {
                FrmCustomer frm = new FrmCustomer();
                frm.isUpdate = true;
                frm.customerSeleccionado = customerSeleccionado;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new CustomerBLL();
                dto = bll.select();
                dataGridViewCustomers.DataSource = dto.Customers;
            }
            

        }
    }
}
