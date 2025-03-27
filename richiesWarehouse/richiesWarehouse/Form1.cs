using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace richiesWarehouse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void searchBttn_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "\"C:\\Users\\betha\\Desktop\\Visual Studio\\richiesWarehouse\\richiesWarehouse\\" +
                "warehouseDB.mdf\";Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();

                int productID; 
                if (!int.TryParse(idTxtBox.Text, out productID)) 
                {
                    MessageBox.Show("Invalid product ID.");
                    return;
                }

                string productName = nameTxtBox.Text;

                string query = "SELECT productID FROM stock WHERE productID = @productID AND productName = @productName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@productID", productID);
                    cmd.Parameters.AddWithValue("@productName", productName);

                    using (SqlDataReader sdr = cmd.ExecuteReader()) 
                    {
                        if (sdr.HasRows)
                        {
                            MessageBox.Show("Product is available.");
                        }
                        else
                        {
                            MessageBox.Show("Incorrect product ID or name.");
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginForm lf = new loginForm();
            lf.ShowDialog();
        }
    }
}
