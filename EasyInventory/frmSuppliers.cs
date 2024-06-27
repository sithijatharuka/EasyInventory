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
    public partial class frmSuppliers : Form
    {
        SuppliersDataClasses1DataContext dc = new SuppliersDataClasses1DataContext();

        public frmSuppliers()
        {
            InitializeComponent();
        }

        private void frmSuppliers_Load(object sender, EventArgs e)
        {
            refreshDataGrid();
        }

        private void supplierDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtId.Text = supplierDataGridView.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = supplierDataGridView.CurrentRow.Cells[1].Value.ToString();
            txtPhoneNo.Text = supplierDataGridView.CurrentRow.Cells[2].Value.ToString();
        }

        private void refreshDataGrid()
        {
            var selectData = from a in dc.tblSuppliers select a;
            supplierDataGridView.DataSource = selectData;

            // Customize  Header text Row
            supplierDataGridView.Columns[0].HeaderText = "ID";
            supplierDataGridView.Columns[1].HeaderText = "Suppliers Name";
            supplierDataGridView.Columns[2].HeaderText = "Phone No";

        }

        // CHECK EMPTY FEILDS

        private bool isEmpty()
        {
            if (txtName.Text == "" || txtPhoneNo.Text == "")
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
                    tblSupplier newSuppliers = new tblSupplier();

                    newSuppliers.suppliersName = txtName.Text;
                    newSuppliers.phoneNo = txtPhoneNo.Text;

                    dc.tblSuppliers.InsertOnSubmit(newSuppliers);
                    dc.SubmitChanges();

                    MessageBox.Show("Supplier has been successfully added.", "Category Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    refreshDataGrid();
                    clearData();
                }
                catch
                {
                    MessageBox.Show("Error while adding Supplier. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                tblSupplier supplier = dc.tblSuppliers.FirstOrDefault(c => c.Id.Equals(txtId.Text));
                dc.tblSuppliers.DeleteOnSubmit(supplier);
                dc.SubmitChanges();
                refreshDataGrid();
                clearData();
                MessageBox.Show("Supplier has been successfully deleted.", "Category Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Error while deleting Supplier. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateData()
        {

            if (!isEmpty())
            {
                try
                {
                    tblSupplier supplier = dc.tblSuppliers.FirstOrDefault(s => s.Id.Equals(txtId.Text));
                    supplier.suppliersName = txtName.Text;
                    supplier.phoneNo = txtPhoneNo.Text;
                    dc.SubmitChanges();
                    refreshDataGrid();
                    clearData();
                    MessageBox.Show("Supplier has been successfully updated.", "Category Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {
                    MessageBox.Show("Error while updating Supplier. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearData()
        {
            txtName.Clear();
            txtPhoneNo.Clear();
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
