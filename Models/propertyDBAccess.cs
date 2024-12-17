using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Airbnb.WebAPI.Models
{
    public class PropertyDBAccess
    {
        public List<PropertyFields> GetPropertyList(string strcon)
        {
            List<PropertyFields> PropertyList = new List<PropertyFields>();
           
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("propertylist",con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                PropertyList.Add(
                    new PropertyFields
                    {
                        p_id = Convert.ToInt32(dr["p_id"]),
                        title = Convert.ToString(dr["title"]),
                        description = Convert.ToString(dr["description"]),
                        image_name = Convert.ToString(dr["image_name"]),
                        price = Convert.ToDouble(dr["price"]),
                        location = Convert.ToString(dr["location"]),
                        geometry_coordinate = Convert.ToString(dr["geometry_coordinate"]),
                        date = Convert.ToDateTime(dr["date"])
                    }
                    );
            }
            return PropertyList;
        }

        public PropertyFields GetRecordbyid(int id,string strcon)
        {
            SqlConnection con= new SqlConnection(strcon);
            //try
            //{
                SqlCommand cmd = new SqlCommand("getPropertydetailsByid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_id", id);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                con.Open();
                sd.Fill(ds);
                con.Close();
                PropertyFields myfields = new PropertyFields();
                myfields.title = ds.Tables[0].Rows[0][1].ToString();
                myfields.description = ds.Tables[0].Rows[0][2].ToString();
                myfields.image_name = ds.Tables[0].Rows[0][3].ToString();
                myfields.price = Convert.ToDouble(ds.Tables[0].Rows[0][4]);
                myfields.location = ds.Tables[0].Rows[0][5].ToString();
                myfields.geometry_coordinate = ds.Tables[0].Rows[0][6].ToString();
                myfields.date = Convert.ToDateTime(ds.Tables[0].Rows[0][7]);
            myfields.country = ds.Tables[0].Rows[0][8].ToString();
            return myfields;


            //}
            //catch (Exception ex) 
            //{
            //    if (con.State == System.Data.ConnectionState.Open)
            //    {
            //        con.Close();
            //    }

            //}

        }

        public string AddProperty(PropertyFields mypropertyfields,string strcon)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                DataTable dtproperty = new DataTable();
                dtproperty.Columns.Add("title", typeof(string));
                dtproperty.Columns.Add("description", typeof(string));
                dtproperty.Columns.Add("image_name", typeof(string));
                dtproperty.Columns.Add("price", typeof(double));
                dtproperty.Columns.Add("location", typeof(string));
                dtproperty.Columns.Add("geometry_coordinte", typeof(string));
                dtproperty.Columns.Add("date", typeof(DateTime));
                dtproperty.Columns.Add("country", typeof(string));
                dtproperty.Columns.Add("user_id", typeof(int));
                DateTime datetime = DateTime.Now;

                dtproperty.Rows.Add(mypropertyfields.title,mypropertyfields.description,mypropertyfields.image_name,mypropertyfields.price,mypropertyfields.location,mypropertyfields.geometry_coordinate, mypropertyfields.date, mypropertyfields.country,mypropertyfields.user_id);


                SqlCommand cmd = new SqlCommand("AddProperty", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Property_Details", dtproperty);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Data saved Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
               return (ex.Message);
            }
        }

       
    }

}
