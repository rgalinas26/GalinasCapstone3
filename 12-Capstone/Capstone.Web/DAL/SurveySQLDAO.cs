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

        public IList<SurveyResultVM> SurveyResults()
        {
            List<SurveyResultVM> results = new List<SurveyResultVM>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"select Count(*) as tally, park.parkCode, park.parkName from park join survey_result on park.parkCode = survey_result.parkCode group by park.parkCode, park.parkName order by tally desc, park.parkName asc";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        results.Add(MapRowToSurvey(reader));
                    }
                    


                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return results;
        }
        private SurveyResultVM MapRowToSurvey(SqlDataReader reader)
        {
            SurveyResultVM surveyResult = new SurveyResultVM();
            surveyResult.SurveyCount = Convert.ToInt32(reader["tally"]);
            surveyResult.ParkCode = Convert.ToString(reader["parkCode"]);
            surveyResult.ParkName = Convert.ToString(reader["ParkName"]);
            return surveyResult;



        }
    }
}
//select Count(*) as tally, park.parkCode, park.parkName from park join survey_result on park.parkCode = survey_result.parkCode group by park.parkCode, park.parkName