#region Usings

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

using GraySystem.Data;

#endregion


namespace SandsOfTime.Actions
{
   public static class List
   {
      #region Methods

      #region Execute

      public static int Execute(string sUserId, int iNumOfTasks)
      {
         Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString);
         int iResult;

         iResult = Execute(connection, sUserId, iNumOfTasks);

         connection.Dispose();

         return (iResult);
      } // end Execute

      public static int Execute(Connection connection, string sUserId, int iNumOfTasks)
      {
         DataTable tblResults;

         tblResults = connection.ExecuteQuery("SELECT Task FROM TimeLog WHERE UserId = '" + sUserId +
                                                 "' ORDER BY ActionTime DESC");

         return (tblResults.Rows.Count);
      } // end Execute

      #endregion

      #endregion
   } // end List Class
} // end SandsOfTime.Actions Namespace