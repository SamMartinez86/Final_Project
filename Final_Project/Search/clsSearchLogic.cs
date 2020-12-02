using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace Final_Project
{
    class clsSearchLogic
    {
        /// <summary>
        /// New clsSearchSQL class
        /// </summary>
        clsSearchSQL SQLSearch = new clsSearchSQL();


        /// <summary>
        /// Instantiating the new clsDataAccess
        /// </summary>
        clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// This is the var that will hold the SQL statement
        /// </summary>
        public string sSQL;

        /// <summary>
        /// Constructing the list that holds the invoices
        /// </summary>
        private List<clsSearch> Invoices;

        /// <summary>
        /// clsSearchLogic object
        /// </summary>
        public clsSearchLogic()
        {

        }

        /// <summary>
        /// This method returns all information for the invoices
        /// </summary>
        /// <returns></returns>
        public List<clsSearch> GetInvoices()
        {
            try
            {
                /// Making a new list to store the invoices
                Invoices = new List<clsSearch>();

                // Creating datat set to hold the data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                sSQL = SQLSearch.SelectAllInvoices();

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for(int i = 0; i < iRet; i++)
                {
                    // Adding the Invoice number, invoice cost and invoice date
                    Invoices.Add(new clsSearch
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

    }
}
