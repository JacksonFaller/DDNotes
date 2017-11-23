using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Notes.Model;

namespace Notes.WinForms.Forms.BL
{
    class MainFormBL
    {
        public readonly ServiceClient ServiceClient = new ServiceClient("http://localhost:59358/api/");
        public User User;
        public readonly BindingList<Note> Notes = new BindingList<Note>();
        public readonly BindingList<Note> SharedNotes = new BindingList<Note>();
        public readonly List<int> FilteredCategoriesIndices = new List<int>();

        public void CreateNote(string title, string text)
        {
            Notes.Add(ServiceClient.CreateNote(
                new Note
                {
                    Title = title,
                    Text = text,
                    Creator = User.Id,
                    Categories = Enumerable.Empty<Category>()
                }));
        }

        public void AddCategoriesToNewNote(List<Category> addedCategories)
        {
            if (addedCategories.Count == 0) return;
            foreach (var category in addedCategories)
            {
                ServiceClient.AddCategoryToNote(Notes[Notes.Count - 1].Id, category.Id);
            }
            Notes.Last().Categories = addedCategories;
            User.Categories.ToList().AddRange(addedCategories);
        }

        public void EditNote(string title, string text, int selected)
        {
            var note = ServiceClient.UpdateNote(new Note
            {
                Id = Notes[selected].Id,
                Title = title,
                Text = text
            });
            Notes[selected].Title = note.Title;
            Notes[selected].Text = note.Text;
            Notes[selected].ChangingDate = note.ChangingDate;
        }

        public void AddCategoriesToNote(List<Category> addedCategories, int selected)
        {
            foreach (var category in addedCategories)
            {
                ServiceClient.AddCategoryToNote(Notes[selected].Id, category.Id);
            }
            User.Categories.ToList().AddRange(addedCategories);
        }

        public void RemoveCategoriesFromNote(List<Category> removedCategories, int selected)
        {
            foreach (var category in removedCategories)
            {
                ServiceClient.RemoveCategoryFromNote(Notes[selected].Id, category.Id);
            }
        }

        public void DeleteNote(int selected)
        {
            ServiceClient.DeleteNote(Notes[selected].Id);
            Notes.RemoveAt(selected);
        }

        public void UpdateSharedNotes()
        {
            SharedNotes.Clear();
            SharedNotes.AddRange(ServiceClient.GetSharedNotes(User.Id));
        }

        public IEnumerable<int> Filter(List<int> selectedCategoriestIndices, IList<Category> categories)
        {
            for (int i = 0; i < Notes.Count; i++)
            {
                foreach (var index in selectedCategoriestIndices)
                {
                    if (!Notes[i].Categories.Select(x => x.Id).Contains(categories[index].Id))
                        continue;
                    yield return i;
                    break;
                }
            }
            FilteredCategoriesIndices.Clear();
            FilteredCategoriesIndices.AddRange(selectedCategoriestIndices);
        }

        public void ClearFilter()
        {
            FilteredCategoriesIndices.Clear();
        }

        public void UpdateSharedNote(int selected)
        {
            var note = ServiceClient.UpdateNote(new Note
            {
                Id = SharedNotes[selected].Id,
                Title = SharedNotes[selected].Title,
                Text = SharedNotes[selected].Text
            });
            SharedNotes[selected].Title = note.Title;
            SharedNotes[selected].Text = note.Text;
            SharedNotes.ResetItem(selected);
        }

        public void ChangeUser()
        {
            Notes.Clear();
            SharedNotes.Clear();
            User = null;
            FilteredCategoriesIndices.Clear();
        }

        public void UpdateNotes()
        {
            Notes.Clear();
            Notes.AddRange(ServiceClient.GetUserNotes(User.Id));
        }

        public void Load(User user)
        {
            User = user;
            User.Notes = ServiceClient.GetUserNotes(User.Id);
            User.Categories = ServiceClient.GetUserCategories(User.Id);
            Notes.AddRange(User.Notes);
        }

        public IEnumerable<int> DeleteCategories(List<Category> removedCategories)
        {
            if (removedCategories.Count == 0) yield break;
            for (int i = 0; i < Notes.Count; i++)
            {
                var categories = Notes[i].Categories as IList<Category> ?? Notes[i].Categories.ToList();
                var result = categories.Except(
                    removedCategories, new CategoriesComparer()).ToList();
                if (result.Count >= categories.Count) continue;
                Notes[i].Categories = result;
                yield return i;
            }
        }

        public IEnumerable<int> UpdateCategories(List<Category> updatedCategories)
        {
            if (updatedCategories.Count == 0) yield break;
            bool isUpdated = false;
            for (int i = 0; i < Notes.Count; i++)
            {
                foreach (var category in Notes[i].Categories)
                {
                    var updCategory = updatedCategories.Find(x => x.Id == category.Id);
                    if (updCategory == null) continue;
                    category.Name = updCategory.Name;
                    isUpdated = true;
                }
                if (!isUpdated) continue;
                yield return i;
                isUpdated = false;
            }
        }

        

        public void UpdateUserCategories(IList<Category> categories)
        {
            User.Categories = categories;
        }

        public class CategoriesComparer : IEqualityComparer<Category>
        {
            public bool Equals(Category x, Category y)
            {
                if (ReferenceEquals(x, y))
                    return true;
                return x?.Id == y?.Id;
            }

            public int GetHashCode(Category category)
            {
                return category.Id.GetHashCode();
            }
        }
    }
}
