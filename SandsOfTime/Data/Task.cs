using System;
using System.Configuration;
using System.Data;
using System.Text;

using GraySystem.Data;
using SandsOfTime.Actions;

namespace SandsOfTime.Data
{
   public class Task
   {
      #region Fields

      private int _iTaskID;
      private string _sTaskName;
      private float _fTimeAllowed;

      #endregion

      #region Properties

      #region TaskID

      public int TaskID
      {
         get { return (_iTaskID); }

         set { _iTaskID = value; }
      }

      #endregion

      #region TaskName

      public string TaskName
      {
         get { return (_sTaskName); }

         set { _sTaskName = value; }
      }

      #endregion

      #region TimeAllowed

      public float TimeAllowed
      {
         get { return (_fTimeAllowed); }

         set { _fTimeAllowed = value; }
      }

      #endregion

      #endregion

      #region Constructors

      public Task(int iTaskID)
      {
         _iTaskID = iTaskID;
      }

      public Task(int iTaskID, string sTaskName)
      {
         _iTaskID = iTaskID;
         _sTaskName = sTaskName;
      }

      public Task(int iTaskID, string sTaskName, float fTimeAllowed)
      {
         _iTaskID = iTaskID;
         _sTaskName = sTaskName;
         _fTimeAllowed = fTimeAllowed;
      }

      #endregion

      #region Methods

      #region Create

      public static int Create(Connection connection, string sTask)
      {
         int iTaskID;

         iTaskID = Get(connection, sTask);
         if (iTaskID <= 0)
         {
            return ((int) (connection.ExecuteScalar("INSERT INTO Tasks (Task) VALUES('" + sTask + "') SELECT CONVERT(int, @@identity)") ?? 0));
         }
         else
         {
            return (iTaskID);
         }
      }

      #endregion

      #region Gets

      #region Get

      public static int Get(Connection connection, string sTask)
      {
         return ((int) (connection.ExecuteScalar("SELECT TaskID FROM Tasks WHERE Task = '" + sTask + "'") ?? 0));
      }

      public static int Get(Connection connection, string sTask, string sUserID)
      {
         return ((int) (connection.ExecuteScalar("SELECT T.TaskID " +
                                                 "FROM Tasks T INNER JOIN TaskLog TL ON (T.TaskID = TL.TaskID) " +
                                                 "WHERE T.Task = '" + sTask + "' AND TL.UserID = '" + sUserID + "'") ?? 0));
      }

      #endregion

      #region GetAll

      public static DataTable GetAll()
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (GetAll(connection, null));
         } // end using
      } // end GetAll

      public static DataTable GetAll(string sUserID)
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (GetAll(connection, sUserID));
         } // end using
      } // end GetAll

      public static DataTable GetAll(Connection connection)
      {
         return (connection.ExecuteQuery("SELECT Task FROM Tasks ORDER BY Task"));
      } // end GetAll

      public static DataTable GetAll(Connection connection, string sUserID)
      {
         if (sUserID == null || sUserID.Length == 0)
         {
            return (GetAll(connection));
         }
         else
         {
            return (connection.ExecuteQuery("SELECT Task " +
                                            "FROM Tasks T INNER JOIN TaskLog TL ON (T.TaskID = TL.TaskID) " +
                                            (((sUserID == null) || (sUserID.Length == 0)) ? String.Empty : ("WHERE TL.UserId = '" + sUserID + "' ")) +
                                            "GROUP BY Task ORDER BY Task"));
         }
      } // end GetAll

      #endregion

      #region GetActive

      public static string GetActive(string sUserID)
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (GetActive(connection, sUserID));
         } // end using
      } // end GetActive

      public static string GetActive(Connection connection, string sUserID)
      {
         return ((string) connection.ExecuteScalar("SELECT T.Task " +
                                                   "FROM Tasks T INNER JOIN TaskLog TKL ON (T.TaskID = TKL.TaskID) " +
                                                      "INNER JOIN TimeLog TML ON (TKL.TaskLogID = TML.TaskLogID) " +
                                                      "INNER JOIN (SELECT MAX(ActionTime) AS ActionTime, TKL.UserID " +
                                                                  "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                                                                  "WHERE TKL.UserID = '" + sUserID + "' " +
                                                                  "GROUP BY TKL.UserID " +
                                                                  "HAVING SUM(CASE TML.ActionTypeID WHEN 1 THEN 1 WHEN 0 THEN -1 ELSE 0 END) < 0" +
                                                                 ") ATML ON (TML.ActionTime = ATML.ActionTime AND TKL.UserID = ATML.UserID) " +
                                                   "WHERE TML.ActionTypeID = " + ActionTypes.Start.ToString("d")) ?? null);
      } // end GetActive

      #endregion

      #region GetPreviouslyActive

      #region GetPreviouslyActive

      public static int GetPreviouslyActive(Connection connection, string sUserID)
      {
         // Gets the previously active by finding the latest time of all stops for the user provided.
         // The result is used to find the correct TimeLog record in order to join to the TaskLog and Task
         // to return the task.
         return ((int) (connection.ExecuteScalar("SELECT T.TaskID " +
                                                 "FROM Tasks T INNER JOIN TaskLog TKL ON (T.TaskID = TKL.TaskID) " +
                                                              "INNER JOIN TimeLog TML ON (TKL.TaskLogID = TML.TaskLogID) " +
                                                              "INNER JOIN (SELECT MAX(ActionTime) AS ActionTime, '" + sUserID + "' AS UserID, " +
                                                                              ActionTypes.Stop.ToString("d") + " AS ActionTypeID " +
                                                                          "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                                                                          "WHERE TKL.UserID = '" + sUserID + "' " +
                                                                             "AND TML.ActionTypeID = " + ActionTypes.Stop.ToString("d") +
                                                                         ") ATML ON (TML.ActionTime = ATML.ActionTime AND TKL.UserID = ATML.UserID " +
                                                                                    "AND TML.ActionTypeID = ATML.ActionTypeID)") ?? 0));
      }

      #endregion

      #endregion

      #endregion

      #region IsThereAnActiveTask

      public static bool IsThereAnActiveTask(string sUserID)
      {
         Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString);
         bool bReturn;

         bReturn = IsThereAnActiveTask(connection, sUserID);

         connection.Dispose();

         return (bReturn);
      } // end IsThereAnActiveTask

      public static bool IsThereAnActiveTask(Connection connection, string sUserID)
      {
         string sTask = GetActive(connection, sUserID);

         return ((sTask != null) && (sTask.Length != 0));
      } // end IsThereAnActiveTask

      #endregion

      #endregion
   }
}
