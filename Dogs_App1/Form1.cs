using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class Form1 : Form
    {
        private readonly string filePath = "Data.xml";
        public Form1()
        {
            InitializeComponent();
        }
        private void SaveToXML()
        {
            dataSet1.Dog.AcceptChanges();
            dataSet1.TrainingSession.AcceptChanges();
            string filePath = "Data.xml";
            dataSet1.WriteXml(filePath);
        }

        private void LoadFromXML()
        {
            dataSet1.Clear();
            if (File.Exists(filePath))
            {
                dataSet1.ReadXml(filePath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFromXML();
            dataGridView1.DataSource = dataSet1.Dog;
        }

        // 2. Автоматическая фильтрация тренировок при выборе собаки в первой таблице
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != DBNull.Value)
            {
                dataGridView2.DataSource = dataSet1.TrainingSession;
                string selectedChipId = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = $"FK_ChipID = '{selectedChipId}'";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FormDog form = new FormDog(dataSet1, true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveToXML();
            }
        }

     
    }
}
