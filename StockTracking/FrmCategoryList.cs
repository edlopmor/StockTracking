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
            
        }

        private void txtBCategoryName_TextChanged(object sender, EventArgs e)
        {
            List<CategoryDetailDTO> list = dto.categories;
            list = list.Where(x => x.CategoryName.Contains(txtBCategoryName.Text)).ToList();
            dataGridViewEmployees.DataSource = list;
        }
    }
}
