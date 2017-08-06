#region Usings

using System;
using System.Configuration;
using System.Data;
using System.Text;

using GraySystem.Data;

#endregion


namespace SandsOfTime.Data.ReportViews
{
   public class Detailed : ReportView
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

      public Detailed() : base()
      {
      } // end Detailed constructor

      public Detailed(string sUserId) : base(sUserId)
      {
      } // end Detailed constructor

      public Detailed(DateTime dtStartDate, DateTime dtEndDate) : base(dtStartDate, dtEndDate)
      {
      } // end Detailed constructor

      public Detailed (string sUserId, DateTime dtStartDate, DateTime dtEndDate) : base(sUserId, dtStartDate, dtEndDate)
      {
      } // end Detailed constructor

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
            "SELECT TML.TimeLogID, TML.ActionTime, TML.ActionTypeID, TKL.UserID, T.Task " +
            "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) " +
                             "INNER JOIN Tasks T ON (TKL.TaskID = T.TaskID) " +
            GetFilter(sUserId, dtStartDate, dtEndDate) +
            "ORDER BY TML.ActionTime DESC";

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
            sFilter = ((sFilter.Length == 0) ? String.Empty : (sFilter + "AND ")) + "ActionTime >= '" + dtStartDate.ToShortDateString() + "' ";
         } // end if

         if (dtEndDate != null && dtEndDate.Ticks > 0)
         {
            sFilter = ((sFilter.Length == 0) ? String.Empty : (sFilter + "AND ")) + "ActionTime <= '" + dtEndDate.ToShortDateString() + "' ";
         } // end if

         return ((sFilter.Length == 0) ? sFilter : ("WHERE " + sFilter));
      } // end GetFilter

      #endregion

      #endregion
   }
}
