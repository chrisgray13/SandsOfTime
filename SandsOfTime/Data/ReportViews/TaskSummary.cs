#region Usings

using System;
using System.Configuration;
using System.Data;
using System.Text;

using GraySystem.Data;

#endregion


namespace SandsOfTime.Data.ReportViews
{
   public class TaskSummary : ReportView
   {
      #region Properties

      #region Filter

      public string Filter
      {
         get { return (GetFilter(UserId, StartDate, EndDate)); }
      } // end Filter property

      #endregion

      #endregion

      #region Constructors

      public TaskSummary()
         : base()
      {
      } // end TaskSummary constructor

      public TaskSummary(string sUserId)
         : base(sUserId)
      {
      } // end TaskSummary constructor

      public TaskSummary(DateTime dtStartDate, DateTime dtEndDate)
         : base(dtStartDate, dtEndDate)
      {
      } // end TaskSummary constructor

      public TaskSummary(string sUserId, DateTime dtStartDate, DateTime dtEndDate)
         : base(sUserId, dtStartDate, dtEndDate)
      {
      } // end TaskSummary constructor

      #endregion

      #region Methods

      #region Fetch

      public override DataTable Fetch()
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (Fetch(connection));
         } // end using
      } // end Fetch

      public override DataTable Fetch(Connection connection)
      {
         return (Fetch(connection, UserId, StartDate, EndDate));
      } // end Fetch

      public override DataTable Fetch(string sUserId)
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (Fetch(connection, sUserId, StartDate, EndDate));
         } // end using
      } // end Fetch

      public override DataTable Fetch(Connection connection, string sUserId)
      {
         return (Fetch(connection, sUserId, StartDate, EndDate));
      } // end Fetch

      public override DataTable Fetch(DateTime dtStartDate, DateTime dtEndDate)
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (Fetch(connection, UserId, dtStartDate, dtEndDate));
         } // end using
      } // end Fetch

      public override DataTable Fetch(Connection connection, DateTime dtStartDate, DateTime dtEndDate)
      {
         return (Fetch(connection, UserId, dtStartDate, dtEndDate));
      } // end Fetch

      public override DataTable Fetch(string sUserId, DateTime dtStartDate, DateTime dtEndDate)
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (Fetch(connection, sUserId, dtStartDate, dtEndDate));
         } // end using
      } // end Fetch

      public override DataTable Fetch(Connection connection, string sUserId, DateTime dtStartDate, DateTime dtEndDate)
      {
         string sSQL =
            "SELECT SUM(CASE TML.ActionTypeID WHEN 1 THEN CONVERT(float, TML.ActionTime) ELSE (CONVERT(float, TML.ActionTime) * -1) END) * 24 AS TaskTime, " +
                   "TimeAllowed, " +
                   "CASE WHEN TimeAllowed = 0 " +
                      "THEN 100.00 " +
                      "ELSE (SUM(CASE TML.ActionTypeID WHEN 1 THEN CONVERT(float, TML.ActionTime) ELSE (CONVERT(float, TML.ActionTime) * -1) END) * 2400) / TimeAllowed END AS '% Complete', " +
                   "UserID, Task " +
            "FROM " +
            "( " +
               "SELECT TKL.TaskDate, TKL.UserID, TKL.TaskID, TML.ActionTime, TML.ActionTypeID " +
               "FROM TaskLog TKL INNER JOIN TimeLog TML ON (TKL.TaskLogID = TML.TaskLogID) " +

               "UNION " +

               // This is to generate inferred stops in order to get current time for active tasks
               "SELECT TKL.TaskDate, TKL.UserID, TKL.TaskID, '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "' AS ActionTime, 1 AS ActionTypeID " +
               "FROM TaskLog TKL INNER JOIN TimeLog TML ON (TKL.TaskLogID = TML.TaskLogID) " +
                                "INNER JOIN (SELECT MAX(ActionTime) AS ActionTime, TKL.UserID " +
                                            "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                                            "GROUP BY TKL.UserID " +
                                            "HAVING SUM(CASE TML.ActionTypeID WHEN 1 THEN 1 WHEN 0 THEN -1 ELSE 0 END) < 0" +
                                           ") ATML ON (TML.ActionTime = ATML.ActionTime AND TKL.UserID = ATML.UserID) " +
               "WHERE TML.ActionTypeID = 0 " +
            ") TML INNER JOIN Tasks T ON (TML.TaskID = T.TaskID) " +

            GetFilter(sUserId, dtStartDate, dtEndDate) +

            "GROUP BY UserID, Task, TimeAllowed " +
            "ORDER BY Task ";

         return (connection.ExecuteQuery(sSQL));
      }

      #endregion

      #region GetFilter

      private static string GetFilter(string sUserId, DateTime dtStartDate, DateTime dtEndDate)
      {
         string sFilter = String.Empty;

         if (sUserId != null && sUserId.Length != 0)
         {
            sFilter = "UserId = '" + sUserId + "' ";
         } // end if

         if (dtStartDate != null && dtStartDate.Ticks > 0)
         {
            sFilter = ((sFilter.Length == 0) ? String.Empty : (sFilter + "AND ")) + "TaskDate >= '" + dtStartDate.ToString("yyyy/dd/MM") + "' ";
         } // end if

         if (dtEndDate != null && dtEndDate.Ticks > 0)
         {
            sFilter = ((sFilter.Length == 0) ? String.Empty : (sFilter + "AND ")) + "TaskDate <= '" + dtEndDate.ToString("yyyy/dd/MM") + "' ";
         } // end if

         return ((sFilter.Length == 0) ? sFilter : ("WHERE " + sFilter));
      } // end GetFilter

      #endregion

      #endregion
   }
}