using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DemoFormularios

{
    #region Clase Persona

    [Serializable()]
    public enum SexEnum
    {
        Man,
        Woman
    }

    [Serializable()]
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsFriend { get; set; }

        public SexEnum Sex { get; set; }

        public Person(string name, string surname, bool isFriend, SexEnum sex)
        {
            Name = name;
            Surname = surname;
            IsFriend = isFriend;
            Sex = sex;
        }
    }

    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Person> people = new List<Person>();
        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                unlistenEvents();
                if (value >= 0 && value <= people.Count - 1)
                    index = value;
                LeftB.IsEnabled = index > 0;
                RightB.IsEnabled = index < people.Count - 1;
                update_people();
                listenEvents();
            }
        }   

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            people.Add(new Person("Federico", "Garcia", true, SexEnum.Man));
            people.Add(new Person("Macarena", "Fernandez", true, SexEnum.Woman));
            people.Add(new Person("Garcia", "Lorca", false, SexEnum.Man));
            people.Add(new Person("Santi", "Lopez", false, SexEnum.Man));
            Index = 0;
            OkB.IsEnabled = false;
        }

        #region ScaleValue Depdency Property
        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                return mainWindow.OnCoerceScaleValue((double)value);
            else return value;
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                mainWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
                return 1.0f;

            value = Math.Max(0.1, value);
            return value;
        }

        protected virtual void OnScaleValueChanged(double oldValue, double newValue) { }

        public double ScaleValue
        {
            get => (double)GetValue(ScaleValueProperty);
            set => SetValue(ScaleValueProperty, value);
        }


        private void MainGrid_SizeChanged(object sender, EventArgs e) => CalculateScale();

        private void CalculateScale()
        {
            double yScale = ActualHeight / 250f;
            double xScale = ActualWidth / 200f;
            double value = Math.Min(xScale, yScale);

            ScaleValue = (double)OnCoerceScaleValue(myMainWindow, value);
        }

        #endregion

        #region Métodos Auxiliares

        private void listenEvents()
        {
            CajaNombre.TextChanged += TextChanged;
            CajaApellidos.TextChanged += TextChanged;
            CheckFriend.Click += Click;
            rdMan.Click += Click;
            rdWoman.Click += Click;
        }

        private void unlistenEvents()
        {
            CajaNombre.TextChanged -= TextChanged;
            CajaApellidos.TextChanged -= TextChanged;
            CheckFriend.Click -= Click;
            rdMan.Click -= Click;
            rdWoman.Click -= Click;
        }

        private void enableModify()
        {
            OkB.IsEnabled = CajaNombre.Text != people[Index].Name ||
                    CajaApellidos.Text != people[Index].Surname ||
                    CheckFriend.IsChecked != people[Index].IsFriend ||
                    (rdMan.IsChecked == true && people[Index].Sex != SexEnum.Man) ||
                    (rdWoman.IsChecked == true && people[Index].Sex != SexEnum.Woman);
        }

        private void update_people()
        {
            if (people.Count == 0)
            {
                setFields("", "", false, false, false);
                setVisibility(false);
            }
            else
            {
                setFields(people[Index].Name, people[Index].Surname, people[Index].IsFriend,
                    people[Index].Sex == SexEnum.Man, people[Index].Sex == SexEnum.Woman);
                if (people.Count == 1)
                    setVisibility(true);
            }


        }

        private void setFields(string name, string surname, bool isFriend, bool isMan, bool isWoman)
        {
            CajaNombre.Text = name;
            CajaApellidos.Text = surname;
            CheckFriend.IsChecked = isFriend;
            rdMan.IsChecked = isMan;
            rdWoman.IsChecked = isWoman;
        }

        private void setVisibility(bool visibility)
        {
            delB.IsEnabled = visibility;
            CajaNombre.IsEnabled = visibility;
            CajaApellidos.IsEnabled = visibility;
            CheckFriend.IsEnabled = visibility;
            rdMan.IsEnabled = visibility;
            rdWoman.IsEnabled = visibility;
            ElSlider.IsEnabled = visibility;
        }

        #endregion

        #region Eventos

        private void Click(object sender, RoutedEventArgs e)
        {
            enableModify();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            enableModify();
        }

        private void ElSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Index = (int)Math.Round(e.NewValue * (people.Count - 1) / 100);
        }

        #endregion

        #region Buttons Click

        private void modB_Click(object sender, RoutedEventArgs e)
        {
            people[Index].Name = CajaNombre.Text;
            people[Index].Surname = CajaApellidos.Text;
            people[Index].IsFriend = (bool)CheckFriend.IsChecked;
            if (rdMan.IsChecked == true)
                people[Index].Sex = SexEnum.Man;
            else if (rdWoman.IsChecked == true)
                people[Index].Sex = SexEnum.Woman;
            OkB.IsEnabled = false;
            MessageBox.Show("Usuario Actualizado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Left_People(object sender, RoutedEventArgs e)
        {
            ElSlider.Value = (Index - 1) / (double)(people.Count - 1) * 100;
        }

        private void Right_People(object sender, RoutedEventArgs e)
        {
            ElSlider.Value = (Index + 1) / (double)(people.Count - 1) * 100;
        }

        private void delB_Click(object sender, RoutedEventArgs e)
        {
            people.RemoveAt(Index);
            Index = Math.Min(Index, people.Count - 1);
            if (Index > 0)
                ElSlider.Value = (Index) / (double)(people.Count - 1) * 100;
            update_people();
        }

        private void addB_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewContact();
            window.ShowDialog();
            if (window.NewPerson == null)
                return;
            people.Add(window.NewPerson);
            Index = people.Count - 1;
            MessageBox.Show("Nuevo usuario añadido exitosamente.", "Información",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region MenuItems Click

        private void MenuItem_Nuevo(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("¿Está seguro de que desea crear una nueva agenda? Todos los cambios no guardados de la vieja agenda se perderán",
                "Nuevo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (response == MessageBoxResult.Yes)
            {
                Index = 0;
                unlistenEvents();
                this.people.Clear();
                update_people();
                OkB.IsEnabled = false;
                LeftB.IsEnabled = false;
                RightB.IsEnabled = false;
                listenEvents();
            }

        }

        private void MenuItem_Salir(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("¿Está seguro de que desea salir? Todos los cambios no guardados se perderán",
                "Salir", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.Yes)
                this.Close();
        }

        private void MenuItem_Guardar(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.DefaultExt = ".ag";
            dialog.Filter = "Agenda Documents (.ag)|*.ag";

            if (dialog.ShowDialog() == true)
            {
                string jsonString = JsonConvert.SerializeObject(people);
                File.WriteAllText(dialog.FileName, jsonString);
            }
        }

        private void MenuItem_Abrir(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".ag";
            dialog.Filter = "Agenda Documents (.ag)|*.ag";
            if (dialog.ShowDialog() == true)
            {
                string readText = null;
                try
                {
                    readText = File.ReadAllText(dialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El fichero no se ha podido leer. Por favor, compruebe el fichero e intentelo de nuevo.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                try
                {
                    people = JsonConvert.DeserializeObject<List<Person>>(readText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El formato del fichero es incorrecto. Por favor, compruebe el fichero e intentelo de nuevo.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                ElSlider.Value = 0;
                update_people();
            }


        }

        #endregion

    }
}
