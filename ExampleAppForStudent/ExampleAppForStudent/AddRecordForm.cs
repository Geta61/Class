using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExampleAppForStudent
{
    public partial class AddRecordForm : Form
    {
        public AddRecordForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "" && txtCount.Text != "" && txtPrice.Text != "")
            {
                try
                {
                    using (var conn = new MySqlConnection(new ConfigDB().GetConnectionString()))
                    {
                        conn.Open();

                        var sql = "INSERT INTO goods(name, count, price) VALUES(@name, @count, @price)";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            var parameterName = new MySqlParameter("@name", txtName.Text);
                            var parameterCount = new MySqlParameter("@count", txtCount.Text);
                            var parameterPrice = new MySqlParameter("@price", txtPrice.Text);
                            cmd.Parameters.Add(parameterName);
                            cmd.Parameters.Add(parameterCount);
                            cmd.Parameters.Add(parameterPrice);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Запись успено добавлена", "Сообщение");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста введите данные в поля для ввода", "Ошибка");
            }
        }
    }
}
