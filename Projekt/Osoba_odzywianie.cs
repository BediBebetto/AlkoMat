using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Projekt
{
    [ImplementPropertyChanged]
    public class Osoba_odzywianie: Osoba, IOsoba
    {

        public double BMI { get; set; }
        public double tluszcze { get; set; }
        public double weglowodany { get; set; }
        public double bialko { get; set; }
        public double zapotrzebowanie { get; set; }
        public double aktywnosc { get; set; }

        private int wspolczynnik_BMI = 10000;
        private double tluszcze_zapotrzebowanie = 0.2/9;
        private double weglowodany_zapotrzebowanie = 0.5/4;
        private double bialko_zapotrzebowanie = 0.3/4;

        private int doba = 24;
        

        public void BodyMaxIndex()
        {
            this.BMI = wspolczynnik_BMI * this.waga / this.wzrost / this.wzrost;
            this.BMI = Math.Round(this.BMI, 1);
        }

        public void Zapotrzebowanie()
        {
            this.zapotrzebowanie = this.waga * doba * aktywnosc;
            this.zapotrzebowanie = Math.Round(this.zapotrzebowanie, 1);
        }
        public void Skladniki_odzywcze()
        {
            this.tluszcze = tluszcze_zapotrzebowanie * zapotrzebowanie;
            this.tluszcze = Math.Round(this.tluszcze, 1);
            this.weglowodany = weglowodany_zapotrzebowanie * zapotrzebowanie;
            this.weglowodany = Math.Round(this.weglowodany, 1);
            this.bialko = bialko_zapotrzebowanie *zapotrzebowanie;
            this.bialko = Math.Round(this.bialko, 1);
        }
    }
}
