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

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            setCurrentDate_toDatepickers();


        }

        public void setCurrentDate_toDatepickers()
        {

            petrol_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            diesel_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            fuel_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.DisplayDateEnd = DateTime.Today.AddDays(0);
        }

        private void meterClosing_petrol_N1_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageBox.Show(meterClosing_petrolN1.Text);
        }
    }
}
