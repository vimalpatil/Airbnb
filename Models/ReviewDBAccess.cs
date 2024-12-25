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

        public ReviewFields GetReviewByid(int id,string strcon)
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("getReviewById", con);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_id", id);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            sd.Fill(ds);
            con.Close() ;
            ReviewFields review_list= new ReviewFields();
            review_list.comment = ds.Tables[0].Rows[0][2].ToString();
            review_list.rating = Convert.ToInt32(ds.Tables[0].Rows[0][3].ToString());
            review_list.review_id = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString());
            review_list.username= ds.Tables[0].Rows[0][4].ToString();
            return review_list;


        }

    }
}
