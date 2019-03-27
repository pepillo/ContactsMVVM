using Contacts.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Contacts.Windows
{
    public partial class ClientDataWindow : Window
    {
        private MainWindow MainWindow = null;
        private Customer Data = null;

        public ClientDataWindow(MainWindow MainWindow, Customer Data)
        {
            this.MainWindow = MainWindow;
            this.Data = Data;

            InitializeComponent();

            this.Title = "Customer - ID:" + Data.id;
            this.Height = 350;
            this.Width = 300;
            this.ResizeMode = ResizeMode.NoResize;

            this.Loaded += new RoutedEventHandler(Window_Loaded);
            this.Closing += new CancelEventHandler(Window_Closing);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ClientDataID.Text = this.Data.id.ToString();
            this.ClientDataNameFirst.Text = this.Data.NameFirst;
            this.ClientDataNameMiddle.Text = this.Data.NameMiddle;
            this.ClientDataNameLast.Text = this.Data.NameLast;
            this.ClientDataCreditScore.Text = this.Data.CreditScore.ToString();
            this.ClientDataPhone.Text = this.Data.Phone;
        }

        public void Window_Closing(object sender, CancelEventArgs e)
        {
            
        }

        private void Button_Customer_Save_Click(object sender, RoutedEventArgs e)
        {
            Customer Customer = new Customer {
                //id = this.Data.id,
                NameFirst = this.ClientDataNameFirst.Text,
                NameMiddle = this.ClientDataNameMiddle.Text,
                NameLast = this.ClientDataNameLast.Text,
                Phone = this.ClientDataPhone.Text,
                CreditScore = int.Parse(this.ClientDataCreditScore.Text),
            };

            if (this.Data.id == 0)
            {
                Customer.Add();
            }

            else
            {
                Customer.id = this.Data.id;
                Customer.Save();
            }

            this.MainWindow.Reload();
            this.Close();
        }
    }
}
