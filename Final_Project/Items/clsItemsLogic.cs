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
        List<Item> items = new List<Item>();
        private clsDataAccess clsDataAccess = new clsDataAccess();
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

            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Item temp = new Item()
                {
                    itemCode = ds.Tables[0].Rows[i].ItemArray[0].ToString(),
                    itemCost = ds.Tables[0].Rows[i].ItemArray[1].ToString(),
                    itemDesc = ds.Tables[0].Rows[i].ItemArray[2].ToString()
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
    }
}
