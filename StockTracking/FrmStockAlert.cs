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
    public partial class FrmStockAlert : Form
    {
        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        ProductDetailDTO productoSelecionado = new ProductDetailDTO();
        bool primeraCarga = true;
        public FrmStockAlert()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FrmMain frm = new FrmMain();
            this.Hide();
            frm.ShowDialog();
            
            
        }

        private void FrmStockAlert_Load(object sender, EventArgs e)
        {
            dto = bll.select();
            dto.Products = dto.Products.Where(x => x.StockAmount <= 10).ToList();
            dataGridView1.DataSource = dto.Products;

            dataGridView1.DataSource = dto.Products;
            dataGridView1.Columns[0].Visible = false; //Product ID 
            dataGridView1.Columns[1].HeaderText = "Nombre producto";
            dataGridView1.Columns[2].HeaderText = "Nombre categoria";
            dataGridView1.Columns[3].HeaderText = "Cantidad almacenada";
            dataGridView1.Columns[4].Visible = false; //Precio
            dataGridView1.Columns[5].Visible = false; //Category ID 

            btnGiveMeStock.Visible = false; 
            if (dto.Products.Count == 0)
            {
                FrmMain frm = new FrmMain();
                this.Hide();
                frm.ShowDialog();
            }
            primeraCarga = false;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!primeraCarga)
            {
                productoSelecionado.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                productoSelecionado.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                productoSelecionado.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                productoSelecionado.StockAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                productoSelecionado.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                productoSelecionado.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                btnGiveMeStock.Visible = true;
            }
        }

        private void btnGiveMeStock_Click(object sender, EventArgs e)
        {
            FrmAddStock frm = new FrmAddStock();
            frm.isUpdate = true;
            frm.productoSeleccionado = productoSelecionado;
            this.Hide();
            frm.ShowDialog();
        }
    }
}
