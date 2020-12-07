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
using System.Reflection;
using System.Collections.ObjectModel;

namespace Final_Project
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Attributes

        /// <summary>
        /// Search window object
        /// </summary>
        wndSearch CurrentSearch;

        /// <summary>
        /// Items window object
        /// </summary>
        wndItems CurrentItems;

        /// <summary>
        /// Search Logic object
        /// </summary>
        clsSearchLogic clsSL;

        /// <summary>
        /// Items logic object
        /// </summary>
        clsItemsLogic clsIL;

        /// <summary>
        /// Items passed from the database
        /// </summary>
        public clsItemsLogic product = new clsItemsLogic();

        /// <summary>
        /// main logic object
        /// </summary>
        clsMainLogic clsML;

        /// <summary>
        /// search class object
        /// </summary>
        clsSearch clsSearch;

        /// <summary>
        /// item class object
        /// </summary>
        Item clsItem;

        /// <summary>
        /// List of items
        /// </summary>
        List<Item> Items = new List<Item>();

        /// <summary>
        /// list of invoice items
        /// </summary>
        List<Item> newInvoiceItems;

        /// <summary>
        /// List constructor
        /// </summary>
        List<clsSearch> Invoices = new List<clsSearch>();

        /// <summary>
        /// store user created invoice number
        /// </summary>
        public string InvcNum;

        /// <summary>
        /// store user created invoice date
        /// </summary>
        public string InvcDt;

        /// <summary>
        /// Boolean variable that tracks if the user has chosen to edit 
        /// an invoice
        /// </summary>
        private bool isOkayToEdit = false;

        /// <summary>
        /// static property for invoice & items
        /// </summary>
        public static clsSearch MainWindowInvoice { get; set; }

        /// <summary>
        /// observable collection object for data grid
        /// </summary>
        public static ObservableCollection<Item> dataGridList;

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            // New search object
            clsSL = new clsSearchLogic();

            // New Items object
            clsIL = new clsItemsLogic();

            // New Main object
            clsML = new clsMainLogic();

            // New search class object
            clsSearch = new clsSearch();

            // new item class object
            clsItem = new Item();

            // new search object
            CurrentSearch = new wndSearch();

            // new items object
            CurrentItems = new wndItems();

            // Removing blank space in main data grid
            MainDataGrid.CanUserAddRows = false;

            // Removing blank space in main invoice data grid
            mainInvDG.CanUserAddRows = false;
            mainInvDG.IsReadOnly = true;

            // Locking the edit region until the user chooses to edit an invoice
            lockEditRegion();

            // create invoice pane enable
            createInvCV.IsEnabled = true;

            // create main window invoice object
            MainWindowInvoice = new clsSearch();

            // Populating the item lists in the drop downs
            popItemLists();

            // new invoice list object
            newInvoiceItems = new List<Item>();

        }

        // string variable to hold passed invoice number
        string InvoiceNum;
        #endregion

        #region Methods

        /// <summary>
        /// Click event for file menu update item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // update current invoice like the save feature.
                wndItems CurrentItems = new wndItems();

                CurrentItems.ShowDialog();

            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Click event for file menu Search item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // new search window object
                wndSearch CurrentSearch = new wndSearch();

                CurrentSearch.ShowDialog();

                // check to see if the invoice number has been set
                if (CurrentSearch.clsSL.invNumSet)
                {
                    // collect invoice number 
                    InvoiceNum = CurrentSearch.clsSL.getInvoiceNum();

                    // add invoice to invoice text box 
                    InvoiceNumberLbl.Content = MainWindowInvoice.InvoiceNum;

                    // Populating the main invoice number on main
                    mainInvDG.ItemsSource = clsSL.getInvoice(InvoiceNum);

                    // Unlocking the edit region
                    lockEditRegion();

                    // enable edit invoice button
                    EditInvoiceBtn.IsEnabled = true;

                    // enable edit invoice button
                    DltInvoiceBtn.IsEnabled = true;

                    // Enable current invoice canvas
                    currentInvoiceCV.IsEnabled = true;

                }


            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        /// <summary>
        /// Click event for file menu exit item (exit program)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // exit menu screen
                Close();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Click event for edit invoice Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Setting the edit button boolean tracker to true
                isOkayToEdit = true;

                // Unlocking the edit region for the user
                unlockEditRegion();

                // Populating the edit invoice item list
                popItemLists();

                // Disabling all but the edit table
                if (createInvCV.IsEnabled == true)
                {
                    createInvCV.IsEnabled = false;
                    EditInvoiceBtn.Content = "Stop Editing";
                    cvEditInvoice.IsEnabled = true;
                }
                else
                {
                    createInvCV.IsEnabled = true;
                    cvEditInvoice.IsEnabled = false;
                    EditInvoiceBtn.Content = "Edit Invoice";
                }
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Click event for delete invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DltInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // instantiate string for invoice number
                string invoiceNum;

                // list of invoices object
                List<clsSearch> invoices = (List<clsSearch>)mainInvDG.ItemsSource;
                clsSearch invoice = invoices.FirstOrDefault();

                // store invoice number from invoice
                invoiceNum = invoice.InvoiceNum;

                // deletes current invoice and line item
                clsML.deleteLineItem(invoiceNum);
                clsML.DeleteInvoice(invoiceNum);

                // clear data grid
                MainDataGrid.ItemsSource = null;

                // clear invoice number Label 
                InvoiceNumberLbl.Content = "";

                // clear data grids
                mainInvDG.ItemsSource = null;
                MainDataGrid.ItemsSource = null;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Click event for new invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // creates new invoice 

                // get new invoice number
                InvcNum = (string)InvoiceNumberLbl.Content;

                // insert new invoice with info from text boxes
                clsML.NewInvoice(InvcNum, InvcDt);

                // enable buttons & text boxes
                enableItems();

            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// click event for add item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemToCurrentBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // select item
                // query cost
                // query item code using item cost / description
                // insert into items

                if (editItemsCB.SelectedIndex != -1)
                {
                    ///This stores the item that will be added to an existing invoice
                    string itemToAddDesc = editItemsCB.SelectedItem.ToString();

                    ///This stores the cost of the item to be added to an existing invoice
                    string addedItemCost = clsML.getItemCost(itemToAddDesc);

                    /// This stores the item code of the item to be added to an existing invoice
                    string addedItemCode = clsML.getItemCode(itemToAddDesc, addedItemCost);

                    /// This stores the max line item count for the given invoice number
                    string maxLineItem = clsML.getMaxLineItem(InvoiceNum);

                    // Saving the max line item for specified invoice number
                    int i = Convert.ToInt32(maxLineItem);

                    // adding one to the max
                    string newMaxLinNum = (i + 1).ToString();

                    // call main logic update existing invoice method
                    clsML.updateExistingInv(InvoiceNum, newMaxLinNum, addedItemCode);
                }

                // fill data grid variable with line items
                dataGridList = clsML.PopulateLineItemsOnInvoiceNum(MainWindowInvoice.InvoiceNum);

                // fill data grid with contents of variable
                MainDataGrid.ItemsSource = dataGridList;

            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// click event for add item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemToNewBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // initialize string variables for items
                string selectedItem = itemsCb.SelectedItem.ToString();
                string cost = clsML.getItemCost(selectedItem);
                string itemCode = clsML.getItemCode(selectedItem, cost);

                // add items
                newInvoiceItems.Add(new Item { itemCode = itemCode, itemCost = cost, itemDesc = selectedItem });

                // clear data grid 
                NewInvoiceDataGrid.ItemsSource = null;

                // fill data grid with items
                NewInvoiceDataGrid.ItemsSource = newInvoiceItems;

            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method removes a selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveItemFromNewBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // fill item object with selected items
                Item selectedItem = (Item)NewInvoiceDataGrid.SelectedItem;

                // initialize variables with selected items
                string desc = selectedItem.itemDesc;
                string cost = selectedItem.itemCost;

                // find items to delete
                Item itemToDelete = newInvoiceItems.Where(a => a.itemDesc == desc && a.itemCost == cost).FirstOrDefault();

                // remove items
                newInvoiceItems.Remove(itemToDelete);

                // clear data grid
                NewInvoiceDataGrid.ItemsSource = null;

                // fill data grid with new invoice items
                NewInvoiceDataGrid.ItemsSource = newInvoiceItems;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Click event for remove item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // getting the item description from the selected index
                Item selectedIndex = ((Item)MainDataGrid.SelectedItem);

                // get item cost from selected index
                Item selectedCost = ((Item)MainDataGrid.SelectedItem);

                // string variables for items class  
                string cost = selectedCost.itemCost;
                string desc = selectedIndex.itemDesc;
                string code = selectedIndex.itemCode;

                // call main logic get line items number method
                string lineItemNum = clsML.getLineItemNum(InvoiceNum, code);

                // main logic remove line items from invoice method
                clsML.removeItemFromInv(InvoiceNum, lineItemNum);

                // fill data grid variable with line items
                dataGridList = clsML.PopulateLineItemsOnInvoiceNum(MainWindowInvoice.InvoiceNum);

                // fill data grid with contents of variable
                MainDataGrid.ItemsSource = dataGridList;
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void SaveNewInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // variable to store total cost
                int totalCost = 0;

                // loop through and add costs
                foreach (Item item in NewInvoiceDataGrid.ItemsSource)
                {
                    int cost = Convert.ToInt32(item.itemCost);
                    totalCost += cost;
                }

                // change selected invoice data to string
                string date = InvoiceDate.SelectedDate.ToString();
                string invoiceNum;

                // get invoice and store in variable
                invoiceNum = clsML.getInvoiceNum();

                // list for item codes
                List<string> itemCodes = new List<string>();

                // loop through and add item codes
                foreach (Item item in NewInvoiceDataGrid.ItemsSource)
                {
                    var cost = item.itemCost;
                    var desc = item.itemDesc;
                    string itemCode = clsML.getItemCode(desc, cost);

                    itemCodes.Add(itemCode);
                }

                // call main logic insert line items method
                clsML.insertLineItems(itemCodes, invoiceNum);
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void CancelNewInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Cancel new invoice creation
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This method enables all buttons after returning from search invoice or creating a new invoice
        /// </summary>
        private void enableItems()
        {
            try
            {
                // enable add item button
                AddItemToCurrentBtn.IsEnabled = true;

                // enable remove button
                RemoveItemBtn.IsEnabled = true;

            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This helper method disables buttons / combo boxes when the user is restricted access
        /// </summary>
        private void disableItems()
        {
            try
            {
                // enable edit invoice button
                EditInvoiceBtn.IsEnabled = false;

                // enable add item button
                AddItemToCurrentBtn.IsEnabled = false;

                // enable remove button
                RemoveItemBtn.IsEnabled = false;

            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// this method handles errors 
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + "->" + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// this method handles the window closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        

        /// <summary>
        /// This helper method locks the edit region
        /// </summary>
        public void lockEditRegion()
        {
            try
            {
                // enable buttons and clear text
                editItemsCB.Text = "";
                InvoiceNumberLbl.Content = "";
                editItemsCB.IsEnabled = false;
                AddItemToCurrentBtn.IsEnabled = false;
                RemoveItemBtn.IsEnabled = false;

                // clear data grid
                MainDataGrid.ItemsSource = null;

                disableItems();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// This helper method unlocks the edit region
        /// </summary>
        public void unlockEditRegion()
        {
            try
            {
                // enable various buttons & fill invoice number
                InvoiceNumberLbl.Content = InvoiceNum;
                editItemsCB.IsEnabled = true;
                AddItemToCurrentBtn.IsEnabled = true;
                RemoveItemBtn.IsEnabled = true;

                // enable buttons & text boxes
                enableItems();

                // calls items for particular invoice into data grid
                dataGridList = clsML.PopulateLineItemsOnInvoiceNum(MainWindowInvoice.InvoiceNum);
                MainDataGrid.ItemsSource = dataGridList;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }


        }

        /// <summary>
        /// This method populates the items drop downs
        /// </summary>
        private void popItemLists()
        {
            try
            {
                // items logic get items method
                Items = clsIL.getItems();

                // loop through and populate items in combo box
                foreach (var item in Items)
                {
                    // add item description
                    itemsCb.Items.Add(item.itemDesc);

                    // If the user has selected
                    if (isOkayToEdit)
                    {
                        editItemsCB.Items.Add(item.itemDesc);
                    }

                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        
        #endregion

    }
}


