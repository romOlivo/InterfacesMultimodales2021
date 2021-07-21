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
using System.Windows.Shapes;

namespace MasterMind
{
    /// <summary>
    /// Lógica de interacción para DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        private readonly MainWindow main;
        public DebugWindow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            main.ClosedDebugWindow();
        }

        public void setSolution(string solution)
        {
            LSol.Content = solution;
        }
    }
}
