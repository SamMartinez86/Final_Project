﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;

namespace Final_Project
{
    class clsMainLogic
    {

        List<Item> items = new List<Item>();
        private clsDataAccess clsDataAccess = new clsDataAccess();
        private clsItemsSQL clsItemsSQL = new clsItemsSQL();




       

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
