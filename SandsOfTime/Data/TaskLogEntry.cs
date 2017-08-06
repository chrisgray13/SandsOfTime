using System;

using GraySystem.Data;
using SandsOfTime.Actions;

namespace SandsOfTime.Data
{
   public class TaskLogEntry
   {
      #region Fields

      private int _iTaskLogID;
      private DateTime _dtTaskDate;
      private string _sUserID;
      private Task _task;
      private TaskStatuses _taskStatus;

      #endregion

      #region Properties

      #region TaskLogID

      public int TaskLogID
      {
         get { return (_iTaskLogID); }

         set { _iTaskLogID = value; }
      }

      #endregion

      #region TaskDate

      public DateTime TaskDate
      {
         get { return (_dtTaskDate); }

         set { _dtTaskDate = value; }
      }

      #endregion

      #region UserID

      public string UserID
      {
         get { return (_sUserID); }

         set { _sUserID = value; }
      }

      #endregion

      #region Task

      public Task Task
      {
         get { return (_task); }

         set { _task = value; }
      }

      #endregion

      #region TaskStatus

      public TaskStatuses TaskStatus
      {
         get { return (_taskStatus); }

         set { _taskStatus = value; }
      }

      #endregion

      #endregion

      #region Constructors

      public TaskLogEntry(int taskLogID)
      {
         _iTaskLogID = taskLogID;
      }

      public TaskLogEntry(int taskLogID, DateTime dtTaskDate, string sUserID, int taskID, TaskStatuses taskStatus)
      {
         _iTaskLogID = taskLogID;
         _dtTaskDate = dtTaskDate;
         _sUserID = sUserID;
         _task = new Task(taskID);
         _taskStatus = taskStatus;
      }

      public TaskLogEntry(int taskLogID, DateTime dtTaskDate, string sUserID, Task task, TaskStatuses taskStatus)
      {
         _iTaskLogID = taskLogID;
         _dtTaskDate = dtTaskDate;
         _sUserID = sUserID;
         _task = task;
         _taskStatus = taskStatus;
      }

      #endregion

      #region Methods

      #region Create

      public static int Create(Connection connection, int iTaskID, string sUserID)
      {
         int iTaskLogEntryID = 0;

         iTaskLogEntryID = Get(connection, iTaskID, sUserID);
         if (iTaskLogEntryID <= 0)
         {
            connection.BeginTransaction();

            iTaskLogEntryID = (int) (connection.ExecuteScalar("INSERT INTO TaskLog (TaskDate, TaskID, UserID, TaskStatusID) " +
                                                              "VALUES ('" + DateTime.Today.ToString("MM/dd/yyyy") + "', " +
                                                              iTaskID.ToString() + ", '" + sUserID + "', 1) " +
                                                              "SELECT CONVERT(int, @@identity)") ?? 0);
            if (iTaskLogEntryID > 0)
            {
               connection.CommitTransaction();
            } // end if
            else
            {
               connection.RollbackTransaction();
            } // end else

            return (iTaskLogEntryID);
         } // end if
         else
         {
            return (iTaskLogEntryID);
         }
      }

      public static int Create(Connection connection, DateTime dtTaskDate, int iTaskID, string sUserID)
      {
         int iTaskLogEntryID = 0;

         iTaskLogEntryID = Get(connection, dtTaskDate, iTaskID, sUserID);
         if (iTaskLogEntryID <= 0)
         {
            connection.BeginTransaction();

            iTaskLogEntryID = (int) (connection.ExecuteScalar("INSERT INTO TaskLog (TaskDate, TaskID, UserID, TaskStatusID) " +
                                                              "VALUES ('" + dtTaskDate.ToShortDateString() + "', " +
                                                       iTaskID.ToString() + ", '" + sUserID + "', 1) " +
                                                       "SELECT CONVERT(int, @@identity)") ?? 0);
            if (iTaskLogEntryID > 0)
            {
               connection.CommitTransaction();
            } // end if
            else
            {
               connection.RollbackTransaction();
            } // end else

            return (iTaskLogEntryID);
         } // end if
         else
         {
            return (iTaskLogEntryID);
         }
      }

      public static int Create(Connection connection, string sTask, string sUserID)
      {
         int iTaskLogEntryID = 0;
         int iTaskID;

         iTaskLogEntryID = Get(connection, sTask, sUserID);
         if (iTaskLogEntryID <= 0)
         {
            connection.BeginTransaction();

            iTaskID = Task.Create(connection, sTask);
            if (iTaskID > 0)
            {
               object test = connection.ExecuteScalar("INSERT INTO TaskLog (TaskDate, TaskID, UserID, TaskStatusID) " +
                                                                 "VALUES ('" + DateTime.Today.ToString("MM/dd/yyyy") + "', " +
                                                                 iTaskID.ToString() + ", '" + sUserID + "', 1) " +
                                                                 "SELECT CONVERT(int, @@identity)");
               iTaskLogEntryID = (int) (test ?? 0);
            }

            if (iTaskLogEntryID > 0)
            {
               connection.CommitTransaction();
            } // end if
            else
            {
               connection.RollbackTransaction();
            } // end else

            return (iTaskLogEntryID);
         } // end if
         else
         {
            return (iTaskLogEntryID);
         }
      }

      public static int Create(Connection connection, DateTime dtTaskDate, string sTask, string sUserID)
      {
         int iTaskLogEntryID = 0;
         int iTaskID;

         iTaskLogEntryID = Get(connection, dtTaskDate, sTask, sUserID);
         if (iTaskLogEntryID <= 0)
         {
            connection.BeginTransaction();

            iTaskID = Task.Create(connection, sTask);
            if (iTaskID > 0)
            {
               iTaskLogEntryID = (int) (connection.ExecuteScalar("INSERT INTO TaskLog (TaskDate, TaskID, UserID, TaskStatusID) " +
                                                                 "VALUES ('" + dtTaskDate.ToShortDateString() + "', " +
                                                                 iTaskID.ToString() + ", '" + sUserID + "', 1) " +
                                                                 "SELECT CONVERT(int, @@identity)") ?? 0);
            }

            if (iTaskLogEntryID > 0)
            {
               connection.CommitTransaction();
            } // end if
            else
            {
               connection.RollbackTransaction();
            } // end else

            return (iTaskLogEntryID);
         } // end if
         else
         {
            return (iTaskLogEntryID);
         }
      }

      #endregion

      #region Gets

      #region Get

      public static int Get(Connection connection, string sTask, string sUserID)
      {
         return ((int) (connection.ExecuteScalar("SELECT TaskLogID " +
                                                 "FROM TaskLog TL INNER JOIN Tasks T ON (TL.TaskID = T.TaskID) " +
                                                 "WHERE TL.TaskDate = '" + DateTime.Today.ToString("MM/dd/yyyy") + "' " +
                                                 "AND T.Task = '" + sTask + "' " +
                                                 "AND TL.UserID = '" + sUserID +"'") ?? 0));
      }

      public static int Get(Connection connection, DateTime dtTaskDate, string sTask, string sUserID)
      {
         return ((int) (connection.ExecuteScalar("SELECT TaskLogID " +
                                                 "FROM TaskLog TL INNER JOIN Tasks T ON (TL.TaskID = T.TaskID) " +
                                                 "WHERE TL.TaskDate = '" + dtTaskDate.ToShortDateString() + "' " +
                                                 "AND T.Task = '" + sTask + "' " +
                                                 "AND TL.UserID = '" + sUserID + "'") ?? 0));
      }

      public static int Get(Connection connection, int iTaskID, string sUserID)
      {
         return ((int) (connection.ExecuteScalar("SELECT TaskLogID " +
                                                 "FROM TaskLog " +
                                                 "WHERE TaskDate = '" + DateTime.Today.ToString("MM/dd/yyyy") + "' " +
                                                 "AND TaskID = " + iTaskID.ToString() + " " +
                                                 "AND UserID = '" + sUserID + "'") ?? 0));
      }

      public static int Get(Connection connection, DateTime dtTaskDate, int iTaskID, string sUserID)
      {
         return ((int) (connection.ExecuteScalar("SELECT TaskLogID " +
                                                 "FROM TaskLog " +
                                                 "WHERE TaskDate = '" + dtTaskDate.ToShortDateString() + "' " +
                                                 "AND TaskID = " + iTaskID.ToString() + " " +
                                                 "AND UserID = '" + sUserID + "'") ?? 0));
      }

      #endregion

      #region GetActive

      public static int GetActive(Connection connection, string sUserID)
      {
         return ((int)(connection.ExecuteScalar("SELECT TaskLogID " +
                                                 "FROM TaskLog TKL INNER JOIN TimeLog TML ON (TKL.TaskLogID = TML.TaskLogID) " +
                                                                  "INNER JOIN (SELECT MAX(ActionTime) AS ActionTime, TKL.UserID " +
                                                                              "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                                                                              "WHERE TKL.UserID = '" + sUserID + "' " +
                                                                              "GROUP BY TKL.UserID " +
                                                                              "HAVING SUM(CASE TML.ActionTypeID WHEN 1 THEN 1 WHEN 0 THEN -1 ELSE 0 END) < 0 " +
                                                                             ") ATML ON (TML.ActionTime = ATML.ActionTime AND TKL.UserID = ATML.UserID) " +
                                                 "WHERE TML.ActionTypeID = 0") ?? 0));
      }

      #endregion

      #endregion

      #region Update

      public static int Update(Connection connection, DateTime dtTaskDate, string sTask, string sUserID)
      {
         return (0);
      }

      #endregion

      #endregion
   }
}
