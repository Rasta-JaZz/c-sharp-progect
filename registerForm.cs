using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleProgect
{
    public partial class registerForm : Form
    {
        public registerForm()
        {
            InitializeComponent();
            userName.Text="Введите имя";
            userSurname.Text = "Введите фамилию";
            password.Text = "Введите пароль";
            password.ForeColor = Color.Gray;
            password.UseSystemPasswordChar = false;
            userLogin.Text = "Введите логин";
            userLogin.ForeColor = Color.Gray;
            userSurname.ForeColor = Color.Gray;
            userName.ForeColor = Color.Gray;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPiont;

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastPiont = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPiont.X;
                this.Top += e.Y - lastPiont.Y;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void userName_Enter(object sender, EventArgs e)
        {
            if(userName.Text == "Введите имя")
            {
                userName.Text = "";
                userName.ForeColor = Color.Black;
            }
           
        }

        private void userName_Leave(object sender, EventArgs e)
        {
            if(userName.Text == "")
            {
                userName.Text = "Введите имя";
                userName.ForeColor = Color.Gray;
            }
        }

        private void userSurname_Enter(object sender, EventArgs e)
        {
            if (userSurname.Text == "Введите фамилию")
            {
                userSurname.Text = "";
                userSurname.ForeColor = Color.Black;
            }

        }

        private void userSurname_Leave(object sender, EventArgs e)
        {
            if (userSurname.Text == "")
            {
                userSurname.Text = "Введите фамилию";
                userSurname.ForeColor = Color.Gray;
            }
        }

        private void password_Enter(object sender, EventArgs e)
        {
            if (password.Text == "Введите пароль")
            {
                password.UseSystemPasswordChar = true;
                password.Text = "";
                password.ForeColor = Color.Black;
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (password.Text == "")
            {
                password.UseSystemPasswordChar = false;
                password.Text = "Введите пароль";
                password.ForeColor = Color.Gray;

            }
        }

        private void userLogin_Leave(object sender, EventArgs e)
        {
            if (userLogin.Text == "")
            {
                userLogin.Text = "Введите логин";
                userLogin.ForeColor = Color.Gray;

            }
        }

        private void userLogin_Enter(object sender, EventArgs e)
        {
            if (userLogin.Text == "Введите логин")
            {
                userLogin.Text = "";
                userLogin.ForeColor = Color.Black;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if(userLogin.Text == "Введите логин")
            {
                MessageBox.Show("Введите ваш логин!");
                return;
            }else if (password.Text == "Введите пароль")
            {
                MessageBox.Show("Введите пароль!");
                return;

            }

            if (isUserExist())
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @password,@name,@surname)", db.getConnection());

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userName.Text;
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = userLogin.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurname.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Вы зарегистрированы!");

            }
            else
            {
                MessageBox.Show("Ошибка регистрации");
            }
            db.closeConnection();
        }   
        
        public Boolean isUserExist()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users`WHERE `login`=@uL" , db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = userLogin.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void auth_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }
    }

}
