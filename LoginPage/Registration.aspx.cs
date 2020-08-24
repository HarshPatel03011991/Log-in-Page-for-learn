using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace LoginPage
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();
                string checkuser = "select count (*) from UserData where UserName = '" + TextBoxUN.Text + "'";
                SqlCommand com = new SqlCommand(checkuser, conn);
                int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
                if (temp == 1)
                {
                    Response.Write("User already Exist");
                }
                conn.Close();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();
                string insert = " insert into USerData (UserName,Email,Password,Counry) values (@uname,@email,@password,@country)";
                SqlCommand com = new SqlCommand(insert, conn);
                com.Parameters.AddWithValue("@uname", TextBoxUN.Text);
                com.Parameters.AddWithValue("@email", TextBoxEmail.Text);
                com.Parameters.AddWithValue("@password", TextBoxPass.Text);
                com.Parameters.AddWithValue("@country", DropDownListCountry.SelectedItem.ToString());
                com.ExecuteNonQuery();
                Response.Redirect("Manager.aspx");
                Response.Write("your registration is successfull");
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write("Error : " + ex.ToString());
            }
        }
    }
}