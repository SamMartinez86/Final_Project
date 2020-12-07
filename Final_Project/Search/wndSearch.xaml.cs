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
        public clsSearchLogic clsSL;

        /// <summary>
        /// List constructor
        /// </summary>
        List<clsSearch> Invoices = new List<clsSearch>();

        

        public wndSearch()
        {
            InitializeComponent();

            // New instance of Search Logic class
            clsSL = new clsSearchLogic();

            // Getting all invoice records from the Db
            Invoices = clsSL.GetAllInvoices();

            foreach(var invoice in Invoices)
            {
                // need to display all invoices in each drop down
                InvoiceCB.Items.Add(invoice.InvoiceNum);
                TotalChargesCB.Items.Add(invoice.InvoiceCost);
                DateCB.Items.Add(invoice.InvoiceDate);
              
            }

            // Populating the data grid
            srchDataGrid.CanUserAddRows = false;
            srchDataGrid.IsReadOnly = true;
            srchDataGrid.ItemsSource = clsSL.GetAllInvoices();


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

        /// <summary>
        /// This var shows if the user has made any invoice selections
        /// </summary>
        bool selectionMade = false;

        /// <summary>
        /// This specifies which date the user picks
        /// </summary>
        private string dateChosen;

        /// <summary>
        /// Determines whether the user wants to reset the board
        /// </summary>
        bool resetSelected = false;

        /// <summary>
        /// This will store the invoice number the the user mouses over on the
        /// search window
        /// </summary>
        private string selectedInv;



        #endregion var init


        #region methods



        /// <summary>
        /// This Selects the specified invoice and redirects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clsSearch selection = (clsSearch)srchDataGrid.SelectedItem;

                if (selection == null)
                {
                    MessageBox.Show("Please select an invoice", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    setVars();


                    // take items from data grid and cast to object to move to main
                    clsSearch Invoice = (clsSearch)srchDataGrid.SelectedItem;

                    // set equal to main window object
                    MainWindow.MainWindowInvoice = Invoice;


                    this.Close();
                }
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
        public void ResetBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                resetSelected = true;

                updateDG();

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
                InvoiceNumChosen = true;
                updateDG();          
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
                TotalChargesChosen = true;
                updateDG();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method is called to each time the user determines search 
        /// criteria and updates the data grid
        /// </summary>
        private void updateDG()
        {
            try
            {
                // The user has specified at least one constraint for their search
                selectionMade = true;

                if (resetSelected)
                {
                    // Resetting the data grid back to initial state
                    srchDataGrid.ItemsSource = clsSL.GetAllInvoices();

                    InvoiceCB.SelectedIndex = -1;
                    TotalChargesCB.SelectedIndex = -1;
                    DateCB.SelectedIndex = -1;

                    // Resetting all bool values to false
                    InvoiceNumChosen = false;
                    TotalChargesChosen = false;
                    InvoiceDateChosen = false;
                    selectionMade = false;

                    // resetting the reset bool
                    resetSelected = false;
                }

                // this checks each booleav var to determine with SQL statement should be executed
                if (InvoiceNumChosen && !TotalChargesChosen && !InvoiceDateChosen)
                {
                    srchDataGrid.ItemsSource = clsSL.getInvoice(InvoiceCB.SelectedItem as string);
                }
                else if(!InvoiceNumChosen && TotalChargesChosen && !InvoiceDateChosen)
                {
                    srchDataGrid.ItemsSource = clsSL.getTotalCharges(TotalChargesCB.SelectedItem as string);
                }
                else if(!InvoiceNumChosen && !TotalChargesChosen && InvoiceDateChosen)
                {
                    srchDataGrid.ItemsSource = clsSL.getInvoiceDate(dateChosen);
                }
                else if(InvoiceNumChosen && !TotalChargesChosen && InvoiceDateChosen)
                {
                    srchDataGrid.ItemsSource = clsSL.getInvoiceNumDate(InvoiceCB.SelectedItem as string, dateChosen);
                }
                else if(InvoiceNumChosen && TotalChargesChosen && InvoiceDateChosen)
                {
                    srchDataGrid.ItemsSource = clsSL.getAllData(InvoiceCB.SelectedItem as string, TotalChargesCB.SelectedItem as string, dateChosen);
                }
                else if(!InvoiceNumChosen && TotalChargesChosen && InvoiceDateChosen)
                {
                    srchDataGrid.ItemsSource = clsSL.getCostDate(TotalChargesCB.SelectedItem as string, dateChosen);
                }
                else if(InvoiceNumChosen && TotalChargesChosen && !InvoiceDateChosen)
                {
                    srchDataGrid.ItemsSource = clsSL.getNumCost(InvoiceCB.SelectedItem as string, TotalChargesCB.SelectedItem as string);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method will set the variable that will be used on the main window
        /// </summary>
        private void setVars()
        {
            try
            {
                srchDataGrid.SelectedIndex = 0;
    
                clsSL.setInvoiceNumber(((clsSearch)srchDataGrid.SelectedItem).InvoiceNum);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        #endregion methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(DateCB.SelectedIndex != -1)
                {
                    InvoiceDateChosen = true;

                    // Splitting the string apart to only get the date and not the time 
                    dateChosen = DateCB.SelectedItem.ToString().Split(' ')[0];

                    updateDG();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method handles click event of the user choosing an invoice from 
        /// the data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setVars(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //selectedInv = (sender as DataGrid);

                clsSL.setInvoiceNumber(((clsSearch)srchDataGrid.SelectedItem).InvoiceNum);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
    }// end class
}
