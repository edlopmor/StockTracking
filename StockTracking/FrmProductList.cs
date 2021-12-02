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
    public partial class FrmProductList : Form
    {
        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        ProductDetailDTO productoSeleccionado = new ProductDetailDTO();

        bool primeraCarga = true;
        
        public FrmProductList()
        {
            InitializeComponent();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmProduct frm = new FrmProduct();
            frm.dto = dto;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.select();
            dataGridViewProducts.DataSource = dto.Products;
            CleanFilters();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProductList_Load(object sender, EventArgs e)
        {
            dto = bll.select();
            //Carga del combobox
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;

            dataGridViewProducts.DataSource = dto.Products;
            dataGridViewProducts.Columns[0].Visible = false; //Product ID 
            dataGridViewProducts.Columns[1].HeaderText = "Nombre producto";
            dataGridViewProducts.Columns[2].HeaderText = "Nombre categoria";
            dataGridViewProducts.Columns[3].HeaderText = "Cantidad almacenada";
            dataGridViewProducts.Columns[4].HeaderText = "Precio";
            dataGridViewProducts.Columns[5].Visible = false; //Category ID 

            primeraCarga = false; 

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ProductDetailDTO> list = dto.Products;
            if (txtProductName.Text.Trim() != null)
                list = list.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();
            if (cmbCategory.SelectedIndex != -1)
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
            if (txtPrice.Text.Trim() != "")
            {
                if (rbPriceEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(txtPrice.Text)).ToList();
                else
                    MessageBox.Show("Selecciona el criterio para buscar por precio");
            }
            if (txtQuantity.Text.Trim() != "")
            {
                if (rbStockEqual.Checked)
                    list = list.Where(x => x.StockAmount == Convert.ToInt32(txtQuantity.Text)).ToList();
                else if (rbStockLess.Checked)
                    list = list.Where(x => x.StockAmount < Convert.ToInt32(txtQuantity.Text)).ToList();
                else if (rbStockMore.Checked)
                    list = list.Where(x => x.StockAmount > Convert.ToInt32(txtQuantity.Text)).ToList();
                else
                    MessageBox.Show("Selecciona el criterio para buscar por cantidad");
            }
            dataGridViewProducts.DataSource = list;
            
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanFilters();
            dto = bll.select();
            dataGridViewProducts.DataSource = dto.Products;
        }
        private void CleanFilters()
        {
            txtProductName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            cmbCategory.SelectedIndex = -1;
            rbPriceEqual.Checked = false;
            rbPriceLess.Checked = false;
            rbPriceMore.Checked = false;
            rbStockEqual.Checked = false;
            rbStockLess.Checked = false;
            rbStockMore.Checked = false;
        }

        private void dataGridViewProducts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!primeraCarga)
            {
                productoSeleccionado = new ProductDetailDTO();
                productoSeleccionado.ID = Convert.ToInt32(dataGridViewProducts.Rows[e.RowIndex].Cells[0].Value.ToString());
                productoSeleccionado.ProductName = dataGridViewProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                productoSeleccionado.CategoryID = Convert.ToInt32(dataGridViewProducts.Rows[e.RowIndex].Cells[5].Value.ToString());
                productoSeleccionado.Price = Convert.ToInt32(dataGridViewProducts.Rows[e.RowIndex].Cells[4].Value.ToString());
            }                                
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado.ID == 0)
                MessageBox.Show("Seleccione un producto para modificarlo");
            else
            {
                FrmProduct frm = new FrmProduct();
                frm.dto = dto;
                frm.productoSeleccionado = productoSeleccionado;
                frm.isUpdate = true; 
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                dto = bll.select();
                dataGridViewProducts.DataSource = dto.Products;
                CleanFilters();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado.ID == 0)
                MessageBox.Show("Por favor seleccione un producto para ser eliminado");
            else
            {
                DialogResult result = MessageBox.Show("Desea eliminar este producto", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(productoSeleccionado))
                    {
                        MessageBox.Show("Producto eliminado con exito");
                        bll = new ProductBLL();
                        dto = bll.select();
                        dataGridViewProducts.DataSource = dto.Products;

                    }
                }
            }
        }
    }   
}
