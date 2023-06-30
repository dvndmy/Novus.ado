using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ado
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Display a heading and line break
            Response.Write("<center><h1>Read data from a database</h1></center><hr/>");
            Response.Write("<br/>");
            if (!IsPostBack)
            {
                

                // Retrieve the connection string from the web.config file
                string s = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

                // Create a SqlConnection object with the connection string
                SqlConnection con = new SqlConnection(s);

                // Create a SQL query to select all records from the customers table
                string sqlString = "SELECT * FROM customers";

                // Create a SqlCommand object with the SQL query and the SqlConnection object
                SqlCommand cmd = new SqlCommand(sqlString, con);

                // Open the database connection
                con.Open();

                // Execute the SQL query and retrieve the result as a SqlDataReader
                SqlDataReader dr = cmd.ExecuteReader();

                // Bind the SqlDataReader to the GridView control for displaying the data
                GridView1.DataSource = dr;
                GridView1.DataBind();

                // Close the SqlDataReader
                dr.Close();

                // Create a SQL query to select distinct countries from the customers table
                string sqlStringDropDownList = "SELECT DISTINCT Country FROM customers";

                // Create another SqlCommand object for the dropdown list query
                SqlCommand cmd2 = new SqlCommand(sqlStringDropDownList, con);

                // Execute the dropdown list query and retrieve the result as a SqlDataReader
                SqlDataReader dr2 = cmd2.ExecuteReader();

                DropDownList1.Items.Add(new ListItem("All", ""));

                // Loop through the result and add items to the DropDownList control
                while (dr2.Read())
                {
                    DropDownList1.Items.Add(new ListItem(dr2["Country"].ToString(), dr2["Country"].ToString()));
                }
                

                // Close the SqlDataReader
                dr2.Close();

                // Close the database connection
                con.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Display a heading and line break
            Response.Write("<center><h1>Read data from a database</h1></center><hr/>");
            Response.Write("<br/>");

            // Get the search criteria from the TextBox control
            string txtValue = TextBox1.Text;

            // Retrieve the connection string from the web.config file
            string s = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            // Create a SqlConnection object with the connection string
            SqlConnection con = new SqlConnection(s);
            string sqlString;
            SqlCommand cmd;
            if (txtValue != "")
            {


                // Create a SQL query to select records from the customers table based on the search criteria
                sqlString = "SELECT * FROM customers WHERE Country=@Country";
                // Create a SqlCommand object with the SQL query and the SqlConnection object
                cmd = new SqlCommand(sqlString, con);

                // Add a parameter to the SqlCommand object to prevent SQL injection
                cmd.Parameters.AddWithValue("@Country", txtValue);
            }
            else
            {
                sqlString = "SELECT * FROM customers";
                // Create a SqlCommand object with the SQL query and the SqlConnection object
                cmd = new SqlCommand(sqlString, con);

            }


            // Open the database connection
            con.Open();

            // Execute the SQL query and retrieve the result as a SqlDataReader
            SqlDataReader dr = cmd.ExecuteReader();

            // Bind the SqlDataReader to the GridView control for displaying the data
            GridView1.DataSource = dr;
            GridView1.DataBind();

            // Close the SqlDataReader
            dr.Close();

            // Close the database connection
            con.Close();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Display a line break
                Response.Write("<br/>");

                // Get the selected country from the DropDownList control
                string txtValue = DropDownList1.SelectedValue.ToString();

                // Retrieve the connection string from the web.config file
                string s = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

                // Create a SqlConnection object with the connection string
                SqlConnection con = new SqlConnection(s);

                string sqlString;
                SqlCommand cmd;
                if (txtValue != "")
                {


                    // Create a SQL query to select records from the customers table based on the search criteria
                    sqlString = "SELECT * FROM customers WHERE Country=@Country";
                    // Create a SqlCommand object with the SQL query and the SqlConnection object
                    cmd = new SqlCommand(sqlString, con);

                    // Add a parameter to the SqlCommand object to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Country", txtValue);
                }
                else
                {
                    sqlString = "SELECT * FROM customers";
                    // Create a SqlCommand object with the SQL query and the SqlConnection object
                    cmd = new SqlCommand(sqlString, con);

                }


                // Open the database connection
                con.Open();

                // Execute the SQL query and retrieve the result as a SqlDataReader
                SqlDataReader dr = cmd.ExecuteReader();

                // Bind the SqlDataReader to the GridView control for displaying the data
                GridView1.DataSource = dr;
                GridView1.DataBind();

                // Close the SqlDataReader
                dr.Close();

                // Close the database connection
                con.Close();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Retrieve the connection string from the web.config file
            string s = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            // Create a SqlConnection object with the connection string
            SqlConnection con = new SqlConnection(s);

            // Create a SQL query to insert data into the customers table
            string sqlString = @"INSERT INTO customers (CustomerId, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax)
                                VALUES (@CustomerId, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax)";

            // Create a SqlCommand object with the SQL query and the SqlConnection object
            SqlCommand cmd = new SqlCommand(sqlString, con);

            // Get the input values from the form controls
            string customerId = TextBoxCustomerId.Text;
            string companyName = TextBoxCompanyName.Text;
            string contactName = TextBoxContactName.Text;
            string contactTitle = TextBoxContactTitle.Text;
            string address = TextBoxAddress.Text;
            string city = TextBoxCity.Text;
            string region = TextBoxRegion.Text;
            string postalCode = TextBoxPostalCode.Text;
            string country = TextBoxCountry.Text;
            string phone = TextBoxPhone.Text;
            string fax = TextBoxFax.Text;

            // Add parameters to the SqlCommand object for each input value
            cmd.Parameters.AddWithValue("@CustomerId", customerId);
            cmd.Parameters.AddWithValue("@CompanyName", string.IsNullOrEmpty(companyName) ? (object)DBNull.Value : companyName);
            cmd.Parameters.AddWithValue("@ContactName", contactName);
            cmd.Parameters.AddWithValue("@ContactTitle", contactTitle);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@City", city);
            cmd.Parameters.AddWithValue("@Region", region);
            cmd.Parameters.AddWithValue("@PostalCode", postalCode);
            cmd.Parameters.AddWithValue("@Country", country);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Fax", fax);

            // Open the database connection
            con.Open();

            // Execute the SQL query to insert the data into the customers table
            cmd.ExecuteNonQuery();

            // Close the database connection
            con.Close();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the GridView row in edit mode
            GridView1.EditIndex = e.NewEditIndex;

            // Rebind the GridView to display the row in edit mode
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the updated values from the GridView row
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string customerId = ((TextBox)(row.Cells[1].Controls[0])).Text;
            string companyName = ((TextBox)(row.Cells[2].Controls[0])).Text;
            string contactName = ((TextBox)(row.Cells[3].Controls[0])).Text;
            string contactTitle = ((TextBox)(row.Cells[4].Controls[0])).Text;
            string address = ((TextBox)(row.Cells[5].Controls[0])).Text;
            string city = ((TextBox)(row.Cells[6].Controls[0])).Text;
            string region = ((TextBox)(row.Cells[7].Controls[0])).Text;
            string postalCode = ((TextBox)(row.Cells[8].Controls[0])).Text;
            string country = ((TextBox)(row.Cells[9].Controls[0])).Text;
            string phone = ((TextBox)(row.Cells[10].Controls[0])).Text;
            string fax = ((TextBox)(row.Cells[11].Controls[0])).Text;

            // Update the record in the database
            UpdateCustomer(customerId, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);

            // Exit edit mode and rebind the GridView to display the updated data
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Exit edit mode and rebind the GridView without saving any changes
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the CustomerId of the row to be deleted
            string customerId = GridView1.Rows[e.RowIndex].Cells[1].Text;

            // Delete the record from the database
            DeleteCustomer(customerId);

            // Rebind the GridView to display the updated data
            BindGridView();
        }

        private void UpdateCustomer(string customerId, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax)
        {
            // Retrieve the connection string from the web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            // Create a SqlConnection object with the connection string
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Create a SQL query to update the customer record
                string sqlString = @"UPDATE customers SET CompanyName = @CompanyName, ContactName = @ContactName, ContactTitle = @ContactTitle,
                             Address = @Address, City = @City, Region = @Region, PostalCode = @PostalCode, Country = @Country,
                             Phone = @Phone, Fax = @Fax WHERE CustomerId = @CustomerId";

                // Create a SqlCommand object with the SQL query and the SqlConnection object
                using (SqlCommand cmd = new SqlCommand(sqlString, con))
                {
                    // Add parameters to the SqlCommand object for each updated value
                    cmd.Parameters.AddWithValue("@CompanyName", string.IsNullOrEmpty(companyName) ? (object)DBNull.Value : companyName);
                    cmd.Parameters.AddWithValue("@ContactName", contactName);
                    cmd.Parameters.AddWithValue("@ContactTitle", contactTitle);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@Region", region);
                    cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Fax", fax);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    // Open the database connection
                    con.Open();

                    // Execute the SQL query to update the record
                    cmd.ExecuteNonQuery();

                    // Close the database connection
                    con.Close();
                }
            }
        }

        private void DeleteCustomer(string customerId)
        {
            // Retrieve the connection string from the web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            // Create a SqlConnection object with the connection string
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Create a SQL query to delete the customer record
                string sqlString = "DELETE FROM customers WHERE CustomerId = @CustomerId";

                // Create a SqlCommand object with the SQL query and the SqlConnection object
                using (SqlCommand cmd = new SqlCommand(sqlString, con))
                {
                    // Add a parameter to the SqlCommand object for the CustomerId
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    // Open the database connection
                    con.Open();

                    // Execute the SQL query to delete the record
                    cmd.ExecuteNonQuery();

                    // Close the database connection
                    con.Close();
                }
            }
        }

        private void BindGridView()
        {
            // Retrieve the connection string from the web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            // Create a SqlConnection object with the connection string
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Create a SQL query to select all records from the customers table
                string sqlString = "SELECT * FROM customers";

                // Create a SqlCommand object with the SQL query and the SqlConnection object
                using (SqlCommand cmd = new SqlCommand(sqlString, con))
                {
                    // Open the database connection
                    con.Open();

                    // Execute the SQL query and retrieve the result as a SqlDataReader
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Bind the SqlDataReader to the GridView control for displaying the data
                    GridView1.DataSource = dr;
                    GridView1.DataBind();

                    // Close the SqlDataReader
                    dr.Close();

                    // Close the database connection
                    con.Close();
                }
            }
        }
    }
}
