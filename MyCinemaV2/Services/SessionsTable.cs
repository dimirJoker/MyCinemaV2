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

        public void Create(SessionModel session)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("INSERT INTO sessiontable (Movie_Id, Session) VALUES (@movieId, @session)", _connection);
                cmd.Parameters.AddWithValue("@movieId", session.Movie_Id);
                cmd.Parameters.AddWithValue("@session", session.Session);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                }
            }
        }
        public SessionModel GetModel(uint id)
        {
            SessionModel session = null;

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM sessiontable WHERE Id = @id;", _connection);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        session = new SessionModel
                        {
                            Id = (uint)reader[0],
                            Movie_Id = (uint)reader[1],
                            Session = (DateTime)reader[2]
                        };
                    }
                }
                catch (Exception ex)
                {
                    _connection.Close();
                }
            }
            return session;
        }
        public List<SessionModel> GetListByMovieId(uint movieId)
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
                    _connection.Close();
                }
            }
            return list;
        }
        public uint? GetIdMaxValue()
        {
            using (_connection)
            {
                MySqlCommand cmd = new("SELECT MAX(Id) FROM sessiontable", _connection);

                try
                {
                    _connection.Open();

                    return (uint)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                }
            }
            return null;
        }
        public void Update(SessionModel session)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("UPDATE sessiontable SET Session = @session WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@session", session.Session);
                cmd.Parameters.AddWithValue("@id", session.Id);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                }
            }
        }
        public void Delete(uint id)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("DELETE FROM sessiontable WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                }
            }
        }
        public void DeleteAllByMovieId(uint movieId)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("DELETE FROM sessiontable WHERE Movie_Id = @movieId", _connection);
                cmd.Parameters.AddWithValue("@movieId", movieId);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    _connection.Close();
                }
            }
        }
    }
}