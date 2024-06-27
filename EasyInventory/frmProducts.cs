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
    public partial class frmProducts : Form
    {
        ProductDataClasses1DataContext dc = new ProductDataClasses1DataContext();
        CategoryDataClasses1DataContext c = new CategoryDataClasses1DataContext();


        public frmProducts()
        {
            InitializeComponent();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            refreshDataGrid();
            loadCategory();
        }

        private void tblDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtId.Text = tblDataGridView.CurrentRow.Cells[0].Value.ToString();
            txtProductName.Text = tblDataGridView.CurrentRow.Cells[1].Value.ToString();
            cmbCategory.Text = tblDataGridView.CurrentRow.Cells[2].Value.ToString();
            txtPrice.Text = tblDataGridView.CurrentRow.Cells[3].Value.ToString();
            txtQty.Text = tblDataGridView.CurrentRow.Cells[4].Value.ToString();

        }

        private void loadCategory()
        {
            var selectCategories = from a in c.tblCategories select a.categoryName;
            cmbCategory.DataSource = selectCategories;
        }

        private void refreshDataGrid()
        {
            var selectData = from a in dc.tblProducts select a;
            tblDataGridView.DataSource = selectData;

            // Customize  Header text Row
            tblDataGridView.Columns[0].HeaderText = "Product ID";
            tblDataGridView.Columns[1].HeaderText = "Product Name";
            tblDataGridView.Columns[2].HeaderText = "Category";
            tblDataGridView.Columns[3].HeaderText = "Price (Rs)";
            tblDataGridView.Columns[4].HeaderText = "Quantity";

        }

        //WORKING WITH DATABASE

        private void insertData() {

            try
            {
                tblProduct newProduct = new tblProduct();

                newProduct.productName = txtProductName.Text;
                newProduct.qty = Convert.ToInt32(txtQty.Text);
                newProduct.price = Convert.ToDecimal(txtPrice.Text);
                newProduct.category = cmbCategory.Text;

                dc.tblProducts.InsertOnSubmit(newProduct);
                dc.SubmitChanges();

                MessageBox.Show("Product has been successfully added.", "Product Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

                refreshDataGrid();
                clearData();
            }
            catch
            {
                MessageBox.Show("Error while adding Product. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteData() {
            try
            {
                tblProduct product = dc.tblProducts.FirstOrDefault(p => p.id.Equals(txtId.Text));        
                dc.tblProducts.DeleteOnSubmit(product);
                dc.SubmitChanges();
                refreshDataGrid();
                clearData();
                MessageBox.Show("Product has been successfully deleted.", "Product Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Error while deleting Product. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateData() {
            try
            {
                tblProduct product = dc.tblProducts.FirstOrDefault(p => p.id.Equals(txtId.Text));
                product.productName = txtProductName.Text;
                product.qty = Convert.ToInt32(txtQty.Text);
                product.price = Convert.ToDecimal(txtPrice.Text);
                product.category = cmbCategory.Text;
                dc.SubmitChanges();
                refreshDataGrid();
                clearData();
                MessageBox.Show("Product has been successfully updated.", "Product Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Error while updating Product. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearData() {
            txtProductName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
        }

 

        // calling crud operations

        private void btnAdd_Click(object sender, EventArgs e)
        {
            insertData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteData();
        }
   
    }
}
