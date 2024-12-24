using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Airbnb.WebAPI.Models
{
    public class ReviewDBAccess
    {
        public string AddReview(ReviewFields reviewfields,string strcon)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                DataTable dtReview = new DataTable();
                dtReview.Columns.Add("rating", typeof(int));
                dtReview.Columns.Add("comment", typeof(string));
                dtReview.Columns.Add("user_id", typeof(int));
                dtReview.Columns.Add("p_id", typeof(int));
                dtReview.Columns.Add("review_date", typeof(DateTime));
                dtReview.Rows.Add(reviewfields.rating, reviewfields.comment, reviewfields.user_id, reviewfields.p_id, reviewfields.review_date);
                SqlCommand cmd = new SqlCommand("addReview", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Review_Details", dtReview);
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
