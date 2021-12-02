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
    public partial class FrmAddStock : Form
    {

        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        public ProductDetailDTO productoSeleccionado = new ProductDetailDTO();

        bool comboFull = false;
        //Variable que nos servira para evitar que por defecto el grid row enter cargue el primer objeto. 
        bool primeraCarga = false;
        //Variable para saber si es una actualización. Viene desde la pantalla inicial.
        public bool isUpdate = false;
        public FrmAddStock()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddStock_Load(object sender, EventArgs e)
        {
            dto = bll.select();
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;

            dataGridViewProducts.DataSource = dto.Products;
            dataGridViewProducts.Columns[0].Visible = false; //IDProducto
            dataGridViewProducts.Columns[1].HeaderText = "Nombre producto";
            dataGridViewProducts.Columns[2].Visible = false; //CategoryName
            dataGridViewProducts.Columns[3].HeaderText = "Cantidad almacenada";
            dataGridViewProducts.Columns[4].HeaderText = "Precio";
            dataGridViewProducts.Columns[5].Visible = false; //Category ID 

            if (dto.Categories.Count > 0)
                comboFull = true;
            if (isUpdate)
            {
                cmbCategory.SelectedValue = productoSeleccionado.CategoryID;
                txtPrice.Text = productoSeleccionado.Price.ToString();
                txtProductName.Text = productoSeleccionado.ProductName;
            }
            primeraCarga = true; 
        }
        
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
                dataGridViewProducts.DataSource = list;

                if (list.Count == 0)
                    txtPrice.Clear();
                    txtProductName.Clear();
                    txtQuantity.Clear();
            }
            

        }

        private void dataGridViewProducts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (primeraCarga)
            {
                //Recuperamos el producto ID del producto seleccionado en el datagrid
                productoSeleccionado.ID = Convert.ToInt32(dataGridViewProducts.Rows[e.RowIndex].Cells[0].Value);
                //Recuperamos el nombre
                productoSeleccionado.ProductName = dataGridViewProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                //Asignar el nombre al textbox de solo lectura
                txtProductName.Text = productoSeleccionado.ProductName;
                //Recuperar el precio 
                productoSeleccionado.Price = Convert.ToInt32(dataGridViewProducts.Rows[e.RowIndex].Cells[4].Value);
                //Asignar el precio al textbox
                txtPrice.Text = productoSeleccionado.Price.ToString();
                //Recuperar la cantidad en almacen 
                productoSeleccionado.StockAmount = Convert.ToInt32(dataGridViewProducts.Rows[e.RowIndex].Cells[3].Value);
                //Asignarla al textbox
                txtQuantity.Text = productoSeleccionado.StockAmount.ToString();

                productoSeleccionado.CategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == "" || txtPrice.Text.Trim() == "")
                MessageBox.Show("Por favor seleccione un producto de la tabla");
            else if (txtQuantity.Text.Trim() == "")
                MessageBox.Show("Por favor introduzca una cantidad");
            else
            {
                int sumStock = productoSeleccionado.StockAmount;
                sumStock += Convert.ToInt32(txtQuantity.Text);
                productoSeleccionado.StockAmount = sumStock;

                if (bll.Update(productoSeleccionado))
                {
                    MessageBox.Show("Añadido stock");
                    bll = new ProductBLL();
                    dto = bll.select();
                    dataGridViewProducts.DataSource = dto.Products;
                    txtQuantity.Clear();
                }
            }
            
        }
    }
}
