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

namespace richiesWarehouse
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void loginBttn_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\" +
                "Users\\betha\\Desktop\\Visual Studio\\richiesWarehouse\\" +
                "richiesWarehouse\\warehouseDB.mdf\";Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();

                // Declared variables
                int ID = int.Parse(productIDTxtBox.Text);
                string name = productNameTxtBox.Text;
                int quantity = int.Parse(productQuantityTxtBox.Text);
                decimal price = decimal.Parse(productPriceTxtBox.Text);

                string query = "SET IDENTITY_INSERT [stock] ON INSERT INTO stock (productID, productName, productAmount, productPrice) VALUES (@productID, @productName, @productAmount, @productPrice)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Set parameters 
                    cmd.Parameters.AddWithValue("@productID", ID);
                    cmd.Parameters.AddWithValue("@productName", name);
                    cmd.Parameters.AddWithValue("@productAmount", quantity);
                    cmd.Parameters.AddWithValue("@productPrice", price);

                    // Execute SQL query 
                    cmd.ExecuteNonQuery();
                }

                // Refresh datagridview 
                string selectQuery = "SELECT * FROM stock";
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'warehouseDBDataSet.stock' table. You can move, or remove it, as needed.
            this.stockTableAdapter.Fill(this.warehouseDBDataSet.stock);

        }
    }
}
