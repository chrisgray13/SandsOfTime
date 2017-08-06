namespace SandsOfTime.DialogForms
{
   partial class TaskEntryForm
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
         this._lblTask = new System.Windows.Forms.Label();
         this._txtTask = new System.Windows.Forms.TextBox();
         this._cmbTask = new System.Windows.Forms.ComboBox();
         this._btnOk = new System.Windows.Forms.Button();
         this._btnCancel = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // _lblTask
         // 
         this._lblTask.AutoSize = true;
         this._lblTask.Location = new System.Drawing.Point(13, 26);
         this._lblTask.Name = "_lblTask";
         this._lblTask.Size = new System.Drawing.Size(34, 13);
         this._lblTask.TabIndex = 0;
         this._lblTask.Text = "Task:";
         // 
         // _txtTask
         // 
         this._txtTask.Location = new System.Drawing.Point(66, 23);
         this._txtTask.Name = "_txtTask";
         this._txtTask.Size = new System.Drawing.Size(292, 20);
         this._txtTask.TabIndex = 1;
         this._txtTask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SubmitOnEnter);
         // 
         // _cmbTask
         // 
         this._cmbTask.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
         this._cmbTask.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
         this._cmbTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this._cmbTask.FormattingEnabled = true;
         this._cmbTask.ItemHeight = 13;
         this._cmbTask.Location = new System.Drawing.Point(66, 23);
         this._cmbTask.MaxDropDownItems = 20;
         this._cmbTask.Name = "_cmbTask";
         this._cmbTask.Size = new System.Drawing.Size(292, 21);
         this._cmbTask.Sorted = true;
         this._cmbTask.TabIndex = 2;
         this._cmbTask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SubmitOnEnter);
         // 
         // _btnOk
         // 
         this._btnOk.Location = new System.Drawing.Point(105, 93);
         this._btnOk.Name = "_btnOk";
         this._btnOk.Size = new System.Drawing.Size(75, 23);
         this._btnOk.TabIndex = 3;
         this._btnOk.Text = "OK";
         this._btnOk.UseVisualStyleBackColor = true;
         this._btnOk.Click += new System.EventHandler(this.Submit);
         // 
         // _btnCancel
         // 
         this._btnCancel.Location = new System.Drawing.Point(205, 93);
         this._btnCancel.Name = "_btnCancel";
         this._btnCancel.Size = new System.Drawing.Size(75, 23);
         this._btnCancel.TabIndex = 4;
         this._btnCancel.Text = "Cancel";
         this._btnCancel.UseVisualStyleBackColor = true;
         this._btnCancel.Click += new System.EventHandler(this.Cancel);
         // 
         // TaskEntryForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(385, 146);
         this.Controls.Add(this._btnCancel);
         this.Controls.Add(this._btnOk);
         this.Controls.Add(this._cmbTask);
         this.Controls.Add(this._txtTask);
         this.Controls.Add(this._lblTask);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "TaskEntryForm";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.Text = "Task Entry";
         this.Load += new System.EventHandler(this.LoadForm);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label _lblTask;
      private System.Windows.Forms.TextBox _txtTask;
      private System.Windows.Forms.ComboBox _cmbTask;
      private System.Windows.Forms.Button _btnOk;
      private System.Windows.Forms.Button _btnCancel;
   }
}