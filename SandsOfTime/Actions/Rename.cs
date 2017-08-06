#region Usings

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using GraySystem.Data;

#endregion


namespace SandsOfTime.Actions
{
   public static class Rename
   {
      #region Methods

      #region Execute

      public static int Execute(string sUserId, string sTask)
      {
         Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString);
         int iResult;

         iResult = Execute(connection, sUserId, sTask);

         connection.Dispose();

         return (iResult);
      } // end Execute

      public static int Execute(Connection connection, string sUserId, string sTask)
      {
         return (connection.ExecuteNonQuery("UPDATE TimeLog SET Task = '" + sTask + "' " +
                                               "WHERE ActionTime IN (SELECT MAX(ActionTime) FROM TimeLog " +
                                               "WHERE UserId = '" + sUserId + "') AND UserId = '" + sUserId +
                                               "' AND ActionTypeID = " + ((short) ActionTypes.Start).ToString()));
      } // end Execute

      #endregion

      #endregion
   } // end Rename Class
} // end SandsOfTime.Actions Namespace
