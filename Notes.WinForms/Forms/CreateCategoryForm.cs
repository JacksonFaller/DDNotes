using System;
using System.Windows.Forms;

namespace Notes.WinForms.Forms
{
    public partial class CreateCategoryForm : Form
    {
        public string CategoryName { get; set; }
        public CreateCategoryForm()
        {
            InitializeComponent();
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
