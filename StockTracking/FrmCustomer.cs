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
    public partial class FrmCustomer : Form
    {
        CustomerBLL bll = new CustomerBLL();
        //Proviene de la pantalla FrmCustomerList; 
        public CustomerDetailDTO customerSeleccionado = new CustomerDetailDTO();
        public bool isUpdate = false; 
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() == "")
                MessageBox.Show("Por favor rellene un nombre de cliente");
            else
            {
                if (!isUpdate) //Añadir nuevo cliente
                {
                    CustomerDetailDTO customer = new CustomerDetailDTO();
                    customer.CustomerName = txtCustomerName.Text;
                    if (bll.Insert(customer))
                    {
                        MessageBox.Show("Cliente añadido con exito");
                        txtCustomerName.Clear();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido añadir el cliente");
                    }
                }
                else //Actualizar cliente
                {
                    customerSeleccionado.CustomerName = txtCustomerName.Text;
                    if(bll.Update(customerSeleccionado))
                    {
                        MessageBox.Show("Actualizado cliente satisfactoriamente");
                        this.Close();
                    }    
                }
                
            }
                
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                btnSave.Text = "Actualizar";
                txtCustomerName.Text = customerSeleccionado.CustomerName;
            }
        }
    }
}
