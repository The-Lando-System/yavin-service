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

        private static Action EmptyDelegate = delegate () { };

        public MainWindow()
        {
            InitializeComponent();
            RefreshTransactions();
            InitChart(); 
        }
            
        private void InitChart()
        {

            decimal[] income = transactions.Where(t => t.Type == YavinServiceReference.TransactionType.INCOME).Select(t => t.Amount).ToArray();
            decimal[] txs = transactions.Where(t => t.Amount >= 0).Select(t => t.Amount).ToArray();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Income",
                    Values = new ChartValues<decimal>(income),                    
                },
                new LineSeries
                {
                    Title = "Transactions",
                    Values = new ChartValues<decimal>(txs)
                }
            };
            

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            DataContext = this;
        }

        private void RefreshChart()
        {
            int numSeries = SeriesCollection.Count;
            for(int i=0; i<numSeries; i++)
            {
                SeriesCollection.RemoveAt(0);
            }

            decimal[] income = transactions.Where(t => t.Type == YavinServiceReference.TransactionType.INCOME).Select(t => t.Amount).ToArray();
            decimal[] txs = transactions.Where(t => t.Amount >= 0).Select(t => t.Amount).ToArray();

            SeriesCollection.Add(
                new LineSeries
                {
                    Title = "Income",
                    Values = new ChartValues<decimal>(income),
                }
            );
            SeriesCollection.Add(
                new LineSeries
                {
                    Title = "Transactions",
                    Values = new ChartValues<decimal>(txs)
                }
            );


            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");
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

            totalIncomeTextBlock.Text = "Total Income: $" + FormatUtils.ToMoney(totalIncome * -1);
            totalTransactionsTextBlock.Text = "Total Transactions: $" + FormatUtils.ToMoney(totalTransactions);

        }

        private YavinServiceReference.Transaction GetTransactionDetails()
        {
            YavinServiceReference.Transaction t = new YavinServiceReference.Transaction();

            t.Id = idTextBlock.Text;
            t.Description = descriptionTextBox.Text;
            t.Amount = decimal.Parse(amountTextBox.Text);
            t.Time = DateTime.Parse(timeDatePicker.Text);

            return t;
        }

        private void button_CreateNewTransaction(object sender, RoutedEventArgs e)
        {
            idTextBlock.Text = "";
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

            descriptionTextBox.Visibility = descriptionTextBox.IsVisible ? Visibility.Hidden : Visibility.Visible;
            amountTextBox.Visibility = amountTextBox.IsVisible ? Visibility.Hidden : Visibility.Visible;
            timeDatePicker.Visibility = timeDatePicker.IsVisible ? Visibility.Hidden : Visibility.Visible;

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
            RefreshChart();
        }
    }
}
