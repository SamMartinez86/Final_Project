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
        #region var init
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
        /// This will store the selected invoice number
        /// </summary>
        private string selectedInvoiceNumber;

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

        #endregion var init

        #region methods

        /// <summary>
        /// This method will set the invoice number
        /// </summary>
        /// <param name="x"></param>
        public void setInvoiceNumber(string invoiceNum)
        {
            try
            {
                selectedInvoiceNumber = invoiceNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method will return the selected invoice number to the main window
        /// </summary>
        /// <returns></returns>
        public string getInvoiceNum()
        {
            try
            {
                return selectedInvoiceNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method returns all information for the invoices
        /// </summary>
        /// <returns></returns>
        public List<clsSearch> GetAllInvoices()
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

        /// <summary>
        /// This method returns all information for the invoices
        /// </summary>
        /// <returns></returns>
        public List<clsSearch> getInvoice(string invNum)
        {
            try
            {
                /// Making a new list to store the invoices
                Invoices = new List<clsSearch>();

                // Creating datat set to hold the data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                // Choosing which SQL to submit
                sSQL = SQLSearch.SelectInvoiceData(invNum);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
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

        /// <summary>
        /// This method returns the data related to the total cost selection
        /// <returns></returns>
        public List<clsSearch> getTotalCharges(string totCost)
        {
            try
            {
                /// Making a new list to store the invoices
                Invoices = new List<clsSearch>();

                // Creating datat set to hold the data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                // Choosing which SQL to submit
                sSQL = SQLSearch.SelectInvoiceCost(totCost);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
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

        /// <summary>
        /// This method returns the data related to the date selection
        /// <returns></returns>
        public List<clsSearch> getInvoiceDate(string invDate)
        {
            try
            {
                /// Making a new list to store the invoices
                Invoices = new List<clsSearch>();

                // Creating datat set to hold the data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                // Choosing which SQL to submit
                sSQL = SQLSearch.SelectInvoiceDate(invDate);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
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

        /// <summary>
        /// This method returns the data related to the invoice number
        /// and the date selected
        /// <returns></returns>
        public List<clsSearch> getInvoiceNumDate(string invNum, string invDate)
        {
            try
            {
                /// Making a new list to store the invoices
                Invoices = new List<clsSearch>();

                // Creating datat set to hold the data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                // Choosing which SQL to submit
                sSQL = SQLSearch.SelectInvoiceDataDate(invNum, invDate);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
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

        /// <summary>
        /// This method returns all information for the invoices
        /// </summary>
        /// <returns></returns>
        public List<clsSearch> getAllData(string invNum, string totCost, string invDate )
        {
            try
            {
                /// Making a new list to store the invoices
                Invoices = new List<clsSearch>();

                // Creating datat set to hold the data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                // Choosing which SQL to submit
                sSQL = SQLSearch.SelectInvoiceDataDateCost(invNum, invDate, totCost);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
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

        /// <summary>
        /// This method returns the information when the user specifies
        /// total cost and the date
        /// </summary>
        /// <returns></returns>
        public List<clsSearch> getCostDate( string totCost, string invDate)
        {
            try
            {
                /// Making a new list to store the invoices
                Invoices = new List<clsSearch>();

                // Creating datat set to hold the data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                // Choosing which SQL to submit
                sSQL = SQLSearch.SelectInvoiceCostDate(totCost, invDate);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
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

        /// <summary>
        /// This method returns the information when the user specifies
        /// the invoice number and the total cost
        /// </summary>
        /// <returns></returns>
        public List<clsSearch> getNumCost(string invNum, string totCost)
        {
            try
            {
                /// Making a new list to store the invoices
                Invoices = new List<clsSearch>();

                // Creating datat set to hold the data
                DataSet ds;

                /// This is the returned from the executed SQL statement
                int iRet = 0;

                // Choosing which SQL to submit
                sSQL = SQLSearch.SelectInvoiceNumCost(invNum, totCost);

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
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

        #endregion methods

    } // end class
}
