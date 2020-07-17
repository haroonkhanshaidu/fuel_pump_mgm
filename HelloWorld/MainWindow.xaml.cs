﻿using System;
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
     
        public MainWindow()
        {
            InitializeComponent();
            new SplashWindow().ShowDialog();
            set_initial_values_petrol("12/7/2020", "40", "23");

            //Entry obj = new Entry(getTotalLiters_petrol());

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
                textbox.ToolTip = "Meter closing can not be less then Meter opening";

            }
        }

        private void meterClosing_petrolN2_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (getReadingN2_petrol() < 0)
            {
                TextBox textbox = sender as TextBox;
                textbox.Background = Brushes.Red;
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

        private void save_petrol_credit_entry_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (getReadingN1_petrol() > -1 && getReadingN2_petrol() > -1 && rate_petrol_TB.Text.Length > 0 && getTotalLiters_petrol() >= getTesting_petrol())
            {
                MessageBox.Show("saved");
            }
        }

        private void discount_amount_petrol_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (discount_amount_petrol_TB.Text.Length > 0)
            {
                if (total_sales_pkrs_petrol_TB.Text.Length > 0)
                {
                    double total = 0;
                    if (getReadingN1_petrol() > -1)
                        total += getReadingN1_petrol();
                    if (getReadingN2_petrol() > -1)
                        total += getReadingN2_petrol();
                    double updatedTotal = total - double.Parse(discount_amount_petrol_TB.Text);
                    total_sales_pkrs_petrol_TB.Text = updatedTotal.ToString();
                }
            }
        }
    }
}
