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
        }

        private void FrmCustomerList_Load(object sender, EventArgs e)
        {
            dto = bll.select();
            dataGridViewCustomers.DataSource = dto.Customers;
            dataGridViewCustomers.Columns[0].Visible = false;
            dataGridViewCustomers.Columns[1].HeaderText = "Nombre customers";
        }
    }
}
