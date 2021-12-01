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
        public CategoryDetailDTO categoriaSeleccionada = new CategoryDetailDTO();
        public bool isUpdate = false;

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
                if (!isUpdate)//Nueva categoria 
                {
                    CategoryDetailDTO category = new CategoryDetailDTO();
                    category.CategoryName = txtBCategoryName.Text;
                    if (bll.Insert(category))
                    {
                        MessageBox.Show("Categoria añadida correctamente");
                        txtBCategoryName.Clear();
                    }
                }
                else if (isUpdate)//Actualizar categoria
                {
                    //Comprobar que el nombre de la categoria seleccionada haya sido modificado
                    if (categoriaSeleccionada.CategoryName == txtBCategoryName.Text)
                        MessageBox.Show("No has modificado el nombre de la categoria");
                    else
                    {
                        //Guardar la categoria nueva . 
                        categoriaSeleccionada.CategoryName = txtBCategoryName.Text;

                        if (bll.Update(categoriaSeleccionada))
                        {
                            MessageBox.Show("Categoria actualizada correctamente");
                            this.Close();
                        }
                    }                  
                }                                  
            }
        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                btnSave.Text = "Actualizar";
                txtBCategoryName.Text = categoriaSeleccionada.CategoryName;               
            }
        }
    }
}
