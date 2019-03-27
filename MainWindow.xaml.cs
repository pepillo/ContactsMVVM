using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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
using Contacts.Data;
using Contacts.Windows;

namespace Contacts
{
    //Main Windo Class
    public partial class MainWindow : Window
    {
        //Current Customer in View
        private Customer CustomerCurrent = null;

        //Constructor
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Window_Loaded);
            //this.Closed += new CancelEventHandler(OnWindowClosing);
        }

        //Call when windows Loads (Initialize), Set windows params
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title  = " Contact";
            this.Height = 450;
            this.Width  = 900;
            this.ResizeMode = ResizeMode.NoResize;

            //Load & Set values in Customers DataGrid Header & Values
            this.SetCustomerDataGridHeader();
            this.LoadCustomerDataGrid();
        }

        private void LoadCustomerDataView(int id)
        {
            Customer CustomerRecord = Customer.GetByID(id);

            if (CustomerRecord == null)
            {
                //MessageBox.Show("Invalid Customer ID.");
                this.CustomerViewData.Visibility = Visibility.Hidden;
                return;
            }

            this.CustomerCurrent = CustomerRecord;

            //Show Customer Pannel Info if double click on a valid Customer
            this.CustomerViewData.Visibility = Visibility.Visible;

            this.ClientDataName.Text = $"{CustomerRecord.NameFirst} {CustomerRecord.NameMiddle} {CustomerRecord.NameLast}";
            this.ClientDataPhone.Text = CustomerRecord.Phone;
            this.ClientDataCreditScore.Text = CustomerRecord.CreditScore.ToString();
        }

        private void SetCustomerDataGridHeader()
        {
            //Set DataGrid Columns
            this.CustomerDataGrid.Columns.Add(new DataGridTextColumn { Header = "#", Binding = new Binding("Index") });
            this.CustomerDataGrid.Columns.Add(new DataGridTextColumn { Header = "id", Binding = new Binding("id") });
            this.CustomerDataGrid.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("NameFirst") });
            this.CustomerDataGrid.Columns.Add(new DataGridTextColumn { Header = "Initial", Binding = new Binding("NameMiddle") });
            this.CustomerDataGrid.Columns.Add(new DataGridTextColumn { Header = "Last Name", Binding = new Binding("NameLast") });
            this.CustomerDataGrid.Columns.Add(new DataGridTextColumn { Header = "Phone", Binding = new Binding("Phone") });
            this.CustomerDataGrid.Columns.Add(new DataGridTextColumn { Header = "Stamp", Binding = new Binding("Stamp") });
            this.CustomerDataGrid.Columns.Add(new DataGridTextColumn { Header = "Actions" });

            //Hide Customer Pannel Info on start
            //this.CustomerViewData.Visibility = Visibility.Hidden;

            //Hide the Customer.id column from DataGrid
            this.CustomerDataGrid.Columns[1].Visibility = Visibility.Hidden;
        }

        private void LoadCustomerDataGrid()
        {
            int i = 0;

            //for (int x = 0; x < 10; x++){
            //Populate DataGrid Columns
            foreach (Customer Customer in Customer.GetAll())
            {
                i++;

                this.CustomerDataGrid.Items.Add(new
                {
                    Index = i,
                    id = Customer.id,
                    NameFirst = Customer.NameFirst,
                    NameMiddle = Customer.NameMiddle,
                    NameLast = Customer.NameLast,
                    Phone = Customer.Phone,
                    Stamp = Customer.Stamp,
                });
            }
            //}
        }

        public void Reload()
        {
            //Clear values from DataGrid
            this.CustomerDataGrid.Items.Clear();

            //Load Customer in DataGrid
            this.LoadCustomerDataGrid();

            //Load CustomerDataView
            if(this.CustomerCurrent != null)
                this.LoadCustomerDataView((int)this.CustomerCurrent.id);
        }

        public void Alert(string Message)
        {
            this.BorderAlert.Visibility = Visibility.Visible;
            this.TextAlert.Text = Message;
        }

        public void AlertClose()
        {
            this.BorderAlert.Visibility = Visibility.Collapsed;
            this.TextAlert.Text = "";
        }

        private void DataGridRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic CustomerData = ((DataGridRow)sender).DataContext;

            if (CustomerData.id <= 0) return;

            this.LoadCustomerDataView(CustomerData.id);
        }

        private void Button_Customer_Edit_Click(object sender, RoutedEventArgs e)
        {
            //Open CustomerEditWindow
            ClientDataWindow ClientDataWindow = new ClientDataWindow(this, this.CustomerCurrent);
            ClientDataWindow.ShowDialog();
            //popup.Show();
        }

        private void Button_Customer_Notes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Customer_Remove_Click(object sender, RoutedEventArgs e)
        {
            string CustomerFullName = this.CustomerCurrent.NameFirst + " " + this.CustomerCurrent.NameMiddle + " " + this.CustomerCurrent.NameLast;

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete [" + CustomerFullName + "] from your address?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.CustomerCurrent.Delete();
                this.Alert(CustomerFullName + " has been removed from you address book.");
            }

            this.Reload();
        }

        private void ButtonCustomerAdd_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Customer CustomerNew = new Customer{ id = 0 };

            ClientDataWindow ClientDataWindow = new ClientDataWindow(this, CustomerNew);
            ClientDataWindow.ShowDialog();
        }

        private void TextAlertClose_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.AlertClose();
        }
    }
}
