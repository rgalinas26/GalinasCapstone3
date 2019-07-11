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

        public Park GetParkById(string id)
        {
            Park park = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM park WHERE parkCode = @id", connection);
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        park = MapRowToProduct(reader);
                    }

                    return park;
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"An error occurred reading product {id} - ${ex}");
                throw;
            }
        }

        public IList<ParkWeather> GetWeather(string id, string unit)
        {
            List<ParkWeather> weather = new List<ParkWeather>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM weather WHERE parkCode = @id", connection);
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        weather.Add(MapWeatherToProduct(reader, unit));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"Exception occurred reading product data - ${ex}");
                throw;
            }

            return weather;
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
        private ParkWeather MapWeatherToProduct(SqlDataReader reader, string unit)
        {
            ParkWeather parkWeather = new ParkWeather();
            parkWeather.Park_Code = Convert.ToString(reader["parkCode"]);
            parkWeather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
            if (unit.ToLower() == "c")
            {
                // todo: convert
                double low = Convert.ToInt32(reader["low"]);
                parkWeather.LowTemp= (int)(((double)low - 32)/ 1.8);
                double high = Convert.ToInt32(reader["high"]);
                parkWeather.HighTemp = (int)(((double)high - 32) / 1.8);
                parkWeather.Temperature_Unit = "C";
                
            }
            else
            {
                parkWeather.LowTemp = Convert.ToInt32(reader["low"]);
                parkWeather.HighTemp = Convert.ToInt32(reader["high"]);
                parkWeather.Temperature_Unit = "F";
            }
            parkWeather.Forecast = Convert.ToString(reader["forecast"]);
            return parkWeather;
        }
    }
}
