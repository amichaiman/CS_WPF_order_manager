using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SortedList<string, Order> Orders = new SortedList<string,Order>();
        private bool IsShowingAll = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order newOrder = new Order(LinkTextBox.Text, TrackingLinkTextBox.Text, OrderNumberTextBox.Text, JobNumberTextBox.Text, ExtraInfoComboBox.Text, StoreNumberComboBox.Text);
                Orders.Add(newOrder.OrderNumber, newOrder);
                ClearOrderInput();
                if (IsShowingAll)
                {
                    ShowAllOrdersInOrderListBox();
                }
            } 
            catch (InvalidValue invalidValueException)
            {
                MessageBox.Show(invalidValueException.Message, "Notice");
            }
        }

        private void ClearOrderInput()
        {
            LinkTextBox.Text = TrackingLinkTextBox.Text = JobNumberTextBox.Text = OrderNumberTextBox.Text = "";
            ExtraInfoComboBox.SelectedIndex = 0;
        }
        
        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsShowingAll)
            {
                IsShowingAll = true;
            }
            ShowAllOrdersInOrderListBox();
           
        }

        private void ShowAllOrdersInOrderListBox()
        {
            ClearOrderListBox();
            foreach (Order order in Orders.Values)
            {
                OrderListBox.Items.Add(order);
            }
        }

        private void ClearOrderListBox()
        {
            OrderListBox.Items.Clear();
        }

        private void OrderListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Order orderSelected = GetOrderFromOrderListBox();
                ShowLinkTextBox.Text = orderSelected.Link;
                ShowTrackingLinkTextBox.Text = orderSelected.TrackingLink;
                ShowJobNumberTextBox.Text = orderSelected.JobNumber;
                ShowExtraInfoTextBox.Text = orderSelected.ExtraInfo;
                ShowStoreNameTextBox.Text = orderSelected.Store;
            }
            catch (Exception){}
        }

        private Order GetOrderFromOrderListBox()
        {
            if (OrderListBox.Items.Count < 1)
            {
                throw new Exception();
            }
            return Orders.ElementAt(Orders.IndexOfKey(OrderListBox.SelectedItem.ToString())).Value;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                MessageBox.Show("Please Enter Search Epression", "Notice");
            }
            else
            {
                ClearShowBoxs();
                ClearOrderListBox();
                foreach (Order order in Orders.Values)
                {
                    if (order.ContainsExpression(SearchTextBox.Text))
                    {
                        OrderListBox.Items.Add(order.OrderNumber);
                    }
                }
            }
        }

        private void ClearShowBoxs()
        {
            ShowJobNumberTextBox.Text = ShowLinkTextBox.Text = ShowTrackingLinkTextBox.Text = ShowExtraInfoTextBox.Text = ShowStoreNameTextBox.Text = "";
        }

        private void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AddButton_Click(sender,e);
            }
        }

        private void EnterPressedSearchBox(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SearchButton_Click(sender, e);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Orders.Remove(GetOrderFromOrderListBox().OrderNumber);
            ClearShowBoxs();
            ShowAllOrdersInOrderListBox();
            
        }


        string path = @"..\..\rans_program.xml";
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Orders.Count > 0)
            {
                XmlManager xmlConnection = new XmlManager(path);
                xmlConnection.Write(Orders);
            }
            else
            {
                File.Delete(path);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(path))
            {
                XmlManager xmlConnection = new XmlManager(path);
                xmlConnection.Read(Orders);
                ShowAllOrdersInOrderListBox();
            }
        }
    }

}
