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

namespace DemoFormularios
{
    /// <summary>
    /// Lógica de interacción para NewContact.xaml
    /// </summary>
    public partial class NewContact : Window
    {
        public Person NewPerson { get; private set; }

        public NewContact()
        {
            InitializeComponent();
            this.NewPerson = null;
        }

        private void addB_Click(object sender, RoutedEventArgs e)
        {
            if (CajaNombre.Text == null || CajaNombre.Text == "")
            {
                show_warning("nombre");
                return;
            }
            if (CajaApellidos.Text == null || CajaApellidos.Text == "")
            {
                show_warning("apellido");
                return;
            }
            if (rdMan.IsChecked != true && rdWoman.IsChecked != true)
            {
                show_warning("sexo");
                return;
            }
            this.NewPerson = new Person(CajaNombre.Text, CajaApellidos.Text, CheckFriend.IsChecked == true, SexEnum.Man);
            if (rdWoman.IsChecked == true)
                this.NewPerson.Sex = SexEnum.Woman;
            this.Close();
        }

        private void show_warning(string text)
        {
            string msg = $"No ha introducido un {text} válido. Por favor, compruebe el {text} e intentelo de nuevo.";
            MessageBox.Show(msg, "Aviso",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}
