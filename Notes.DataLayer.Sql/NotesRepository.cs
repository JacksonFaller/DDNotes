using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Notes.Model;
using System.Text;

namespace Notes.DataLayer.Sql
{
    public class NotesRepository : INotesRepository
    {
        private readonly string _connectionString;
        public NotesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Note Create(Note note)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    note.CreationDate = DateTime.Now;
                    note.ChangingDate = DateTime.Now;

                    command.CommandText = "insert into Notes output Inserted.Id values (@title, @text, @changingDate, @creationDate, @creator)";
                    command.Parameters.AddWithValue("@title", note.Title);
                    command.Parameters.AddWithValue("@text", note.Text);
                    command.Parameters.AddWithValue("@changingDate", note.ChangingDate);
                    command.Parameters.AddWithValue("@creationDate", note.CreationDate);
                    command.Parameters.AddWithValue("@creator", note.Creator);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new IOException("Insert query did not return Id");
                        note.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    }
                    return note;
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
                    command.CommandText = "delete from Notes output deleted.Id where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Заметка с id: {id} не найдена");
                    }
                }
            }
        }

        public void AddCategory(int noteId, int categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into NotesToCategories values (@categoryId, @noteId)";
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveCategory(int noteId, int categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from NotesToCategories where CategoryId = @categoryId and NoteId = @noteId";
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Note> GetUsersNotes(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = 
                        "select * from Notes where Creator = @userId";
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Note note = new Note()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Text = reader.GetString(reader.GetOrdinal("Text")),
                                ChangingDate = reader.GetDateTime(reader.GetOrdinal("Changing date")),
                                CreationDate = reader.GetDateTime(reader.GetOrdinal("Creation date")),
                                Creator = reader.GetInt32(reader.GetOrdinal("Creator"))
                            };
                            note.Categories = GetNoteCategories(note.Id);
                            //note.Shared = GetSharedUsers(note.Id);
                            yield return note;
                        }
                    }
                }
            }
        }

        public IEnumerable<Note> GetSharedNotes(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select NoteId from NotesToUsers where UserId = @userId";
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        using (var getNoteConnection = new SqlConnection(_connectionString))
                        {
                            getNoteConnection.Open();
                            while (reader.Read())
                            {
                                yield return Get(reader.GetInt32(reader.GetOrdinal("NoteId")), getNoteConnection);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<User> GetSharedUsers(int noteId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "select * from Users where Id in (select UserId from NotesToUsers where NoteId = @noteId)";
                    command.Parameters.AddWithValue("@noteId", noteId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new User()
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                Id = reader.GetInt32(reader.GetOrdinal("Id"))
                            };
                        }
                    }
                }
            }
        }

        public IEnumerable<Category> GetNoteCategories(int noteId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "select * from Categories where Id in (select CategoryId from NotesToCategories where NoteId = @noteId)";
                    command.Parameters.AddWithValue("@noteId", noteId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Category
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                            };
                        }
                    }
                }
            }
        }

        public Note Get(string title)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Notes where Title = @title";
                    command.Parameters.AddWithValue("@title", title);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Заметка с заголовком: {title} не найдена");

                        Note note = new Note()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Text = reader.GetString(reader.GetOrdinal("Text")),
                            Creator = reader.GetInt32(reader.GetOrdinal("Creator")),
                            ChangingDate = reader.GetDateTime(reader.GetOrdinal("Changing date")),
                            CreationDate = reader.GetDateTime(reader.GetOrdinal("Creation date"))
                        };
                        note.Categories = GetNoteCategories(note.Id);
                        note.Shared = GetSharedUsers(note.Id);
                        return note;
                    }
                }
            }
        }

        public Note Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return Get(id, connection);
            }
        }

        private Note Get(int id, SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select * from Notes where Id = @id";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new ArgumentException($"Заметка с id: {id} не найдена");

                    Note note = new Note
                    {
                        Id = id,
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Text = reader.GetString(reader.GetOrdinal("Text")),
                        ChangingDate = reader.GetDateTime(reader.GetOrdinal("Changing date")),
                        CreationDate = reader.GetDateTime(reader.GetOrdinal("Creation date")),
                        Creator = reader.GetInt32(reader.GetOrdinal("Creator"))
                    };
                    note.Categories = GetNoteCategories(note.Id);
                    //note.Shared = GetSharedUsers(note.Id);
                    return note;
                }
            }
        }

        public Note Update(Note note)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    note.ChangingDate = DateTime.Now;
                    StringBuilder commmandBuilder = new StringBuilder("update Notes set ");
                    bool isTitleNull = note.Title == null, isTextNull = note.Text == null;

                    StringBuilder outputParam = new StringBuilder("output inserted.[Creation date], inserted.Creator");
                    if (isTitleNull)
                    {
                        outputParam.Append(", inserted.Title");
                    }
                    else
                    {
                        commmandBuilder.Append("Title = @title, ");
                        command.Parameters.AddWithValue("@title", note.Title);
                    }
                    if (isTextNull)
                    {
                        outputParam.Append(", inserted.Text");
                    }
                    else
                    {
                        commmandBuilder.Append("Text = @text, ");
                        command.Parameters.AddWithValue("@text", note.Text);
                    }
                    commmandBuilder.Append($"[Changing date] = @date {outputParam} where Id = @id");

                    command.CommandText = commmandBuilder.ToString();
                    
                    command.Parameters.AddWithValue("@date", note.ChangingDate);
                    command.Parameters.AddWithValue("@id", note.Id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Заметка с id: {note.Id} не найдена");

                        note.CreationDate = reader.GetDateTime(reader.GetOrdinal("Creation date")).ToLocalTime();
                        note.Creator = reader.GetInt32(reader.GetOrdinal("Creator"));
                        if (isTitleNull)
                            note.Title = reader.GetString(reader.GetOrdinal("Title"));
                        if (isTextNull)
                            note.Text = reader.GetString(reader.GetOrdinal("Text"));
                    }
                    return note;
                }
            }
        }

        public void Share(int noteId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into NotesToUsers values (@noteId, @userId)";
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Unshare(int noteId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from NotesToUsers where NoteId = @noteId and UserId = @userId";
                    command.Parameters.AddWithValue("@noteId", noteId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
