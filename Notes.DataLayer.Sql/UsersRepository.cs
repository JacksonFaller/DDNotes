using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Notes.Model;

namespace Notes.DataLayer.Sql
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connectionString;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly INotesRepository _notesRepository;
        public UsersRepository(
            string connectionString, ICategoriesRepository categoriesRepository, INotesRepository notesRepository)
        {
            _connectionString = connectionString;
            _categoriesRepository = categoriesRepository;
            _notesRepository = notesRepository;
        }
        public User Create(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Users output Inserted.Id values (@name, @password)";
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@password", user.Password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new IOException("Insert query did not return Id");
                        user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    }
                    return user;
                }
            }
        }

        public void Delete(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Users where Name = @name";
                    command.Parameters.AddWithValue("@name", name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Users where Id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public User Get(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Users where Name = @name";
                    command.Parameters.AddWithValue("@name", name);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Пользователь с именем: {name} не найден");

                        User user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = name,
                            Password = reader.GetString(reader.GetOrdinal("Password"))
                        };
                        user.Categories = _categoriesRepository.GetCategories(user.Id);
                        user.Notes = _notesRepository.GetUsersNotes(user.Id);
                        return user;
                    }
                }
            }
        }

        public User Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Users where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Пользователь с id: {id} не найден");

                        User user = new User
                        {
                            Id = id,
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Password = reader.GetString(reader.GetOrdinal("Password"))
                        };
                        user.Categories = _categoriesRepository.GetCategories(user.Id);
                        user.Notes = _notesRepository.GetUsersNotes(user.Id);
                        return user;
                    }
                }
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Users";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Password = reader.GetString(reader.GetOrdinal("Password"))
                            };
                            user.Categories = _categoriesRepository.GetCategories(user.Id);
                            user.Notes = _notesRepository.GetUsersNotes(user.Id);
                            yield return user;
                        }
                    }
                }
            }
        }
    }
}
