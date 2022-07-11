using MyCinemaV2.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MyCinemaV2.Services
{
    public class MoviesTable
    {
        private static MySqlConnectionStringBuilder _connectionStringBuilder = new()
        {
            Server = "localhost",
            Database = "moviesdb",
            UserID = "root",
            Password = "root"
        };
        private static MySqlConnection _connection = new(_connectionStringBuilder.ConnectionString);

        public List<MovieModel> GetMoviesList(uint status)
        {
            List<MovieModel> list = new();

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM moviestable WHERE Status = @status", _connection);
                cmd.Parameters.AddWithValue("@status", status);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new MovieModel
                        {
                            Id = (uint)reader[0],
                            Name = (string)reader[1],
                            Description = (string)reader[2],
                            Duration = (TimeSpan)reader[3],
                            Thumbnail = (string)reader[4],
                            Price = (float)reader[5],
                            Genre = (string)reader[6],
                            Status = (uint)reader[7]
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return list;
        }
        public List<MovieModel> GetMoviesList()
        {
            List<MovieModel> list = new();

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM moviestable", _connection);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new MovieModel
                        {
                            Id = (uint)reader[0],
                            Name = (string)reader[1],
                            Description = (string)reader[2],
                            Duration = (TimeSpan)reader[3],
                            Thumbnail = (string)reader[4],
                            Price = (float)reader[5],
                            Genre = (string)reader[6],
                            Status = (uint)reader[7]
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return list;
        }
        public MovieModel GetMovieModel(uint id)
        {
            MovieModel movie = null;

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM moviestable WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        movie = new MovieModel
                        {
                            Id = (uint)reader[0],
                            Name = (string)reader[1],
                            Description = (string)reader[2],
                            Duration = (TimeSpan)reader[3],
                            Thumbnail = (string)reader[4],
                            Price = (float)reader[5],
                            Genre = (string)reader[6],
                            Status = (uint)reader[7]
                        };
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return movie;
        }
    }
}