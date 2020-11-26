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
    public partial class MainWindow : Window
    {
        List<animal> animals = new List<animal>();
        int dead = 0;

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
            if (!string.IsNullOrEmpty(Animal_Name.Text))
            {
                if (Animal_Kind.Text == "Aap")
                {
                    animal animal = new Monkey
                    {
                        name = Animal_Name.Text,
                        energy = 60
                    };
                    animals.Add(animal);
                }
                else if (Animal_Kind.Text == "Leeuw")
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
            }
            update();
        }

        void Feed(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            foreach (var animal in animals)
            {
                if ((btn.Name == "M" || btn.Name == "A") && animal is Monkey)
                {
                    animal.energy = animal.Eat();
                }
                else if ((btn.Name == "L" || btn.Name == "A") && animal.GetType() == typeof(Lion))
                {
                    animal.energy = animal.Eat();
                }
                else if ((btn.Name == "E" || btn.Name == "A") && animal.GetType() == typeof(Elephant))
                {
                    animal.energy = animal.Eat();
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
                animals[i].energy = animals[i].UseEnergy();
                if (animals[i].energy < 0)
                {
                    animals.Remove(animals[i]);
                    dead++;
                    Dead_Counter.Content = "Dieren die zijn gestorven = " + dead;
                }
            }
            update();
        }

        abstract class animal
        {
            public string name {get; set;}

            public int energy { get; set; }

            public virtual int Eat()
            {
                return energy + 25;
            }

            public abstract int UseEnergy();
        }

        sealed class Monkey : animal 
        {
            public override int Eat()
            {
                return energy + 10;
            }

            public override int UseEnergy()
            {
                return energy - 2;
            }
        }

        sealed class Lion : animal
        {
            public override int UseEnergy()
            {
                return energy - 10;
            }
        }

        sealed class Elephant: animal 
        {
            public override int Eat()
            {
                return energy + 50;
            }
            public override int UseEnergy()
            {
                return energy - 5;
            }
        }
    }
}
