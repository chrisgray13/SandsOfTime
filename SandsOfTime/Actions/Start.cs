#region Usings

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using GraySystem.Data;
using SandsOfTime.Data;

#endregion


namespace SandsOfTime.Actions
{
   public static class Start
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
         int iTimeLogID;

         connection.BeginTransaction();

         iTimeLogID = Stop.Execute(connection, sUserId);
         if (iTimeLogID >= 0)
         {
            // Pause for a second to ensure the start is not the same as the stop
            System.Threading.Thread.Sleep(1000);

            iTimeLogID = TimeLogEntry.Create(connection, ActionTypes.Start, sUserId, sTask);
         } // end if

         if (iTimeLogID > 0)
         {
            connection.CommitTransaction();

            return (iTimeLogID);
         } // end if
         else
         {
            connection.RollbackTransaction();

            return (iTimeLogID);
         } // end else
      } // end Execute

      public static int Execute(string sUserId, int iTaskID)
      {
         Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString);
         int iTimeLogID;

         iTimeLogID = Execute(connection, sUserId, iTaskID);

         connection.Dispose();

         return (iTimeLogID);
      } // end Execute

      public static int Execute(Connection connection, string sUserId, int iTaskID)
      {
         int iTimeLogID;

         connection.BeginTransaction();

         iTimeLogID = Stop.Execute(connection, sUserId);
         if (iTimeLogID >= 0)
         {
            // Pause for a second to ensure the start is not the same as the stop
            System.Threading.Thread.Sleep(1000);

            iTimeLogID = TimeLogEntry.Create(connection, ActionTypes.Start, sUserId, iTaskID);
         } // end if

         if (iTimeLogID > 0)
         {
            connection.CommitTransaction();

            return (iTimeLogID);
         } // end if
         else
         {
            connection.RollbackTransaction();

            return (iTimeLogID);
         } // end else
      } // end Execute

      public static int Execute(Connection connection, DateTime dtStartTime, string sUserId, int iTaskID)
      {
         return (TimeLogEntry.Create(connection, dtStartTime, ActionTypes.Start, sUserId, iTaskID));
      } // end Execute

      #endregion

      #endregion
   } // end Start Class
} // end SandsOfTime.Actions Namespace
