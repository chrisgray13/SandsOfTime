#region Usings

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

using GraySystem.Data;

#endregion


namespace SandsOfTime.Data.ReportViews
{
   public abstract class ReportView
   {
      #region Fields

      private Connection _connection;

      private string _sUserId;
      private DateTime _dtStartDate;
      private DateTime _dtEndDate;

      #endregion

      #region Properties

      #region UserId

      public string UserId
      {
         get { return (_sUserId); }

         set { _sUserId = value; }
      } // end UserId property

      #endregion

      #region StartDate

      public DateTime StartDate
      {
         get { return (_dtStartDate); }

         set { _dtStartDate = value; }
      } // end StartDate property

      #endregion

      #region EndDate

      public DateTime EndDate
      {
         get { return (_dtEndDate); }

         set { _dtEndDate = value; }
      } // end EndDate property

      #endregion

      #endregion

      #region Constructors

      public ReportView()
      {
      } // end ReportView constructor

      public ReportView(string sUserId)
      {
         _sUserId = sUserId;
      } // end ReportView constructor

      public ReportView(DateTime dtStartDate, DateTime dtEndDate)
      {
         _dtStartDate = dtStartDate;
         _dtEndDate = dtEndDate;
      } // end ReportView constructor

      public ReportView(string sUserId, DateTime dtStartDate, DateTime dtEndDate)
      {
         _sUserId = sUserId;
         _dtStartDate = dtStartDate;
         _dtEndDate = dtEndDate;
      } // end ReportView constructor

      #endregion

      #region Methods

      #region Fetch

      public abstract DataTable Fetch();

      public abstract DataTable Fetch(Connection connection);

      public abstract DataTable Fetch(string sUserId);

      public abstract DataTable Fetch(Connection connection, string sUserId);

      public abstract DataTable Fetch(DateTime dtStartDate, DateTime dtEndDate);

      public abstract DataTable Fetch(Connection connection, DateTime dtStartDate, DateTime dtEndDate);

      public abstract DataTable Fetch(string sUserId, DateTime dtStartDate, DateTime dtEndDate);

      public abstract DataTable Fetch(Connection connection, string sUserId, DateTime dtStartDate, DateTime dtEndDate);

      #endregion

      #endregion
   } // end ReportView
} // end SandsOfTime.Data.ReportViews Namespace
