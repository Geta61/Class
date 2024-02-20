using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExampleAppForStudent
{
    public partial class MainForm : Form
    {
        public class Goods
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Count { get; set; }
            public decimal Price { get; set; }
        }

        public List<Goods> listOfGoods = null;

        public MainForm()
        {
            InitializeComponent();

            listOfGoods = new List<Goods>();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            this.GetData();
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            var addRecordForm = new AddRecordForm();
            addRecordForm.ShowDialog();
        }

        private void btnDelData_Click(object sender, EventArgs e)
        {
            var index = Convert.ToInt32(dataGridViewDB.CurrentRow.Cells[0].Value);

            try
            {
                using (var conn = new MySqlConnection(new ConfigDB().GetConnectionString()))
                {
                    conn.Open();

                    var sql = "DELETE FROM goods WHERE id = @id";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        var parameter = new MySqlParameter("@id", index);
                        cmd.Parameters.Add(parameter);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Запись успено удалена", "Сообщение");

                        this.GetData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        protected void GetData()
        {
            listOfGoods.Clear();

            try
            {
                using (var conn = new MySqlConnection(new ConfigDB().GetConnectionString()))
                {
                    conn.Open();

                    var sql = "SELECT * FROM goods";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            listOfGoods.Add(new Goods() { 
                                Id = Convert.ToInt32(reader[0].ToString()), 
                                Name = reader[1].ToString(), 
                                Count = Convert.ToInt32(reader[2].ToString()),
                                Price = Convert.ToDecimal(reader[3].ToString()),
                            });
                        }

                        dataGridViewDB.DataSource = null;

                        dataGridViewDB.Rows.Clear();
                        dataGridViewDB.Refresh();

                        dataGridViewDB.DataSource = listOfGoods;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}
