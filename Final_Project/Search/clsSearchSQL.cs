using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Final_Project
{
    class clsSearchSQL
    {
        /// <summary>
        /// This SQL gets all data from the Invoice table
        /// </summary>
        /// <returns>All data</returns>
        public string SelectAllInvoices()
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }         
        }

        /// <summary>
        /// This SQL gets all data on an invoice for a given Invoice ID
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data</param>
        /// <returns>All Data from the given invoice</returns>
        public string SelectInvoiceData(string sInvoiceID)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL gets all of the data on an invoice for the given InvoiceId
        /// and the given InvoiceDate
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data</param>
        /// <param name="sInvoiceDate">The InvoiceDate for the invoice to retrieve all data</param>
        /// <returns></returns>
        public string SelectInvoiceDataDate(string sInvoiceID, string sInvoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum =" + sInvoiceID + " AND InvoiceDate = #" + sInvoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This invoice gets all of the data for the specified InvoiceNumber, InvoiceDate
        /// and InvoiceCost
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <param name="sInvoiceDate"></param>
        /// <param name="InvoiceCost">The InvoiceCost for the invoice to retrieve all data</param>
        /// <returns></returns>
        public string SelectInvoiceDataDateCost(string sInvoiceID, string sInvoiceDate, string InvoiceCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum =" + sInvoiceID + " AND InvoiceDate = #" + sInvoiceDate + "#" + "AND TotalCost = " + InvoiceCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL Returns All data from Invoices where the cost
        /// matches the specified InvoiceCost
        /// </summary>
        /// <param name="sInvoiceCost">InvoiceCost to retrieve the data</param>
        /// <returns>All data for the given invoice</returns>
        public string SelectInvoiceCost(string sInvoiceCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + sInvoiceCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL returns all data for a specified cost 
        /// and a specified date
        /// </summary>
        /// <param name="sInvoiceCost">Specified Cost</param>
        /// <param name="sInvoiceDate">Specified Date</param>
        /// <returns>All data for the given invoice</returns>
        public string SelectInvoiceCostDate(string sInvoiceCost, string sInvoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + sInvoiceCost + " AND InvoiceDate = #" + sInvoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL returns allinvoice data for a specified date
        /// </summary>
        /// <param name="sInvoiceDate"></param>
        /// <returns>All data for the given invoice</returns>
        public string SelectInvoiceDate(string sInvoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + sInvoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

    }// end clsSearchSQL
}

