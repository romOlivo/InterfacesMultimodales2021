using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoGestureRecog
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<string> Etiquetas { get; set; }
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Etiquetas = new ObservableCollection<string>();
            etiquetasLB.ItemsSource = Etiquetas;
            NombreTB.TextChanged += NombreTB_TextChanged;
            NombreTB.Focus();
        }

        private void NombreTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddB.IsEnabled = !(NombreTB.Text == null || NombreTB.Text == "");
        }

        private void RadB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NombreTB.Text == null || NombreTB.Text == "")
                return;
            Etiquetas.Add(NombreTB.Text);
            NombreTB.Text = "";
            NombreTB.Focus();
        }
    }
}
