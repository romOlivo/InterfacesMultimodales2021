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
    [Serializable()]
    enum SexEnum
    {
        Man,
        Woman
    }

    [Serializable()]
    class Person
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
                CajaNombre.TextChanged -= TextChanged;
                CajaApellidos.TextChanged -= TextChanged;
                CheckFriend.Click -= Click;
                rdMan.Click -= Click;
                rdWoman.Click -= Click;
                if (value >= 0 && value <= people.Count - 1)
                    index = value;
                LeftB.IsEnabled = index > 0;
                RightB.IsEnabled = index < people.Count - 1;
                update_people();
                CajaNombre.TextChanged += TextChanged;
                CajaApellidos.TextChanged += TextChanged;
                CheckFriend.Click += Click;
                rdMan.Click += Click;
                rdWoman.Click += Click;
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            enableModify();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            enableModify();
        }

        private void enableModify()
        { 
        OkB.IsEnabled = CajaNombre.Text != people[Index].Name || 
                CajaApellidos.Text != people[Index].Surname ||
                CheckFriend.IsChecked != people[Index].IsFriend ||
                (rdMan.IsChecked == true && people[Index].Sex != SexEnum.Man) ||
                (rdWoman.IsChecked == true && people[Index].Sex != SexEnum.Woman);
        }

    public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void update_people()
        {
            CajaNombre.Text = people[Index].Name;
            CajaApellidos.Text = people[Index].Surname;
            CheckFriend.IsChecked = people[Index].IsFriend;
            rdMan.IsChecked = people[Index].Sex == SexEnum.Man;
            rdWoman.IsChecked = people[Index].Sex == SexEnum.Woman;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            people[Index].Name = CajaNombre.Text;
            people[Index].Surname = CajaApellidos.Text;
            people[Index].IsFriend = (bool)CheckFriend.IsChecked;
            if (rdMan.IsChecked == true)
                people[Index].Sex = SexEnum.Man;
            else if(rdWoman.IsChecked == true)
                people[Index].Sex = SexEnum.Woman;
            OkB.IsEnabled = false;
            MessageBox.Show("Usuario Actualizado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Left_People(object sender, RoutedEventArgs e)
        {
            Index--;
        }

        private void Right_People(object sender, RoutedEventArgs e)
        {
            Index++;
        }

        private void ElSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Index = (int)Math.Round(e.NewValue * (people.Count - 1) / 100);
        }

        private void MenuItem_Abrir(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".ag";
            dialog.Filter = "Agenda Documents (.ag)|*.ag";
            if(dialog.ShowDialog() == true)
            {
                string readText = File.ReadAllText(dialog.FileName);
                people = JsonConvert.DeserializeObject<List<Person>>(readText);
                Index = 0;
            }


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
    }
}
