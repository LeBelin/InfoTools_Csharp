using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace InfoTools
{
    public partial class GraphiqueWindow : Window
    {
        // Constructeur qui prend une liste de factures en paramètre
        public GraphiqueWindow(List<Commande> factures)
        {
            InitializeComponent();

            // Créer la collection de séries du graphique
            SeriesCollection seriesCollection = new SeriesCollection();

            // Créer la série de type colonne (ColumnSeries)
            var columnSeries = new ColumnSeries
            {
                Title = "Montant des Factures",
                Values = new ChartValues<int>()
            };

            // Ajouter les montants des factures à la série
            foreach (var facture in factures)
            {
                columnSeries.Values.Add(facture.MontantTotal); // Montant total des factures
            }

            // Ajouter la série à la collection de séries
            seriesCollection.Add(columnSeries);

            // Mettre à jour les axes du graphique
            chart.Series = seriesCollection;

            // Configuration de l'axe X (pour les Factures)
            chart.AxisX = new AxesCollection
            {
                new Axis
                {
                    Title = "Factures",
                    Labels = factures.Select(f => $"Facture N° {f.IdCommande}").ToArray() // IDs des factures
                }
            };

            // Configuration de l'axe Y (pour les Montants)
            chart.AxisY = new AxesCollection
            {
                new Axis
                {
                    Title = "Montant Total",
                    LabelFormatter = value => value.ToString("C") // Format monétaire
                }
            };
        }
    }
}
