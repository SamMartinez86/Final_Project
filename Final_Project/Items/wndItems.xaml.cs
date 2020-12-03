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

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region Class Members
        /// <summary>
        /// This is a boolean to tell the Main 
        /// Page whether or not it needs to refresh.
        /// </summary>
        public bool changesMade;
        public clsItemsLogic product = new clsItemsLogic();
        /// <summary>
        /// This will hold a list of items passed in
        /// from the database
        /// </summary>
        public List<DataGrid> items;
        #endregion

        public wndItems()
        {
            InitializeComponent();
            ItemDataGrid.ItemsSource = product.getItems();
        }

        /// <summary>
        /// Button to Add an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string code = codeTxtBox.Text;
                string desc = descTxtBox.Text;
                string cost = costTxtBox.Text;
                product.addItem(code, desc, cost);

                ItemDataGrid.ItemsSource = null;           
                ItemDataGrid.ItemsSource = product.getItems();
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Button to delete an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            ///When an item in the items list is selected and this button
            ///is clicked it will delete that item from the list
        }
    }
}
