﻿using System;
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
        /// List of items
        /// </summary>
        List<Item> Items = new List<Item>();


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
        /// Bool var that tracks if the user has chosen to edit 
        /// an invoice
        /// </summary>
        private bool isOkayToEdit = false;

        public static clsSearch MainWindowInvoice { get; set; }

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

            // new search object
            CurrentSearch = new wndSearch();

            // new items object
            CurrentItems = new wndItems();

            // Removing blank space in main dg
            MainDataGrid.CanUserAddRows = false;

            // Removing blank space in main invoice dg
            mainInvDG.CanUserAddRows = false;

            // Locking the edit region until the user chooses to edit an invoice
            lockEditRegion();
            


            MainWindowInvoice = new clsSearch();

            // Populating the item lists in the drop downs
            popItemLists();

        }

        string InvoiceNum;
        #endregion

        #region Properties

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

                wndSearch CurrentSearch = new wndSearch();
                
                CurrentSearch.ShowDialog();

                // collect invoice number 
                //InvoiceNum = CurrentSearch.clsSL.getInvoiceNum();
         
                // check to see if the invoice number has been set
                if(CurrentSearch.clsSL.invNumSet)
                {
                    // collect invoice number 
                    InvoiceNum = CurrentSearch.clsSL.getInvoiceNum();

                    // add invoice to invoice text box 
                    InvoiceNumberLbl.Content = MainWindowInvoice.InvoiceNum;

                    // Populating the main inv num on main
                    mainInvDG.ItemsSource = clsSL.getInvoice(InvoiceNum);

                    // Unlocking the edit region
                    lockEditRegion();

                    // enable edit invoice button
                    EditInvoiceBtn.IsEnabled = true;
                    // enable edit invoice button
                    DltInvoiceBtn.IsEnabled = true;

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
                // Setting the edit button bool tracker to true
                isOkayToEdit = true;

                // Unlocking the edit region for the user
                unlockEditRegion();

                // Populating the edit invoice item list
                popItemLists();

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
                
                // collect invoice number 
                InvoiceNum = CurrentSearch.clsSL.getInvoiceNum();

                // deletes current invoice
                clsML.DeleteInvoice(InvoiceNum);

                // clear data grid
                MainDataGrid.ItemsSource = null;

                // clear invoice number Label 
                InvoiceNumberLbl.Content = "";



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

                // populate new invoice "TBD" number
                //InvoiceNumberTxtBx.Text = 
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Click event for search for invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchForInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentSearch = new wndSearch();

                // hide main menu
                this.Hide();

                // show search menu
                CurrentSearch.ShowDialog();

                // show this menu
                this.Show();

                // collect invoice number 
                //InvoiceNum = CurrentSearch.clsSL.getInvoiceNum();


                // check to see if search window has been visited
                if (MainWindowInvoice.InvoiceNum != null)
                {

                    // add invoice to invoice text box 
                    InvoiceNumberLbl.Content = MainWindowInvoice.InvoiceNum;


                    // calls items for particular invoice into data grid
                    dataGridList = clsML.PopulateLineItemsOnInvoiceNum(MainWindowInvoice.InvoiceNum);
                    MainDataGrid.ItemsSource = dataGridList;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void AddItemBtn(object sender, RoutedEventArgs e)
        {

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
                // adds an items to the current invoice
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
                // adds an items to the current invoice
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
                // removes items from current invoice
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Click event for save invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // save current invoice
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Item selected from combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                  
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
                // enable edit invoice button
                EditInvoiceBtn.IsEnabled = true;

                // enable delete invoice button   
                DltInvoiceBtn.IsEnabled = true;

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
        /// This helper method disables buttons / cb's when the user is restricted access
        /// </summary>
        private void disableItems()
        {
            try
            {
                // enable edit invoice button
                EditInvoiceBtn.IsEnabled = false;

                // enable delete invoice button   
                DltInvoiceBtn.IsEnabled = false;

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
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        private void RemoveItemFromNewBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// This helper method locks the edit region
        /// </summary>
        public void lockEditRegion()
        {
            try
            {
                editItemsCB.Text = "";
                InvoiceNumberLbl.Content = "";
                editItemsCB.IsEnabled = false;
                AddItemToCurrentBtn.IsEnabled = false;
                RemoveItemBtn.IsEnabled = false;

                disableItems();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This helper method unlocks the edit region
        /// </summary>
        public void unlockEditRegion()
        {
            try
            {
                InvoiceNumberLbl.Content = InvoiceNum;
                editItemsCB.IsEnabled = true;
                AddItemToCurrentBtn.IsEnabled = true;
                RemoveItemBtn.IsEnabled = true;

                // enable buttons & text boxes
                enableItems();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

            // calls items for particular invoice into data grid
            //dataGridList = clsML.PopulateLineItemsOnInvoiceNum(MainWindowInvoice.InvoiceNum);
            //MainDataGrid.ItemsSource = dataGridList;
        }

        /// <summary>
        /// This method populates the items drop downs
        /// </summary>
        private void popItemLists()
        {
            try
            {

                Items = clsIL.getItems();

                // loop through and populate items in combo box
                foreach (var item in Items)
                {

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
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}


