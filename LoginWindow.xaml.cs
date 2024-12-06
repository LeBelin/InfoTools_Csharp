using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace InfoTools
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (ValidateUser(email, password))
            {
                MessageBox.Show("Connexion réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Ouvrir MainWindow
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Fermer LoginWindow
                this.Close();
            }
            else
            {
                MessageBox.Show("Email ou mot de passe incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool ValidateUser(string email, string password)
        {
            try
            {
                bdd.Initialize();
                string query = "SELECT COUNT(*) FROM users WHERE email = @Email AND password = @Password";
                MySqlCommand cmd = new MySqlCommand(query, bdd.Connection);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password); // Assurez-vous d'avoir un hash sécurisé pour le mot de passe

                bdd.OpenConnection();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                bdd.CloseConnection();

                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
