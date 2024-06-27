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
    public partial class frmCategory : Form
    {
        CategoryDataClasses1DataContext dc = new CategoryDataClasses1DataContext();

        public frmCategory()
        {
            InitializeComponent();
        }

        private void categoryDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txtId.Text = categoryDataGridView.CurrentRow.Cells[0].Value.ToString();
            txtCategory.Text = categoryDataGridView.CurrentRow.Cells[1].Value.ToString();
            descRichTextBox.Text = categoryDataGridView.CurrentRow.Cells[2].Value.ToString();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            refreshDataGrid();
        }

        private void refreshDataGrid()
        {
            var selectData = from a in dc.tblCategories select a;
            categoryDataGridView.DataSource = selectData;

            // Customize  Header text Row
            categoryDataGridView.Columns[0].HeaderText = "ID";
            categoryDataGridView.Columns[1].HeaderText = "Category Name";
            categoryDataGridView.Columns[2].HeaderText = "Description";

        }

        // CHECK EMPTY FEILDS

        private bool isEmpty()
        {
            if (txtCategory.Text == "" || descRichTextBox.Text == "")
            {
               return true;
            }
            else
            {
                return false;
            }

        }

        //WORKING WITH DATABASE

        private void insertData()
        {
            if (!isEmpty())
            {
                try
                {
                    tblCategory newCategory = new tblCategory();

                    newCategory.categoryName = txtCategory.Text;
                    newCategory.description = descRichTextBox.Text;

                    dc.tblCategories.InsertOnSubmit(newCategory);
                    dc.SubmitChanges();

                    MessageBox.Show("Category has been successfully added.", "Category Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    refreshDataGrid();
                    clearData();
                }
                catch
                {
                    MessageBox.Show("Error while adding Category. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void deleteData()
        {
            try
            {
                tblCategory category = dc.tblCategories.FirstOrDefault(c => c.Id.Equals(txtId.Text));
                dc.tblCategories.DeleteOnSubmit(category);
                dc.SubmitChanges();
                refreshDataGrid();
                clearData();
                MessageBox.Show("Category has been successfully deleted.", "Category Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Error while deleting Category. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateData()
        {

            if (!isEmpty())
            {
                try
                {
                    tblCategory category = dc.tblCategories.FirstOrDefault(c => c.Id.Equals(txtId.Text));
                    category.categoryName = txtCategory.Text;
                    category.description = descRichTextBox.Text;
                    dc.SubmitChanges();
                    refreshDataGrid();
                    clearData();
                    MessageBox.Show("Category has been successfully updated.", "Category Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {
                    MessageBox.Show("Error while updating Category. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearData()
        {
            txtCategory.Clear();
            descRichTextBox.Clear();
        }

        // calling crud operations

        private void btnAdd_Click(object sender, EventArgs e)
        {
            insertData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearData();
        }
    }
}