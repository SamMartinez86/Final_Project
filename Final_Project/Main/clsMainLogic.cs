using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Data;

namespace Final_Project
{
    class clsMainLogic
    {

        List<Item> items = new List<Item>();
        private clsDataAccess clsDataAccess = new clsDataAccess();
        private clsItemsSQL clsItemsSQL = new clsItemsSQL();


        /// <summary>
        /// get items
        /// <returns></returns>
        public List<Item> getItems()
        {

            DataSet ds;
            int iRef = 0;
            items = new List<Item>();

            ds = clsDataAccess.ExecuteSQLStatement(clsItemsSQL.SelectItemCodeDescCost(), ref iRef);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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
        /// HandleError 
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C://Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }



    }
}
