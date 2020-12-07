using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class clsItemsLogic
    {
        #region attributes

        /// <summary>
        /// This member will hold a list of all items in the database
        /// </summary>
        List<Item> items = new List<Item>();

        /// <summary>
        /// This member is an object of the data access class giving access to the database
        /// </summary>
        private clsDataAccess clsDataAccess = new clsDataAccess();

        /// <summary>
        /// This member is an object of the clsItemsSql class which holds all of the SQL
        /// statements
        /// </summary>
        private clsItemsSQL clsItemsSQL = new clsItemsSQL();

        #endregion

        #region methods
        /// <summary>
        /// This method gets all of the items from the 
        /// database and passes them back to the UI in a list
        /// </summary>
        /// <returns></returns>
        public List<Item> getItems()
        {
            // initialize variables
            DataSet ds;
            int iRef = 0;
            items = new List<Item>();

            // call SQL statement
            ds = clsDataAccess.ExecuteSQLStatement(clsItemsSQL.SelectItemCodeDescCost(), ref iRef);

            // loop through and add items to item list
            for (int i = 0; i < iRef; i++)
            {
                Item temp = new Item()
                {
                    itemCode = ds.Tables[0].Rows[i].ItemArray[0].ToString(),
                    itemDesc = ds.Tables[0].Rows[i].ItemArray[1].ToString(),
                    itemCost = ds.Tables[0].Rows[i].ItemArray[2].ToString()
                };

                items.Add(temp);
            }

            // return items
            return items;
        }

        /// <summary>
        /// This method adds an item to the database
        /// </summary>
        /// <param name="selectedItem"></param>
        public void addItem(string code, string desc, string cost)
        {
            // call SQL statement to add item
            clsDataAccess.ExecuteNonQuery(clsItemsSQL.InsertItem(code, desc, cost));
        }

        /// <summary>
        /// This method will update an item in the data grid
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        public void updateItem(string code, string desc, string cost)
        {
            // store update item in variable
            string query = clsItemsSQL.UpdateItem(desc, cost, code);

            // call SQL statement
            clsDataAccess.ExecuteNonQuery(query);
        }

        /// <summary>
        /// This method will delete an item from the data grid if 
        /// it is okay to delete
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool deleteItem(string code)
        {
            // if deleted 
            if (!okayToDelete(code))
                return false; //unsuccessful delete

            // get deleted item
            string query = clsItemsSQL.DeleteItem(code);

            // SQL statement
            clsDataAccess.ExecuteNonQuery(query);

            return true; //successful delete
        }

        /// <summary>
        /// This method will get an invoice number of an invoice attached to an item if one exists
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string getAttachedInvoiceNum(string code)
        {
            // initialize variables
            DataSet ds;
            int iRef = 0;

            // SQL statement 
            ds = clsDataAccess.ExecuteSQLStatement(clsItemsSQL.SelectDistinctItem(code), ref iRef);

            // return specific row 
            if (ds.Tables[0].Rows.Count >= 1)
                return ds.Tables[0].Rows[0].ItemArray[0].ToString();


            return null;
        }

        /// <summary>
        /// This is a boolean that will hold true if the item selected is able to be deleted
        /// and false if it is not
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool okayToDelete(string code)
        {

            // call get invoice attached method and store in variable
            string invoiceNum = getAttachedInvoiceNum(code);

            // return variable if null
            return invoiceNum == "" || invoiceNum == null ? true : false;
        }
    }

    #endregion
}
