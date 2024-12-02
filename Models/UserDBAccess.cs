using System.Data.SqlClient;
using System.Data;


namespace Airbnb.WebAPI.Models
{
    public class UserDBAccess
    {
        public int AddUser(UserFields userfields, string strcon)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                System.Data.DataTable dtUser = new DataTable();
                dtUser.Columns.Add("username", typeof(string));
                dtUser.Columns.Add("email", typeof(string));
                dtUser.Columns.Add("password", typeof(string));
                dtUser.Columns.Add("user_type", typeof(string));

                //JArray jsonArray = JArray.Parse(userfields);
                //foreach (JObject jsonObject in jsonArray)
                //{
                //    DataRow row = dtUser.NewRow();
                //    row["username"] = jsonObject["username"];
                //    row["email"] = jsonObject["username"];
                //    row["password"] = jsonObject["password"];
                //    row["user_type"] = jsonObject["user_type"];
                //    dtUser.Rows.Add(row);
                //}

              dtUser.Rows.Add(userfields.username,userfields.email,userfields.password,userfields.user_type);


                SqlCommand cmd = new SqlCommand("Add_user", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_Details", dtUser);
                cmd.Parameters.AddWithValue("@Username", userfields.username);
                cmd.Parameters.AddWithValue("@Email", userfields.email);
                cmd.Parameters.AddWithValue("@userid", SqlDbType.Int).Direction = ParameterDirection.Output;
                //cmd.Parameters.AddWithValue("@username", userfields.username);
                //cmd.Parameters.AddWithValue("@email", userfields.email);
                //cmd.Parameters.AddWithValue("@password", userfields.password);
                //cmd.Parameters.AddWithValue("@user_type", userfields.user_type);
               
                con.Open();
                cmd.ExecuteNonQuery();
                 int userid = Convert.ToInt32(cmd.Parameters["@userid"].Value);
                con.Close();
                return (userid);

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                return 1;
               // return (ex.Message);
            }
        }

        public UserFields GetRecordBy(string username,string password,string strcon)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
              
                SqlCommand cmd = new SqlCommand("getLoginUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                con.Open();
                sd.Fill(ds);
                con.Close();
                UserFields userfields = new UserFields();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    userfields.user_id = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    userfields.username = ds.Tables[0].Rows[0][1].ToString();
                    userfields.user_type = ds.Tables[0].Rows[0][2].ToString();
                }
                return userfields;

            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                  {
                       con.Close();
                   }
                return null;
            }

        }
    }
}
