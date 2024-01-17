using Microsoft.Data.Sqlite;

namespace ADO.NET_EF_Test_Aplication.AdoNet {
    static public class AdoNet
    {
        public static void AdoNetDatabase()
        {
            using (var connection = new SqliteConnection("Data Source=./usersdata.db"))
            {
                connection.Open();
                CreateDatabase(connection);
                InsertData(connection);
                ReadData(connection);
            }
            Console.Read();
        }

        private static void ReadData(SqliteConnection connection)
        {
            var sql = "SELECT id, name FROM users;";
            var command = connection.CreateCommand();
            command.CommandText = sql;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var message = reader.GetInt32(0) + ": " + reader.GetString(1);
                    Console.WriteLine(message);
                }
            }
        }

        private static void InsertData(SqliteConnection connection)
        {
            // var randomName = Guid.NewGuid().ToString();
            // InsertUser(connection, randomName.Remove(4, randomName.Length-4));
            if (CheckThatTableExists(connection, out var command, out var sql)) return;
            using (var transaction = connection.CreateCommand())
            {
                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO users (name) VALUES ('John Doe');";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO users (name) VALUES ('Jane Doe');";
                command.ExecuteNonQuery();
            }
        }

        private static void CreateDatabase(SqliteConnection connection)
        {
            if (CheckThatTableExists(connection, out var command, out var sql)) return;
            command.CommandText = "CREATE TABLE users (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL);";
            command.ExecuteNonQuery();
            using (command = connection.CreateCommand())
            {
                command.CommandText = "CREATE INDEX idx_users_name ON users (name);";
                command.ExecuteNonQuery();
            }
        }

        private static bool CheckThatTableExists(SqliteConnection connection, out SqliteCommand command, out string sql)
        {
            command = connection.CreateCommand();
            sql = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='users';";
            command.CommandText = sql;
            var tableExists = (long)command.ExecuteScalar();
            if (tableExists == 1) return true;
            return false;
        }
        
        private static void InsertUser(SqliteConnection connection, string name)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO users (name) VALUES (@name);";
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
        }
    }
}