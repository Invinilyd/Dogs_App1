using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class Form1 : Form
    {
        private readonly string filePath = "Data.xml";

        public Form1()
        {
            InitializeComponent();
            // dataSet1 уже создан в Form1.Designer.cs, здесь его объявлять не нужно!
        }

        private void SaveToXML()
        {
            // Подтверждаем изменения в обеих таблицах перед записью в файл
            dataSet1.Dog.AcceptChanges();
            dataSet1.TrainingSession.AcceptChanges();
            dataSet1.WriteXml(filePath, XmlWriteMode.WriteSchema);
            UpdateButtonStates();
        }

        private void LoadFromXML()
        {
            dataSet1.Clear();
            if (File.Exists(filePath))
            {
                dataSet1.ReadXml(filePath, XmlReadMode.ReadSchema);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFromXML();

            // BindingSource'ы уже привязаны к dataSet1 в Designer'е (dogBindingSource,
            // trainingSessionBindingSource) — принудительно обновляем их после ReadXml,
            // не переопределяя DataSource напрямую (иначе ломается фильтрация в гриде тренировок).
            dogBindingSource.ResetBindings(false);
            trainingSessionBindingSource.ResetBindings(false);

            // ВАЖНО: Блок с ручным изменением HeaderText УДАЛЕН. 
            // Русские заголовки ("ЧипID", "Кличка" и т.д.) уже настроены в Form1.Designer.cs!

            // Сортировка списка собак по кличке при запуске
            if (dataGridView1.Columns.Count > 1)
            {
                dataGridView1.Sort(dataGridView1.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
            }

            UpdateButtonStates();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToXML();
        }

        private void UpdateButtonStates()
        {
            bool hasDogs = dataSet1.Dog.Rows.Count > 0;
            bool hasTrainings = dataSet1.TrainingSession.Rows.Count > 0;

            button2.Enabled = hasDogs;
            button3.Enabled = hasDogs;
            button4.Enabled = hasDogs;
            button5.Enabled = hasDogs && hasTrainings;
            button6.Enabled = hasDogs && hasTrainings;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Фильтруем через BindingSource.Filter, а не через прямой каст DataSource
            // в DataView (который ломался, т.к. DataSource — это DataTable, а не DataView)
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[chipIDDataGridViewTextBoxColumn.Index].Value != DBNull.Value)
            {
                string selectedChipId = dataGridView1.CurrentRow.Cells[chipIDDataGridViewTextBoxColumn.Index].Value.ToString();
                trainingSessionBindingSource.Filter = $"FK_Chip_ID = '{selectedChipId.Replace("'", "''")}'";
            }
            else
            {
                trainingSessionBindingSource.Filter = "";
            }

            // Выделяем (подсвечиваем) все отображаемые тренировки выбранной собаки
            dataGridView2.ClearSelection();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.Selected = true;
            }
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[0];
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Оставляем пустым, как требует Designer
        }

        // Выделяет и прокручивает грид к строке с указанным индексом
        private void SelectGridRow(DataGridView dataGrid, int index)
        {
            if (index >= 0 && index < dataGrid.Rows.Count)
            {
                dataGrid.ClearSelection();
                dataGrid.CurrentCell = dataGrid.Rows[index].Cells[0];
                dataGrid.Rows[index].Selected = true;
                dataGrid.FirstDisplayedScrollingRowIndex = index;
            }
        }

        // Находит и выделяет собаку по Chip_ID
        private void SelectDogRow(string chipId)
        {
            if (string.IsNullOrEmpty(chipId))
                return;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[chipIDDataGridViewTextBoxColumn.Index].Value != null &&
                    row.Cells[chipIDDataGridViewTextBoxColumn.Index].Value.ToString() == chipId)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = row.Cells[0];
                    row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }

        // Находит и выделяет тренировку по Training_ID (в пределах текущего фильтра дочернего грида)
        private void SelectTrainingRow(string trainingId)
        {
            if (string.IsNullOrEmpty(trainingId))
                return;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[trainingIDDataGridViewTextBoxColumn.Index].Value != null &&
                    row.Cells[trainingIDDataGridViewTextBoxColumn.Index].Value.ToString() == trainingId)
                {
                    dataGridView2.ClearSelection();
                    dataGridView2.CurrentCell = row.Cells[0];
                    row.Selected = true;
                    dataGridView2.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormDog form = new FormDog(dataSet1, true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveToXML();
                SelectDogRow(form.SavedChipId);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataRowView rowView = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                if (rowView != null)
                {
                    string chipId = rowView.Row["Chip_ID"].ToString();
                    FormDog form = new FormDog(dataSet1, false, chipId);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        SaveToXML();
                        SelectDogRow(form.SavedChipId);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите собаку для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (MessageBox.Show("Вы уверены? Это также удалит все связанные тренировки этой собаки.",
                    "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int deletedIndex = dataGridView1.CurrentRow.Index;
                    DataRowView rowView = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        string chipId = rowView.Row["Chip_ID"].ToString();
                        DataRow[] relatedTrainings = dataSet1.TrainingSession.Select($"FK_Chip_ID = '{chipId}'");
                        foreach (DataRow training in relatedTrainings)
                        {
                            training.Delete();
                        }
                        rowView.Row.Delete();
                    }
                    SaveToXML();
                    SelectGridRow(dataGridView1, Math.Min(deletedIndex, dataGridView1.Rows.Count - 1));
                }
            }
            else
            {
                MessageBox.Show("Выберите собаку для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataSet1.Dog.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавьте хотя бы одну собаку!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Если в списке собак что-то выделено — подставляем эту собаку в форму создания тренировки
            string selectedChipId = null;
            if (dataGridView1.CurrentRow != null)
            {
                DataRowView dogRowView = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                if (dogRowView != null)
                {
                    selectedChipId = dogRowView.Row["Chip_ID"].ToString();
                }
            }

            FormTraining form = new FormTraining(dataSet1, false, null, selectedChipId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveToXML();
                // Сначала выделяем собаку тренировки (это обновит фильтр грида тренировок),
                // затем саму новую запись о тренировке
                SelectDogRow(form.SavedChipId);
                SelectTrainingRow(form.SavedTrainingId);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                try
                {
                    DataRowView rowView = dataGridView2.CurrentRow.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        string trainingId;

                        if (rowView.Row.Table.Columns.Contains("Training_ID"))
                            trainingId = rowView.Row["Training_ID"].ToString();
                        else if (rowView.Row.Table.Columns.Contains("TrainingID"))
                            trainingId = rowView.Row["TrainingID"].ToString();
                        else if (rowView.Row.Table.Columns.Contains("ID"))
                            trainingId = rowView.Row["ID"].ToString();
                        else
                            trainingId = rowView.Row[0].ToString();

                        FormTraining form = new FormTraining(dataSet1, true, trainingId);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            SaveToXML();
                            SelectDogRow(form.SavedChipId);
                            SelectTrainingRow(form.SavedTrainingId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при редактировании: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите тренировку для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                if (MessageBox.Show("Удалить эту запись о тренировке?",
                    "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int deletedIndex = dataGridView2.CurrentRow.Index;
                    DataRowView rowView = dataGridView2.CurrentRow.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        rowView.Row.Delete();
                    }
                    SaveToXML();
                    SelectGridRow(dataGridView2, Math.Min(deletedIndex, dataGridView2.Rows.Count - 1));
                }
            }
            else
            {
                MessageBox.Show("Выберите тренировку для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}