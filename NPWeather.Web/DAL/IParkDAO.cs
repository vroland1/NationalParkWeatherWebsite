using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IParkDAO
    {
        Park GetPark(string parkCode);

        IList<Park> GetAllParks();

        IList<Park> GetRankedParks();

        IList<Park> MapToParks(SqlDataReader reader);
    }
}
