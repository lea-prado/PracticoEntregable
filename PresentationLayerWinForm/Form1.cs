using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayerWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'practicoNetDataSe.Employee' Puede moverla o quitarla según sea necesario.
            this.employeeTableAdapter.Fill(this.practicoNetDataSe.Employee);

        }
        private void AddNewEmployee_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'practicoEntregableDataSet5.EmployeesTPH' Puede moverla o quitarla según sea necesario.
            this.employeeTableAdapter.Fill(this.practicoNetDataSe.Employee);
            comboBox1.Items.Add("FullTimeEmployee");
            comboBox1.Items.Add("ParTimeEmployee");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                int id = (int)row.Cells[0].Value;
                Controlador c = new Controlador();
                c.DeleteEmployee(id);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataAccessLayer.DALEmployeesEF en = new DataAccessLayer.DALEmployeesEF();
            BusinessLogicLayer.BLEmployees bus = new BusinessLogicLayer.BLEmployees(en);

            if (comboBox1.Text == "FullTimeEmployee")
            {
                Shared.Entities.FullTimeEmployee fte = new Shared.Entities.FullTimeEmployee()
                {
                    Name = textBox1.Text,
                    StartDate = dateTimePicker1.Value,
                    Salary = Int32.Parse(textBox2.Text)
                };
                bus.AddEmployee(fte);
            }
            else
            {
                Shared.Entities.PartTimeEmployee fte = new Shared.Entities.PartTimeEmployee()
                {
                    Name = textBox1.Text,
                    StartDate = dateTimePicker1.Value,
                    HourlyRate = Int32.Parse(textBox3.Text)
                };
                bus.AddEmployee(fte);
            }
            this.Close();
        }
    
    }
}
