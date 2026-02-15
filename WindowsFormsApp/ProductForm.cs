using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp
{
    public partial class ProductForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname,pprice,pdescription,pcategory) LIKE '%" + textSearch.Text + "%' ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModuleForm productModule = new ProductModuleForm();
            productModule.buttonSave.Enabled = true;
            productModule.buttonUpdate.Enabled = false;
            productModule.ShowDialog();
            LoadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm productModule = new ProductModuleForm();
                productModule.lalPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModule.textBookName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModule.textQuantity.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModule.textQuantityT.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModule.textPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModule.textDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                productModule.comboCategory.Text = dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString();


                productModule.buttonSave.Enabled = false;
                productModule.buttonUpdate.Enabled = true;
                productModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this book?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pid LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Book has been deleted.");
                }
            }
            LoadProduct();
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();

        }
    }
}
