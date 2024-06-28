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
    public partial class frmPos : Form
    {
        ProductDataClasses1DataContext dc = new ProductDataClasses1DataContext();
        CartDataClasses1DataContext c = new CartDataClasses1DataContext();

        public frmPos()
        {
            InitializeComponent();
        }


        private void frmPos_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            lblDate.Text = now.ToString("D");
            refreshDataGrid();
            btnPay.Enabled = false;
        }   

        private void refreshDataGrid()
        {
            var selectData = from a in dc.tblProducts select new
            {
                 a.productName,
                 a.qty,
                 a.price
            };
            tblDataGridView.DataSource = selectData;

            // Customize  Header text Row
            tblDataGridView.Columns[0].HeaderText = "Product Name";
            tblDataGridView.Columns[1].HeaderText = "Quantity";
            tblDataGridView.Columns[2].HeaderText = "Price (Rs)";
        }

        private void loadCart()
        {
            var selectData = from a in c.tblCarts select a;
            cartDataGridView.DataSource = selectData;

            // Customize  Header text Row
            cartDataGridView.Columns[0].HeaderText = "Id";
            cartDataGridView.Columns[1].HeaderText = "Product Name";
            cartDataGridView.Columns[2].HeaderText = "Price";
            cartDataGridView.Columns[3].HeaderText = "Quantity";
            cartDataGridView.Columns[4].HeaderText = "Total (Rs)";
        }

        // CALCULATE TOTAL AMOUNT
        private void calculateTotal()
        {
            var selectData = from a in c.tblCarts select a;
            txtTotal.Text = c.tblCarts.Sum(c => c.total).ToString();

        }

        // WORKING WITH DATABASE

        private void insertData()
        {

            try
            {
                tblCart cartItem = new tblCart();

                cartItem.productName = tblDataGridView.CurrentRow.Cells[0].Value.ToString();
                cartItem.price = Convert.ToDecimal(tblDataGridView.CurrentRow.Cells[2].Value.ToString());
                cartItem.qty = int.Parse(txtQty.Text);
                cartItem.total = Convert.ToDecimal(cartItem.price * cartItem.qty);


                c.tblCarts.InsertOnSubmit(cartItem);
                c.SubmitChanges();

                loadCart();
                calculateTotal();
                clearData();               
                btnPay.Enabled = true;
                
            }
            catch
            {
                MessageBox.Show("Error while adding Item. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
           

        private void btnAdd_Click(object sender, EventArgs e)
        {
            insertData();
        }

        // PAYMENT BUTTON
        private void btnPay_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Payment Done", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cartDataGridView.Rows.Clear();
            txtTotal.Text = "";
            btnPay.Enabled = false;
            clearCart();
        }

        private void clearData()
        {
            txtQty.Clear();
        }

        private void clearCart()
        {
            try
            {
                var cartItems = from item in c.tblCarts select item;

                // Delete all selected items
                c.tblCarts.DeleteAllOnSubmit(cartItems);
                c.SubmitChanges();
                clearData();
            }
            catch
            {
                MessageBox.Show("The cart table did not clear", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
