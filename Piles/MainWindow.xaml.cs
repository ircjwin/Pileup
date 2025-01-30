using Piles.Models;
using Piles.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Piles
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Rumination rumination = new Rumination("This is your first task");
            List<Rumination> ruminations = new List<Rumination>();
            ruminations.Add(rumination);
            Pile pile = new Pile("General", ruminations);
            DataContext = new PileViewModel(pile);
        }
    }
}
