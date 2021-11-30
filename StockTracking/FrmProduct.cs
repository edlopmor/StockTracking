using StockTracking.BLL;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockTracking
{
    public partial class FrmProduct : Form
    {
        
        public ProductDTO dto = new ProductDTO();
        ProductBLL bll = new ProductBLL();

        public FrmProduct()
        {
            InitializeComponent();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            
            //Carga del combobox
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == "")
                MessageBox.Show("El nombre del producto no puede estar vacio");
            else if (cmbCategory.SelectedIndex == -1)
                MessageBox.Show("Debe seleccionar una categoria");
            else if (txtPrice.Text.Trim() == "")
                MessageBox.Show("Debe seleccionar un precio");
            else
            {
                ProductDetailDTO product = new ProductDetailDTO();
                product.ProductName = txtProductName.Text;
                product.CategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
                product.Price = Convert.ToInt32(txtPrice.Text);
                if (bll.Insert(product))
                {
                    MessageBox.Show("Producto insertado correctamente");
                    txtProductName.Clear();
                    cmbCategory.SelectedIndex = -1;
                    txtPrice.Clear();
                }
                    
                else
                {
                    MessageBox.Show("No se ha podido añadir el producto");
                }
                    
            }
        }
    }
}
