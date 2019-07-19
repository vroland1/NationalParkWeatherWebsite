using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveyDAO : ISurveyDAO
    {
        private readonly string connectionString;

        public SurveyDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool SubmitSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string submitSurvey = @"INSERT into survey_result (parkCode, emailAddress, state, activityLevel) VALUES (@parkCode, @emailAddress, @state, @activityLevel)";

                    SqlCommand cmd = new SqlCommand(submitSurvey, conn);

                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.Email);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                return false;
            }

            return true;
        }
    }
}
