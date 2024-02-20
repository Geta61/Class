using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExampleAppForStudent
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    using(var conn = new MySqlConnection(new ConfigDB().GetConnectionString()))
                    {
                        conn.Open();

                        var sql = "SELECT password FROM users WHERE username = @username";

                        using(var cmd = new MySqlCommand(sql, conn))
                        {
                            var sha1 = SHA1.Create();
                            var pass = Convert.ToBase64String((sha1.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Text))));

                            var parameter = new MySqlParameter("@username", txtUsername.Text);
                            cmd.Parameters.Add(parameter);

                            var passFromDb = cmd.ExecuteScalar();

                            if(pass == passFromDb.ToString())
                            {
                                this.Hide();
                                var mainForm = new MainForm();
                                mainForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Пароли не совпадают", "Ошибка");
                            }
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
