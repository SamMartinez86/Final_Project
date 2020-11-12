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
        public wndSearch()
        {
            InitializeComponent();
        }

        #region var init

        /// <summary>
        /// This boolean var show if the user
        /// selected the InvoiceNumber as search criteria
        /// </summary>
        bool InvoiceNumChosen;

        /// <summary>
        /// This var shows if the user selected 
        /// the total charges as search criteria
        /// </summary>
        bool TotalChargesChosen;

        /// <summary>
        /// This var shows if the user selected
        /// the Invoice Date as search criteria
        /// </summary>
        bool InvoiceDateChosen;



        #endregion var init

        /// <summary>
        /// This Selects the specified invoice and redirects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            /* From here, we will use the combo box boolean variables to decide
             * which SQL statement to choose. For example, if there are non chosen, 
             * we will use the SelectAllInvoices method within the clsSearchSQL
             * If the Invoice number CB has been chosen we will call to the 
             * SelectInvoiceData method in clsSearchSQL (and pass the invoice number argument
             * and set the SQL statement to return all values with that Invoice Number. If all 3 variables are 
             * true, we will call / pass the correct method and argument to clsSearchSQL
             * 
             * The correct SQL return objects will be stored in the clsSearchLogic variables
             * From the clsSearchLogic, we will store the values where they can then be accessed any other class that needs it
             * 
             * I plan on obtaining exactly what the user chose by using code like:
             * 
             */

            try
            {
                this.Close();
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
            /* We will reset this screen so the user can search for different criteria
             */
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
    }
}
