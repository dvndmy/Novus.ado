using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ado
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Display heading and line break
            Response.Write("<center><h1>Read data from a database</h1></center><hr/>");
            Response.Write("<br/>");

            if (!IsPostBack)
            {
                // Read the connection string from the configuration file
                string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

                // Create a SqlConnection object
                SqlConnection connection = new SqlConnection(connectionString);

                // Define the SQL query
                string sqlQuery = "select * from customers";

                // Create a SqlCommand object
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Open the connection
                connection.Open();

                // Execute the command and get the SqlDataReader
                SqlDataReader dataReader = command.ExecuteReader();

                // Bind the data reader to the GridView control
                GridView1.DataSource = dataReader;
                GridView1.DataBind();

                // Close the data reader
                dataReader.Close();

                // Define the SQL query for populating the DropDownList
                string sqlQueryDropDownList = "select DISTINCT Country from customers";

                // Create another SqlCommand object
                SqlCommand command2 = new SqlCommand(sqlQueryDropDownList, connection);

                // Execute the command and get the SqlDataReader
                SqlDataReader dataReader2 = command2.ExecuteReader();

                // Populate the DropDownList with data from the data reader
                while (dataReader2.Read() == true)
                {
                    DropDownList1.Items.Add(new ListItem(dataReader2["Country"].ToString(), dataReader2["Country"].ToString()));
                }

                // Close the data reader
                dataReader2.Close();

                // Close the connection
                connection.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Display heading and line break
            Response.Write("<br/>");

            // Get the input value from the TextBox
            string txtValue = TextBox1.Text;

            // Read the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            // Create a SqlConnection object
            SqlConnection connection = new SqlConnection(connectionString);

            // Define the SQL query with parameterized value
            string sqlQuery = "select * from customers where Country=@Country";

            // Create a SqlCommand object
            SqlCommand command = new SqlCommand(sqlQuery, connection);


            if (txtValue.Equals(""))
            {
                command = new SqlCommand("select * from customers", connection);
            }
            

            // Add the parameter to the command to prevent SQL injection
            command.Parameters.AddWithValue("@Country", txtValue);

            // Open the connection
            connection.Open();

            // Execute the command and get the SqlDataReader
            SqlDataReader dataReader = command.ExecuteReader();

            // Bind the data reader to the GridView control
            GridView1.DataSource = dataReader;
            GridView1.DataBind();

            // Close the data reader
            dataReader.Close();

            // Close the connection
            connection.Close();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Display line break
                Response.Write("<br/>");

                // Get the selected value from the DropDownList
                string txtValue = DropDownList1.SelectedValue.ToString();

                // Read the connection string from the configuration file
                string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

                // Create a SqlConnection object
                SqlConnection connection = new SqlConnection(connectionString);

                // Define the SQL query with parameterized value
                string sqlQuery = "select DISTINCT * from customers where Country=@Country";

                // Create a SqlCommand object
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Add the parameter to the command
                command.Parameters.AddWithValue("@Country", txtValue);

                // Open the connection
                connection.Open();

                // Execute the command and get the SqlDataReader
                SqlDataReader dataReader = command.ExecuteReader();

                // Bind the data reader to the GridView control
                GridView1.DataSource = dataReader;
                GridView1.DataBind();

                // Close the data reader
                dataReader.Close();

                // Close the connection
                connection.Close();
            }
        }
    }
}
