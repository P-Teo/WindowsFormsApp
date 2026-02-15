using System.Data.SqlClient;
using System.Windows.Forms;


namespace WindowsFormsApp
{

    public partial class UserForm : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\dbLMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr ;
        public UserForm()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm= new SqlCommand("SELECT * FROM tbUser", con);
            con.Open();
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.textUserName.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.textFullName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.textPass.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.textPhone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.buttonSave.Enabled = false;
                userModule.buttonUpdate.Enabled = true;
                userModule.textUserName.Enabled = false;
                userModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE username LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been deleted.");
                }
            }
            LoadUser();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            UserModuleForm userModule = new UserModuleForm();
            userModule.buttonSave.Enabled = true;
            userModule.buttonUpdate.Enabled = false;
            userModule.ShowDialog();
            LoadUser();
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
