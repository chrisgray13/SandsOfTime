#region Usings

using System;
using System.Configuration;
using System.Data.OleDb;

using GraySystem.Data;
using SandsOfTime.Data;

#endregion


namespace SandsOfTime.Actions
{
   public static class Stop
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
         TimeLogEntry timeLogEntry;
         int iTimeLogID;

         connection.BeginTransaction();

         timeLogEntry = TimeLogEntry.LoadActive(connection, sUserId);
         if (timeLogEntry == null)
         {
            connection.RollbackTransaction();

            return (0);
         } // end if
         else
         {
            if (timeLogEntry.ActionTime.Date == DateTime.Today)
            {
               iTimeLogID = TimeLogEntry.CreateStop(connection, timeLogEntry.TimeLogID);
            } // end if
            else
            {
               iTimeLogID = HandleDayOverlaps(connection, timeLogEntry.TaskLogEntry);
            } // end else

            if (iTimeLogID > 0)
            {
               connection.CommitTransaction();

               return (iTimeLogID);
            } // end if
            else
            {
               connection.RollbackTransaction();

               return (0);
            } // end else
         } // end else
      } // end Execute

      #endregion

      #region HandleDayOverlaps

      private static int HandleDayOverlaps(Connection connection, string sUserId)
      {
         OleDbDataReader dataReader;
         DateTime dtMaxStart;
         DateTime dtMaxStop;
         TimeSpan timeBetween;

         dataReader = connection.ExecuteReader("SELECT MAX(ActionTime) " +
                                               "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                                               "WHERE UserId = '" + sUserId + "' AND ActionTypeID = " + ActionTypes.Start.ToString("d") +
                                               "UNION " +
                                               "SELECT MAX(ActionTime) " +
                                               "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                                               "WHERE UserId = '" + sUserId + "' AND ActionTypeID = " + ActionTypes.Stop.ToString("d"));

         if (dataReader.Read())
         {
            dtMaxStart = dataReader.GetDateTime(0);

            if (dataReader.Read())
            {
               dtMaxStop = dataReader.GetDateTime(0);

               if (dtMaxStart.Date == dtMaxStop.Date)
               {
                  return (0);
               } // end if
               else
               {
                  timeBetween = dtMaxStop.Date.Subtract(dtMaxStart.Date);

                  return (AddEndOfDayStops(connection, sUserId, dtMaxStart.Date, timeBetween.Days));
               } // end else
            } // end if
            else
            {
               return (-1);
            } // end else
         } // end if
         else
         {
            return (-1);
         } // end else
      } // end HandleDayOverlaps

      private static int HandleDayOverlaps(Connection connection, TaskLogEntry taskLogEntry)
      {
         DateTime dtStartDateTime;
         DateTime dtEndDateTime;
         int iTimeLogID = 0;

         for (dtEndDateTime = taskLogEntry.TaskDate.AddTicks(863990000000), dtStartDateTime = taskLogEntry.TaskDate.AddDays(1);
              dtEndDateTime.Date <= DateTime.Today;
              dtEndDateTime = dtEndDateTime.AddDays(1), dtStartDateTime = dtStartDateTime.AddDays(1))
         {
            if (dtEndDateTime.Date == DateTime.Today)
            {
               iTimeLogID = TimeLogEntry.CreateStop(connection, iTimeLogID);
               if (iTimeLogID > 0)
               {
                  return (iTimeLogID);
               } // end if
               else
               {
                  return (0);
               } // end else
            } // end if
            else
            {
               iTimeLogID = TimeLogEntry.Create(connection, dtEndDateTime, ActionTypes.Stop, taskLogEntry.TaskLogID);
               if (iTimeLogID > 0)
               {
                  iTimeLogID = Start.Execute(connection, dtStartDateTime, taskLogEntry.UserID, taskLogEntry.Task.TaskID);
                  if (iTimeLogID <= 0)
                  {
                     return (0);
                  }
               } // end if
               else
               {
                  return (0);
               } // end else
            } // end else
         } // end for

         return (iTimeLogID);
      } // end HandleDayOverlaps

      #endregion

      #region AddEndOfDayStops

      private static int AddEndOfDayStops(Connection connection, string sUserId, DateTime dtStart, int iNumOfDays)
      {
         for (int i = 0; i < iNumOfDays; i++)
         {
            if (connection.ExecuteNonQuery("INSERT INTO TimeLog (ActionTime, UserId, ActionTypeID, Task) " +
                                           "SELECT '" + dtStart.ToString("MM/dd/yyyy") + " 11:59:59 PM' AS ActionTime, UserId, " + ((short) ActionTypes.Stop).ToString() + " AS ActionTypeID, Task " +
                                           "FROM TimeLog " +
                                           "WHERE UserId = '" + sUserId + "' AND ActionTime = (SELECT MAX(ActionTime) " +
                                                                                              "FROM TimeLog " +
                                                                                              "WHERE UserId = '" + sUserId + "')") > 0)
            {
               dtStart = dtStart.AddDays(1);

               if (connection.ExecuteNonQuery("INSERT INTO TimeLog (ActionTime, UserId, ActionTypeID, Task) " +
                                              "SELECT '" + dtStart.ToString("MM/dd/yyyy") + " 12:00:00 AM' AS ActionTime, UserId, " + ((short) ActionTypes.Start).ToString() + " AS ActionTypeID, Task " +
                                              "FROM TimeLog " +
                                              "WHERE UserId = '" + sUserId + "' AND ActionTime = (SELECT MAX(ActionTime) " +
                                                                                                 "FROM TimeLog " +
                                                                                                 "WHERE UserId = '" + sUserId + "')") <= 0)
               {
                  return (-1);
               } // end if
            } // end if
            else
            {
               return (-1);
            } // end else
         } // end for

         return (iNumOfDays);
      } // end AddEndOfDayStops

      #endregion

      #endregion
   } // end Stop Class
} // end SandsOfTime.Actions Namespace