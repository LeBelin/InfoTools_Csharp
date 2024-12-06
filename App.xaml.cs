using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InfoTools
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Ouvrir la fenêtre de connexion
            //LoginWindow loginWindow = new LoginWindow();
            //loginWindow.ShowDialog(); // Bloque l'exécution jusqu'à fermeture de LoginWindow

            // Si la connexion est réussie, MainWindow est déjà ouverte par LoginWindow
        }

    }
}
