using System.Collections.Generic;
using Notes.Model;

namespace Notes.DataLayer
{
    public interface ICategoriesRepository
    {
        Category Create(int userId, string name);
        void Delete(int id);
        Category Get(int id);
        Category Update(string name, int id);
    }
}
