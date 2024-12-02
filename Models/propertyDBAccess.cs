using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                        date = Convert.ToString(dr["date"])
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
                myfields.date = ds.Tables[0].Rows[0][7].ToString();
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

       
    }

}
