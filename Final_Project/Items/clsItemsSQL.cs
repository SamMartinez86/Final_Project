using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Final_Project
{
    /// <summary>
    /// This class sets up all of the SQL statements
    /// needed for the item class / window
    /// </summary>
    /// 
    class clsItemsSQL
    {
        /// <summary>
        /// This SQL returns data information from the Item Description
        /// table
        /// </summary>
        /// <returns></returns>
        public string SelectItemCodeDescCost()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc ORDER BY ItemCode";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        public string SelectDistinctItemCodeDescCost(string code)
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc WHERE ItemCode = " + "'" + code + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This is a SQL Query to get the Item cost based on the item
        /// description
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <returns></returns>
        public string SelectDistinctItemCostByDesc(string itemDesc)
        {
            try
            {
                string sSQL = "SELECT Cost FROM ItemDesc WHERE ItemDesc = " + "'" + itemDesc + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL returns a distinct Item Code
        /// </summary>
        /// <param name="itemDesc"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        public string SelectDistinctItemCodeByDescCost(string itemDesc, string cost)
        {
            try
            {
                string sSQL = "SELECT ItemCode FROM ItemDesc WHERE ItemDesc = " + "'" + itemDesc + "'" + " AND Cost = " + cost;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// This SQL returns a distinct invoice number 
        /// using an item code as reference
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string SelectDistinctItem(string sItemCode)
        {
            try
            {
                string sSQL = "SELECT distinct(InvoiceNum) FROM LineItems WHERE ItemCode = '" + sItemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This SQL Updates an item with reference to the item description,
        /// the item cost and the item code
        /// </summary>
        /// <param name="sItemDesc"></param>
        /// <param name="sItemCost"></param>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string UpdateItem(string sItemDesc, string sItemCost, string sItemCode)
        {
            try
            {
                string sSQL = "UPDATE ItemDesc SET ItemDesc = " + "'" + sItemDesc +  "'" + ", " + "Cost = " + "'" + sItemCost + "'" + 
                              " WHERE ItemCode = " + "'" + sItemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL Inserts items into the item table with reference to the item code, 
        /// description and item cost
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <param name="sItemCost"></param>
        /// <returns></returns>
        public string InsertItem(string code, string sItemDesc, string sItemCost)
        {
            try
            {
                string sSQL = "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) " +
                              "VALUES (" + "'" + code + "'" + ", " + "'" + sItemDesc + "'" + ", " + "'" + sItemCost + "'" + ");";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This SQL deletes an item from the item table with reference
        /// to the item code
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string DeleteItem(string sItemCode)
        {
            try
            {
                string sSQL = "DELETE FROM ItemDesc WHERE ItemCode = " +  "'" + sItemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
