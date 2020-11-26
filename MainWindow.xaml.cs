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
        List<Animal> animals = new List<Animal>();
        int dead = 0;

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            timer.Tick += Tick;
            timer.Start();

            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        void Animal_Add(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Animal_Name.Text))
            {
                if (Animal_Kind.Text == "Aap")
                {
                    Monkey animal = new Monkey
                    {
                        Name = Animal_Name.Text,
                        Energy = 60
                    };
                    animals.Add(animal);
                }
                else if (Animal_Kind.Text == "Leeuw")
                {
                    Animal animal = new Lion
                    {
                        Name = Animal_Name.Text,
                        Energy = 100
                    };
                    animals.Add(animal);
                }
                else
                {
                    Animal animal = new Elephant
                    {
                        Name = Animal_Name.Text,
                        Energy = 100
                    };
                    animals.Add(animal);
                }
            }
            Update();
        }

        void Feed(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            foreach (var animal in animals)
            {
                if ((btn.Name == "M" || btn.Name == "A") && animal is Monkey)
                {
                    animal.Energy = animal.Eat();
                }
                else if ((btn.Name == "L" || btn.Name == "A") && animal is Lion)
                {
                    animal.Energy = animal.Eat();
                }
                else if ((btn.Name == "E" || btn.Name == "A") && animal is Elephant)
                {
                    animal.Energy = animal.Eat();
                }
            }
            Update();
        }        

        void Update()
        {
            List<Monkey> Monkeys = animals.OfType<Monkey>().ToList();
            List<Lion> lions = animals.OfType<Lion>().ToList();
            List<Elephant> elephants = animals.OfType<Elephant>().ToList();
            List<Animal> view = new List<Animal>();
            if(Check_Monkey.IsChecked.Value)
            {
                view.AddRange(Monkeys);
            }
            if(Check_Lion.IsChecked.Value)
            {
                view.AddRange(lions);
            }
            if(Check_Elephant.IsChecked.Value)
            {
                view.AddRange(elephants);
            }
            Animal_View.ItemsSource = view.ToList();
        }

        void Tick(object sender, EventArgs e)
        {
            for (int i = animals.Count - 1; i >= 0; i--)
            {
                animals[i].Energy = animals[i].UseEnergy();
                if (animals[i].Energy < 0)
                {
                    animals.Remove(animals[i]);
                    dead++;
                    Dead_Counter.Content = "Dieren die zijn gestorven = " + dead;
                }
            }
            Update();
        }

        abstract class Animal
        {
            public string Name {get; set;}

            public int Energy { get; set; }

            public virtual int Eat()
            {
                return Energy + 25;
            }

            public abstract int UseEnergy();
        }

        sealed class Monkey : Animal 
        {
            public override int Eat()
            {
                return Energy + 10;
            }

            public override int UseEnergy()
            {
                return Energy - 2;
            }
        }

        sealed class Lion : Animal
        {
            public override int UseEnergy()
            {
                return Energy - 10;
            }
        }

        sealed class Elephant: Animal 
        {
            public override int Eat()
            {
                return Energy + 50;
            }
            public override int UseEnergy()
            {
                return Energy - 5;
            }
        }
    }
}