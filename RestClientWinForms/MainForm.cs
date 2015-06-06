using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestClientWinForms
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
            //restaurantControll1.myClicked += new EventHandler(this.restContr_DoubleClick);
        }

        private void btnInvenotry_Click(object sender, EventArgs e)
        {
            var frm = new InvetoryForm(this);
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            var frm = new OrdersForm(this);
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }
    }
}
