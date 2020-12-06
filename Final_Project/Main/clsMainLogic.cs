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
using System.Runtime.CompilerServices;

namespace Final_Project
{
    public class clsMainLogic
    {

        /// <summary>
        /// main SQL Object
        /// </summary>
        clsMainSQL SQLMain;
        /// <summary>
        /// Item SQL object
        /// </summary>
        clsItemsSQL SQLItems;
        /// <summary>
        /// Search SQL object
        /// </summary>
        clsSearchSQL SQLSearch;
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
        List<Item> items;



        public clsMainLogic()
        {
            SQLMain = new clsMainSQL();
            SQLItems = new clsItemsSQL();
            items = new List<Item>();

            db = new clsDataAccess();
        }


        /// <summary>
        /// This method will return the selected invoice number to the main window
        /// </summary>
        /// <returns></returns>
        public string getInvoiceNum(bool isNewInvoice = false)
        {
            try
            {
                if(!isNewInvoice)
                    return selectedInvoiceNumber;
                DataSet ds;
                int iRet = 0;                

                ds = db.ExecuteSQLStatement(SQLMain.SelectInvoiceNumber(), ref iRet);

                return ds.Tables[0].Rows[0].ItemArray[0].ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method deletes invoices
        /// </summary>
        public void DeleteInvoice(string invoiceNum)
        {
            try
            {
                
                // SQL statement to delete invoice
                sSQL = SQLMain.DeleteInvoices(invoiceNum);

                // Passing the sSQL string to be executed
                db.ExecuteNonQuery(sSQL);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method will delete all items from an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void deleteLineItem(string invoiceNum)
        {
            try
            {
                string sSQL = SQLMain.DeleteLineItems(invoiceNum);
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
        /// Method returns the item cost to 
        /// the UI
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string getItemCost(string desc)
        {
            int iRet = 0;
            DataSet ds = new DataSet();

            ds = db.ExecuteSQLStatement(SQLItems.SelectDistinctItemCostByDesc(desc), ref iRet);

            return ds.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        public string getItemCode(string desc, string cost)
        {
            int iRet = 0;
            DataSet ds = new DataSet();

            ds = db.ExecuteSQLStatement(SQLItems.SelectDistinctItemCodeByDescCost(desc, cost), ref iRet);

            return ds.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        /// <summary>
        /// Method removes an item from 
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        public void deleteItem(string desc, string cost)
        {
            string itemCode = SQLItems.SelectDistinctItemCodeByDescCost(desc, cost);
            string query = SQLItems.DeleteItem(itemCode);
            db.ExecuteNonQuery(query);
        }

        /// <summary>
        /// Method will insert several line items to a single invoice.
        /// </summary>
        /// <param name="itemCodes"></param>
        /// <param name="invoiceNum"></param>
        public void insertLineItems(List<string> itemCodes, string invoiceNum)
        {
            int lineNum = 1;
            foreach(var item in itemCodes)
            {
                db.ExecuteNonQuery(SQLMain.InsertLineItems(invoiceNum, lineNum.ToString(), item));
                lineNum++;
            }
        }
        /// <summary>
        /// This method will return the max line item number from 
        /// a specified invoice number
        /// </summary>
        /// <param name="invNum"></param>
        /// <returns></returns>
        public string getMaxLineItem(string invNum)
        {
            try
            {
                sSQL = SQLMain.selectMaxLineItem(invNum);

                string maxLine = db.ExecuteScalarSQL(sSQL);

                return maxLine;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method will insert a new line item into the and existing invoice
        /// </summary>
        /// <param name="invNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public void updateExistingInv(string invNum, string lineItemNum, string itemCode)
        {
            try
            {
                sSQL = SQLMain.InsertLineItems(invNum, lineItemNum, itemCode);

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method will remove an item from a specified invoice
        /// </summary>
        /// <param name="invNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public void removeItemFromInv(string invNum, string lineItemNum)
        {
            try
            {
              
                sSQL = SQLMain.DeleteLineItem(invNum, lineItemNum);

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// gets the line item numer
        /// </summary>
        /// <param name="invNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string getLineItemNum(string invNum, string code)
        {
            try
            {
                DataSet ds = new DataSet();

                int iRet = 0;

                sSQL = SQLMain.getLineItemNum(invNum, code);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                return ds.Tables[0].Rows[0].ItemArray[0].ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        //public List<Item> getItems(string code)
        //{

        //}

        /// <summary>
        /// Method adds a new invoice into the database
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="date"></param>
        public void createInvoice(string totalCost, string date)
        {
            db.ExecuteNonQuery(SQLMain.InsertInvoices(date, totalCost));
        }
        
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
