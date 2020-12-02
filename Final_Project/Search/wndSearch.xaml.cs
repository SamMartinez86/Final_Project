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
using System.Windows.Shapes;
using System.Reflection;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// Search Logic constructor
        /// </summary>
        clsSearchLogic clsSL;
        List<clsSearch> Invoices = new List<clsSearch>();
        public wndSearch()
        {
            InitializeComponent();

            // New instance of Search Logic class
            clsSL = new clsSearchLogic();

            Invoices = clsSL.GetInvoices();

            foreach(var invoice in Invoices)
            {
                // need to display all invoices in each drop down
                InvoiceCB.Items.Add(invoice.InvoiceNum);
                TotalChargesCB.Items.Add(invoice.InvoiceCost);
              
            }

            srchDataGrid.ItemsSource = clsSL.GetInvoices();

            TotalChargesCB.Items.Add(clsSL.GetInvoices());

        }

        #region var init

        /// <summary>
        /// This boolean var show if the user
        /// selected the InvoiceNumber as search criteria
        /// </summary>
        bool InvoiceNumChosen = false;

        /// <summary>
        /// This var shows if the user selected 
        /// the total charges as search criteria
        /// </summary>
        bool TotalChargesChosen = false;

        /// <summary>
        /// This var shows if the user selected
        /// the Invoice Date as search criteria
        /// </summary>
        bool InvoiceDateChosen = false;

        #endregion var init

        /// <summary>
        /// This Selects the specified invoice and redirects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // sends all selected db info back to main page
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// This resets the DataGrid holding the information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                InvoiceCB.SelectedIndex = -1;
                TotalChargesCB.SelectedIndex = -1;
                srchDataGrid.ItemsSource = clsSL.GetInvoices();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method handles when the user selects a different invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method handles the when a user changes the total charges box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalChargesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
