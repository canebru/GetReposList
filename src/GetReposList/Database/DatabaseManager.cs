using System.Collections.Generic;
using System.Data.SqlClient;
using GetReposList.Data;

namespace GetReposList.Database
{
    public class DatabaseManager : IDatabaseManager
    {
        private const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<RepositoryItem> GetItems()
        {
            List<RepositoryItem> items = new List<RepositoryItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    connection.Open();

                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"SELECT * FROM dbo.TestTable";
                    
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var item = new RepositoryItem();
                        item.Id = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.FullName = reader.GetString(2);
                        items.Add(item);
                    }
                }
            }

            return items;
        }

        public void InsertRow(int id, string name, string fullName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    connection.Open();

                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = @"INSERT INTO dbo.TestTable(Id,name,fullName) VALUES(@id,@name,@full)";

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@full", fullName);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
