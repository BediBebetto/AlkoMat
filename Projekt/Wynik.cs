using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [ImplementPropertyChanged]
    public class Wynik
    {
        public double promile { get; set; }
        public double godzina { get; set; }

        private int wspolczynnik_wzrostu = 1000;
        private int wspolczynnik_nasycenia = 70;


        public void Oblicz(double wzrost, double waga, double nasycenie, double alkohol, double gender)
        {
            this.promile = (wspolczynnik_wzrostu - wzrost) * (wspolczynnik_nasycenia - nasycenie) * 10 * alkohol / waga / wspolczynnik_nasycenia / gender / wspolczynnik_wzrostu;
            this.promile = Math.Round(this.promile, 2);
        }
        public void Oblicz()
        {
            this.godzina = 10 * this.promile / 1.5;
            this.godzina = Math.Round(this.godzina, 1);

        }

    }
}
