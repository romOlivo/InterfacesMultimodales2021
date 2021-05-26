using System;
using System.Collections.Generic;
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

namespace FormularioProg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var g = new Grid();
            g.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            g.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            this.Content = g;

            var menu = new Menu();
            MenuItem fileMenu = new MenuItem() { Header = "_File" };
            fileMenu.Items.Add(new MenuItem() { Header = "_New" });
            fileMenu.Items.Add(new MenuItem() { Header = "_Open" });
            fileMenu.Items.Add(new MenuItem() { Header = "_Save" });
            fileMenu.Items.Add(new Separator());
            fileMenu.Items.Add(new MenuItem() { Header = "E_xit" });
            menu.Items.Add(fileMenu);
            Grid.SetRow(menu, 0);
            Grid.SetColumn(menu, 0);
            g.Children.Add(menu);

            Grid subGrid = new Grid();
            for (int i = 0; i < 6; i++)
                subGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            subGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            subGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            subGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            subGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            Grid.SetRow(subGrid, 1); 
            Grid.SetColumn(subGrid, 0);
            g.Children.Add(subGrid);

            Label label = new Label() { Content = "_Nombre:" }; 
            Grid.SetRow(label, 1); 
            Grid.SetColumn(label, 0); 
            subGrid.Children.Add(label); 
            TextBox textbox = new TextBox() { Width = 150 }; 
            Grid.SetRow(textbox, 1); Grid.SetColumn(textbox, 1); 
            subGrid.Children.Add(textbox); 
            label.Target = textbox;

            Button button = new Button() { Content = "Ok" }; 
            // button.Click += button_Click; 
            Grid.SetColumn(button, 0); 
            Grid.SetColumnSpan(button, 2);
            Grid.SetRow(button, 4); 
            subGrid.Children.Add(button);
        }

    }
}
