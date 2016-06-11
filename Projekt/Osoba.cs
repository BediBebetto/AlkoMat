using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Projekt
{
    [ImplementPropertyChanged]
    public class Osoba 
    {
        public double waga { get; set; }
        public double wzrost { get; set; }
        public Gender plec { get; set; }

    }
}
