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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Teodora\Documents\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty=0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT cid, cname FROM tbCustomer WHERE CONCAT( cid, cname) LIKE '%"+textSearchCustomer.Text+"'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT *  FROM tbProduct WHERE CONCAT(pid, pname,pprice,pdescription,pcategory) LIKE '%" + textSearchProduct.Text + "%' ", con);
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
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer(); 
        }

        private void textSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }



        

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();

            if (Convert.ToInt16(numericUpDown1.Value) > qty)
            {
                MessageBox.Show("Quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDown1.Value = numericUpDown1.Value - 1;
                return;
            }
            if (Convert.ToInt16(numericUpDown1.Value) > 0)
            {
                int total = Convert.ToInt16(numericUpDown1.Value) * Convert.ToInt16(textPrice.Text);
                textTotal.Text = total.ToString();
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textCId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            textCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textPId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            textPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            textPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
            
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if(textCId.Text == "" )
                {
                    MessageBox.Show("Please select a customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (textPId.Text == "")
                {
                    MessageBox.Show("Please select a product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to insert this order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbOrder(odate,pid,cid,qtyt,price,total)VALUES(@odate,@pid,@cid,@qtyt,@price,@total)", con);
                    cm.Parameters.AddWithValue("@odate", dateOrder.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt16(textPId.Text));
                    cm.Parameters.AddWithValue("@cid", Convert.ToInt16(textCId.Text));
                    cm.Parameters.AddWithValue("@qtyt", Convert.ToInt16(numericUpDown1.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToDecimal(textPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToDecimal(textTotal.Text));


                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Order has been successfully saved.");
                    


                    cm = new SqlCommand("UPDATE tbProduct SET pqtyt = (pqtyt - @pqty) WHERE pid LIKE '" + textPId.Text + "' ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(numericUpDown1.Value));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    
                    Clear();
                    LoadProduct();

                }
            }
            

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            textCId.Clear();
            textCName.Clear();
            textPId.Clear();
            textPName.Clear();
            textPrice.Clear();
            textTotal.Clear();
            numericUpDown1.Value = 1;
            dateOrder.Value = DateTime.Now;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }


        
        public void GetQty()
        {
            cm = new SqlCommand("SELECT pqtyt FROM tbProduct WHERE pid LIKE '" + textPId.Text + "' ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

    }
}
