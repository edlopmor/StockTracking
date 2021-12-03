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
    public partial class FrmCategoryList : Form
    {
        CategoryDTO dto = new CategoryDTO();
        CategoryBLL bll = new CategoryBLL();
        CategoryDetailDTO categoriaSeleccionada = new CategoryDetailDTO();

        bool primeraCarga = true ;
        public FrmCategoryList()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            //Estas 2 lineas refrescan la tabla cada vez que creemos una nueva categoria. 
            dto = bll.select();
            dataGridViewEmployees.DataSource = dto.categories;
        }

        private void FrmCategoryList_Load(object sender, EventArgs e)
        {
            
            dto = bll.select();
            dataGridViewEmployees.DataSource = dto.categories;

            dataGridViewEmployees.Columns[0].Visible = false;
            dataGridViewEmployees.Columns[1].HeaderText = "Nombre categoria";
            primeraCarga = false; 
            
        }

        private void txtBCategoryName_TextChanged(object sender, EventArgs e)
        {
            List<CategoryDetailDTO> list = dto.categories;
            list = list.Where(x => x.CategoryName.Contains(txtBCategoryName.Text)).ToList();
            dataGridViewEmployees.DataSource = list;
        }

        private void dataGridViewEmployees_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!primeraCarga)
            {
                categoriaSeleccionada = new CategoryDetailDTO();
                categoriaSeleccionada.CategoryName = dataGridViewEmployees.Rows[e.RowIndex].Cells[1].Value.ToString();
                categoriaSeleccionada.ID = Convert.ToInt32(dataGridViewEmployees.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (categoriaSeleccionada.ID == 0)
                MessageBox.Show("Debe seleccionar una categoria");
            else
            {
                FrmCategory frm = new FrmCategory();
                frm.categoriaSeleccionada = categoriaSeleccionada;
                frm.isUpdate = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                //Refrescamos la tabla tras realizar la actualizacion 
                bll = new CategoryBLL();
                dto = bll.select();
                dataGridViewEmployees.DataSource = dto.categories;
            }
            

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (categoriaSeleccionada.ID == 0)
                MessageBox.Show("Por favor seleccione una categoria para eliminarla");
            else
            {
                DialogResult resultado = MessageBox.Show($"Estas seguro de eliminar : {categoriaSeleccionada.CategoryName} ", "Warning", MessageBoxButtons.YesNo);
                if(resultado == DialogResult.Yes)
                {
                    if (bll.Delete(categoriaSeleccionada))
                    {
                        MessageBox.Show("Categoria eliminada correctamente");
                        dto = bll.select();
                        dataGridViewEmployees.DataSource = dto.categories;
                        txtBCategoryName.Clear();
                    }
                }
            }
        }
    }
}
