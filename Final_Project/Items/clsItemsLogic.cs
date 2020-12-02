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
        private clsDataAccess clsDataAccess = new clsDataAccess();
        private clsItemsSQL clsItemsSQL = new clsItemsSQL();
        #endregion

        public List<Item> getItems()
        {
            List<Item> items = new List<Item>();
            DataSet ds;
            int iRef = 0;

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
    }
}
