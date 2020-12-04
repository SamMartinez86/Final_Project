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
        #region Class Members
        /// <summary>
        /// This member will hold a list of all items in the database
        /// </summary>
        List<Item> items = new List<Item>();
        /// <summary>
        /// This member is an object of the data access class giving access to the database
        /// </summary>
        private clsDataAccess clsDataAccess = new clsDataAccess();
        /// <summary>
        /// This member is an object of the clsItemsSql class which holds all of the sql
        /// statements
        /// </summary>
        private clsItemsSQL clsItemsSQL = new clsItemsSQL();
        #endregion

        /// <summary>
        /// This method gets all of the items from the 
        /// database and passes them back to the UI in a list
        /// </summary>
        /// <returns></returns>
        public List<Item> getItems()
        {
            DataSet ds;
            int iRef = 0;
            items = new List<Item>();

            ds = clsDataAccess.ExecuteSQLStatement(clsItemsSQL.SelectItemCodeDescCost(), ref iRef);

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

            return items;
        }

        /// <summary>
        /// This method adds an item to the database
        /// </summary>
        /// <param name="selectedItem"></param>
        public void addItem(string code, string desc, string cost)
        {
            clsDataAccess.ExecuteNonQuery(clsItemsSQL.InsertItem(code, desc, cost));
        }

        /// <summary>
        /// This method will update an item in the datagrid
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        public void updateItem(string code, string desc, string cost)
        {
            string query = clsItemsSQL.UpdateItem(desc, cost, code);

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
            if (!okayToDelete(code))
                return false; //unsuccessful delete

            string query = clsItemsSQL.DeleteItem(code);
            clsDataAccess.ExecuteNonQuery(query);

            return true; //successsful delete
        }

        /// <summary>
        /// This method will get an invoice number of an invoice attached to an item if one exists
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string getAttachedInvoiceNum(string code)
        {
            DataSet ds;
            int iRef = 0;            
            ds = clsDataAccess.ExecuteSQLStatement(clsItemsSQL.SelectDistinctItem(code), ref iRef);

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
            string invoiceNum = getAttachedInvoiceNum(code);

            return invoiceNum == "" || invoiceNum == null ? true : false;
        }
    }
}
