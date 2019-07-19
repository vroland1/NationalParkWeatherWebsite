using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkDAO : IParkDAO
    {
        private readonly string connectionString;

        public ParkDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Park> GetAllParks()
        {
            IList<Park> parks = new List<Park>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM park";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                parks = MapToParks(reader);
            }

            return parks;
        }

        public Park GetPark(string parkCode)
        {
            IList<Park> parks = new List<Park>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM park WHERE parkCode = @parkCode";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                parks = MapToParks(reader);
            }

            return parks[0];
        }

        public IList<Park> GetRankedParks()
        {
            IList<Park> parks = new List<Park>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT p.parkName, p.parkCode, COUNT(sr.parkCode) AS surveycount
                                FROM park p
                                INNER JOIN survey_result sr
                                ON p.parkCode = sr.parkCode
                                GROUP BY p.parkName, p.parkCode
                                ORDER BY surveycount DESC";


                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Park park = new Park();

                    park.ParkCode = Convert.ToString(reader["parkCode"]);
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.SurveyCount = Convert.ToInt32(reader["surveycount"]);

                    parks.Add(park);
                }

            }

            return parks;
        }

        public IList<Park> MapToParks(SqlDataReader reader)
        {
            IList<Park> parks = new List<Park>();

            while (reader.Read())
            {
                Park park = new Park();

                park.ParkCode = Convert.ToString(reader["parkCode"]);
                park.ParkName = Convert.ToString(reader["parkName"]);
                park.State = Convert.ToString(reader["state"]);
                park.Acreage = Convert.ToInt32(reader["acreage"]);
                park.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
                park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                park.Climate = Convert.ToString(reader["climate"]);
                park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                park.AnnualVisitors = Convert.ToInt32(reader["annualVisitorCount"]);
                park.Quote = Convert.ToString(reader["inspirationalQuote"]);
                park.QuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                park.Description = Convert.ToString(reader["parkDescription"]);
                park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                park.AnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

                parks.Add(park);
            }

            return parks;
        }

    }
}
