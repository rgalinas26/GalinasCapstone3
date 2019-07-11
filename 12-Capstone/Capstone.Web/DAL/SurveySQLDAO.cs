using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveySQLDAO : ISurveyDAO
    {
        private string connectionString;
        public SurveySQLDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void AddSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"INSERT INTO survey_result VALUES (@parkCode, @email, @state, @activityLevel);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@email", survey.Email);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();
                   
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

    }
}
