﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace Final_Project
{   
    /// <summary>
    /// This sets all of the SQL query strings that will be used to
    /// gather information and data from the database
    /// </summary>
    class clsMainSQL {



        /// <summary>
        /// This SQL updates the data for a given Invoice number
        /// </summary>
        /// <param name="sTotalCost"></param>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string UpdateInvoices(string sTotalCost, string sInvoiceNum)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET TotalCost = " + sTotalCost + " WHERE " + sInvoiceNum + " = 123";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL deletes data with reference to the specified
        /// invoice number
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string DeleteLineItems(string sInvoiceNum)
        {
            try
            {
                string sSQL = "DELETE From LineItems WHERE InvoiceNum = " + sInvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL Deletes the invoice data with reference to
        /// the given invoice number
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string DeleteInvoices(string sInvoiceNum)
        {
            try
            {
                string sSQL = "DELETE From Invoices WHERE InvoiceNum = " + sInvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL Inserts line item data with 3 arguments
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <param name="sval3"></param>
        /// <returns></returns>
        public string InsertLineItems(string sInvoiceNum, string sLineItemNum , string sItemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values ("+ sInvoiceNum + "," + sLineItemNum + "," + sItemCode + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL inserts data into the invoices table with the invoice date
        /// and the invoice total cost as reference
        /// </summary>
        /// <param name="sInvoiceDate"></param>
        /// <param name="sTotalCost"></param>
        /// <returns></returns>
        public string InsertInvoices(string sInvoiceDate, string sTotalCost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) Values ('#" + sInvoiceDate + "#', " + sTotalCost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL returns all data for a specified invoice number
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string SelectInvoiceNumDateCost(string sInvoiceNum)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + sInvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL returns the Item code, description and cost 
        /// from the ItemDesc table
        /// </summary>
        /// <returns></returns>
        public string SelectItemCodeDescCost()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL returns data that match on the item code
        /// that use the invoice number as reference
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string SelectLineItemDesc(string sInvoiceNum)
        {
            try
            {
                string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                              "FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode " +
                              "And LineItems.InvoiceNum = " + sInvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
    }
}
