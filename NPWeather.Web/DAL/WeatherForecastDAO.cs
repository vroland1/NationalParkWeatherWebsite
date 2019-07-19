using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class WeatherForecastDAO : IWeatherForecastDAO
    {
        private readonly string connectionString;

        public WeatherForecastDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<WeatherForecast> GetWeatherForecasts(string parkCode)
        {
            IList<WeatherForecast> forecasts = new List<WeatherForecast>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM weather WHERE parkCode = @parkCode";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    WeatherForecast forecast = new WeatherForecast();

                    forecast.ParkCode = Convert.ToString(reader["parkCode"]);
                    forecast.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                    forecast.LowTempF = Convert.ToInt32(reader["low"]);
                    forecast.HighTempF = Convert.ToInt32(reader["high"]);
                    forecast.Forecast = Convert.ToString(reader["forecast"]);

                    forecasts.Add(forecast);
                }
            }

            return forecasts;
        }
    }
}
