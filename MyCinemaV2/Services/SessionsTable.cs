using MyCinemaV2.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MyCinemaV2.Services
{
    public class SessionsTable
    {
        private static MySqlConnectionStringBuilder _connectionStringBuilder = new()
        {
            Server = "localhost",
            Database = "moviesdb",
            UserID = "root",
            Password = "root"
        };
        private static MySqlConnection _connection = new(_connectionStringBuilder.ConnectionString);

        public List<SessionModel> GetSessionsList(uint movieId)
        {
            List<SessionModel> list = new();

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM sessiontable WHERE Movie_Id = @movieId ORDER BY Session ASC", _connection);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new SessionModel
                        {
                            Id = (uint)reader[0],
                            Movie_Id = (uint)reader[1],
                            Session = (DateTime)reader[2]
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return list;
        }
    }
}