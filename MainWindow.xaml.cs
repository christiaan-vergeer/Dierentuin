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
using System.Windows.Threading;

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
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Tick;
            timer.Start();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            update();
        }

        void Animal_Add(object sender, RoutedEventArgs e)
        { 
            if(Animal_Kind.Text == "Monkey")
            {
                animal animal = new Monkey
                {
                    name = Animal_Name.Text,
                    energy = 60
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
            update();
        }

        void Feed_All(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            foreach (var a in animals)
            {
                if ((btn.Name == "M" || btn.Name == "A") && a.GetType() == typeof(Monkey))
                {
                    a.energy = a.Eat(2.5);
                }
                else if ((btn.Name == "L" || btn.Name == "A") && a.GetType() == typeof(Lion))
                {
                    a.energy = a.Eat(1);
                }
                else if ((btn.Name == "E" || btn.Name == "A") && a.GetType() == typeof(Elephant))
                {
                    a.energy = a.Eat(0.5);
                }
            }
            update();
        }        

        void update()
        {
            Animal_View.ItemsSource = animals.ToList();
        }

        void Tick(object sender, EventArgs e)
        {
            for (int i = animals.Count - 1; i >= 0; i--)
            {
                if (animals[i].GetType() == typeof(Monkey))
                {
                    animals[i].energy = animals[i].UseEnergy(5);
                }
                else if (animals[i].GetType() == typeof(Lion))
                {
                    animals[i].energy = animals[i].UseEnergy(1);
                }
                else
                {
                    animals[i].energy = animals[i].UseEnergy(2);
                }
                if (animals[i].energy < 0)
                {
                    animals.Remove(animals[i]);
                }
            }
            update();
        }

        public class animal
        {
            public string name {get; set;}

            public int energy { get; set; }

            public int Eat(double devider)
            {
                return energy + (int)(25 / devider);
            }

            public int UseEnergy(double devider)
            {
                return energy - (int)(10 / devider);
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
