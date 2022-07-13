using MyCinemaV2.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MyCinemaV2.Services
{
    public class SeatsTable
    {
        private static MySqlConnectionStringBuilder _connectionStringBuilder = new()
        {
            Server = "localhost",
            Database = "moviesdb",
            UserID = "root",
            Password = "root"
        };
        private static MySqlConnection _connection = new(_connectionStringBuilder.ConnectionString);

        public List<SeatModel> GetList(uint sessionId)
        {
            List<SeatModel> list = new();

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM seatstable WHERE Session_Id = @sessionId", _connection);
                cmd.Parameters.AddWithValue("@sessionId", sessionId);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new SeatModel
                        {
                            Id = (uint)reader[0],
                            Movie_Id = (uint)reader[1],
                            Session_Id = (uint)reader[2],
                            Seat_Row = (uint)reader[3],
                            Seat_Number = (uint)reader[4],
                            Status = (uint)reader[5]
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
        public SeatModel GetModel(uint id)
        {
            SeatModel seat = null;

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM seatstable WHERE Id = @id;", _connection);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        seat = new SeatModel
                        {
                            Id = (uint)reader[0],
                            Movie_Id = (uint)reader[1],
                            Session_Id = (uint)reader[2],
                            Seat_Row = (uint)reader[3],
                            Seat_Number = (uint)reader[4],
                            Status = (uint)reader[5]
                        };
                    }
                }
                catch (Exception ex)
                {
                    _connection.Close();
                }
            }
            return seat;
        }
        public void UpdateModel(SeatModel seat)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("UPDATE seatstable SET Status = @status WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@id", seat.Id);
                cmd.Parameters.AddWithValue("@status", seat.Status);

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
        public void SetStatusById(uint id, uint status)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("UPDATE seatstable SET Status = @status WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@status", status);

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
        public void DeleteByMovieId(uint movieId)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("DELETE FROM seatstable WHERE Movie_Id = @movieId", _connection);
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