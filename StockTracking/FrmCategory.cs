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
    public partial class FrmCategory : Form
    {
        CategoryBLL bll = new CategoryBLL();
        public FrmCategory()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBCategoryName.Text.Trim() == "")
                MessageBox.Show("La categoria no puede estar vacia");
            else
            {
                CategoryDetailDTO category = new CategoryDetailDTO();
                category.CategoryName = txtBCategoryName.Text;
                if (bll.Insert(category))
                {
                    MessageBox.Show("Categoria añadida correctamente");
                    txtBCategoryName.Clear();
                }
                else
                {
                    MessageBox.Show("No se ha podido añadir la categoria");
                }
                    
            }
        }
    }
}
