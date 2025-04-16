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

                string query = "SELECT password FROM users WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, bdd.Connection);
                cmd.Parameters.AddWithValue("@Email", email);

                bdd.OpenConnection();
                object result = cmd.ExecuteScalar();
                bdd.CloseConnection();

                if (result != null)
                {
                    string storedHash = result.ToString();
                    // Vérification du mot de passe avec BCrypt
                    return BCrypt.Net.BCrypt.Verify(password, storedHash);
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

    }
}
