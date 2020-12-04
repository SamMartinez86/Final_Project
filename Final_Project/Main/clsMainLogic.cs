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
    public class clsMainLogic
    {

        clsMainSQL SQLMain = new clsMainSQL();
        private List<invoiceCls> Invoices;
        clsDataAccess db = new clsDataAccess();
        public string sSQL;

        /// <summary>
        /// returns all invoices
        /// </summary>
        /// <returns></returns>
        public List<invoiceCls> invoiceGtr()
        {
            try
            {
                Invoices = new List<invoiceCls>();

                // Data set hold data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                sSQL = SQLMain.SelectAllInvoices();

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
                {
                    // Adding the Invoice number, invoice cost and invoice date
                    Invoices.Add(new invoiceCls
                    {
                        InvoiceNum = ds.Tables[0].Rows[i][0].ToString(),
                        InvoiceDate = ds.Tables[0].Rows[i][1].ToString(),
                        InvoiceCost = ds.Tables[0].Rows[i][2].ToString()
                    });
                }
                return Invoices;


            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }


        /// <summary>
        /// this method handles errors 
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
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
