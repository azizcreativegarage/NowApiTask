using Microsoft.Data.SqlClient;
using NowApiTask.IServices;
using NowApiTask.Model;
using System.Data;
using Dapper;
using NowApiTask.auth;

namespace NowApiTask.Services
{
    public class BalanceRepository : IBalanceRepository
    {
        public List<Balance> GetBalance()
        {
            List<Balance> balancetable = new List<Balance>();

            using (IDbConnection con = new SqlConnection(ConnectionClass.connectionstringvalue))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    balancetable = con.Query<Balance>("SP_Balance", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            return balancetable;
        }
    }
}
