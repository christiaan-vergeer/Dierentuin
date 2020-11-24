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

namespace Dierentuin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<animal> animals = new List<animal>();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Animal_View.ItemsSource = animals.ToList();
        }

        void Animal_Add(object sender, RoutedEventArgs e)
        { 
            if(Animal_Kind.Text == "Monkey")
            {
                animal animal = new Monkey
                {
                    name = Animal_Name.Text,
                    energy = 100
                };
                animals.Add(animal);
            }
            else if(Animal_Kind.Text == "Lion")
            {
                animal animal = new Lion
                {
                    name = Animal_Name.Text,
                    energy = 100
                };
                animals.Add(animal);
            }
            else
            {
                animal animal = new Elephant
                {
                    name = Animal_Name.Text,
                    energy = 100
                };
                animals.Add(animal);
            }            
            Animal_View.ItemsSource = animals.ToList();
        }

        void Feed_All(object sender, RoutedEventArgs e)
        {
            foreach a in animals{

            }
        }


        public class animal
        {
            public string name {get; set;}

            public int energy { get; set; }

            public int Eat()
            {
                return 25;
            }
        }

        public class Monkey : animal
        {

        }
        public class Lion : animal
        {

        }
        public class Elephant: animal
        {

        }

    }
}
