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
   public static class Resume
   {
      #region Methods

      #region Execute

      public static int Execute(string sUserId)
      {
         Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString);
         int iResult;

         iResult = Execute(connection, sUserId);

         connection.Dispose();

         return (iResult);
      } // end Execute

      public static int Execute(Connection connection, string sUserId)
      {
         int iTaskID;
         int iTimeLogID;

         connection.BeginTransaction();

         iTaskID = Task.GetPreviouslyActive(connection, sUserId);
         if (iTaskID > 0)
         {
            iTimeLogID = Start.Execute(connection, sUserId, iTaskID);

            connection.CommitTransaction();

            return (iTimeLogID);
         } // end if
         else
         {
            connection.RollbackTransaction();

            return (-1);
         } // end else
      } // end Execute

      public static int Execute(string sUserId, string sTask)
      {
         Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString);
         int iResult;

         if (sTask == null)
         {
            iResult = Execute(connection, sUserId);
         } // end if
         else
         {
            iResult = Execute(connection, sUserId, sTask);
         } // end if

         connection.Dispose();

         return (iResult);
      } // end Execute

      public static int Execute(Connection connection, string sUserId, string sTask)
      {
         int iTaskID;
         int iTimeLogEntryID;

         if (sTask == null)
         {
            return (Execute(connection, sUserId));
         } // end if
         else
         {
            connection.BeginTransaction();

            iTaskID = Task.Get(connection, sTask, sUserId);
            if (iTaskID > 0)
            {
               iTimeLogEntryID = Start.Execute(connection, sUserId, iTaskID);

               connection.CommitTransaction();

               return (iTimeLogEntryID);
            } // end if
            else
            {
               connection.RollbackTransaction();

               return (-1);
            } // end else
         } // end else
      } // end Execute

      #endregion

      #endregion
   } // end Resume Class
} // end SandsOfTime.Actions Namespace
