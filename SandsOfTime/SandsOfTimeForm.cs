#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GraySystem.Data;

using SandsOfTime.Data;
using SandsOfTime.Actions;
using SandsOfTime.Data.ReportViews;
using SandsOfTime.DialogForms;

#endregion


namespace SandsOfTime
{
   public partial class SandsOfTimeForm : Form
   {
      #region Fields

      private Connection _connection;
      private string _sUserId;

      #endregion

      #region Constructors

      public SandsOfTimeForm()
      {
         InitializeComponent();

         _connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString);
      } // end SandsOfTimeForm constructor

      #endregion

      #region Methods

      #region InitializeForm

      private void InitializeForm(object sender, EventArgs e)
      {
         if (Convert.ToBoolean(ConfigurationManager.AppSettings["UseActiveDirectorySecurity"]))
         {
            LogInUser(sender, e);
         }

         LoadTasks();


         ShowDetailedView(sender, e);
      } // end InitializeForm

      #endregion

      #region LogInUser

      private void LogInUser(object sender, EventArgs e)
      {
         if (LoginForm.Show(this, Convert.ToBoolean(ConfigurationManager.AppSettings["UseActiveDirectorySecurity"])) == DialogResult.OK)
         {
            _sUserId = LoginForm.UserID;

            Text = "Sands of Time - " + _sUserId;

            _mnItmFile_LogOut.Enabled = true;

            _toolCmbxTasks_Tasks.Enabled = true;
            SelectActiveTask();

            _toolBtnTasks_Start.Enabled = _mnItmTools_StartTask.Enabled = true;
            _toolBtnTasks_Resume.Enabled = _mnItmTools_ResumeTask.Enabled = true;
            _toolBtnTasks_Stop.Enabled =
               _mnItmTools_StopTask.Enabled =
                  Task.IsThereAnActiveTask(_connection, _sUserId);

            RefreshReportView(sender, e);
         } // end if
      } // end LogInUser

      #endregion

      #region LogOutUser

      private void LogOutUser(object sender, EventArgs e)
      {
         _sUserId = String.Empty;

         Text = "Sands of Time";

         _mnItmFile_LogOut.Enabled = false;

         _toolCmbxTasks_Tasks.Enabled = false;
         _toolBtnTasks_Start.Enabled = _mnItmTools_StartTask.Enabled = false;
         _toolBtnTasks_Resume.Enabled = _mnItmTools_ResumeTask.Enabled = false;
         _toolBtnTasks_Stop.Enabled = _mnItmTools_StopTask.Enabled = false;

         RefreshReportView(sender, e);
      } // end LogOutUser

      #endregion

      #region ExitApplication

      private void ExitApplication(object sender, EventArgs e)
      {
         Close();
      } // end ExitApplication

      #endregion

      private void EditEntry(object sender, EventArgs e)
      {

      }

      #region DeleteEntry

      private void DeleteEntry(object sender, EventArgs e)
      {
         DataRow[] rows;
         int iResult;

         if (_grdTimeEntry.SelectedRows.Count == 0)
         {
            MessageBox.Show(this, "Please select at least one row.", "Time Entry Deletion Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         else
         {
            rows = new DataRow[_grdTimeEntry.SelectedRows.Count];

            for (int i = 0; i < _grdTimeEntry.SelectedRows.Count; i++)
            {
               rows[i] = ((DataRowView) _grdTimeEntry.SelectedRows[i].DataBoundItem).Row;
            }

            try
            {
               iResult = TimeLogEntry.Delete(_connection, rows);
               if (iResult == _grdTimeEntry.SelectedRows.Count)
               {
                  RefreshReportView(null, null);
               }
               else
               {
                  MessageBox.Show(this, "Unable to delete selected rows.", "Time Entry Deletion Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
            }
            catch (Exception ex)
            {
               MessageBox.Show(this, "Unable to delete selected rows [Details:  " + ex.Message + "].", "Time Entry Deletion Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }
      }

      private void DeleteEntry(object sender, KeyPressEventArgs e)
      {
         if (_mnItmView_Detailed.Checked && (int) e.KeyChar == 127 /* DEL */)
         {
            DeleteEntry(sender, (EventArgs) e);
         }
      }

      #endregion

      #region Report Viewing

      #region RefreshReportView

      private void RefreshReportView(object sender, EventArgs e)
      {
         if (_mnItmView_Detailed.Checked)
         {
            ShowDetailedView(sender, e);
         } // end if
         else if (_mnItmView_DailyTaskSummary.Checked)
         {
            ShowDailyTaskSummaryView(sender, e);
         } // end else if
         else if (_mnItmView_DailySummary.Checked)
         {
            ShowDailySummaryView(sender, e);
         } // end else if
      } // end RefreshReportView

      #endregion

      #region ShowDetailedView

      private void ShowDetailedView(object sender, EventArgs e)
      {
         Detailed rptVwDetailed = new Detailed();

         _grdTimeEntry.DataSource = null;
         _grdTimeEntry.DataSource = rptVwDetailed.Fetch(_connection, _sUserId);

         _grdTimeEntry.Columns["TimeLogID"].Visible = false;  // It is only used for deletion and edit

         _mnItmEdit_DeleteEntry.Enabled = _grdTimeEntry.SelectedRows.Count != 0;

         _mnItmView_Detailed.Checked = true;
         _mnItmView_DailyTaskSummary.Checked = false;
         _mnItmView_DailySummary.Checked = false;
         _mnItmView_TaskSummary.Checked = false;
         _mnItmView_DailyTaskStatus.Checked = false;
      }

      #endregion

      #region ShowDailyTaskSummaryView

      private void ShowDailyTaskSummaryView(object sender, EventArgs e)
      {
         DailyTaskSummary rptVwDailyTaskSummary = new DailyTaskSummary();

         _grdTimeEntry.DataSource = null;
         _grdTimeEntry.DataSource = rptVwDailyTaskSummary.Fetch(_connection, _sUserId);

         _grdTimeEntry.Columns["TaskType"].Visible = false;  // It is only used for the initial sort.

         _mnItmEdit_DeleteEntry.Enabled = false;

         _mnItmView_Detailed.Checked = false;
         _mnItmView_DailyTaskSummary.Checked = true;
         _mnItmView_DailySummary.Checked = false;
         _mnItmView_TaskSummary.Checked = false;
         _mnItmView_DailyTaskStatus.Checked = false;

         #region Old SQL Script

         //StringBuilder strSql = new StringBuilder();

         //strSql.AppendLine("SELECT NumMonth, NumDay, SUM(Time) * 24 AS ElapsedTime, Task ");
         //strSql.AppendLine("FROM (SELECT DATEPART(mm, ActionTime) AS NumMonth, DATEPART(dd, ActionTime) AS NumDay, ");
         //strSql.AppendLine(             "SUM(CONVERT(float, ActionTime)) * - 1 AS Time, UserId, ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) INNER JOIN Tasks T ON (TKL.TaskID = T.TaskID) ");
         //strSql.AppendLine(      "WHERE ActionTypeID = 0 ");
         //strSql.AppendLine(      "GROUP BY DATEPART(mm, ActionTime), DATEPART(dd, ActionTime), UserId, ActionTypeID, Task ");

         //strSql.AppendLine(      "UNION ");

         //strSql.AppendLine(      "SELECT DATEPART(mm, ActionTime) AS NumMonth, DATEPART(dd, ActionTime) AS NumDay, ");
         //strSql.AppendLine(             "SUM(CONVERT(float, ActionTime)) AS Time, UserId, ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) INNER JOIN Tasks T ON (TKL.TaskID = T.TaskID) ");
         //strSql.AppendLine(      "WHERE ActionTypeID = 1 ");
         //strSql.AppendLine(      "GROUP BY DATEPART(mm, ActionTime), DATEPART(dd, ActionTime), UserId, ActionTypeID, Task ");

         //strSql.AppendLine(      "UNION ");

         //strSql.AppendLine(      "SELECT DATEPART(mm, GETDATE()) AS NumMonth, DATEPART(dd, GETDATE()) AS NumDay, ");
         //strSql.AppendLine(             "CASE WHEN (ActionTypeID = 0) THEN CONVERT(float, GETDATE()) ELSE 0 END AS Time, ");
         //strSql.AppendLine(             "UserID, 1 AS ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM (SELECT TOP 1 TML.ActionTime, TML.ActionTypeID, TKL.UserID, T.Task FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) INNER JOIN Tasks T ON (TKL.TaskID = T.TaskID) ORDER BY ActionTime DESC) AS LSTACT) AS TBL ");
         //strSql.AppendLine("WHERE UserId = '" + _sUserId + "' ");
         //strSql.AppendLine("GROUP BY NumMonth, NumDay, Task ");

         //strSql.AppendLine("UNION ");

         //strSql.AppendLine("SELECT  NumMonth, NumDay, SUM(Time) * 24 AS ElapsedTime, '---TOTAL_FOR_DAY---' AS Task ");
         //strSql.AppendLine("FROM (SELECT DATEPART(mm, ActionTime) AS NumMonth, DATEPART(dd, ActionTime) AS NumDay, ");
         //strSql.AppendLine(             "SUM(CONVERT(float, ActionTime)) * - 1 AS Time, UserId, ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) INNER JOIN Tasks T ON (TKL.TaskID = T.TaskID) ");
         //strSql.AppendLine(      "WHERE ActionTypeID = 0 ");
         //strSql.AppendLine(      "GROUP BY DATEPART(mm, ActionTime), DATEPART(dd, ActionTime), UserId, ActionTypeID, Task ");

         //strSql.AppendLine(      "UNION ");

         //strSql.AppendLine(      "SELECT DATEPART(mm, ActionTime) AS NumMonth, DATEPART(dd, ActionTime) AS NumDay, ");
         //strSql.AppendLine(             "SUM(CONVERT(float, ActionTime)) AS Time, UserId, ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) INNER JOIN Tasks T ON (TKL.TaskID = T.TaskID) ");
         //strSql.AppendLine(      "WHERE ActionTypeID = 1 ");
         //strSql.AppendLine(      "GROUP BY DATEPART(mm, ActionTime), DATEPART(dd, ActionTime), UserId, ActionTypeID, Task ");

         //strSql.AppendLine(      "UNION ");

         //strSql.AppendLine(      "SELECT DATEPART(mm, GETDATE()) AS NumMonth, DATEPART(dd, GETDATE()) AS NumDay, ");
         //strSql.AppendLine(             "CASE WHEN (ActionTypeID = 0) THEN CONVERT(float, GETDATE()) ELSE 0 END AS Time, ");
         //strSql.AppendLine(             "UserID, 1 AS ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM (SELECT TOP 1 TML.ActionTime, TML.ActionTypeID, TKL.UserID, T.Task FROM TimeLog TML INNER JOIN TaskLog TKL ON (TML.TaskLogID = TKL.TaskLogID) INNER JOIN Tasks T ON (TKL.TaskID = T.TaskID) ORDER BY ActionTime DESC) AS LSTACT) AS TBL ");
         //strSql.AppendLine("WHERE UserId = '" + _sUserId + "' ");
         //strSql.AppendLine("GROUP BY NumMonth, NumDay ");

         //strSql.AppendLine("ORDER BY NumMonth, NumDay");

         #endregion
      }

      #endregion

      #region ShowDailySummaryView

      private void ShowDailySummaryView(object sender, EventArgs e)
      {
         DailySummary rptVwDailySummary = new DailySummary();

         _grdTimeEntry.DataSource = null;
         _grdTimeEntry.DataSource = rptVwDailySummary.Fetch(_connection, _sUserId);

         _mnItmEdit_DeleteEntry.Enabled = false;

         _mnItmView_Detailed.Checked = false;
         _mnItmView_DailyTaskSummary.Checked = false;
         _mnItmView_DailySummary.Checked = true;
         _mnItmView_TaskSummary.Checked = false;
         _mnItmView_DailyTaskStatus.Checked = false;

         #region Old Script

         //StringBuilder strSql = new StringBuilder();

         //strSql.AppendLine("SELECT  NumMonth, NumDay, SUM(Time) * 24 AS ElapsedTime ");
         //strSql.AppendLine("FROM (SELECT DATEPART(mm, ActionTime) AS NumMonth, DATEPART(dd, ActionTime) AS NumDay, ");
         //strSql.AppendLine(             "SUM(CONVERT(float, ActionTime)) * - 1 AS Time, UserId, ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM TimeLog ");
         //strSql.AppendLine(      "WHERE ActionTypeID = 0 ");
         //strSql.AppendLine(      "GROUP BY DATEPART(mm, ActionTime), DATEPART(dd, ActionTime), UserId, ActionTypeID, Task ");

         //strSql.AppendLine(      "UNION ");

         //strSql.AppendLine(      "SELECT DATEPART(mm, ActionTime) AS NumMonth, DATEPART(dd, ActionTime) AS NumDay, ");
         //strSql.AppendLine(             "SUM(CONVERT(float, ActionTime)) AS Time, UserId, ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM TimeLog AS TimeLog_1 ");
         //strSql.AppendLine(      "WHERE ActionTypeID = 1 ");
         //strSql.AppendLine(      "GROUP BY DATEPART(mm, ActionTime), DATEPART(dd, ActionTime), UserId, ActionTypeID, Task ");

         //strSql.AppendLine(      "UNION ");

         //strSql.AppendLine(      "SELECT DATEPART(mm, GETDATE()) AS NumMonth, DATEPART(dd, GETDATE()) AS NumDay, ");
         //strSql.AppendLine(             "CASE WHEN (ActionTypeID = 0) THEN CONVERT(float, GETDATE()) ELSE 0 END AS Time, ");
         //strSql.AppendLine(             "UserID, 1 AS ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM (SELECT TOP 1 * FROM TimeLog ORDER BY ActionTime DESC) AS LSTACT) AS TBL ");
         //strSql.AppendLine("WHERE UserId = '" + _sUserId + "' ");
         //strSql.AppendLine("GROUP BY NumMonth, NumDay ");
         //strSql.AppendLine("ORDER BY NumMonth, NumDay");

         #endregion
      }

      #endregion

      #region ShowTaskSummary

      private void ShowTaskSummary(object sender, EventArgs e)
      {
         TaskSummary rptVwTaskSummary = new TaskSummary();

         _grdTimeEntry.DataSource = null;
         _grdTimeEntry.DataSource = rptVwTaskSummary.Fetch(_connection, _sUserId);

         _mnItmEdit_DeleteEntry.Enabled = false;

         _mnItmView_Detailed.Checked = false;
         _mnItmView_DailyTaskSummary.Checked = false;
         _mnItmView_DailySummary.Checked = false;
         _mnItmView_TaskSummary.Checked = true;
         _mnItmView_DailyTaskStatus.Checked = false;

         #region Old Script

         //StringBuilder strSql = new StringBuilder();

         //strSql.AppendLine("SELECT Task, SUM(Time) * 24 AS ElapsedTime ");
         //strSql.AppendLine("FROM (SELECT DATEPART(mm, ActionTime) AS NumMonth, DATEPART(dd, ActionTime) AS NumDay, ");
         //strSql.AppendLine(             "SUM(CONVERT(float, ActionTime)) * -1 AS Time, UserId, ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM TimeLog ");
         //strSql.AppendLine(      "WHERE ActionTypeID = 0 ");
         //strSql.AppendLine(      "GROUP BY DATEPART(mm, ActionTime), DATEPART(dd, ActionTime), UserId, ActionTypeID, Task ");

         //strSql.AppendLine(      "UNION ");

         //strSql.AppendLine(      "SELECT DATEPART(mm, ActionTime) AS NumMonth, DATEPART(dd, ActionTime) AS NumDay, ");
         //strSql.AppendLine(             "SUM(CONVERT(float, ActionTime)) AS Time, UserId, ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM TimeLog AS TimeLog_1 ");
         //strSql.AppendLine(      "WHERE ActionTypeID = 1 ");
         //strSql.AppendLine(      "GROUP BY DATEPART(mm, ActionTime), DATEPART(dd, ActionTime), UserId, ActionTypeID, Task ");

         //strSql.AppendLine(      "UNION ");

         //strSql.AppendLine(      "SELECT DATEPART(mm, GETDATE()) AS NumMonth, DATEPART(dd, GETDATE()) AS NumDay, ");
         //strSql.AppendLine(             "CASE WHEN (ActionTypeID = 0) THEN CONVERT(float, GETDATE()) ELSE 0 END AS Time, ");
         //strSql.AppendLine(             "UserID, 1 AS ActionTypeID, Task ");
         //strSql.AppendLine(      "FROM (SELECT TOP 1 * FROM TimeLog ORDER BY ActionTime DESC) AS LSTACT) AS TBL ");
         //strSql.AppendLine("WHERE UserId = '" + _sUserId + "' ");
         //strSql.AppendLine("GROUP BY Task ");
         //strSql.AppendLine("ORDER BY Task");

         #endregion
      }

      #endregion

      #region ShowDailyTaskStatus

      private void ShowDailyTaskStatus(object sender, EventArgs e)
      {
         DailyTaskStatus rptVwDailyTaskStatus = new DailyTaskStatus();

         _grdTimeEntry.DataSource = null;
         _grdTimeEntry.DataSource = rptVwDailyTaskStatus.Fetch(_connection, _sUserId);

         _mnItmEdit_DeleteEntry.Enabled = false;

         _mnItmView_Detailed.Checked = false;
         _mnItmView_DailyTaskSummary.Checked = false;
         _mnItmView_DailySummary.Checked = false;
         _mnItmView_TaskSummary.Checked = false;
         _mnItmView_DailyTaskStatus.Checked = true;
      }

      #endregion

      #endregion

      #region Task Handlers

      #region Start Task

      #region ShowStartTaskEntryForm

      private void ShowStartTaskEntryForm(object sender, EventArgs e)
      {
         if (TaskEntryForm.Show(_connection, false) == DialogResult.OK)
         {
            StartTask(TaskEntryForm.Task);
         } // end if
      } // end ShowStartTaskEntryForm

      #endregion

      #region StartTask

      private void StartTask(object sender, EventArgs e)
      {
         if (_toolCmbxTasks_Tasks.Text.Length == 0)
         {
            MessageBox.Show(this, "Please enter a task to start.", "Start Task Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
         } // end if
         else
         {
            StartTask(_toolCmbxTasks_Tasks.Text);
         } // end else
      } // end StartTask

      private void StartTask(string sTask)
      {
         if (Start.Execute(_connection, _sUserId, sTask.Replace("'", "''")) <= 0)
         {
            MessageBox.Show(this, "There was an error starting the task.", "Start Task Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
         } // end if
         else
         {
            RefreshReportView(null, null);

            LoadTasks();

            _toolBtnTasks_Stop.Enabled = _mnItmTools_StopTask.Enabled = true;
         } // end else
      } // end StartTask

      #endregion

      #endregion

      #region Resume Task

      #region ShowResumeTaskEntryForm

      private void ShowResumeTaskEntryForm(object sender, EventArgs e)
      {
         if (TaskEntryForm.Show(_connection, true, _sUserId) == DialogResult.OK)
         {
            ResumeTask(TaskEntryForm.Task);
         } // end if
      }

      #endregion

      #region ResumeTask

      private void ResumeTask(object sender, EventArgs e)
      {
         if (_toolCmbxTasks_Tasks.Text.Length == 0)
         {
            ResumeTask(null);
         } // end if
         else
         {
            ResumeTask(_toolCmbxTasks_Tasks.Text);
         } // end else
      } // end ResumeTask

      private void ResumeTask(string sTask)
      {
         int iResult = Resume.Execute(_connection, _sUserId, sTask.Replace("'", "''"));

         if (iResult == 0)
         {
            MessageBox.Show(this, "There was an error resuming the task.", "Resume Task Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
         } // end if
         else if (iResult == -1)
         {
            MessageBox.Show(this, "The specified task does not exist in order to resume.",
                            "Resume Task Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         } // end else if
         else
         {
            RefreshReportView(null, null);

            _toolBtnTasks_Stop.Enabled = _mnItmTools_StopTask.Enabled = true;
         } // end else
      } // end ResumeTask

      #endregion

      #endregion

      #region Stop Task

      #region StopTask

      private void StopTask(object sender, EventArgs e)
      {
         int iResult;

         iResult = Stop.Execute(_connection, _sUserId);
         if (iResult == 0)
         {
            MessageBox.Show(this, "There are currently no tasks started.", "Stop Task Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
         } // end if
         else if (iResult == -1)
         {
            MessageBox.Show(this, "There was an error stopping the task.", "Stop Task Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
         } // end else if
         else
         {
            RefreshReportView(null, null);

            _toolBtnTasks_Stop.Enabled = _mnItmTools_StopTask.Enabled = false;
         } // end else
      } // end StopTask

      #endregion

      #endregion

      #endregion

      #region LoadTasks

      private void LoadTasks()
      {
         _toolCmbxTasks_Tasks.ComboBox.DataSource = Task.GetAll(_connection, _sUserId);
         _toolCmbxTasks_Tasks.ComboBox.DisplayMember = "Task";
         _toolCmbxTasks_Tasks.ComboBox.ValueMember = "Task";

         SelectActiveTask();
      } // end LoadTasks

      #endregion

      #region SelectActiveTask

      private void SelectActiveTask()
      {
         string sCurrentTask;
         int iActiveTaskIndex;

         if ((_sUserId == null) || (_sUserId.Length == 0))
         {
            _toolCmbxTasks_Tasks.ComboBox.SelectedIndex = -1;
         }
         else
         {
            sCurrentTask = Task.GetActive(_connection, _sUserId);
            if (sCurrentTask == null)
            {
               _toolCmbxTasks_Tasks.ComboBox.SelectedIndex = -1;
            }
            else
            {
               iActiveTaskIndex = _toolCmbxTasks_Tasks.ComboBox.FindStringExact(sCurrentTask);

               _toolCmbxTasks_Tasks.ComboBox.SelectedIndex = (iActiveTaskIndex > 0) ? iActiveTaskIndex : -1;
            }
         }
      }

      #endregion

      #region SelectRow

      private void SelectRow(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
      {
         if (e.RowIndex == -1)
         {
         } // end if
         else
         {
            _grdTimeEntry.Rows[e.RowIndex].Selected = true;

            if (_mnItmView_Detailed.Checked)
            {
               _mnItmEdit_DeleteEntry.Enabled = true;
            }
         } // end else
      }

      #endregion

      #region UnselectRow

      private void UnselectRow(object sender, DataGridViewCellEventArgs e)
      {
         if (_mnItmView_Detailed.Checked)
         {
            _mnItmEdit_DeleteEntry.Enabled = false;
         }
      }

      #endregion

      #region ResizeForm

      private void ResizeForm(object sender, System.EventArgs e)
      {
         _grdTimeEntry.Height = Height - 111;
         _grdTimeEntry.Width = Width - 32;
      } // end ResizeForm

      #endregion

      #endregion
   }
}