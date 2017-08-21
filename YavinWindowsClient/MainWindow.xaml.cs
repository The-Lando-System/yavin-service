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
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Threading;

namespace YavinWindowsClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private YavinServiceReference.TransactionServiceClient dataServiceClient;
        private bool editMode = false;
        private List<YavinServiceReference.Transaction> transactions;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        decimal[] income;
        decimal[] txs;

        public MainWindow()
        {
            InitializeComponent();
            RefreshTransactions();
            InitChart(false); 
        }
            
        private void InitChart(bool refresh)
        {
            Dictionary<int, decimal> incomeByMonth = new Dictionary<int, decimal>();
            Dictionary<int, decimal> purchasesByMonth = new Dictionary<int, decimal>();
            Dictionary<int, int> months = new Dictionary<int, int>();

            foreach (var transaction in transactions)
            {
                int month = transaction.Time.Month;
                if (!months.ContainsKey(month))
                    months.Add(month, month);

                if (transaction.Type == YavinServiceReference.TransactionType.INCOME)
                {
                    if (!incomeByMonth.ContainsKey(month))
                        incomeByMonth.Add(month, transaction.Amount);
                    else
                        incomeByMonth[month] += transaction.Amount;
                }

                if (transaction.Type == YavinServiceReference.TransactionType.PURCHASE)
                {
                    if (!purchasesByMonth.ContainsKey(month))
                        purchasesByMonth.Add(month, transaction.Amount);
                    else 
                        purchasesByMonth[month] += transaction.Amount;
                }

            }

            var incomeKeys = incomeByMonth.Keys.ToList();
            incomeKeys.Sort();

            var purchaseKeys = purchasesByMonth.Keys.ToList();
            purchaseKeys.Sort();

            var monthKeys = months.Keys.ToList();
            monthKeys.Sort();

            decimal[] incomeVals = monthKeys.Select(month => incomeByMonth.ContainsKey(month) ? incomeByMonth[month] : 0).ToArray();
            decimal[] txVals = monthKeys.Select(month => purchasesByMonth.ContainsKey(month) ? purchasesByMonth[month] : 0).ToArray();

            LineSeries incomeLs = new LineSeries
            {
                Title = "Income",
                Values = new ChartValues<decimal>(incomeVals),
                
            };

            LineSeries txLs = new LineSeries
            {
                Title = "Transactions",
                Values = new ChartValues<decimal>(txVals)
            };

            if (refresh)
            {
                int numSeries = SeriesCollection.Count;
                for (int i = 0; i < numSeries; i++)
                {
                    SeriesCollection.RemoveAt(0);
                }
                SeriesCollection.Add(incomeLs);
                SeriesCollection.Add(txLs);
            }
            else
            {
                SeriesCollection = new SeriesCollection { incomeLs, txLs };
            }

            Labels = monthKeys.Select(month => month.ToString()).ToArray();
            //Labels = incomeKeys.Count > purchaseKeys.Count ?
            //    incomeKeys.Select(key => key.ToString()).ToArray() :
            //    purchaseKeys.Select(key => key.ToString()).ToArray();

            YFormatter = value => value.ToString("C");

            DataContext = this;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void button_saveTransaction(object sender, RoutedEventArgs e)
        {
            dataServiceClient = new YavinServiceReference.TransactionServiceClient();

            YavinServiceReference.Transaction t = null; 

            if (editMode)
            {
                t = dataServiceClient.EditTransaction(GetTransactionDetails());
            }
            else
            {
                t = dataServiceClient.CreateTransaction(GetTransactionDetails());
            }

            if (t.Amount < 0)
            {
                SeriesCollection[0].Values.Add(-1 * t.Amount);
            }
            else
            {
                SeriesCollection[1].Values.Add(t.Amount);
            }

            editMode = false;
            ToggleEditMode();

            RefreshTransactions();
        }

        private void RefreshTransactions()
        {
            dataServiceClient = new YavinServiceReference.TransactionServiceClient();

            System.Windows.Data.CollectionViewSource transactionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("transactionViewSource")));
            transactions = new List<YavinServiceReference.Transaction>(dataServiceClient.GetTransactions());
            transactionViewSource.Source = transactions;

            decimal totalIncome = 0;
            decimal totalTransactions = 0;
            foreach (YavinServiceReference.Transaction t in transactions)
            {
                if (t.Amount < 0)
                    totalIncome += t.Amount;
                else
                    totalTransactions += t.Amount;
            }

            totalIncomeTextBlock.Text = "Total Income: $99" + FormatUtils.ToMoney(totalIncome * -1);
            totalTransactionsTextBlock.Text = "Total Transactions: $" + FormatUtils.ToMoney(totalTransactions);

        }

        private YavinServiceReference.Transaction GetTransactionDetails()
        {
            YavinServiceReference.Transaction t = new YavinServiceReference.Transaction();

            t.Id = idTextBlock.Text;
            t.Type = typeTextBox.Text == "PURCHASE" ? YavinServiceReference.TransactionType.PURCHASE : YavinServiceReference.TransactionType.INCOME;
            t.Description = descriptionTextBox.Text;
            t.Amount = decimal.Parse(amountTextBox.Text);
            t.Time = DateTime.Parse(timeDatePicker.Text);

            return t;
        }

        private void button_CreateNewTransaction(object sender, RoutedEventArgs e)
        {
            idTextBlock.Text = "";
            typeTextBox.Text = "PURCHASE";
            descriptionTextBox.Text = "";
            amountTextBox.Text = "";
            timeDatePicker.DisplayDate = DateTime.Now;
            ToggleEditMode();
        }

        private void button_DeleteTransaction(object sender, RoutedEventArgs e)
        {
            dataServiceClient = new YavinServiceReference.TransactionServiceClient();
            dataServiceClient.DeleteTransaction(idTextBlock.Text);
            RefreshTransactions();
        }

        private void button_editTransaction(object sender, RoutedEventArgs e)
        {
            editMode = true;
            ToggleEditMode();
        }

        private void ToggleEditMode()
        {
            cancelButton.Visibility = cancelButton.IsVisible ? Visibility.Hidden : Visibility.Visible;
            saveButton.Visibility = saveButton.IsVisible ? Visibility.Hidden : Visibility.Visible;

            createButton.Visibility = createButton.IsVisible ? Visibility.Hidden : Visibility.Visible;
            editButton.Visibility = editButton.IsVisible ? Visibility.Hidden : Visibility.Visible;

            typeTextBox.Visibility = typeTextBox.IsVisible ? Visibility.Hidden : Visibility.Visible;
            descriptionTextBox.Visibility = descriptionTextBox.IsVisible ? Visibility.Hidden : Visibility.Visible;
            amountTextBox.Visibility = amountTextBox.IsVisible ? Visibility.Hidden : Visibility.Visible;
            timeDatePicker.Visibility = timeDatePicker.IsVisible ? Visibility.Hidden : Visibility.Visible;

            typeTextBlock.Visibility = typeTextBlock.IsVisible ? Visibility.Hidden : Visibility.Visible;
            descriptionTextBlock.Visibility = descriptionTextBlock.IsVisible ? Visibility.Hidden : Visibility.Visible;
            amountTextBlock.Visibility = amountTextBlock.IsVisible ? Visibility.Hidden : Visibility.Visible;
            timeTextBlock.Visibility = timeTextBlock.IsVisible ? Visibility.Hidden : Visibility.Visible;
        }

        private void button_cancelTransaction(object sender, RoutedEventArgs e)
        {
            editMode = false;
            ToggleEditMode();
            RefreshTransactions();
        }

        private void button_refreshChart(object sender, RoutedEventArgs e)
        {
            InitChart(true);
        }
    }
}
