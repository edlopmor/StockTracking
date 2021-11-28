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
    public partial class FrmDeleted : Form
    {
        public FrmDeleted()
        {
            InitializeComponent();
        }

        private void cmbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDeletedData.Items.Add("Categoria");
            cmbDeletedData.Items.Add("Productos");
            cmbDeletedData.Items.Add("Clientes");
            cmbDeletedData.Items.Add("Ventas");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
