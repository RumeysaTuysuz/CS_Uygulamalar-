using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual_Odevi
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		OleDbConnection Connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\Users\\Rumeysa Tüysüz\\OneDrive\\Belgeler\\Database1.accdb\"");

		private void showInformation()
		{
			DataTable dataTable = new DataTable();
			OleDbDataAdapter dataAdapter = new OleDbDataAdapter("SELECT * FROM information", Connection);
			dataAdapter.Fill(dataTable);
			dataGridView1.DataSource = dataTable;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			showInformation();
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			showInformation();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Connection.Open();
			OleDbCommand save = new OleDbCommand("INSERT INTO information (NAME,SURNAME,MAIL,mPosition) VALUES (@p1,@p2,@p3,@p4)", Connection);
			save.Parameters.AddWithValue("@p1", txtName.Text);
			save.Parameters.AddWithValue("@p2", txtSurname.Text);
			save.Parameters.AddWithValue("@p3", txtMail.Text);
			save.Parameters.AddWithValue("@p4", comboBox1.Text);

			save.ExecuteNonQuery();
			Connection.Close();
			MessageBox.Show("Saved in the system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

			txtId.Clear();
			txtName.Clear();
			txtSurname.Clear();
			txtMail.Clear();
			comboBox1.ResetText();

			showInformation();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			Connection.Open();
			OleDbCommand update = new OleDbCommand("UPDATE information SET NAME=@p1,SURNAME=@p2,MAIL=@p3,mPosition=@p4 WHERE ID=@p5", Connection);
			update.Parameters.AddWithValue("@p1", txtName.Text);
			update.Parameters.AddWithValue("@p2", txtSurname.Text);
			update.Parameters.AddWithValue("@p3", txtMail.Text);
			update.Parameters.AddWithValue("@p4", comboBox1.Text);
			update.Parameters.AddWithValue("@p5", txtId.Text);

			update.ExecuteNonQuery();
			Connection.Close();
			MessageBox.Show("Updated in the system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

			txtId.Clear();
			txtName.Clear();
			txtSurname.Clear();
			txtMail.Clear();
			comboBox1.ResetText();

			showInformation();

		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			Connection.Open();
			OleDbCommand delete = new OleDbCommand("DELETE FROM information WHERE ID=@p1", Connection);
			delete.Parameters.AddWithValue("@p1", txtId.Text);
			delete.ExecuteNonQuery();
			Connection.Close();
			MessageBox.Show("Deleted in the system.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

			txtId.Clear();
			txtName.Clear();
			txtSurname.Clear();
			txtMail.Clear();
			comboBox1.ResetText();

			showInformation();

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			int select = dataGridView1.SelectedCells[0].RowIndex;

			txtId.Text = dataGridView1.Rows[select].Cells[0].Value.ToString();
			txtName.Text = dataGridView1.Rows[select].Cells[1].Value.ToString();
			txtSurname.Text = dataGridView1.Rows[select].Cells[2].Value.ToString();
			txtMail.Text = dataGridView1.Rows[select].Cells[3].Value.ToString();
			comboBox1.Text = dataGridView1.Rows[select].Cells[4].Value.ToString();
		}

	}
}
