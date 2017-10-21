using System.Collections.Generic;
using Notes.Model;

namespace Notes.DataLayer
{
    public interface IUsersRepository
    {
        User Create(User user);
        void Delete(int id);
        User Get(int id);
        IEnumerable<User> GetUsers();
        IEnumerable<Category> GetCategories(int id);
    }
}
