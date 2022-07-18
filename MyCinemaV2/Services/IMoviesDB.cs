using MyCinemaV2.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MyCinemaV2.Services
{
    public interface IMoviesDB
    {
        private static MySqlConnectionStringBuilder _connectionStringBuilder = new()
        {
            Server = "localhost",
            Database = "moviesdb",
            UserID = "root",
            Password = "root"
        };
        private static MySqlConnection _connection = new(_connectionStringBuilder.ConnectionString);

         static void Create(MovieModel movie)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("INSERT INTO moviestable (Name, Description, Duration, Thumbnail, Price, Genre, Status) VALUES (@name, @description, @duration, @thumbnail, @price, @genre, @status)", _connection);
                cmd.Parameters.AddWithValue("@name", movie.Name);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@duration", movie.Duration);
                cmd.Parameters.AddWithValue("@thumbnail", movie.Thumbnail);
                cmd.Parameters.AddWithValue("@price", movie.Price);
                cmd.Parameters.AddWithValue("@genre", movie.Genre);
                cmd.Parameters.AddWithValue("@status", movie.Status);

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
         static void Create(SessionModel session)
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
         static void Create(SeatModel seat)
        {
            using (_connection)
            {
                var row = 1;
                for (var num = 1; num <= 50; num++)
                {
                    if (num == 11 || num == 21 || num == 31 || num == 41)
                    {
                        row++;
                    }

                    MySqlCommand cmd = new("INSERT INTO seatstable (Movie_Id, Session_Id, Seat_Row, Seat_Number) VALUES (@movieId, @sessionId, @seatRow, @seatNumber)", _connection);
                    cmd.Parameters.AddWithValue("@movieId", seat.Movie_Id);
                    cmd.Parameters.AddWithValue("@sessionId", seat.Session_Id);
                    cmd.Parameters.AddWithValue("@seatRow", row);
                    cmd.Parameters.AddWithValue("@seatNumber", num);

                    try
                    {
                        _connection.Open();

                        MySqlDataReader reader = cmd.ExecuteReader();

                        _connection.Close();
                    }
                    catch (Exception ex)
                    {
                        _connection.Close();
                    }
                }
            }
        }

         static MovieModel GetModel(MovieModel movie)
        {
            MovieModel result = null;

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM moviestable WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@id", movie.Id);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new MovieModel
                        {
                            Id = (uint)reader[0],
                            Name = (string)reader[1],
                            Description = (string)reader[2],
                            Duration = (TimeSpan)reader[3],
                            Thumbnail = (string)reader[4],
                            Price = (decimal)reader[5],
                            Genre = (string)reader[6],
                            Status = (uint)reader[7]
                        };
                    }
                }
                catch (Exception ex)
                {
                    _connection.Close();
                }
            }
            return result;
        }
         static SessionModel GetModel(SessionModel session)
        {
            SessionModel result = null;

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM sessiontable WHERE Id = @id;", _connection);
                cmd.Parameters.AddWithValue("@id", session.Id);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new SessionModel
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
            return result;
        }
         static SeatModel GetModel(SeatModel seat)
        {
            SeatModel result = null;

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM seatstable WHERE Id = @id;", _connection);
                cmd.Parameters.AddWithValue("@id", seat.Id);

                try
                {
                    _connection.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new SeatModel
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
            return result;
        }

         static List<MovieModel> GetList(MovieModel movie)
        {
            List<MovieModel> list = new();

            using (_connection)
            {
                MySqlCommand cmd = null;
                if (movie == null)
                {
                    cmd = new("SELECT * FROM moviestable", _connection);
                }
                else
                {
                    cmd = new("SELECT * FROM moviestable WHERE Status = @status", _connection);
                    cmd.Parameters.AddWithValue("@status", movie.Status);
                }

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
                            Price = (decimal)reader[5],
                            Genre = (string)reader[6],
                            Status = (uint)reader[7]
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
         static List<SessionModel> GetList(SessionModel session)
        {
            List<SessionModel> list = new();

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM sessiontable WHERE Movie_Id = @movieId ORDER BY Session ASC", _connection);
                cmd.Parameters.AddWithValue("@movieId", session.Movie_Id);

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
         static List<SeatModel> GetList(SeatModel seat)
        {
            List<SeatModel> list = new();

            using (_connection)
            {
                MySqlCommand cmd = new("SELECT * FROM seatstable WHERE Session_Id = @sessionId", _connection);
                cmd.Parameters.AddWithValue("@sessionId", seat.Session_Id);

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

         static uint? GetIdMaxValue(SessionModel session)
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

         static void Update(MovieModel movie)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("UPDATE moviestable SET Name = @name, Description = @description, Duration = @duration, Thumbnail = @thumbnail, Price = @price, Genre = @genre, Status = @status WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@id", movie.Id);
                cmd.Parameters.AddWithValue("@name", movie.Name);
                cmd.Parameters.AddWithValue("@description", movie.Description);
                cmd.Parameters.AddWithValue("@duration", movie.Duration);
                cmd.Parameters.AddWithValue("@thumbnail", movie.Thumbnail);
                cmd.Parameters.AddWithValue("@price", movie.Price);
                cmd.Parameters.AddWithValue("@genre", movie.Genre);
                cmd.Parameters.AddWithValue("@status", movie.Status);

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
         static void Update(SessionModel session)
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
         static void Update(SeatModel seat)
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

         static void SetStatus(SeatModel seat, uint status)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("UPDATE seatstable SET Status = @status WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@id", seat.Id);
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

         static void Delete(MovieModel movie)
        {
            using (_connection)
            {
                MySqlCommand cmd = new("DELETE FROM moviestable WHERE Id = @id", _connection);
                cmd.Parameters.AddWithValue("@id", movie.Id);

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
         static void Delete(SessionModel session)
        {
            using (_connection)
            {
                MySqlCommand cmd = null;
                if (session.Movie_Id == null)
                {
                    cmd = new("DELETE FROM sessiontable WHERE Id = @id", _connection);
                    cmd.Parameters.AddWithValue("@id", session.Id);
                }
                else
                {
                    cmd = new("DELETE FROM sessiontable WHERE Movie_Id = @movieId", _connection);
                    cmd.Parameters.AddWithValue("@movieId", session.Movie_Id);
                }

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
         static void Delete(SeatModel seat)
        {
            using (_connection)
            {
                MySqlCommand cmd = null;
                if (seat.Movie_Id == null)
                {
                    cmd = new("DELETE FROM seatstable WHERE Session_Id = @sessionId", _connection);
                    cmd.Parameters.AddWithValue("@sessionId", seat.Session_Id);
                }
                else
                {
                    cmd = new("DELETE FROM seatstable WHERE Movie_Id = @movieId", _connection);
                    cmd.Parameters.AddWithValue("@movieId", seat.Movie_Id);
                }

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