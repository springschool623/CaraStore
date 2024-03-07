using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CaraLuggage.Models
{
    public class DatabaseHelper
    {
        public void CalculateStuQuantity()
        {
            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-469B4JS;Initial Catalog=ProExamDB;Integrated Security=True;"))
            {
                connection.Open();

                // Gọi Stored Procedure
                using (SqlCommand cmd = new SqlCommand("CalculateStuQuantity", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}