using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyInventory
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void LoadfrmHome()
        {
            frmWelcome frmWelcome = new frmWelcome();
            frmWelcome.TopLevel = false; // Set TopLevel to false
            frmWelcome.FormBorderStyle = FormBorderStyle.None; // Remove the border
            frmWelcome.Dock = DockStyle.Fill; // Fill the panel with the form

            HomePanel.Controls.Add(frmWelcome); // Add form2 to panel1
                                                //HomePanel.Tag = frmProduct; // Set panel tag to form2

            frmWelcome.BringToFront();

            frmWelcome.Show(); // Show form2
        }

        private void LoadfrmProduct()
        {
            frmProducts frmProduct = new frmProducts();
            frmProduct.TopLevel = false; // Set TopLevel to false
            frmProduct.FormBorderStyle = FormBorderStyle.None; // Remove the border
            frmProduct.Dock = DockStyle.Fill; // Fill the panel with the form

            HomePanel.Controls.Add(frmProduct); // Add form2 to panel1
            //HomePanel.Tag = frmProduct; // Set panel tag to form2

             frmProduct.BringToFront();

            frmProduct.Show(); // Show form2
        }

        private void LoadfrmCategory()
        {
            frmCategory frmCategory = new frmCategory();
            frmCategory.TopLevel = false; // Set TopLevel to false
            frmCategory.FormBorderStyle = FormBorderStyle.None; // Remove the border
            frmCategory.Dock = DockStyle.Fill; // Fill the panel with the form
            HomePanel.Controls.Add(frmCategory); // Add form2 to panel1
            //HomePanel.Tag = frmCategory; // Set panel tag to form2
             frmCategory.BringToFront();
            frmCategory.Show(); // Show form2
        }

        private void LoadfrmSuppliers()
        {
            frmSuppliers frmSuppliers = new frmSuppliers();
            frmSuppliers.TopLevel = false; // Set TopLevel to false
            frmSuppliers.FormBorderStyle = FormBorderStyle.None; // Remove the border
            frmSuppliers.Dock = DockStyle.Fill; // Fill the panel with the form
            HomePanel.Controls.Add(frmSuppliers); // Add form2 to panel1
            //HomePanel.Tag = frmSuppliers; // Set panel tag to form2
             frmSuppliers.BringToFront();
            frmSuppliers.Show(); // Show form2
        }

  

        private void btnProduct_Click(object sender, EventArgs e)
        {
            LoadfrmProduct();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            LoadfrmHome();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            LoadfrmCategory();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            LoadfrmSuppliers();
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            frmPos frmPOS = new frmPos();
            // Open the frmPOS from
            frmPOS.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            LoadfrmHome();
        }
    }
}
