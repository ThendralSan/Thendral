using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Curd_Opertaon
{
    public partial class _Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            databind();
            Btn_Save.Visible = true;
            btn_Update.Visible = false;

        }
         
        public void databind()
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Customers_CRUD"))
                {
                    cmd.Parameters.AddWithValue("@Action", "SELECT");
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            string ID = txt_id.Text;
            string name = txt_Name.Text;
            string country = txt_cuntry.Text;
            DateTime DOB = DateTime.Parse(TextBox1.Text);
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Customers_CRUD"))
                {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "INSERT");
                        cmd.Parameters.AddWithValue("@DOB", DOB);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Country", country);

                        //cmd.Parameters.AddWithValue("@Email", country);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        txt_cuntry.Text = ""; txt_id.Text = ""; txt_Name.Text = ""; TextBox1.Text = "";
                }

                using (SqlCommand cmd = new SqlCommand("EMAIL"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Action", "INSERT");
                    //cmd.Parameters.AddWithValue("@DOB", DOB);
                    //cmd.Parameters.AddWithValue("@Name", name);
                    //cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@Email", country);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    txt_cuntry.Text = ""; txt_id.Text = ""; txt_Name.Text = ""; TextBox1.Text = "";
                }
            }
            this.databind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridView1.EditIndex = e.NewEditIndex;
            //databind();


            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            //databind();
            txt_id.Text = row.Cells[1].Text;
            txt_Name.Text = row.Cells[2].Text;
            txt_cuntry.Text = row.Cells[3].Text;
            TextBox1.Text = row.Cells[4].Text;
            Btn_Save.Visible = false;
            btn_Update.Visible = true;

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            GridViewRow row = GridView1.Rows[e.RowIndex];
            string id = row.Cells[1].Text;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Customers_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DELETE");
                    cmd.Parameters.AddWithValue("@CustomerId", id);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    txt_cuntry.Text = ""; txt_id.Text = ""; txt_Name.Text = ""; TextBox1.Text = "";

                }
            }
            databind();
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            string id = txt_id.Text;
            string name = txt_Name.Text;
            string country = txt_cuntry.Text;
            string DOB = TextBox1.Text;
            {
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("Customers_CRUD"))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "UPDATE");
                        cmd.Parameters.AddWithValue("@CustomerId", id);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Country", country);
                        cmd.Parameters.AddWithValue("@DOB", DOB);


                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        txt_cuntry.Text = ""; txt_id.Text = ""; txt_Name.Text = ""; TextBox1.Text = "";
                    }
                }
            }
            this.databind();
        }
    }
}