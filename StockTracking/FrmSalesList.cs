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
    public partial class FrmSalesList : Form
    {
        SalesBLL bll = new SalesBLL();
        SalesDTO dto = new SalesDTO();
        public FrmSalesList()
        {
            InitializeComponent();
        }

        private void FrmSalesList_Load(object sender, EventArgs e)
        {
            dto = bll.select();
           
            //0-SalesID 1-CustomerName 2-ProductName 3-CategoryName 4-CustomerId 5-ProductId 6-CategoryId 7-SalesAmount 8-Price 9-SalesDate 10-StockAmount
            dataGridViewSales.DataSource = dto.Sales;
            dataGridViewSales.Columns[0].Visible = false; //Product ID 
            dataGridViewSales.Columns[1].HeaderText = "Nombre cliente";
            dataGridViewSales.Columns[2].HeaderText = "Nombre producto";
            dataGridViewSales.Columns[3].HeaderText = "Nombre categoria";
            dataGridViewSales.Columns[4].Visible = false;
            dataGridViewSales.Columns[5].Visible = false;
            dataGridViewSales.Columns[6].Visible = false;
            dataGridViewSales.Columns[7].HeaderText = "Cantidad vendida";
            dataGridViewSales.Columns[8].HeaderText = "Precio de venta";
            dataGridViewSales.Columns[9].HeaderText = "Fecha de venta";
            dataGridViewSales.Columns[10].Visible = false;


            //Carga del combobox
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;

        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmSales frm = new FrmSales();
            this.Hide();
            frm.dto = dto;
            frm.ShowDialog();
            this.Visible = true;

            dto = bll.select();          
            dataGridViewSales.DataSource = dto.Sales;
            CleanFilters();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SalesDetailDto> listaFiltrada = dto.Sales;
            if (txtProductName.Text.Trim() != "")
                listaFiltrada = listaFiltrada.Where(x => x.ProductName.Contains(txtProductName.Text)).ToList();
            if (txtCustomerName.Text.Trim() != "")
                listaFiltrada = listaFiltrada.Where(x => x.CustomerName.Contains(txtCustomerName.Text)).ToList();
            if (cmbCategory.SelectedIndex != -1)
                listaFiltrada = listaFiltrada.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
            if (txtPrice.Text.Trim() != "")
            {
                if (rbPriceEqual.Checked)
                    listaFiltrada = listaFiltrada.Where(x => x.Price == Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceLess.Checked)
                    listaFiltrada = listaFiltrada.Where(x => x.Price < Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceMore.Checked)
                    listaFiltrada = listaFiltrada.Where(x => x.Price > Convert.ToInt32(txtPrice.Text)).ToList();
                else
                    MessageBox.Show("Selecciona el criterio para buscar por precio");
            }
            if (txtBoxSalesAmount.Text.Trim() != "")
            {
                if (rbSalesAmountEqual.Checked)
                    listaFiltrada = listaFiltrada.Where(x => x.SalesAmount == Convert.ToInt32(txtBoxSalesAmount.Text)).ToList();
                else if (rbSalesAmountLess.Checked)
                    listaFiltrada = listaFiltrada.Where(x => x.SalesAmount < Convert.ToInt32(txtBoxSalesAmount.Text)).ToList();
                else if (rbSalesAmountMore.Checked)
                    listaFiltrada = listaFiltrada.Where(x => x.SalesAmount > Convert.ToInt32(txtBoxSalesAmount.Text)).ToList();
                else
                    MessageBox.Show("Selecciona el criterio para buscar por cantidad vendida");
            }
            if (checkBoxDate.Checked)
                listaFiltrada = listaFiltrada.Where(x => x.SalesDate > dateTimePickerStart.Value && x.SalesDate < dateTimePickerFinish.Value).ToList();

            dataGridViewSales.DataSource = listaFiltrada ;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanFilters();
            dto = bll.select();           
            dataGridViewSales.DataSource = dto.Sales;


        }
        void CleanFilters()
        {
            txtCustomerName.Clear();
            txtPrice.Clear();
            txtProductName.Clear();
            txtBoxSalesAmount.Clear();
            cmbCategory.SelectedIndex = -1;

            rbPriceEqual.Checked = true;
            rbPriceEqual.Checked = false;
            rbSalesAmountEqual.Checked = true;
            rbSalesAmountEqual.Checked = false;
            dateTimePickerFinish.Value = DateTime.Today;
            dateTimePickerStart.Value = DateTime.Today;
            checkBoxDate.Checked = false;
        }
    }
}
