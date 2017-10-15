using Notes.Model;

namespace Notes.DataLayer
{
    public interface IUsersRepository
    {
        User Create(User user);
        void Delete(string name);
        User Get(string name);
    }
}
