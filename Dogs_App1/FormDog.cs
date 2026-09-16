using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class FormDog : Form
    {
        private DataSet1 dataSet1;
        private bool isNew;
        private string editChipId;

        public FormDog(DataSet1 ds, bool isNew, string editChipId = "")
        {
            InitializeComponent();
            this.dataSet1 = ds;
            this.isNew = isNew;
            this.editChipId = editChipId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChipID.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isNew)
            {
                DataRow newRow = dataSet1.Dog.NewRow();
                newRow["Chip_ID"] = txtChipID.Text;
                newRow["Name"] = txtName.Text;
                newRow["Breed"] = txtBreed.Text;
                dataSet1.Dog.Rows.Add(newRow);
            }

            dataSet1.Dog.AcceptChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FormDog_Load(object sender, EventArgs e)
        {
            // Оставьте пустым
        }
    }
}
