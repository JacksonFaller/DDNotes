using System;
using System.Windows.Forms;

namespace Notes.WinForms.Forms
{
    public partial class CreateOrEditCategoryForm : Form
    {
        public string CategoryName { get; set; }

        public CreateOrEditCategoryForm()
        {
            InitializeComponent();
        }

        public CreateOrEditCategoryForm(string categoryName) : this()
        {
            txtCategoryName.Text = categoryName;
            btnCreate.Text = "Изменить";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text.Length == 0)
            {
                MessageBox.Show("Необходимо ввести название категории", "Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
            }
            CategoryName = txtCategoryName.Text;
        }
    }
}
