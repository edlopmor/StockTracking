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
    public partial class FrmDeleted : Form
    {
        SalesDTO dto = new SalesDTO();
        SalesBLL bll = new SalesBLL();
        SalesDetailDto salesDetailDTO = new SalesDetailDto();
        ProductDetailDTO productDetailDTO = new ProductDetailDTO();
        CategoryDetailDTO CategoryDetailDTO = new CategoryDetailDTO();
        CustomerDetailDTO customerDetailDTO = new CustomerDetailDTO();

        SalesBLL salesBLL = new SalesBLL();
        ProductBLL productBLL = new ProductBLL();
        CategoryBLL categoryBLL = new CategoryBLL();
        CustomerBLL customerBLL = new CustomerBLL();

        bool primeraCarga = true;
        public FrmDeleted()
        {
            InitializeComponent();
        }

        private void cmbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbDeletedData.SelectedIndex == 0)
            {
                dataGridView1.DataSource = dto.Categories;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Nombre categoria";
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                dataGridView1.DataSource = dto.Products;
                dataGridView1.Columns[0].Visible = false; //Product ID 
                dataGridView1.Columns[1].HeaderText = "Nombre producto";
                dataGridView1.Columns[2].HeaderText = "Nombre categoria";
                dataGridView1.Columns[3].HeaderText = "Cantidad almacenada";
                dataGridView1.Columns[4].HeaderText = "Precio";
                dataGridView1.Columns[5].Visible = false; //Category ID 
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                dataGridView1.DataSource = dto.Customers;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Nombre customers";
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                dataGridView1.DataSource = dto.Sales;
                dataGridView1.Columns[0].Visible = false; //Product ID 
                dataGridView1.Columns[1].HeaderText = "Nombre cliente";
                dataGridView1.Columns[2].HeaderText = "Nombre producto";
                dataGridView1.Columns[3].HeaderText = "Nombre categoria";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].HeaderText = "Cantidad vendida";
                dataGridView1.Columns[8].HeaderText = "Precio de venta";
                dataGridView1.Columns[9].HeaderText = "Fecha de venta";
                dataGridView1.Columns[10].Visible = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDeleted_Load(object sender, EventArgs e)
        {
            cmbDeletedData.Items.Add("Categoria");
            cmbDeletedData.Items.Add("Productos");
            cmbDeletedData.Items.Add("Clientes");
            cmbDeletedData.Items.Add("Ventas");

            dto = bll.select(true);
            dataGridView1.DataSource = dto.Sales;
            dataGridView1.Columns[0].Visible = false; //Product ID 
            dataGridView1.Columns[1].HeaderText = "Nombre cliente";
            dataGridView1.Columns[2].HeaderText = "Nombre producto";
            dataGridView1.Columns[3].HeaderText = "Nombre categoria";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Cantidad vendida";
            dataGridView1.Columns[8].HeaderText = "Precio de venta";
            dataGridView1.Columns[9].HeaderText = "Fecha de venta";
            dataGridView1.Columns[10].Visible = false;

            primeraCarga = false;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!primeraCarga)
            {

                if (cmbDeletedData.SelectedIndex == 0)
                {
                    CategoryDetailDTO = new CategoryDetailDTO();
                    CategoryDetailDTO.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    CategoryDetailDTO.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                else if (cmbDeletedData.SelectedIndex == 1)
                {
                    productDetailDTO = new ProductDetailDTO();
                    productDetailDTO.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    productDetailDTO.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    productDetailDTO.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    productDetailDTO.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                }
                else if (cmbDeletedData.SelectedIndex == 2)
                {
                    customerDetailDTO = new CustomerDetailDTO();
                    customerDetailDTO.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    customerDetailDTO.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
                else if (cmbDeletedData.SelectedIndex == 3)
                {
                    salesDetailDTO = new SalesDetailDto();
                    salesDetailDTO.SalesID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    salesDetailDTO.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    salesDetailDTO.ProductName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    salesDetailDTO.CustomerID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    salesDetailDTO.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                    salesDetailDTO.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                    salesDetailDTO.SalesAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                    salesDetailDTO.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                    salesDetailDTO.StockAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                }
            }    
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0)
            {
                if (categoryBLL.GetBack(CategoryDetailDTO))
                {
                    MessageBox.Show("Categoria recuperada");
                    dto = bll.select();
                    dataGridView1.DataSource = dto.Categories;
                }
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                if (productBLL.GetBack(productDetailDTO))
                {
                    MessageBox.Show("Producto recuperado");
                    dto = bll.select();
                    dataGridView1.DataSource = dto.Products;
                }
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                if (customerBLL.GetBack(customerDetailDTO))
                {
                    MessageBox.Show("Cliente recuperado");
                    dto = bll.select();
                    dataGridView1.DataSource = dto.Customers;
                }
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                if (salesBLL.GetBack(salesDetailDTO))
                {
                    MessageBox.Show("Venta recuperada");
                    dto = bll.select();
                    dataGridView1.DataSource = dto.Sales;
                }
            }
        }
    }
}
