using System.Data;
using Microsoft.Data.SqlClient;

namespace TinyUrl_Api.Db_Handler
{
    public class DbHelper
    {
        private readonly string _connectionString;
        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TinyUrlDb")!;
        }

        public async Task<DataTable> ExcuteQueryTable(string procName, SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(procName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                        await connection.OpenAsync();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex) 
            {
                throw;
            }
            return table;
        }

    }
}
