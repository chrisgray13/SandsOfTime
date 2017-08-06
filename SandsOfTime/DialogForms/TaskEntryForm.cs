#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GraySystem.Data;
using SandsOfTime.Data;

#endregion


namespace SandsOfTime.DialogForms
{
   public partial class TaskEntryForm : Form
   {
      #region Fields

      private Connection _connection;
      private bool _bShowTasks;
      private string _sUserId;

      private static string _sTask;

      #endregion

      #region Properties

      #region Task

      public static string Task
      {
         get { return (((_sTask == null) || (_sTask.Length == 0)) ? null : _sTask); }
      } // end Task property

      #endregion

      #endregion

      #region Constructors

      private TaskEntryForm()
      {
         InitializeComponent();
      }

      private TaskEntryForm(Connection connection, bool bShowTasks) : this(connection, bShowTasks, null)
      {
      } // TaskEntryForm

      private TaskEntryForm(Connection connection, bool bShowTasks, string sUserId)
      {
         _connection = connection;
         _bShowTasks = bShowTasks;
         _sUserId = ((sUserId == null) || (sUserId.Length == 0)) ? null : sUserId;

         _sTask = null;

         InitializeComponent();
      } // TaskEntryForm

      #endregion

      #region Methods

      #region Show

      public static DialogResult Show(Connection connection, bool bShowTasks)
      {
         return (Show(connection, bShowTasks, null));
      } // end Show

      public static DialogResult Show(Connection connection, bool bShowTasks, string sUserId)
      {
         TaskEntryForm frmTaskEntry = new TaskEntryForm(connection, bShowTasks, sUserId);

         frmTaskEntry.DialogResult = frmTaskEntry.ShowDialog();

         return (frmTaskEntry.DialogResult);
      } // end Show

      public static DialogResult Show(IWin32Window owner, Connection connection, bool bShowTasks)
      {
         return (Show(owner, connection, bShowTasks, null));
      } // end Show

      public static DialogResult Show(IWin32Window owner, Connection connection, bool bShowTasks,
                                      string sUserId)
      {
         TaskEntryForm frmTaskEntry = new TaskEntryForm(connection, bShowTasks, sUserId);

         frmTaskEntry.DialogResult = frmTaskEntry.ShowDialog(owner);

         return (frmTaskEntry.DialogResult);
      } // end Show

      #endregion

      #region LoadForm

      private void LoadForm(object sender, EventArgs e)
      {
         if (_bShowTasks)
         {
            _txtTask.Visible = false;
            _cmbTask.Visible = true;

            _cmbTask.DataSource = SandsOfTime.Data.Task.GetAll(_connection, _sUserId);
            _cmbTask.DisplayMember = "Task";
            _cmbTask.ValueMember = "Task";
         } // end if
         else
         {
            _cmbTask.Visible = false;
            _txtTask.Visible = true;

            _txtTask.Text = String.Empty;
         } // end else
      } // end LoadForm

      #endregion

      #region Submit

      private void Submit(object sender, EventArgs e)
      {
         if (_bShowTasks)
         {
            if ((_cmbTask.SelectedIndex == -1) && (_cmbTask.Text.Trim().Length > 0))
            {
               MessageBox.Show(this, "The specified task does not exist in order to resume.",
                               "Resume Task Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end if
            else
            {
               _sTask = _cmbTask.Text.Trim();

               this.DialogResult = System.Windows.Forms.DialogResult.OK;

               Close();
            } // end else
         } // end if
         else
         {
            _sTask = _txtTask.Text.Trim();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            Close();
         } // end else
      } // end Submit

      #endregion

      #region SubmitOnEnter

      private void SubmitOnEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
      {
         if ((int) e.KeyChar == 13)
         {
            Submit(sender, e);
            e.Handled = true;
         } // end if
      } // end SubmitOnEnter

      #endregion

      #region Cancel

      private void Cancel(object sender, System.EventArgs e)
      {
         this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

         Close();
      } // end Cancel

      #endregion

      #endregion
   }
} // end SandsOfTime.DialogForms Namespace