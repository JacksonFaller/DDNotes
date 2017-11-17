using Notes.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Notes.WinForms
{
    internal class ServiceClient
    {
        private readonly HttpClient _client;

        public ServiceClient(string connectionString)
        {
            _client = new HttpClient {BaseAddress = new Uri(connectionString)};
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public User CreateUser(User user)
        {
            var response = _client.PostAsJsonAsync("users", user).Result;
            return ResponseParse<User>(response);
        }

        public User GetUser(string name)
        {
            var response = _client.GetAsync($"users/byName/{name}").Result;
            return ResponseParse<User>(response);
        }

        public IEnumerable<Note> GetUserNotes(int userId)
        {
            var response = _client.GetAsync($"users/{userId}/notes").Result;
            return ResponseParse<IEnumerable<Note>>(response);
        }

        public Note CreateNote(Note note)
        {
            var response = _client.PostAsJsonAsync("notes", note).Result;
            return ResponseParse<Note>(response);
        }

        public Note UpdateNote(Note note)
        {
            var response = _client.PutAsJsonAsync($"notes/{note.Id}", note).Result;
            return ResponseParse<Note>(response);
        }

        public void DeleteNote(int id)
        {
            var response = _client.DeleteAsync($"notes/{id}").Result;
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
        }

        public IEnumerable<User> GetSharedUsers(int noteId)
        {
            var response = _client.GetAsync($"notes/{noteId}/sharedUsers").Result;
            return ResponseParse<IEnumerable<User>>(response);
        }

        public IEnumerable<Category> GetUserCategories(int userId)
        {
            var response = _client.GetAsync($"users/{userId}/categories").Result;
            return ResponseParse<IEnumerable<Category>>(response);
        }

        public Category CreateCategory(string name, int userId)
        {
            var category = new Category {Name = name, UserId = userId};
            var response = _client.PostAsJsonAsync("categories", category).Result;
            return ResponseParse<Category>(response);
        }

        public Category UpdateCategory(int id, string name)
        {
            var response = _client.PutAsJsonAsync($"categories/{id}", name).Result;
            return ResponseParse<Category>(response);
        }

        public void DeleteCategory(int id)
        {
            var response = _client.DeleteAsync($"categories/{id}").Result;
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
        }

        public void ShareNote(int noteId, int userId)
        {
            var response = _client.PostAsJsonAsync($"notes/{noteId}/share/{userId}", string.Empty).Result;
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
        }

        public void UnshareNote(int noteId, int userId)
        {
            var response = _client.DeleteAsync($"notes/{noteId}/unshare/{userId}").Result;
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
        }

        public IEnumerable<Note> GetSharedNotes(int userId)
        {
            var response = _client.GetAsync($"users/{userId}/sharedNotes").Result;
            return ResponseParse<IEnumerable<Note>>(response);
        }

        public User ValidateUser(User user)
        {
            var response = _client.PostAsJsonAsync("users/validate", user).Result;
            return ResponseParse<User>(response);
        }

        private T ResponseParse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<T>().Result;
            }
            throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
        }

        public void AddCategoryToNote(int noteId, int categoryId)
        {
            var response = _client.PutAsJsonAsync($"notes/{noteId}/addCategory", categoryId).Result;
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
        }

        public void RemoveCategoryFromNote(int noteId, int categoryId)
        {
            var response = _client.PutAsJsonAsync($"notes/{noteId}/removeCategory", categoryId).Result;
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(response.Content.ReadAsStringAsync().Result);
        }

        public IEnumerable<Category> GetNoteCategories(int noteId)
        {
            var response = _client.GetAsync($"notes/{noteId}/categories").Result;
            return ResponseParse<IEnumerable<Category>>(response);
        }
    }
}
