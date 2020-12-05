using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Data;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace Final_Project
{
    public class clsMainLogic
    {

        /// <summary>
        /// main SQL Object
        /// </summary>
        clsMainSQL SQLMain;

        /// <summary>
        /// data access class
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// SQL statement 
        /// </summary>
        public string sSQL;

        // list from search logic
        /////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Search window object
        /// </summary>
        wndSearch CurrentSearch;

        /// <summary>
        /// Search Logic object
        /// </summary>
        clsSearchLogic clsSL;

        /// <summary>
        /// This will store the selected invoice number
        /// </summary>
        public string selectedInvoiceNumber;
        /////////////////////////////////////////////////////////////////////////////////////////


        public clsMainLogic()
        {
            SQLMain = new clsMainSQL();

            db = new clsDataAccess();
        }


        /// <summary>
        /// This method will return the selected invoice number to the main window
        /// </summary>
        /// <returns></returns>
        public string getInvoiceNum()
        {
            try
            {
                return selectedInvoiceNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method deletes invoices
        /// </summary>
        public void DeleteInvoice(string invoicenum)
        {
            try
            {
                
                // SQL statement to delete invoice
                sSQL = SQLMain.DeleteInvoices(invoicenum);

                // Passing the sSQL string to be executed
                db.ExecuteNonQuery(sSQL);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method inserts new invoice into database
        /// </summary>
        /// <param name="sInvoiceDate"></param>
        /// <param name="sTotalCost"></param>
        public void NewInvoice(string sInvoiceDate, string sTotalCost)
        {
            try
            {
                // insert new item into database
                sSQL = SQLMain.InsertInvoices(sInvoiceDate, sTotalCost);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        internal ObservableCollection<Item> PopulateLineItemsOnInvoiceNum(string invoiceNum)
        {
            ObservableCollection<Item> list = new ObservableCollection<Item>();
            int iRet = 0;
            DataSet ds = new DataSet();

            ds = db.ExecuteSQLStatement(SQLMain.SelectLineItemDesc(invoiceNum), ref iRet);


            for (int i = 0; i < iRet; i++)
            {
                list.Add(new Item
                {
                    itemCode = ds.Tables[0].Rows[i][0].ToString(),
                    itemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString(),
                    itemCost = ds.Tables[0].Rows[i]["Cost"].ToString()
                });
            }
            return list;


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

    }
}
