#region Usings

using System;
using System.Configuration;
using System.Data;
using System.Text;

using GraySystem.Data;
using SandsOfTime.Actions;

#endregion


namespace SandsOfTime.Data
{
   public class TimeLogEntry
   {
      #region Fields

      private int _iTimeLogID;
      private DateTime _dtActionTime;
      private ActionTypes _actionType;
      private TaskLogEntry _taskLogEntry;

      #endregion

      #region Properties

      #region TimeLogID

      public int TimeLogID
      {
         get { return (_iTimeLogID); }

         set { _iTimeLogID = value; }
      }

      #endregion

      #region ActionTime

      public DateTime ActionTime
      {
         get { return (_dtActionTime); }

         set { _dtActionTime = value; }
      }

      #endregion

      #region ActionTypeID

      public ActionTypes ActionType
      {
         get { return (_actionType); }

         set { _actionType = value; }
      }

      #endregion

      #region TaskLogEntry

      public TaskLogEntry TaskLogEntry
      {
         get { return (_taskLogEntry); }

         set { _taskLogEntry = value; }
      }

      #endregion

      #endregion

      #region Constructors

      public TimeLogEntry(int iTimeLogID, DateTime dtActionTime, ActionTypes actionType, int iTaskLogID)
      {
         _iTimeLogID = iTimeLogID;
         _dtActionTime = dtActionTime;
         _actionType = actionType;
         _taskLogEntry = new TaskLogEntry(iTaskLogID);
      }

      public TimeLogEntry(int iTimeLogID, DateTime dtActionTime, ActionTypes actionType, TaskLogEntry taskLogEntry)
      {
         _iTimeLogID = iTimeLogID;
         _dtActionTime = dtActionTime;
         _actionType = actionType;
         _taskLogEntry = taskLogEntry;
      }

      #endregion

      #region Methods

      #region Creates

      #region Create

      public static int Create(Connection connection, ActionTypes actionType, string sUserID, string sTask)
      {
         int iTaskLogEntryID = 0;
         int iTimeLogEntryID = 0;

         connection.BeginTransaction();

         iTaskLogEntryID = TaskLogEntry.Create(connection, sTask, sUserID);
         if (iTaskLogEntryID > 0)
         {
            iTimeLogEntryID = Create(connection, actionType, iTaskLogEntryID);
         }

         if (iTimeLogEntryID > 0)
         {
            connection.CommitTransaction();
         } // end if
         else
         {
            connection.RollbackTransaction();
         } // end else

         return (iTimeLogEntryID);
      }

      public static int Create(Connection connection, DateTime dtActionTime, ActionTypes actionType, string sUserID, string sTask)
      {
         int iTaskLogEntryID = 0;
         int iTimeLogEntryID = 0;

         connection.BeginTransaction();

         iTaskLogEntryID = TaskLogEntry.Create(connection, dtActionTime, sTask, sUserID);
         if (iTaskLogEntryID > 0)
         {
            iTimeLogEntryID = Create(connection, dtActionTime, actionType, iTaskLogEntryID);
         }

         if (iTimeLogEntryID > 0)
         {
            connection.CommitTransaction();
         } // end if
         else
         {
            connection.RollbackTransaction();
         } // end else

         return (iTimeLogEntryID);
      }

      public static int Create(Connection connection, ActionTypes actionType, string sUserID, int iTaskID)
      {
         int iTaskLogEntryID = 0;
         int iTimeLogEntryID = 0;

         connection.BeginTransaction();

         iTaskLogEntryID = TaskLogEntry.Create(connection, iTaskID, sUserID);
         if (iTaskLogEntryID > 0)
         {
            iTimeLogEntryID = Create(connection, actionType, iTaskLogEntryID); ;
         }

         if (iTimeLogEntryID > 0)
         {
            connection.CommitTransaction();
         } // end if
         else
         {
            connection.RollbackTransaction();
         } // end else

         return (iTimeLogEntryID);
      }

      public static int Create(Connection connection, DateTime dtActionTime, ActionTypes actionType, string sUserID, int iTaskID)
      {
         int iTaskLogEntryID = 0;
         int iTimeLogEntryID = 0;

         connection.BeginTransaction();

         iTaskLogEntryID = TaskLogEntry.Create(connection, dtActionTime, iTaskID, sUserID);
         if (iTaskLogEntryID > 0)
         {
            iTimeLogEntryID = Create(connection, dtActionTime, actionType, iTaskLogEntryID);
         }

         if (iTimeLogEntryID > 0)
         {
            connection.CommitTransaction();
         } // end if
         else
         {
            connection.RollbackTransaction();
         } // end else

         return (iTimeLogEntryID);
      }

      public static int Create(Connection connection, ActionTypes actionType, int iTaskLogEntryID)
      {
         return ((int) (connection.ExecuteScalar("INSERT INTO TimeLog (ActionTime, ActionTypeID, TaskLogID) " +
                                                 "VALUES ('" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                                                 actionType.ToString("d") + ", " +
                                                 iTaskLogEntryID.ToString() + ") " +
                                                 "SELECT CONVERT(int, @@identity)") ?? 0));
      }

      public static int Create(Connection connection, DateTime dtActionTime, ActionTypes actionType, int iTaskLogEntryID)
      {
         return ((int) (connection.ExecuteScalar("INSERT INTO TimeLog (ActionTime, ActionTypeID, TaskLogID) " +
                                                 "VALUES ('" + dtActionTime.ToString() + "', " +
                                                 actionType.ToString("d") + ", " +
                                                 iTaskLogEntryID.ToString() + ") " +
                                                 "SELECT CONVERT(int, @@identity)") ?? 0));
      }

      #endregion

      #region CreateStop

      public static int CreateStop(Connection connection, int iTimeLogID)
      {
         if (iTimeLogID > 0)
         {
            return ((int)(connection.ExecuteScalar("INSERT INTO TimeLog (ActionTime, ActionTypeID, TaskLogID) " +
                                                    "SELECT '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                                                    ActionTypes.Stop.ToString("d") + ", TaskLogID " +
                                                    "FROM TimeLog " +
                                                    "WHERE TimeLogID = " + iTimeLogID.ToString() + " " +
                                                    "SELECT CONVERT(int, @@identity)") ?? 0));
         }
         else
         {
            return (0);
         }
      }

      #endregion

      #endregion

      #region Gets

      #region GetActive

      public static int GetActive(Connection connection, string sUserID)
      {
         return ((int) (connection.ExecuteScalar("SELECT TimeLogID " +
                                                 "FROM TaskLog TKL INNER JOIN TimeLog TML ON (TKL.TaskLogID = TML.TaskLogID) " +
                                                                  "INNER JOIN (SELECT MAX(ActionTime) AS ActionTime, TKL.UserID " +
                                                                              "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                                                                              "WHERE TKL.UserID = '" + sUserID + "' " +
                                                                              "GROUP BY TKL.UserID " +
                                                                              "HAVING SUM(CASE TML.ActionTypeID WHEN 1 THEN 1 WHEN 0 THEN -1 ELSE 0 END) < 0 " +
                                                                             ") ATML ON (TML.ActionTime = ATML.ActionTime AND TKL.UserID = ATML.UserID) " +
                                                 "WHERE TML.ActionTypeID = 0") ?? 0));
      }

      #region Obsolete

      //public static int GetActive(Connection connection, string sUserID)
      //{
      //   return ((int)(connection.ExecuteScalar("SELECT TimeLogID " +
      //                                             "FROM TimeLog " +
      //                                             "WHERE ActionTypeID = " + ActionTypes.Start.ToString("d") +
      //                                             " AND TimeLogID = (SELECT MAX(TimeLogID) " +
      //                                                               "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
      //                                                               "WHERE TKL.UserId = '" + sUserID + "')") ?? 0));
      //}

      #endregion

      #endregion

      public static TimeLogEntry LoadActive(Connection connection, string sUserID)
      {
         DataTable tblTimeLogEntry;
         TimeLogEntry timeLogEntry;

         tblTimeLogEntry = connection.ExecuteQuery("SELECT TML.TimeLogID, TML.ActionTime, TML.ActionTypeID, TKL.TaskLogID, " +
                                                   "TKL.TaskDate, TKL.UserID, TKL.TaskID, TKL.TaskStatusID " +
                                                   "FROM TaskLog TKL INNER JOIN TimeLog TML ON (TKL.TaskLogID = TML.TaskLogID) " +
                                                                    "INNER JOIN (SELECT MAX(ActionTime) AS ActionTime, TKL.UserID " +
                                                                                "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                                                                                "WHERE TKL.UserID = '" + sUserID + "' " +
                                                                                "GROUP BY TKL.UserID " +
                                                                                "HAVING SUM(CASE TML.ActionTypeID WHEN 1 THEN 1 WHEN 0 THEN -1 ELSE 0 END) < 0 " +
                                                                               ") ATML ON (TML.ActionTime = ATML.ActionTime AND TKL.UserID = ATML.UserID) " +
                                                   "WHERE TML.ActionTypeID = 0");

         if (tblTimeLogEntry.Rows.Count == 0)
         {
            return (null);
         }
         else
         {
            return (new TimeLogEntry(Convert.ToInt32(tblTimeLogEntry.Rows[0]["TimeLogID"]),
                                     Convert.ToDateTime(tblTimeLogEntry.Rows[0]["ActionTime"]),
                                     (ActionTypes) Enum.Parse(typeof(ActionTypes), tblTimeLogEntry.Rows[0]["ActionTypeID"].ToString()),
                                     new TaskLogEntry(Convert.ToInt32(tblTimeLogEntry.Rows[0]["TaskLogID"]),
                                                      Convert.ToDateTime(tblTimeLogEntry.Rows[0]["TaskDate"]),
                                                      tblTimeLogEntry.Rows[0]["UserID"].ToString(),
                                                      new Task(Convert.ToInt32(tblTimeLogEntry.Rows[0]["TaskID"])),
                                                      (TaskStatuses) Enum.Parse(typeof(TaskStatuses), tblTimeLogEntry.Rows[0]["TaskStatusID"].ToString()))));
         }
      }

      #endregion

      #region Update

      #endregion

      #region Delete

      public static int Delete(DataRow[] timeEntries)
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (Delete(connection, timeEntries));
         }
      }

      public static int Delete(Connection connection, DataRow[] timeEntries)
      {
         StringBuilder strSql;
         int iResult = 0;

         if (timeEntries.Length > 0)
         {
            strSql = new StringBuilder(40 + (12 * timeEntries.Length));

            strSql.Append("DELETE FROM TimeLog WHERE TimeLogID IN (");
            strSql.Append(timeEntries[0]["TimeLogID"].ToString());

            for (int i = 1; i < timeEntries.Length; i++)
            {
               strSql.Append(", ");
               strSql.Append(timeEntries[0]["TimeLogID"].ToString());
            }

            strSql.Append(")");

            iResult = connection.ExecuteNonQuery(strSql.ToString());
         }

         return (iResult);
      }

      #endregion

      #endregion
   }
}
