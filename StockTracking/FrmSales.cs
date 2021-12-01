using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.DAL.DTO;
using StockTracking.BLL;


namespace StockTracking
{
    public partial class FrmSales : Form
    {
        public SalesDTO dto = new SalesDTO();
        SalesBLL bll = new SalesBLL();
        SalesDetailDto productSeleccionado = new SalesDetailDto();


        bool comboFull = false;
        bool primerCarga = true; 
        public FrmSales()
        {
            InitializeComponent();
        }

        private void txtSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmSales_Load(object sender, EventArgs e)
        {
            dto = bll.select();
            //Carga del combobox
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            
            dataGridProducts.DataSource = dto.Products;
            dataGridProducts.Columns[0].Visible = false; //Product ID 
            dataGridProducts.Columns[1].HeaderText = "Nombre producto";
            dataGridProducts.Columns[2].HeaderText = "Nombre categoria";
            dataGridProducts.Columns[3].HeaderText = "Cantidad almacenada";
            dataGridProducts.Columns[4].HeaderText = "Precio";
            dataGridProducts.Columns[5].Visible = false; //Category ID 

            dataGridViewCustomers.DataSource = dto.Customers;
            dataGridViewCustomers.Columns[0].Visible = false;
            dataGridViewCustomers.Columns[1].HeaderText = "Nombre customers";
            
            if(dto.Categories.Count > 0)
            {
                comboFull = true;
            }
            primerCarga = false; 

        }
        
        private void dataGridProducts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!primerCarga)
            {
                productSeleccionado.ProductName = dataGridProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                productSeleccionado.Price = Convert.ToInt32(dataGridProducts.Rows[e.RowIndex].Cells[4].Value);
                productSeleccionado.StockAmount = Convert.ToInt32(dataGridProducts.Rows[e.RowIndex].Cells[3].Value);
                productSeleccionado.ProductID = Convert.ToInt32(dataGridProducts.Rows[e.RowIndex].Cells[0].Value);
                productSeleccionado.CategoryID = Convert.ToInt32(dataGridProducts.Rows[e.RowIndex].Cells[5].Value);

                txtProductName.Text = productSeleccionado.ProductName;
                txtPrice.Text = productSeleccionado.Price.ToString();
                txtQuantityStock.Text = productSeleccionado.StockAmount.ToString();
            }
            
            
        }

        private void dataGridViewCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!primerCarga)
            {
                productSeleccionado.CustomerName = dataGridViewCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
                productSeleccionado.CustomerID =  Convert.ToInt32(dataGridViewCustomers.Rows[e.RowIndex].Cells[0].Value);
                txtCustomerName.Text = productSeleccionado.CustomerName;
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<ProductDetailDTO> listaProductosFiltrados = dto.Products;
                listaProductosFiltrados = listaProductosFiltrados.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
                dataGridProducts.DataSource = listaProductosFiltrados;

                if(listaProductosFiltrados.Count == 0)
                {
                    txtPrice.Clear();
                    txtProductName.Clear();
                    txtCustomerName.Clear();
                }
            }

        }

        private void textBoxCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> listaClientesFiltrada = dto.Customers;
            listaClientesFiltrada = listaClientesFiltrada.Where(x => x.CustomerName.Contains(textBoxCustomerSearch.Text)).ToList();
            dataGridViewCustomers.DataSource = listaClientesFiltrada;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (productSeleccionado.ProductID == 0)
                MessageBox.Show("Seleccione un producto");
            else if (productSeleccionado.CustomerID == 0)
                MessageBox.Show("Seleccione un cliente");
            else if (productSeleccionado.StockAmount < Convert.ToInt32(txtSalesAmount.Text))
                MessageBox.Show("No se puede vender mayor cantidad de la que se encuentra en el almacen");
            else
            {
                productSeleccionado.SalesAmount = Convert.ToInt32(txtSalesAmount.Text);
                productSeleccionado.SalesDate = DateTime.Today;
                if (bll.Insert(productSeleccionado))
                {
                    MessageBox.Show("Venta añadida con exito");
                    bll = new SalesBLL();
                    dto = bll.select();
                    dataGridProducts.DataSource = dto.Products;
                    comboFull = false;

                    cmbCategory.DataSource = dto.Categories;
                    if (dto.Products.Count > 0)
                        comboFull = true;
                    txtSalesAmount.Clear();
                }
            }

        }
    }
}
