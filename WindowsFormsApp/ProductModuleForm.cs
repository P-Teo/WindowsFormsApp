using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp
{
    public partial class ProductModuleForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Teodora\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public ProductModuleForm()
        {
            InitializeComponent();
            loaderCategory();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void loaderCategory()
        {
            comboCategory.Items.Clear();
            cm= new SqlCommand("SELECT * FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboCategory.Items.Add(dr[1].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this book?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbProduct(pname,pqty,pqtyt,pprice,pdescription,pcategory)VALUES(@pname,@pqty,@pqtyt,@pprice,@pdescription,@pcategory)", con);
                    cm.Parameters.AddWithValue("@pname", textBookName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(textQuantity.Text));
                    cm.Parameters.AddWithValue("@pqtyt", Convert.ToInt16(textQuantityT.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt16(textPrice.Text));
                    cm.Parameters.AddWithValue("@pdescription", textDescription.Text);
                    cm.Parameters.AddWithValue("@pcategory", comboCategory.Text);


                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book has been successfully saved.");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            textBookName.Clear();
            textQuantity.Clear();
            textQuantityT.Clear();
            textPrice.Clear();
            textDescription.Clear();
            comboCategory.Text = "";
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this book?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("UPDATE tbProduct SET  pname=@pname, pqty=@pqty, pqtyt=@pqtyt, pprice=@pprice, pdescription=@pdescription, pcategory=@pcategory WHERE pid LIKE '" + lalPid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pname", textBookName.Text);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(textQuantity.Text));
                    cm.Parameters.AddWithValue("@pqtyt", Convert.ToInt16(textQuantityT.Text));
                    cm.Parameters.AddWithValue("@pprice", Convert.ToInt16(textPrice.Text));
                    cm.Parameters.AddWithValue("@pcategory", comboCategory.Text);
                    cm.Parameters.AddWithValue("@pdescription", textDescription.Text);


                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book has been successfully updated.");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
