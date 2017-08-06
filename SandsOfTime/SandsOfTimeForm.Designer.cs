namespace SandsOfTime
{
   partial class SandsOfTimeForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SandsOfTimeForm));
         this._menu = new System.Windows.Forms.MenuStrip();
         this._mnItmFile = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmFile_LogIn = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmFile_LogOut = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmFile_Separator1 = new System.Windows.Forms.ToolStripSeparator();
         this._mnItmFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmEdit = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmEdit_EditEntry = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmEdit_DeleteEntry = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmView = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmView_Detailed = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmView_DailyTaskSummary = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmView_DailySummary = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmView_TaskSummary = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmTools = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmTools_StartTask = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmTools_ResumeTask = new System.Windows.Forms.ToolStripMenuItem();
         this._mnItmTools_StopTask = new System.Windows.Forms.ToolStripMenuItem();
         this._grdTimeEntry = new System.Windows.Forms.DataGridView();
         this._toolbarTasks = new System.Windows.Forms.ToolStrip();
         this._toolCmbxTasks_Tasks = new System.Windows.Forms.ToolStripComboBox();
         this._toolBtnTasks_Start = new System.Windows.Forms.ToolStripButton();
         this._toolBtnTasks_Resume = new System.Windows.Forms.ToolStripButton();
         this._toolBtnTasks_Stop = new System.Windows.Forms.ToolStripButton();
         this._mnItmView_Separator1 = new System.Windows.Forms.ToolStripSeparator();
         this._mnItmView_DailyTaskStatus = new System.Windows.Forms.ToolStripMenuItem();
         this._menu.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this._grdTimeEntry)).BeginInit();
         this._toolbarTasks.SuspendLayout();
         this.SuspendLayout();
         // 
         // _menu
         // 
         this._menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
         this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnItmFile,
            this._mnItmEdit,
            this._mnItmView,
            this._mnItmTools});
         this._menu.Location = new System.Drawing.Point(0, 0);
         this._menu.Name = "_menu";
         this._menu.Size = new System.Drawing.Size(751, 24);
         this._menu.TabIndex = 0;
         this._menu.Text = "Menu";
         // 
         // _mnItmFile
         // 
         this._mnItmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnItmFile_LogIn,
            this._mnItmFile_LogOut,
            this._mnItmFile_Separator1,
            this._mnItmFile_Exit});
         this._mnItmFile.Name = "_mnItmFile";
         this._mnItmFile.Size = new System.Drawing.Size(35, 20);
         this._mnItmFile.Text = "&File";
         // 
         // _mnItmFile_LogIn
         // 
         this._mnItmFile_LogIn.Name = "_mnItmFile_LogIn";
         this._mnItmFile_LogIn.Size = new System.Drawing.Size(123, 22);
         this._mnItmFile_LogIn.Text = "Log In";
         this._mnItmFile_LogIn.Click += new System.EventHandler(this.LogInUser);
         // 
         // _mnItmFile_LogOut
         // 
         this._mnItmFile_LogOut.Enabled = false;
         this._mnItmFile_LogOut.Name = "_mnItmFile_LogOut";
         this._mnItmFile_LogOut.Size = new System.Drawing.Size(123, 22);
         this._mnItmFile_LogOut.Text = "Log Out";
         this._mnItmFile_LogOut.Click += new System.EventHandler(this.LogOutUser);
         // 
         // _mnItmFile_Separator1
         // 
         this._mnItmFile_Separator1.Name = "_mnItmFile_Separator1";
         this._mnItmFile_Separator1.Size = new System.Drawing.Size(120, 6);
         // 
         // _mnItmFile_Exit
         // 
         this._mnItmFile_Exit.Name = "_mnItmFile_Exit";
         this._mnItmFile_Exit.Size = new System.Drawing.Size(123, 22);
         this._mnItmFile_Exit.Text = "E&xit";
         this._mnItmFile_Exit.Click += new System.EventHandler(this.ExitApplication);
         // 
         // _mnItmEdit
         // 
         this._mnItmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnItmEdit_EditEntry,
            this._mnItmEdit_DeleteEntry});
         this._mnItmEdit.Name = "_mnItmEdit";
         this._mnItmEdit.Size = new System.Drawing.Size(37, 20);
         this._mnItmEdit.Text = "&Edit";
         // 
         // _mnItmEdit_EditEntry
         // 
         this._mnItmEdit_EditEntry.Enabled = false;
         this._mnItmEdit_EditEntry.Name = "_mnItmEdit_EditEntry";
         this._mnItmEdit_EditEntry.Size = new System.Drawing.Size(145, 22);
         this._mnItmEdit_EditEntry.Text = "Edit Entry";
         this._mnItmEdit_EditEntry.Click += new System.EventHandler(this.EditEntry);
         // 
         // _mnItmEdit_DeleteEntry
         // 
         this._mnItmEdit_DeleteEntry.Enabled = false;
         this._mnItmEdit_DeleteEntry.Name = "_mnItmEdit_DeleteEntry";
         this._mnItmEdit_DeleteEntry.Size = new System.Drawing.Size(145, 22);
         this._mnItmEdit_DeleteEntry.Text = "Delete Entry";
         this._mnItmEdit_DeleteEntry.Click += new System.EventHandler(this.DeleteEntry);
         // 
         // _mnItmView
         // 
         this._mnItmView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnItmView_Detailed,
            this._mnItmView_DailyTaskSummary,
            this._mnItmView_DailySummary,
            this._mnItmView_TaskSummary,
            this._mnItmView_Separator1,
            this._mnItmView_DailyTaskStatus});
         this._mnItmView.Name = "_mnItmView";
         this._mnItmView.Size = new System.Drawing.Size(41, 20);
         this._mnItmView.Text = "&View";
         // 
         // _mnItmView_Detailed
         // 
         this._mnItmView_Detailed.CheckOnClick = true;
         this._mnItmView_Detailed.Name = "_mnItmView_Detailed";
         this._mnItmView_Detailed.Size = new System.Drawing.Size(180, 22);
         this._mnItmView_Detailed.Text = "Detailed";
         this._mnItmView_Detailed.Click += new System.EventHandler(this.ShowDetailedView);
         // 
         // _mnItmView_DailyTaskSummary
         // 
         this._mnItmView_DailyTaskSummary.CheckOnClick = true;
         this._mnItmView_DailyTaskSummary.Name = "_mnItmView_DailyTaskSummary";
         this._mnItmView_DailyTaskSummary.Size = new System.Drawing.Size(180, 22);
         this._mnItmView_DailyTaskSummary.Text = "Daily Task Summary";
         this._mnItmView_DailyTaskSummary.Click += new System.EventHandler(this.ShowDailyTaskSummaryView);
         // 
         // _mnItmView_DailySummary
         // 
         this._mnItmView_DailySummary.CheckOnClick = true;
         this._mnItmView_DailySummary.Name = "_mnItmView_DailySummary";
         this._mnItmView_DailySummary.Size = new System.Drawing.Size(180, 22);
         this._mnItmView_DailySummary.Text = "Daily Summary";
         this._mnItmView_DailySummary.Click += new System.EventHandler(this.ShowDailySummaryView);
         // 
         // _mnItmView_TaskSummary
         // 
         this._mnItmView_TaskSummary.CheckOnClick = true;
         this._mnItmView_TaskSummary.Name = "_mnItmView_TaskSummary";
         this._mnItmView_TaskSummary.Size = new System.Drawing.Size(180, 22);
         this._mnItmView_TaskSummary.Text = "Task Summary";
         this._mnItmView_TaskSummary.Click += new System.EventHandler(this.ShowTaskSummary);
         // 
         // _mnItmTools
         // 
         this._mnItmTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnItmTools_StartTask,
            this._mnItmTools_ResumeTask,
            this._mnItmTools_StopTask});
         this._mnItmTools.Name = "_mnItmTools";
         this._mnItmTools.Size = new System.Drawing.Size(44, 20);
         this._mnItmTools.Text = "&Tools";
         // 
         // _mnItmTools_StartTask
         // 
         this._mnItmTools_StartTask.Enabled = false;
         this._mnItmTools_StartTask.Name = "_mnItmTools_StartTask";
         this._mnItmTools_StartTask.Size = new System.Drawing.Size(148, 22);
         this._mnItmTools_StartTask.Text = "Start Task";
         this._mnItmTools_StartTask.Click += new System.EventHandler(this.ShowStartTaskEntryForm);
         // 
         // _mnItmTools_ResumeTask
         // 
         this._mnItmTools_ResumeTask.Enabled = false;
         this._mnItmTools_ResumeTask.Name = "_mnItmTools_ResumeTask";
         this._mnItmTools_ResumeTask.Size = new System.Drawing.Size(148, 22);
         this._mnItmTools_ResumeTask.Text = "Resume Task";
         this._mnItmTools_ResumeTask.Click += new System.EventHandler(this.ShowResumeTaskEntryForm);
         // 
         // _mnItmTools_StopTask
         // 
         this._mnItmTools_StopTask.Enabled = false;
         this._mnItmTools_StopTask.Name = "_mnItmTools_StopTask";
         this._mnItmTools_StopTask.Size = new System.Drawing.Size(148, 22);
         this._mnItmTools_StopTask.Text = "Stop Task";
         this._mnItmTools_StopTask.Click += new System.EventHandler(this.StopTask);
         // 
         // _grdTimeEntry
         // 
         this._grdTimeEntry.AllowUserToAddRows = false;
         this._grdTimeEntry.AllowUserToDeleteRows = false;
         this._grdTimeEntry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
         this._grdTimeEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this._grdTimeEntry.Location = new System.Drawing.Point(12, 65);
         this._grdTimeEntry.Name = "_grdTimeEntry";
         this._grdTimeEntry.ReadOnly = true;
         this._grdTimeEntry.Size = new System.Drawing.Size(727, 642);
         this._grdTimeEntry.TabIndex = 1;
         this._grdTimeEntry.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UnselectRow);
         this._grdTimeEntry.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SelectRow);
         this._grdTimeEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DeleteEntry);
         // 
         // _toolbarTasks
         // 
         this._toolbarTasks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolCmbxTasks_Tasks,
            this._toolBtnTasks_Start,
            this._toolBtnTasks_Resume,
            this._toolBtnTasks_Stop});
         this._toolbarTasks.Location = new System.Drawing.Point(0, 24);
         this._toolbarTasks.Name = "_toolbarTasks";
         this._toolbarTasks.Size = new System.Drawing.Size(751, 25);
         this._toolbarTasks.TabIndex = 2;
         this._toolbarTasks.Text = "Task Toolbar";
         // 
         // _toolCmbxTasks_Tasks
         // 
         this._toolCmbxTasks_Tasks.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
         this._toolCmbxTasks_Tasks.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
         this._toolCmbxTasks_Tasks.DropDownWidth = 321;
         this._toolCmbxTasks_Tasks.Enabled = false;
         this._toolCmbxTasks_Tasks.MaxDropDownItems = 20;
         this._toolCmbxTasks_Tasks.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
         this._toolCmbxTasks_Tasks.Name = "_toolCmbxTasks_Tasks";
         this._toolCmbxTasks_Tasks.Size = new System.Drawing.Size(221, 25);
         this._toolCmbxTasks_Tasks.Sorted = true;
         // 
         // _toolBtnTasks_Start
         // 
         this._toolBtnTasks_Start.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this._toolBtnTasks_Start.Enabled = false;
         this._toolBtnTasks_Start.Image = global::SandsOfTime.Properties.Resources.Start;
         this._toolBtnTasks_Start.ImageTransparentColor = System.Drawing.Color.Magenta;
         this._toolBtnTasks_Start.Name = "_toolBtnTasks_Start";
         this._toolBtnTasks_Start.Size = new System.Drawing.Size(23, 22);
         this._toolBtnTasks_Start.Text = "Start";
         this._toolBtnTasks_Start.ToolTipText = "Start Task";
         this._toolBtnTasks_Start.Click += new System.EventHandler(this.StartTask);
         // 
         // _toolBtnTasks_Resume
         // 
         this._toolBtnTasks_Resume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this._toolBtnTasks_Resume.Enabled = false;
         this._toolBtnTasks_Resume.Image = global::SandsOfTime.Properties.Resources.Resume;
         this._toolBtnTasks_Resume.ImageTransparentColor = System.Drawing.Color.Magenta;
         this._toolBtnTasks_Resume.Name = "_toolBtnTasks_Resume";
         this._toolBtnTasks_Resume.Size = new System.Drawing.Size(23, 22);
         this._toolBtnTasks_Resume.Text = "Resume";
         this._toolBtnTasks_Resume.ToolTipText = "Resume Task";
         this._toolBtnTasks_Resume.Click += new System.EventHandler(this.ResumeTask);
         // 
         // _toolBtnTasks_Stop
         // 
         this._toolBtnTasks_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this._toolBtnTasks_Stop.Enabled = false;
         this._toolBtnTasks_Stop.Image = global::SandsOfTime.Properties.Resources.Stop;
         this._toolBtnTasks_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
         this._toolBtnTasks_Stop.Name = "_toolBtnTasks_Stop";
         this._toolBtnTasks_Stop.Size = new System.Drawing.Size(23, 22);
         this._toolBtnTasks_Stop.Text = "Stop";
         this._toolBtnTasks_Stop.ToolTipText = "Stop Task";
         this._toolBtnTasks_Stop.Click += new System.EventHandler(this.StopTask);
         // 
         // _mnItmView_Separator1
         // 
         this._mnItmView_Separator1.Name = "_mnItmView_Separator1";
         this._mnItmView_Separator1.Size = new System.Drawing.Size(177, 6);
         // 
         // _mnItmView_DailyTaskStatus
         // 
         this._mnItmView_DailyTaskStatus.Name = "_mnItmView_DailyTaskStatus";
         this._mnItmView_DailyTaskStatus.Size = new System.Drawing.Size(180, 22);
         this._mnItmView_DailyTaskStatus.Text = "Daily Task Status";
         this._mnItmView_DailyTaskStatus.Click += new System.EventHandler(this.ShowDailyTaskStatus);
         // 
         // SandsOfTimeForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(751, 719);
         this.Controls.Add(this._toolbarTasks);
         this.Controls.Add(this._grdTimeEntry);
         this.Controls.Add(this._menu);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this._menu;
         this.Name = "SandsOfTimeForm";
         this.Text = "Sands of Time";
         this.Load += new System.EventHandler(this.InitializeForm);
         this.Resize += new System.EventHandler(this.ResizeForm);
         this._menu.ResumeLayout(false);
         this._menu.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this._grdTimeEntry)).EndInit();
         this._toolbarTasks.ResumeLayout(false);
         this._toolbarTasks.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip _menu;
      private System.Windows.Forms.ToolStripMenuItem _mnItmFile;
      private System.Windows.Forms.ToolStripMenuItem _mnItmFile_LogIn;
      private System.Windows.Forms.ToolStripMenuItem _mnItmFile_LogOut;
      private System.Windows.Forms.ToolStripSeparator _mnItmFile_Separator1;
      private System.Windows.Forms.ToolStripMenuItem _mnItmFile_Exit;
      private System.Windows.Forms.ToolStripMenuItem _mnItmEdit;
      private System.Windows.Forms.ToolStripMenuItem _mnItmView;
      private System.Windows.Forms.ToolStripMenuItem _mnItmView_Detailed;
      private System.Windows.Forms.ToolStripMenuItem _mnItmView_DailyTaskSummary;
      private System.Windows.Forms.ToolStripMenuItem _mnItmTools;
      private System.Windows.Forms.ToolStripMenuItem _mnItmView_DailySummary;
      private System.Windows.Forms.ToolStripMenuItem _mnItmEdit_EditEntry;
      private System.Windows.Forms.ToolStripMenuItem _mnItmEdit_DeleteEntry;
      private System.Windows.Forms.ToolStripMenuItem _mnItmTools_StartTask;
      private System.Windows.Forms.ToolStripMenuItem _mnItmTools_ResumeTask;
      private System.Windows.Forms.ToolStripMenuItem _mnItmTools_StopTask;
      private System.Windows.Forms.DataGridView _grdTimeEntry;
      private System.Windows.Forms.ToolStrip _toolbarTasks;
      private System.Windows.Forms.ToolStripComboBox _toolCmbxTasks_Tasks;
      private System.Windows.Forms.ToolStripButton _toolBtnTasks_Start;
      private System.Windows.Forms.ToolStripButton _toolBtnTasks_Resume;
      private System.Windows.Forms.ToolStripButton _toolBtnTasks_Stop;
      private System.Windows.Forms.ToolStripMenuItem _mnItmView_TaskSummary;
      private System.Windows.Forms.ToolStripSeparator _mnItmView_Separator1;
      private System.Windows.Forms.ToolStripMenuItem _mnItmView_DailyTaskStatus;
   }
}

