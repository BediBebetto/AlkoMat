using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;
using System.Linq;
using System.Xml.Serialization;


namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Alkohol alkohol;
        public Wynik wynik;
        public Osoba osoba;
        public Osoba_odzywianie osoba_odzywianie;
        public ObservableCollection<Alkohol> Alkolista { get; set; }

        public MainWindow()
        {
            InitializeComponent();


            this.Voltage_Box.SelectedText = "%";
            this.Ilosc_ComboBox.SelectedIndex = 0;
            this.Trunek_ComboBox.SelectedIndex = 0;
            this.Gender_ComboBox.SelectedIndex = 0;

            this.wynik = new Wynik();
            this.osoba = new Osoba();
            this.osoba_odzywianie = new Osoba_odzywianie();
            this.Alkolista = new ObservableCollection<Alkohol>();

            this.DataContext = this;
            this.WynikBox.DataContext = wynik;
            this.GodzinaBox.DataContext = wynik;
            this.BMI_TextBox.DataContext = osoba_odzywianie;
            this.Tluszcze_Box.DataContext = osoba_odzywianie;
            this.Weglowodany_Box.DataContext = osoba_odzywianie;
            this.Bialko_Box.DataContext = osoba_odzywianie;
            this.Zapotrzebowanie_TextBox.DataContext = osoba_odzywianie;

            List<int> Num = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                Num.Add(i);
            }

            this.Trunek_ComboBox.ItemsSource = Enum.GetValues(typeof(Trunek));
            this.Ilosc_ComboBox.ItemsSource = Num;
            this.Gender_ComboBox.ItemsSource = Enum.GetValues(typeof(Gender));



        }

        private void Oblicz_Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            Oblicz_Button.FontSize = 17;
        }

        private void Oblicz_Button_MouseLeave(object sender, RoutedEventArgs e)
        {
            Oblicz_Button.FontSize = 15;
        }

        private void Oblicz_Button_Click(object sender, RoutedEventArgs e)
        {
            double tmpalk = 0.0;
            double alkohol_w_gramach = 0.0;
            double współczynnik_gender = 0.0;
            double tmpnasycenie = this.Stopień_Nasycenia_Slider.Value;

            try
            {
                osoba.wzrost = double.Parse(this.Wzrost_Box.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Nieprawidłowa wartość lub jej brak", "Wzrost");
            }

            try
            {
                osoba.waga = double.Parse(this.Waga_Box.Text);
            }
            catch (Exception x1)
            {
                MessageBox.Show("Nieprawidłowa wartość lub jej brak", "Waga");
            }



            switch (this.Gender_ComboBox.SelectedIndex)
            {
                case 0: współczynnik_gender = 0.7; break;
                case 1: współczynnik_gender = 0.8; break;
            }


            foreach (Alkohol alkohol in Alkolista)
            {

                tmpalk = alkohol.litraz * alkohol.woltaz * alkohol.ilosc;
                alkohol_w_gramach += tmpalk;

            }

            wynik.Oblicz(osoba.wzrost, osoba.waga, tmpnasycenie, alkohol_w_gramach, współczynnik_gender);
            wynik.Oblicz();

        }

        private void DodajAlkohol_Button_Click(object sender, RoutedEventArgs e)
        {
            int tmpilosc = this.Ilosc_ComboBox.SelectedIndex + 1;
            double tmplitraz = 0.0;
            double tmpwoltaz = 0.0;
            bool error = false;

            try
            {
                tmpwoltaz = double.Parse(this.Voltage_Box.Text);
            }
            catch (Exception trunek)
            {
                MessageBox.Show("Nieprawidłowa wartość lub jej brak", "Woltaż");
                error = true;
            }

            if (!error)
            {

                Trunek tmptrunek = (Trunek)Enum.Parse(typeof(Trunek), this.Trunek_ComboBox.Text);


                switch (this.Trunek_ComboBox.SelectedIndex)
                {
                    case 0: tmplitraz = 0.5; break;
                    case 1: tmplitraz = 0.33; break;
                    case 2: tmplitraz = 0.05; break;
                    case 3: tmplitraz = 0.5; break;
                    case 4: tmplitraz = 0.7; break;
                    case 5: tmplitraz = 1; break;
                }

                Alkohol alkohol = new Alkohol(tmpilosc, tmplitraz, tmpwoltaz, tmptrunek);
                Alkolista.Add(alkohol);
            }
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            Alkolista.Clear();
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Alkolista.RemoveAt(this.Lista_Trunkow.SelectedIndex);
            }

            catch (Exception ex)

            {
                MessageBox.Show("Najpierw wybierz trunek z listy", "Uważaj");
            }
        }

        private void Informacje_zdrowotne_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                osoba_odzywianie.wzrost = double.Parse(this.Wzrost_Box.Text);
            }
            catch (Exception eh)
            {
                MessageBox.Show("Nieprawidłowa wartość lub jej brak", "Wzrost");

            }
            try
            {
                osoba_odzywianie.waga = double.Parse(this.Waga_Box.Text);
            }
            catch (Exception e1)
            {
                MessageBox.Show("Nieprawidłowa wartość lub jej brak", "Waga");
            }
            osoba_odzywianie.aktywnosc = this.Tryb_zycia_Slider.Value;

            osoba_odzywianie.BodyMaxIndex();
            osoba_odzywianie.Zapotrzebowanie();
            osoba_odzywianie.Skladniki_odzywcze();


        }

        private void Oblicz_odzywianie_Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Oblicz_odzywianie_Button.FontSize = 13;
        }

        private void Oblicz_odzywianie_Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Oblicz_odzywianie_Button.FontSize = 12;
        }
    }
}
