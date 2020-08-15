using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SQLite;
using System.Data;

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Months[] months_list;

        Years[] years_list;

        Boolean validPetrolData = true;

        Boolean validDieselData = true;


        public MainWindow()
        {

            InitializeComponent();
            //new SplashWindow().ShowDialog();
            //new Dashboard1().ShowDialog();

            set_initial_values_diesel();
            set_initial_values_petrol();

            expensesEvents();
            demandDraftEvents();
            OverviewEvents();

            FillMonthsCombobox();
            FillYearsCombobox();

            get_data("petrol");

            testing_diesel_TB.Text = "0";

            //Entry obj = new Entry(getTotalLiters_diesel());

        }


        private void set_initial_values_petrol()
        {
            //petrol_Last_EntryDate_TB.Text = "Last Entry " + date;
            meterOpening_petrol_N1_TB.Text = Entry.getLastEntry("petrol", "closing1");
            meterOpening_petrolN2_TB.Text = Entry.getLastEntry("petrol", "closing2");

            lastEntryPetrol.Text = GlobalFunctions.epochToDateTime(long.Parse(Entry.getLastEntry("petrol", "date")));

            petrol_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            diesel_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            fuel_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.DisplayDateEnd = DateTime.Today.AddDays(0);

            testing_petrol_TB.Text = "0";
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
                                validPetrolData = true;
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
                            validPetrolData = true;
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
                                validPetrolData = true;
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
                            validPetrolData = true;
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
                    double totalupdate = getTotalLiters_petrol() - testing;
                    if (totalupdate > -1)
                    {
                        total_sales_ltrs_petrol_TB.Text = totalupdate.ToString();
                        (sender as TextBox).Background = Brushes.White;
                        validPetrolData = true;

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

            if (getTotalLiters_petrol() > 0 && rate_petrol_TB.Text.Length > 0)
            {

                double total = getTotalLiters_petrol();
                if (testing_petrol_TB.Text.Length > 0)
                    total = total - double.Parse(testing_petrol_TB.Text);
                double rate = double.Parse(rate_petrol_TB.Text);

                if (total > 0)
                {
                    total_sales_pkrs_petrol_TB.Text = (total * rate).ToString();

                }
                else
                {
                    total_sales_pkrs_petrol_TB.Text = "0";
                }

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
                    validPetrolData = true;
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
            } if (rate_petrol_TB.Text.Length < 1)
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

                    if (total >= 0)
                    {
                        total_sales_pkrs_petrol_TB.Text = total.ToString();
                        textbox.Background = Brushes.White;
                        validPetrolData = true;
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

                if (getDiscountAmount_petrol() <= getTotalPKRs_petrol())
                {
                    discount_amount_petrol_TB.Background = Brushes.White;
                    if (meterClosing_petrolN2_TB.Background == Brushes.White && meterClosing_petrol_N1_TB.Background == Brushes.White)
                    {
                        validPetrolData = true;
                    }
                    else
                    {
                        validPetrolData = false;
                    }
                }
                else
                {
                    discount_amount_petrol_TB.Background = Brushes.Red;
                    validPetrolData = false;

                }
            }
        }


        private void crediterName_petrol_TB_KeyUp(object sender, KeyEventArgs e)
        {
            crediterOldAmount_petrol_TB.Visibility = System.Windows.Visibility.Hidden;
            crediterName_petrol_TB.Background = Brushes.Transparent;
            bool found = false;
            //var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Creditors.creditorGetData("creditorData");

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack_petrol.Children.Clear();
                scrollView_petrol.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {

                scrollView_petrol.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            resultStack_petrol.Children.Clear();

            // Add the result   
            foreach (var obj in data)
            {
                if (obj.Key.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must wo
                    addItem_petrol(obj.Key);
                    found = true;
                }
            }

            if (!found)
            {
                resultStack_petrol.Children.Clear();
                scrollView_petrol.Visibility = System.Windows.Visibility.Collapsed;
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
                var data = Creditors.creditorGetData("creditorData");
                string creditorName = (sender as TextBlock).Text;
                crediterName_petrol_TB.Text = creditorName;
                crediterOldAmount_petrol_TB.Content = "+" + data[creditorName];
                crediterOldAmount_petrol_TB.Visibility = System.Windows.Visibility.Visible;
                scrollView_petrol.Visibility = System.Windows.Visibility.Collapsed;
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
            resultStack_petrol.Children.Add(block);
        }

        private void save_petrol_credit_entry_BTN_Click(object sender, RoutedEventArgs e)
        {

            string name = crediterName_petrol_TB.Text;
            string amount = creditedAmount_petrol_TB.Text;
            DateTime dateTime = petrol_entry_datepicker.SelectedDate.Value;
            string epochdate = GlobalFunctions.epochTimeParam(dateTime);

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
            var data = Creditors.creditorGetData("creditorData");
            if (data.ContainsKey(name))
            {
                int totalCredit = int.Parse(amount) + int.Parse(data[name]);
                amount = totalCredit.ToString();
                Creditors.creditorUpdate("creditorData", name, amount, petrol_entry_datepicker);
            }
            else
                Creditors.creditorinsert("creditorData", name, amount, petrol_entry_datepicker);

            //Add the name and amount to left sidebar
            TextBlock block = new TextBlock();
            block.Text = name + "            " + amount;
            block.Foreground = Brushes.Black;
            block.FontSize = 15;
            block.Margin = new Thickness(2, 3, 2, 3);
            credit_added_users_petrol.Children.Add(block);

            //clear fields
            crediterName_petrol_TB.Text = "";
            creditedAmount_petrol_TB.Text = "";
            crediterOldAmount_petrol_TB.Visibility = System.Windows.Visibility.Hidden;
        }

        private void save_petrol_entry_BTN_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = petrol_entry_datepicker.SelectedDate.Value;

            if (validPetrolData)
            {
                if (getTotalPKRs_petrol() > 0)
                {
                    Dictionary<string, double> dict = new Dictionary<string, double>();

                    dict.Add("n1opening", double.Parse(meterOpening_petrol_N1_TB.Text));
                    dict.Add("n1closing", double.Parse(meterClosing_petrol_N1_TB.Text));
                    dict.Add("n2opening", double.Parse(meterOpening_petrolN2_TB.Text));
                    dict.Add("n2closing", double.Parse(meterClosing_petrolN2_TB.Text));
                    dict.Add("rate", double.Parse(rate_petrol_TB.Text));
                    dict.Add("testing", double.Parse(testing_petrol_TB.Text));
                    dict.Add("discount", double.Parse(discount_amount_petrol_TB.Text));
                    dict.Add("totalLtrs", double.Parse(total_sales_ltrs_petrol_TB.Text));
                    dict.Add("totalPkrs", double.Parse(total_sales_pkrs_petrol_TB.Text));

                    if (!Entry.dateFound(petrol_entry_datepicker, "petrol"))
                    //if (Entry.InsertEntry(sender, e, "petrol", dict, petrol_entry_datepicker) == 1)
                    {
                        if (FuelTank.FuelDeduction(dict["totalLtrs"], "ddPetrol"))
                        {
                            if (Entry.InsertEntry(sender, e, "petrol", dict, petrol_entry_datepicker) == 1)
                            {
                                meterOpening_petrol_N1_TB.Text = meterClosing_petrol_N1_TB.Text;
                                meterOpening_petrolN2_TB.Text = meterClosing_petrolN2_TB.Text;
                                meterClosing_petrol_N1_TB.Text = "";
                                meterClosing_petrolN2_TB.Text = "";
                                testing_petrol_TB.Text = "0";
                                discount_amount_petrol_TB.Text = "0";
                                total_sales_ltrs_petrol_TB.Text = "";
                                total_sales_pkrs_petrol_TB.Text = "";
                                rate_petrol_TB.Text = "";


                                lastEntryPetrol.Text = date.ToString("dd/MM/yyyy");

                                MessageBox.Show("Entry saved for " + date);
                            }
                            else
                            {
                                MessageBox.Show("Error  try again");
                            }

                        } else
                        {
                            MessageBox.Show("Not enough fuel in Tank \nmust be invalid entry value");
                        }
                    }
                    else
                    {
                        MessageBox.Show(" Entry for " + date + " is already available in database   \n if you want to edit an existing record on " + date + " goto gridview");
                        return;
                    }

                }
                else if (getTotalPKRs_petrol() == 0 && rate_petrol_TB.Text.Length > 0)
                {
                    MessageBox.Show("note that only testing was performed no sales ware made");
                }

            }

        }

















        private void set_initial_values_diesel()
        {
            //diesel_Last_EntryDate_TB.Text = "Last Entry " + date;
            meterOpening_diesel_N1_TB.Text = Entry.getLastEntry("diesel", "closing1");
            meterOpening_dieselN2_TB.Text = Entry.getLastEntry("diesel", "closing2");
            lastEntryDiesel.Text = GlobalFunctions.epochToDateTime(long.Parse(Entry.getLastEntry("diesel", "date")));

            diesel_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            diesel_entry_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            fuel_datepicker.SelectedDate = DateTime.Today.AddDays(0);
            expense_datepicker.DisplayDateEnd = DateTime.Today.AddDays(0);

            testing_diesel_TB.Text = "0";
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
                                validDieselData = true;
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
                            validDieselData = true;
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
                                validDieselData = true;
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
                            validDieselData = true;
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
                        validDieselData = true;

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
            if (getTotalLiters_diesel() > 0 && rate_diesel_TB.Text.Length > 0)
            {
                double total = getTotalLiters_diesel();
                if (testing_diesel_TB.Text.Length > 0)
                    total = total - double.Parse(testing_diesel_TB.Text);
                double rate = double.Parse(rate_diesel_TB.Text);

                if (total > 0)
                {
                    total_sales_pkrs_diesel_TB.Text = (total * rate).ToString();

                }
                else
                {
                    total_sales_pkrs_diesel_TB.Text = "0";
                }
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
                    validDieselData = true;
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
            if (rate_diesel_TB.Text.Length < 1)
            {
                total_sales_pkrs_diesel_TB.Text = "";
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
            string name = crediterName_diesel_TB.Text;
            string amount = creditedAmount_diesel_TB.Text;
            DateTime dateTime = diesel_entry_datepicker.SelectedDate.Value;
            string epochdate = GlobalFunctions.epochTimeParam(dateTime);

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
            var data = Creditors.creditorGetData("creditorDataDiesel");
            if (data.ContainsKey(name))
            {
                int totalCredit = int.Parse(amount) + int.Parse(data[name]);
                amount = totalCredit.ToString();
                Creditors.creditorUpdate("creditorDataDiesel", name, amount, diesel_entry_datepicker);
            }
            else
                Creditors.creditorinsert("creditorDataDiesel", name, amount, diesel_entry_datepicker);

            //Add the name and amount to left sidebar
            TextBlock block = new TextBlock();
            block.Text = name + "            " + amount;
            block.Foreground = Brushes.DarkBlue;
            block.FontSize = 15;
            block.Margin = new Thickness(2, 3, 2, 3);
            credit_added_users_diesel.Children.Add(block);

            //clear fields
            crediterName_diesel_TB.Text = "";
            creditedAmount_diesel_TB.Text = "";
            crediterOldAmount_diesel_TB.Visibility = System.Windows.Visibility.Hidden;
        }

        private void crediterName_diesel_TB_KeyUp(object sender, KeyEventArgs e)
        {
            crediterOldAmount_diesel_TB.Visibility = System.Windows.Visibility.Hidden;
            crediterName_diesel_TB.Background = Brushes.Transparent;
            bool found = false;
            //var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Creditors.creditorGetData("creditorDataDiesel");

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack_diesel.Children.Clear();
                scrollView_diesel.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {

                scrollView_diesel.Visibility = System.Windows.Visibility.Visible;
            }

            // Clear the list   
            resultStack_diesel.Children.Clear();

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
                resultStack_diesel.Children.Clear();
                scrollView_diesel.Visibility = System.Windows.Visibility.Collapsed;
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
                var data = Creditors.creditorGetData("creditorDataDiesel");
                string creditorName = (sender as TextBlock).Text;
                crediterName_diesel_TB.Text = creditorName;
                crediterOldAmount_diesel_TB.Content = "+" + data[creditorName];
                crediterOldAmount_diesel_TB.Visibility = System.Windows.Visibility.Visible;
                scrollView_diesel.Visibility = System.Windows.Visibility.Collapsed;
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
            resultStack_diesel.Children.Add(block);
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

                    if (total >= 0)
                    {
                        total_sales_pkrs_diesel_TB.Text = total.ToString();
                        textbox.Background = Brushes.White;
                        validDieselData = true;
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
            if (discount_amount_diesel_TB.Text.Length > 0)
            {
                return double.Parse(discount_amount_diesel_TB.Text);
            }
            return 0;
        }

        private void total_sales_pkrs_diesel_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text.Length > 0)
            {
                if (getDiscountAmount_diesel() <= getTotalPKRs_diesel())
                {
                    discount_amount_diesel_TB.Background = Brushes.White;
                    if (meterClosing_dieselN2_TB.Background == Brushes.White && meterClosing_diesel_N1_TB.Background == Brushes.White)
                    {
                        validDieselData = true;
                    }
                    else
                    {
                        validDieselData = false;
                    }
                }
                else
                {
                    discount_amount_diesel_TB.Background = Brushes.Red;
                    validDieselData = false;
                }
            }
        }

        private void save_diesel_entry_BTN_Click(object sender, RoutedEventArgs e)
        {


            DateTime date = diesel_entry_datepicker.SelectedDate.Value;

            if (validDieselData)
            {
                if (getTotalPKRs_diesel() > 0)
                {
                    Dictionary<string, double> dict = new Dictionary<string, double>();

                    dict.Add("n1opening", double.Parse(meterOpening_diesel_N1_TB.Text));
                    dict.Add("n1closing", double.Parse(meterClosing_diesel_N1_TB.Text));
                    dict.Add("n2opening", double.Parse(meterOpening_dieselN2_TB.Text));
                    dict.Add("n2closing", double.Parse(meterClosing_dieselN2_TB.Text));
                    dict.Add("rate", double.Parse(rate_diesel_TB.Text));
                    dict.Add("testing", double.Parse(meterClosing_diesel_N1_TB.Text));
                    dict.Add("discount", double.Parse(discount_amount_diesel_TB.Text));
                    dict.Add("totalLtrs", double.Parse(total_sales_ltrs_diesel_TB.Text));
                    dict.Add("totalPkrs", double.Parse(total_sales_pkrs_diesel_TB.Text));

                    if (!Entry.dateFound(diesel_entry_datepicker, "diesel"))
                    {
                        if (FuelTank.FuelDeduction(dict["totalLtrs"], "ddDiesel"))
                        {
                            if (Entry.InsertEntry(sender, e, "diesel", dict, diesel_entry_datepicker) == 1)
                            {
                                meterOpening_diesel_N1_TB.Text = meterClosing_diesel_N1_TB.Text;
                                meterOpening_dieselN2_TB.Text = meterClosing_dieselN2_TB.Text;

                                meterClosing_dieselN2_TB.Text = "";
                                meterClosing_diesel_N1_TB.Text = "";
                                testing_diesel_TB.Text = "0";
                                discount_amount_diesel_TB.Text = "0";
                                total_sales_ltrs_diesel_TB.Text = "";
                                total_sales_pkrs_diesel_TB.Text = "";
                                rate_diesel_TB.Text = "";


                                lastEntryDiesel.Text = date.ToString("dd/MM/yyyy");

                                MessageBox.Show("Entry saved for " + date);
                            }
                            else
                            {
                                MessageBox.Show("Error  try again");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Not enough fuel in Tank \nmust be invalid entry value");
                        }
                    }
                    else
                    {
                        MessageBox.Show(" Entry for " + date + " is already available in database   \n if you want to edit an existing record on " + date + " goto gridview");
                        return;
                    }

                }
                else if (getTotalPKRs_diesel() == 0 && rate_diesel_TB.Text.Length > 0)
                {
                    MessageBox.Show("note that only testing was performed no sales ware made");
                }

            }

        }







        private void OverviewEvents()
        {
            ArrayList salesLabels = new ArrayList();
            salesLabels.Add(LTR_petrol_sold_Lbl);
            salesLabels.Add(PKR_petrol_sold_Lbl);
            salesLabels.Add(LTR_diesel_sold_Lbl);
            salesLabels.Add(Pkr_diesel_sold_Lbl);
            Overview.SalesOverview(salesLabels, GlobalFunctions.dateTimeToMonthRange(DateTime.Now));
            

            ArrayList fuelLabels = new ArrayList();
            fuelLabels.Add(Fueltank_petrol_lbl);
            fuelLabels.Add(Fueltank_diesel_lbl);
            Overview.FuelTank(fuelLabels);

            ArrayList totalProfit = new ArrayList();
            totalProfit.Add(totalprofit_petrol_lbl);
            totalProfit.Add(totalprofit_diesel_lbl);
            Overview.TotalProfit(totalProfit);



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
            save_button_expenses.Click += (sender, e) => Expenses.saveExpenseData(sender, e, expenseboxes, expense_datepicker);
            save_button_deposit.Click += (sender, e) => Expenses.ownerDeposit(sender, e, owner_deposit_TB, expense_datepicker);
            owner_deposit_TB.TextChanged += Expenses.depositBoxClear;
        }

        private void demandDraftEvents()
        {

            ArrayList petrollist = new ArrayList();
            petrollist.Add(DD_petrolRF_TB);
            petrollist.Add(DD_petrolPKR_TB);
            petrollist.Add(DD_petrolLTR_TB);

            DD_petrolRF_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, petrollist);
            DD_petrolPKR_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, petrollist);
            DD_petrolLTR_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, petrollist);
            savebtn_DD_petrol.Click += (sender, e) => DemandDraft.FuelDemandDraftEntry(sender, e, petrollist, fuel_datepicker, "ddPetrol");

            ArrayList diesellist = new ArrayList();
            diesellist.Add(DD_dieselRF_TB);
            diesellist.Add(DD_dieselPKR_TB);
            diesellist.Add(DD_dieselLTR_TB);

            DD_dieselPKR_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, diesellist);
            DD_dieselLTR_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, diesellist);
            DD_dieselRF_TB.TextChanged += (sender, e) => DemandDraft.BoxesBackgroundClear(sender, e, diesellist);
            savebtn_DD_diesel.Click += (sender, e) => DemandDraft.FuelDemandDraftEntry(sender, e, diesellist, fuel_datepicker, "ddDiesel");

        }








        private void AddPresetButton_Click(object sender, RoutedEventArgs e)
        {
            var addButton = sender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
                   
        }

        public void get_data(string table)
        {
            //SQLiteCommand sqlite_cmd;
            //sqlite_cmd = GlobalFunctions.Connect().CreateCommand();
            //sqlite_cmd.CommandText = "SELECT * FROM " + table ;




            string CommandText = "SELECT * FROM " + table;
            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(CommandText, GlobalFunctions.Connect());

            DataTable dt;
            using (dt = new DataTable())
            {
                sqlda.Fill(dt);
                PetrolSales_dataGrid.ItemsSource = dt.DefaultView;
                GlobalFunctions.CloseConnection();
            }
        }


        class Months
        {
            public string Name
            {
                get;
                set;
            }
            public int index
            {
                get;
                set;
            }
           
            public Months(int i, string n)
            {
                index = i;
                Name = n;
               
            }
        }


        class Years
        {
            public string Name
            {
                get;
                set;
            }
            public int index
            {
                get;
                set;
            }

            public Years(int i, string n)
            {
                index = i;
                Name = n;

            }
        }

        private void FillMonthsCombobox()
        {
            try
            {
               months_list = new Months[] {
                new Months(0, "All"),
                new Months(1, "January"),
                new Months(2, "Fabuary"),
                new Months(3, "March"),
                new Months(4, "April"),
                new Months(5, "May"),
                new Months(6, "June"),
                new Months(7, "July"),
                new Months(8, "August"),
                new Months(9, "September"),
                new Months(10, "October"),
                new Months(11, "November"),
                new Months(12, "December")
        };
                sales_combobox_month.ItemsSource = months_list;
                sales_combobox_month.DisplayMemberPath = "Name";
                sales_combobox_month.SelectedIndex = 0;


                sales_details_combobox_months.ItemsSource = months_list;
                sales_details_combobox_months.DisplayMemberPath = "Name";
                sales_details_combobox_months.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        private void FillYearsCombobox()
        {
            try
            {
                years_list = new Years[] {
                new Years(1, "2020"),
                new Years(2, "2021"),
                new Years(3, "2022"),
                new Years(4, "2023"),
                new Years(5, "2024"),
                new Years(6, "2025")
        };
                sales_combobox_year.ItemsSource = years_list;
                sales_combobox_year.DisplayMemberPath = "Name";
                sales_combobox_year.SelectedIndex = 0;

                sales_details_combobox_year.ItemsSource = years_list;
                sales_details_combobox_year.DisplayMemberPath = "Name";
                sales_details_combobox_year.SelectedIndex = 0;

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        private void sales_combobox_year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ArrayList salesLabels = new ArrayList();
            salesLabels.Add(LTR_petrol_sold_Lbl);
            salesLabels.Add(PKR_petrol_sold_Lbl);
            salesLabels.Add(LTR_diesel_sold_Lbl);
            salesLabels.Add(Pkr_diesel_sold_Lbl);
            ArrayList dateRange =  GlobalFunctions.comboBoxtoDateRangeList(sales_combobox_month, sales_combobox_year);
            Overview.SalesOverview(salesLabels, dateRange);
        }

        private void sales_combobox_month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ArrayList salesLabels = new ArrayList();
            salesLabels.Add(LTR_petrol_sold_Lbl);
            salesLabels.Add(PKR_petrol_sold_Lbl);
            salesLabels.Add(LTR_diesel_sold_Lbl);
            salesLabels.Add(Pkr_diesel_sold_Lbl);
            ArrayList dateRange = GlobalFunctions.comboBoxtoDateRangeList(sales_combobox_month, sales_combobox_year);
            Overview.SalesOverview(salesLabels, dateRange);

        }

        private void sales_details_combobox_year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void sales_details_combobox_months_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

   

 
   


}
