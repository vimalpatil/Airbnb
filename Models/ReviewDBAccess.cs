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

        public List<ReviewFields> GetReviewByid(int id,string strcon)
        {
            List<ReviewFields> ReviewList = new List<ReviewFields>();
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("getReviewById", con);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_id", id);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
               ReviewList.Add(
                    new ReviewFields
                    {
                        review_id = Convert.ToInt32(dr["review_id"]),
                        rating = Convert.ToInt32(dr["rating"]),
                        comment = Convert.ToString(dr["comment"]),
                        username = Convert.ToString(dr["username"]),
                        //review_date = Convert.ToDateTime(dr["review_date"])
                    }
                    );
            }
            return ReviewList;


        }

        public string DeleteReview(int id,string strcon)
        {
            SqlConnection con = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("delete_review", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("review_id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Review Deleted successfully";
            }
            catch(Exception ex)
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
