using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Boolean validPetrolData = true;

        Boolean validDieselData = true;
        public MainWindow()
        {
            InitializeComponent();
            //new SplashWindow().ShowDialog();
            set_initial_values_diesel("12/7/2020", "40", "23");
            set_initial_values_petrol("12/7/2020", "40", "23");
            set_initial_values_petrol("12/7/2020", "40", "23"); 
            expensesEvents();
            demandDraftEvents();

            //Entry obj = new Entry(getTotalLiters_diesel());

        }

        private void set_initial_values_petrol(String date, String N1_Opening, String N2_Opening)
        {
            //petrol_Last_EntryDate_TB.Text = "Last Entry " + date;
            meterOpening_petrol_N1_TB.Text = N1_Opening;
            meterOpening_petrolN2_TB.Text = N2_Opening;

            petrol_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            diesel_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            fuel_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.DisplayDateEnd = DateTime.Today.AddDays(0);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;

            //Regex regex = new Regex("^\\d+(\\.\\d+)?$");
            //e.Handled = regex.IsMatch(e.Text);
        }

        private void meterClosing_petrol_N1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Background = Brushes.White;
            textbox.ToolTip = "Enter nuzzel 1 reading";
            double total = getTotalLiters_petrol();

            if (textbox.Text.Length > 0)
            {
                if (double.Parse(meterClosing_petrol_N1_TB.Text) > double.Parse(meterOpening_petrol_N1_TB.Text))
                {

                    if (getReadingN1_petrol() > -1)
                    {

                        if (getTesting_petrol() >= 0)
                        {
                            if (total >= getTesting_petrol())
                            {
                                testing_petrol_TB.Background = Brushes.White;
                                testing_petrol_TB.ToolTip = "Enter testing Liters";
                                total = total - getTesting_petrol();
                                total_sales_ltrs_petrol_TB.Text = total.ToString();

                            }
                            else
                            {
                                total_sales_ltrs_petrol_TB.Text = "0";
                                testing_petrol_TB.Background = Brushes.Red;
                                validPetrolData = false;
                                testing_petrol_TB.ToolTip = "Testing must be less than total sales" + getTotalLiters_petrol();
                            }
                        }
                        upDateTotalPrice_petrol();
                    }
                    total_sales_ltrs_petrol_TB.Text = total.ToString();
                }
                else
                {
                    if (getTesting_petrol() >= 0)
                    {
                        double testltrs = getTesting_petrol();

                        if (testltrs < getReadingN2_petrol())
                        {
                            total_sales_ltrs_petrol_TB.Text = (getReadingN2_petrol() - testltrs).ToString();
                            testing_petrol_TB.Background = Brushes.White;
                            testing_petrol_TB.ToolTip = "Enter testing Liters";
                            total_sales_ltrs_petrol_TB.Text = getReadingN2_petrol().ToString();

                        }
                        else
                        {
                            total_sales_ltrs_petrol_TB.Text = "0";
                            testing_petrol_TB.Background = Brushes.Red;
                            validPetrolData = false;
                            testing_petrol_TB.ToolTip = "Testing must be less than total sales" + getTotalLiters_petrol();

                        }
                    }
                    else
                    {
                        total_sales_ltrs_petrol_TB.Text = total.ToString();
                    }
                }
            }

        }

        private void meterClosing_petrolN2_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Background = Brushes.White;
            textbox.ToolTip = "Enter nuzzel 2 reading";
            double total = getTotalLiters_petrol();
            if (textbox.Text.Length > 0)
            {
                if (double.Parse(meterClosing_petrolN2_TB.Text) > double.Parse(meterOpening_petrolN2_TB.Text))
                {

                    if (getReadingN2_petrol() > -1)
                    {

                        if (getTesting_petrol() >= 0)
                        {
                            if (total >= getTesting_petrol())
                            {
                                testing_petrol_TB.Background = Brushes.White;
                                testing_petrol_TB.ToolTip = "Enter testing Liters";
                                total = total - getTesting_petrol();
                                total_sales_ltrs_petrol_TB.Text = total.ToString();

                            }
                            else
                            {
                                total_sales_ltrs_petrol_TB.Text = "0";
                                testing_petrol_TB.Background = Brushes.Red;
                                validPetrolData = false;
                                testing_petrol_TB.ToolTip = "Testing must be less than total sales" + getTotalLiters_petrol();

                            }

                        }

                        upDateTotalPrice_petrol();
                    }
                    total_sales_ltrs_petrol_TB.Text = total.ToString();
                }
                else
                {
                    if (getTesting_petrol() >= 0)
                    {
                        double testltrs = getTesting_petrol();

                        if (testltrs < getReadingN1_petrol())
                        {
                            total_sales_ltrs_petrol_TB.Text = (getReadingN1_petrol() - testltrs).ToString();
                            testing_petrol_TB.Background = Brushes.White;
                            testing_petrol_TB.ToolTip = "Enter testing Liters";
                            total_sales_ltrs_petrol_TB.Text = getReadingN1_petrol().ToString();

                        }
                        else
                        {
                            total_sales_ltrs_petrol_TB.Text = "0";
                            testing_petrol_TB.Background = Brushes.Red;
                            validPetrolData = false;
                            testing_petrol_TB.ToolTip = "Testing must be less than total sales" + getTotalLiters_petrol();

                        }
                    }
                    else
                    {
                        total_sales_ltrs_petrol_TB.Text = total.ToString();
                    }

                }
            }
        }

        private void testing_petrol_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Background = Brushes.White;

            if (textbox.Text.Length > 0)
            {
                if (getTotalLiters_petrol() > -1)
                {
                    double testing = double.Parse(textbox.Text);
                    double total = getTotalLiters_petrol();
                    double totalupdate = total - testing;
                    if (totalupdate > -1)
                    {
                        total_sales_ltrs_petrol_TB.Text = totalupdate.ToString();
                        (sender as TextBox).Background = Brushes.White;

                    }
                    else
                    {
                        total_sales_ltrs_petrol_TB.Text = "0";
                    }
                }
            }
            else
            {
                double total = getTotalLiters_petrol();
                total_sales_ltrs_petrol_TB.Text = total.ToString();
                textbox.Text = "";
            }
        }

        private void total_sales_ltrs_petrol_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (getTotalLiters_petrol() > -1 && rate_petrol_TB.Text.Length > 0)
            {
                double total = getTotalLiters_petrol();
                double rate = double.Parse(rate_petrol_TB.Text);
                total_sales_pkrs_petrol_TB.Text = (total * rate).ToString();

            }
        }

        private void meterClosing_petrol_N1_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (getReadingN1_petrol() < 0)
            {
                TextBox textbox = sender as TextBox;
                textbox.Background = Brushes.Red;
                validPetrolData = false;
                textbox.ToolTip = "Meter closing can not be less then Meter opening";

            }
        }

        private void meterClosing_petrolN2_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (getReadingN2_petrol() < 0)
            {
                TextBox textbox = sender as TextBox;
                textbox.Background = Brushes.Red;
                validPetrolData = false;
                textbox.ToolTip = "Meter closing can not be less then Meter opening";

            }
        }

        private void testing_petrol_TB_LostFocus(object sender, RoutedEventArgs e)
        {

            TextBox textbox = sender as TextBox;
            if (textbox.Text.Length > 0)
            {
                if (getTotalLiters_petrol() < double.Parse(textbox.Text))
                {
                    textbox.Background = Brushes.Red;
                    validPetrolData = false;
                    textbox.ToolTip = "Testing must be less than total sales";

                }
                else
                {
                    textbox.Background = Brushes.White;
                    textbox.ToolTip = "Enter Testing Liters";

                }
            }

        }

        private void rate_petrol_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
              upDateTotalPrice_petrol();
        }

        private void upDateTotalPrice_petrol()
        {
            if (total_sales_ltrs_petrol_TB.Text.Length > 0 && rate_petrol_TB.Text.Length > 0)
            {
                double totalLiters = double.Parse(total_sales_ltrs_petrol_TB.Text);
                double rate = double.Parse(rate_petrol_TB.Text);
                double totalPkr = totalLiters * rate;
                total_sales_pkrs_petrol_TB.Text = totalPkr.ToString();
            }if(rate_petrol_TB.Text.Length < 1)
            {
                total_sales_pkrs_petrol_TB.Text = "";
            }
        }

        private double getReadingN1_petrol()
        {
            if (meterClosing_petrol_N1_TB.Text.Length > 0)
            {
                double opening = double.Parse(meterOpening_petrol_N1_TB.Text);
                double closing = double.Parse(meterClosing_petrol_N1_TB.Text);
                double nuzzel1Reading = closing - opening;
                return nuzzel1Reading;
            }
            return -1;

        }

        private double getReadingN2_petrol()
        {

            if (meterClosing_petrolN2_TB.Text.Length > 0)
            {
                double opening = double.Parse(meterOpening_petrolN2_TB.Text);
                double closing = double.Parse(meterClosing_petrolN2_TB.Text);
                double nuzzel2Reading = closing - opening;
                return nuzzel2Reading;

            }
            return -1;

        }

        private double getTotalLiters_petrol()
        {
            double total = 0;
            if (getReadingN1_petrol() > -1)
            {
                total = getReadingN1_petrol();
            }
            if (getReadingN2_petrol() > -1)
            {
                total += getReadingN2_petrol();
            }
            return total;

        }

        private double getTotalPKRs_petrol()
        {
            if (total_sales_pkrs_petrol_TB.Text.Length > 0)
            {
                return double.Parse(total_sales_pkrs_petrol_TB.Text);
            }
            return 0;
        }

        private double getTotalNetProfit_petrol()
        {
            if (today_profit_petrol_TB.Text.Length > 0)
            {
                return double.Parse(today_profit_petrol_TB.Text);
            }
            return 0;
        }

        private String getSelectedDate_petrol()
        {

            String dateTime = petrol_entry_datepicker.SelectedDate.Value.Date.ToShortDateString();
            return dateTime;
        }

        private double getTesting_petrol()
        {
            if (testing_petrol_TB.Text.Length > 0)
            {
                return double.Parse(testing_petrol_TB.Text);
            }
            return -1;
        }

        private void save_petrol_entry_BTN_Click(object sender, RoutedEventArgs e)
        {

            if(validPetrolData && getTotalPKRs_petrol() > 0)
            {
                MessageBox.Show("saved");
            }
            if (getReadingN1_petrol() > -1 && getReadingN2_petrol() > -1 && rate_petrol_TB.Text.Length > 0 && getTotalLiters_petrol() >= getTesting_petrol())
            {
                MessageBox.Show("saved");
            }
        }

        private void discount_amount_petrol_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text.Length > 0)
            {
                if (textbox.Text.Length > 0)
                {
                    double discount = double.Parse(textbox.Text);
                    double total = 0;
                    if (getTotalPKRs_petrol() > 0)
                        total = getTotalPKRs_petrol() - discount;

                    if (total > 0)
                    {
                        total_sales_pkrs_petrol_TB.Text = total.ToString();
                        textbox.Background = Brushes.White;
                    }
                    else
                    {
                        textbox.Background = Brushes.Red;
                        validPetrolData = false;
                        textbox.ToolTip = "discount can not be more then total pkr";
                    }

                }
            }
        }

        private double getDiscountAmount_petrol()
        {
            if (discount_amount_petrol_TB.Text.Length > 0)
            {
                return double.Parse(discount_amount_petrol_TB.Text);
            }
            return 0;
        }

        private void total_sales_pkrs_petrol_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text.Length > 0)
            {
                if (getDiscountAmount_petrol() < getTotalPKRs_petrol())
                {
                    discount_amount_petrol_TB.Background = Brushes.White;
                }
                else
                {
                    discount_amount_petrol_TB.Background = Brushes.Red;
                    validPetrolData = false;
                    
                }
            }
        }

    
        private void set_initial_values_diesel(String date, String N1_Opening, String N2_Opening)
        {
            //diesel_Last_EntryDate_TB.Text = "Last Entry " + date;
            meterOpening_diesel_N1_TB.Text = N1_Opening;
            meterOpening_dieselN2_TB.Text = N2_Opening;

            diesel_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            diesel_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            fuel_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.DisplayDateEnd = DateTime.Today.AddDays(0);
        }

        private void meterClosing_diesel_N1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Background = Brushes.White;
            textbox.ToolTip = "Enter nuzzel 1 reading";
            double total = getTotalLiters_diesel();

            if (textbox.Text.Length > 0)
            {
                if (double.Parse(meterClosing_diesel_N1_TB.Text) > double.Parse(meterOpening_diesel_N1_TB.Text))
                {

                    if (getReadingN1_diesel() > -1)
                    {

                        if (getTesting_diesel() >= 0)
                        {
                            if (total >= getTesting_diesel())
                            {
                                testing_diesel_TB.Background = Brushes.White;
                                testing_diesel_TB.ToolTip = "Enter testing Liters";
                                total = total - getTesting_diesel();
                                total_sales_ltrs_diesel_TB.Text = total.ToString();

                            }
                            else
                            {
                                total_sales_ltrs_diesel_TB.Text = "0";
                                testing_diesel_TB.Background = Brushes.Red;
                                validDieselData = false;
                                testing_diesel_TB.ToolTip = "Testing must be less than total sales" + getTotalLiters_diesel();

                            }
                        }
                        upDateTotalPrice_diesel();
                    }
                    total_sales_ltrs_diesel_TB.Text = total.ToString();
                }
                else
                {
                    if (getTesting_diesel() >= 0)
                    {
                        double testltrs = getTesting_diesel();

                        if (testltrs < getReadingN2_diesel())
                        {
                            total_sales_ltrs_diesel_TB.Text = (getReadingN2_diesel() - testltrs).ToString();
                            testing_diesel_TB.Background = Brushes.White;
                            testing_diesel_TB.ToolTip = "Enter testing Liters";
                            total_sales_ltrs_diesel_TB.Text = getReadingN2_diesel().ToString();

                        }
                        else
                        {
                            total_sales_ltrs_diesel_TB.Text = "0";
                            testing_diesel_TB.Background = Brushes.Red;
                            validDieselData = false;
                            testing_diesel_TB.ToolTip = "Testing must be less than total sales" + getTotalLiters_diesel();

                        }
                    }
                    else
                    {
                        total_sales_ltrs_diesel_TB.Text = total.ToString();
                    }
                }
            }

        }

        private void meterClosing_dieselN2_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Background = Brushes.White;
            textbox.ToolTip = "Enter nuzzel 2 reading";
            double total = getTotalLiters_diesel();
            if (textbox.Text.Length > 0)
            {
                if (double.Parse(meterClosing_dieselN2_TB.Text) > double.Parse(meterOpening_dieselN2_TB.Text))
                {

                    if (getReadingN2_diesel() > -1)
                    {

                        if (getTesting_diesel() >= 0)
                        {
                            if (total >= getTesting_diesel())
                            {
                                testing_diesel_TB.Background = Brushes.White;
                                testing_diesel_TB.ToolTip = "Enter testing Liters";
                                total = total - getTesting_diesel();
                                total_sales_ltrs_diesel_TB.Text = total.ToString();

                            }
                            else
                            {
                                total_sales_ltrs_diesel_TB.Text = "0";
                                testing_diesel_TB.Background = Brushes.Red;
                                validDieselData = false;
                                testing_diesel_TB.ToolTip = "Testing must be less than total sales" + getTotalLiters_diesel();

                            }

                        }

                        upDateTotalPrice_diesel();
                    }
                    total_sales_ltrs_diesel_TB.Text = total.ToString();
                }
                else
                {
                    if (getTesting_diesel() >= 0)
                    {
                        double testltrs = getTesting_diesel();

                        if (testltrs < getReadingN1_diesel())
                        {
                            total_sales_ltrs_diesel_TB.Text = (getReadingN1_diesel() - testltrs).ToString();
                            testing_diesel_TB.Background = Brushes.White;
                            testing_diesel_TB.ToolTip = "Enter testing Liters";
                            total_sales_ltrs_diesel_TB.Text = getReadingN1_diesel().ToString();

                        }
                        else
                        {
                            total_sales_ltrs_diesel_TB.Text = "0";
                            testing_diesel_TB.Background = Brushes.Red;
                            validDieselData = false;
                            testing_diesel_TB.ToolTip = "Testing must be less than total sales" + getTotalLiters_diesel();

                        }
                    }
                    else
                    {
                        total_sales_ltrs_diesel_TB.Text = total.ToString();
                    }

                }
            }
        }

        private void testing_diesel_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Background = Brushes.White;

            if (textbox.Text.Length > 0)
            {
                if (getTotalLiters_diesel() > -1)
                {
                    double testing = double.Parse(textbox.Text);
                    double total = getTotalLiters_diesel();
                    double totalupdate = total - testing;
                    if (totalupdate > -1)
                    {
                        total_sales_ltrs_diesel_TB.Text = totalupdate.ToString();
                        (sender as TextBox).Background = Brushes.White;

                    }
                    else
                    {
                        total_sales_ltrs_diesel_TB.Text = "0";
                    }
                }
            }
            else
            {
                double total = getTotalLiters_diesel();
                total_sales_ltrs_diesel_TB.Text = total.ToString();
                textbox.Text = "";
            }
        }

        private void total_sales_ltrs_diesel_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (getTotalLiters_diesel() > -1 && rate_diesel_TB.Text.Length > 0)
            {
                double total = getTotalLiters_diesel();
                double rate = double.Parse(rate_diesel_TB.Text);
                total_sales_pkrs_diesel_TB.Text = (total * rate).ToString();

            }
        }

        private void meterClosing_diesel_N1_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (getReadingN1_diesel() < 0)
            {
                TextBox textbox = sender as TextBox;
                textbox.Background = Brushes.Red;
                validDieselData = false;
                textbox.ToolTip = "Meter closing can not be less then Meter opening";

            }
        }

        private void meterClosing_dieselN2_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (getReadingN2_diesel() < 0)
            {
                TextBox textbox = sender as TextBox;
                textbox.Background = Brushes.Red;
                validDieselData = false;
                textbox.ToolTip = "Meter closing can not be less then Meter opening";

            }
        }

        private void testing_diesel_TB_LostFocus(object sender, RoutedEventArgs e)
        {

            TextBox textbox = sender as TextBox;
            if (textbox.Text.Length > 0)
            {
                if (getTotalLiters_diesel() < double.Parse(textbox.Text))
                {
                    textbox.Background = Brushes.Red;
                    validDieselData = false;
                    textbox.ToolTip = "Testing must be less than total sales";

                }
                else
                {
                    textbox.Background = Brushes.White;
                    textbox.ToolTip = "Enter Testing Liters";

                }
            }

        }

        private void rate_diesel_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            upDateTotalPrice_diesel();
        }

        private void upDateTotalPrice_diesel()
        {
            if (total_sales_ltrs_diesel_TB.Text.Length > 0 && rate_diesel_TB.Text.Length > 0)
            {
                double totalLiters = double.Parse(total_sales_ltrs_diesel_TB.Text);
                double rate = double.Parse(rate_diesel_TB.Text);
                double totalPkr = totalLiters * rate;
                total_sales_pkrs_diesel_TB.Text = totalPkr.ToString();
            }
        }

        private double getReadingN1_diesel()
        {
            if (meterClosing_diesel_N1_TB.Text.Length > 0)
            {
                double opening = double.Parse(meterOpening_diesel_N1_TB.Text);
                double closing = double.Parse(meterClosing_diesel_N1_TB.Text);
                double nuzzel1Reading = closing - opening;
                return nuzzel1Reading;
            }
            return -1;

        }

        private double getReadingN2_diesel()
        {

            if (meterClosing_dieselN2_TB.Text.Length > 0)
            {
                double opening = double.Parse(meterOpening_dieselN2_TB.Text);
                double closing = double.Parse(meterClosing_dieselN2_TB.Text);
                double nuzzel2Reading = closing - opening;
                return nuzzel2Reading;

            }
            return -1;

        }

        private double getTotalLiters_diesel()
        {
            double total = 0;
            if (getReadingN1_diesel() > -1)
            {
                total = getReadingN1_diesel();
            }
            if (getReadingN2_diesel() > -1)
            {
                total += getReadingN2_diesel();
            }
            return total;

        }

        private double getTotalPKRs_diesel()
        {
            if (total_sales_pkrs_diesel_TB.Text.Length > 0)
            {
                return double.Parse(total_sales_pkrs_diesel_TB.Text);
            }
            return 0;
        }

        private double getTotalNetProfit_diesel()
        {
            if (today_profit_diesel_TB.Text.Length > 0)
            {
                return double.Parse(today_profit_diesel_TB.Text);
            }
            return 0;
        }

        private String getSelectedDate_diesel()
        {

            String dateTime = diesel_entry_datepicker.SelectedDate.Value.Date.ToShortDateString();
            return dateTime;
        }

        private double getTesting_diesel()
        {
            if (testing_diesel_TB.Text.Length > 0)
            {
                return double.Parse(testing_diesel_TB.Text);
            }
            return -1;
        }

        private void save_diesel_credit_entry_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (getReadingN1_diesel() > -1 && getReadingN2_diesel() > -1 && rate_diesel_TB.Text.Length > 0 && getTotalLiters_diesel() >= getTesting_diesel())
            {
                MessageBox.Show("saved");
            }
        }

        private void crediterName_diesel_TB_KeyUp(object sender, KeyEventArgs e)
        {
            crediterOldAmount_diesel_TB.Visibility = System.Windows.Visibility.Hidden;
            crediterName_diesel_TB.Background = Brushes.Transparent;
            bool found = false;
            //var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Creditors.creditorGetData();

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack.Children.Clear();
                scrollView.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {

                scrollView.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            resultStack.Children.Clear();

            // Add the result   
            foreach (var obj in data)
            {
                if (obj.Key.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work   
                    addItem_diesel(obj.Key);
                    found = true;
                }
            }

            if (!found)
            {
                resultStack.Children.Clear();
                scrollView.Visibility = System.Windows.Visibility.Collapsed;
                //resultStack.Children.Add(new TextBlock() { Text = "No results found.", FontSize = 15, Background = Brushes.White, Foreground = Brushes.Black }) ;
            }

        }

        private void addItem_diesel(string text)
        {
            TextBlock block = new TextBlock();

            // Add the text   
            block.Text = text;
            block.Foreground = Brushes.Black;
            block.FontSize = 15;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                var data = Creditors.creditorGetData();
                string creditorName = (sender as TextBlock).Text;
                crediterName_diesel_TB.Text = creditorName;
                crediterOldAmount_diesel_TB.Content = "+"+data[creditorName];
                crediterOldAmount_diesel_TB.Visibility = System.Windows.Visibility.Visible;
                scrollView.Visibility = System.Windows.Visibility.Collapsed;
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel   
            resultStack.Children.Add(block);
        }

        private void save_diesel_entry_BTN_Click(object sender, RoutedEventArgs e)
        {
            
            string name = crediterName_diesel_TB.Text;
            string amount = creditedAmount_diesel_TB.Text;

            if (name.Length < 1)
            {
                crediterName_diesel_TB.Background = Brushes.Red;
                return;
            }
            if (amount.Length < 1)
            {
                creditedAmount_diesel_TB.Background = Brushes.Blue;
                return;
            }

            //If old creditor then update else create
            var data = Creditors.creditorGetData();
            if (data.ContainsKey(name))
            {
                int totalCredit = int.Parse(amount)+ int.Parse(data[name]);
                amount = totalCredit.ToString();
                Creditors.creditorUpdate(name, amount);
            }
            else
                Creditors.creditorinsert(name, amount);

            //Add the name and amount to left sidebar
            TextBlock block = new TextBlock(); 
            block.Text = name+"            "+amount;
            block.Foreground = Brushes.Black;
            block.FontSize = 15;
            block.Margin = new Thickness(2, 3, 2, 3);
            credit_added_users.Children.Add(block);

            //clear fields
            crediterName_diesel_TB.Text = "";
            creditedAmount_diesel_TB.Text = "";
            crediterOldAmount_diesel_TB.Visibility = System.Windows.Visibility.Hidden;
        }

        private void creditedAmount_diesel_TB_KeyDown(object sender, KeyEventArgs e)
        {
            creditedAmount_diesel_TB.Background = Brushes.Transparent;
        }

        private void discount_amount_diesel_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;

            if (textbox.Text.Length > 0)
            {
                if (textbox.Text.Length > 0)
                {
                    double discount = double.Parse(textbox.Text);
                    double total = 0;
                    if (getTotalPKRs_diesel() > 0)
                        total = getTotalPKRs_diesel() - discount;

                    if (total > 0)
                    {
                        total_sales_pkrs_diesel_TB.Text = total.ToString();
                        textbox.Background = Brushes.White;
                    }
                    else
                    {
                        textbox.Background = Brushes.Red;
                        validDieselData = false;
                        textbox.ToolTip = "discount can not be more then total pkr";
                    }

                }
            }
        }

        private double getDiscountAmount_diesel()
        {
            if(discount_amount_petrol_TB.Text.Length > 0)
            {
                return double.Parse(discount_amount_petrol_TB.Text);
            }
            return 0;
        } 

        private void total_sales_pkrs_diesel_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if(textbox.Text.Length > 0)
            {
               if(getDiscountAmount_diesel() < getTotalPKRs_diesel())
                {
                    discount_amount_diesel_TB.Background = Brushes.White;
                }
                else
                {
                    discount_amount_diesel_TB.Background = Brushes.Red;
                    validDieselData = false;
                }
            }
        }




        private void crediterName_petrol_TB_KeyUp(object sender, KeyEventArgs e)
        {
            crediterOldAmount_petrol_TB.Visibility = System.Windows.Visibility.Hidden;
            crediterName_petrol_TB.Background = Brushes.Transparent;
            bool found = false;
            //var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Creditors.creditorGetData();

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack.Children.Clear();
                scrollView.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {

                scrollView.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            resultStack.Children.Clear();

            // Add the result   
            foreach (var obj in data)
            {
                if (obj.Key.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work   
                    addItem_petrol(obj.Key);
                    found = true;
                }
            }

            if (!found)
            {
                resultStack.Children.Clear();
                scrollView.Visibility = System.Windows.Visibility.Collapsed;
                //resultStack.Children.Add(new TextBlock() { Text = "No results found.", FontSize = 15, Background = Brushes.White, Foreground = Brushes.Black }) ;
            }

        }

        private void addItem_petrol(string text)
        {
            TextBlock block = new TextBlock();

            // Add the text   
            block.Text = text;
            block.Foreground = Brushes.Black;
            block.FontSize = 15;

            // A little style...   
            block.Margin = new Thickness(2, 3, 2, 3);
            block.Cursor = Cursors.Hand;

            // Mouse events   
            block.MouseLeftButtonUp += (sender, e) =>
            {
                var data = Creditors.creditorGetData();
                string creditorName = (sender as TextBlock).Text;
                crediterName_petrol_TB.Text = creditorName;
                crediterOldAmount_petrol_TB.Content = "+" + data[creditorName];
                crediterOldAmount_petrol_TB.Visibility = System.Windows.Visibility.Visible;
                scrollView.Visibility = System.Windows.Visibility.Collapsed;
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel   
            resultStack.Children.Add(block);
        }
        private void save_petrol_credit_entry_BTN_Click(object sender, RoutedEventArgs e)
        {

            string name = crediterName_petrol_TB.Text;
            string amount = creditedAmount_petrol_TB.Text;

            if (name.Length < 1)
            {
                crediterName_petrol_TB.Background = Brushes.Red;
                return;
            }
            if (amount.Length < 1)
            {
                creditedAmount_petrol_TB.Background = Brushes.Blue;
                return;
            }

            //If old creditor then update else create
            var data = Creditors.creditorGetData();
            if (data.ContainsKey(name))
            {
                int totalCredit = int.Parse(amount) + int.Parse(data[name]);
                amount = totalCredit.ToString();
                Creditors.creditorUpdate(name, amount);
            }
            else
                Creditors.creditorinsert(name, amount);

            //Add the name and amount to left sidebar
            TextBlock block = new TextBlock();
            block.Text = name + "            " + amount;
            block.Foreground = Brushes.Black;
            block.FontSize = 15;
            block.Margin = new Thickness(2, 3, 2, 3);
            credit_added_users.Children.Add(block);

            //clear fields
            crediterName_petrol_TB.Text = "";
            creditedAmount_petrol_TB.Text = "";
            crediterOldAmount_petrol_TB.Visibility = System.Windows.Visibility.Hidden;
        }


        private void expensesEvents()
        {
            ArrayList expenseboxes = new ArrayList();
            expenseboxes.Add(staffSalaries_expense_TB);
            expenseboxes.Add(electricity_expense_TB);
            expenseboxes.Add(maintenance_expense_TB);
            expenseboxes.Add(others_expense_TB);
            expenseboxes.Add(miansahid_expense_TB);
            expenseboxes.Add(total_expense_TB);

            staffSalaries_expense_TB.TextChanged += (sender, e) => Expenses.calculate(sender, e, expenseboxes);
            electricity_expense_TB.TextChanged += (sender, e) => Expenses.calculate(sender, e, expenseboxes);
            maintenance_expense_TB.TextChanged += (sender, e) => Expenses.calculate(sender, e, expenseboxes);
            others_expense_TB.TextChanged += (sender, e) => Expenses.calculate(sender, e, expenseboxes);
            miansahid_expense_TB.TextChanged += (sender, e) => Expenses.calculate(sender, e, expenseboxes);
            save_button_expenses.Click += (sender, e) => Expenses.saveExpenseData(sender, e, expenseboxes);
            save_button_deposit.Click += (sender, e) => Expenses.ownerDeposit(sender, e, owner_deposit_TB);
            owner_deposit_TB.TextChanged += Expenses.depositBoxClear;
        }

        private void demandDraftEvents()
        {
            ArrayList petrollist = new ArrayList();
            petrollist.Add(DD_petrolPKR_TB);
            petrollist.Add(DD_petrolLTR_TB);

            DD_petrolPKR_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, petrollist);
            DD_petrolLTR_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, petrollist);
            savebtn_DD_petrol.Click += (sender, e) => DemandDraft.petrolEntry(sender, e, petrollist);

            ArrayList diesellist = new ArrayList();
            diesellist.Add(DD_dieselPKR_TB);
            diesellist.Add(DD_dieselLTR_TB);

            DD_dieselPKR_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, diesellist);
            DD_dieselLTR_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, diesellist);
            savebtn_DD_diesel.Click += (sender, e) => DemandDraft.dieselEntry(sender, e, diesellist);

        }

    }
}
