using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace xml
{
    public partial class Form1 : Form
    {
        private List<person> p1;
        private string xmlFilePath = Environment.CurrentDirectory + "\\personas.xml";

        public Form1()
        {
            InitializeComponent();
            p1 = new List<person>();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            p1.Add(new person() { id = 1, nombre = "Isabela" });
            p1.Add(new person() { id = 2, nombre = "Andres" });

            SaveToXml();
            MessageBox.Show("Creado");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            LoadFromXml();
        }

      
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                p1[selectedIndex].nombre = txtNombre.Text;
                SaveToXml();
                LoadFromXml();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                p1.RemoveAt(selectedIndex);
                SaveToXml();
                LoadFromXml();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                txtNombre.Text = p1[selectedIndex].nombre;
            }
        }

        private void LoadFromXml()
        {
            XmlSerializer serial = new XmlSerializer(typeof(List<person>));
            using (System.IO.FileStream fs = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
            {
                p1 = serial.Deserialize(fs) as List<person>;
            }
            dataGridView1.DataSource = p1;
        }

        private void SaveToXml()
        {
            XmlSerializer serial = new XmlSerializer(typeof(List<person>));
            using (System.IO.FileStream fs = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write))
            {
                serial.Serialize(fs, p1);
            }
        }

    }
}
