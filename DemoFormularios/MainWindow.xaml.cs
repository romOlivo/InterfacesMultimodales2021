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

namespace DemoFormularios
{

    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Person> people = new List<Person>();
        private int index = 0;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void update_people()
        {
            CajaNombre.Text = people[index].Name;
            CajaApellidos.Text = people[index].Surname;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            people.Add(new Person("Federico", "Garcia"));
            people.Add(new Person("Macarena", "Fernandez"));
            people.Add(new Person("Garcia", "Lorca"));
            people.Add(new Person("Santi", "Lopez"));
            update_people();
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
            people[index].Name = CajaNombre.Text;
            people[index].Surname = CajaApellidos.Text;

            MessageBox.Show("Usuario Actualizado");
        }

        private void Left_People(object sender, RoutedEventArgs e)
        {
            if (index == 0)
            {
                index = people.Count - 1;
            }
            else
            {
                index--;
            }
            update_people();
        }

        private void Right_People(object sender, RoutedEventArgs e)
        {
            index = (index + 1) % people.Count;
            update_people();
        }
    }
}
