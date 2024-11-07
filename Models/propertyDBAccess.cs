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
    }

}
