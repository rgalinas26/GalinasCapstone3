using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkSQLDAO : IParkDAO
    {
        private string connectionString;
        public ParkSQLDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IList<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM park", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        parks.Add(MapRowToProduct(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"Exception occurred reading product data - ${ex}");
                throw;
            }

            return parks;
        }

        public Park GetParkById(int id)
        {
            throw new NotImplementedException();
        }

        private Park MapRowToProduct(SqlDataReader reader)
        {
            Park park = new Park();
            park.Park_Code = Convert.ToString(reader["parkCode"]);
            park.Park_Name = Convert.ToString(reader["parkName"]);
            park.State = Convert.ToString(reader["state"]);
            park.Acerage = Convert.ToInt32(reader["acreage"]);
            park.Elevation_In_Feet = Convert.ToInt32(reader["elevationInFeet"]);
            park.Miles_Of_Trail = Convert.ToDecimal(reader["milesOfTrail"]);
            park.Number_Of_Campsites = Convert.ToInt32(reader["numberOfCampsites"]);
            park.Climate = Convert.ToString(reader["climate"]);
            park.Year_Founded = Convert.ToInt32(reader["yearFounded"]);
            park.Annual_Visitor_Count = Convert.ToInt32(reader["annualVisitorCount"]);
            park.Quote = Convert.ToString(reader["inspirationalQuote"]);
            park.Quote_Source = Convert.ToString(reader["inspirationalQuoteSource"]);
            park.Description = Convert.ToString(reader["parkDescription"]);
            park.Entry_Fee = Convert.ToInt32(reader["entryFee"]);
            park.Number_Of_Species = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
            return park;
        }
    }
}
