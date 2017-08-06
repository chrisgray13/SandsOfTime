namespace SandsOfTime.DialogForms
{
   partial class LoginForm
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
         this._lblUserId = new System.Windows.Forms.Label();
         this._txtUserId = new System.Windows.Forms.TextBox();
         this._btnOk = new System.Windows.Forms.Button();
         this._btnCancel = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // _lblUserId
         // 
         this._lblUserId.AutoSize = true;
         this._lblUserId.Location = new System.Drawing.Point(20, 36);
         this._lblUserId.Name = "_lblUserId";
         this._lblUserId.Size = new System.Drawing.Size(46, 13);
         this._lblUserId.TabIndex = 0;
         this._lblUserId.Text = "User ID:";
         // 
         // _txtUserId
         // 
         this._txtUserId.AcceptsReturn = true;
         this._txtUserId.Location = new System.Drawing.Point(72, 33);
         this._txtUserId.Name = "_txtUserId";
         this._txtUserId.Size = new System.Drawing.Size(127, 20);
         this._txtUserId.TabIndex = 1;
         this._txtUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SubmitOnEnter);
         // 
         // _btnOk
         // 
         this._btnOk.Location = new System.Drawing.Point(48, 88);
         this._btnOk.Name = "_btnOk";
         this._btnOk.Size = new System.Drawing.Size(75, 23);
         this._btnOk.TabIndex = 2;
         this._btnOk.Text = "OK";
         this._btnOk.UseVisualStyleBackColor = true;
         this._btnOk.Click += new System.EventHandler(this.LoginUser);
         // 
         // _btnCancel
         // 
         this._btnCancel.Location = new System.Drawing.Point(133, 88);
         this._btnCancel.Name = "_btnCancel";
         this._btnCancel.Size = new System.Drawing.Size(75, 23);
         this._btnCancel.TabIndex = 3;
         this._btnCancel.Text = "Cancel";
         this._btnCancel.UseVisualStyleBackColor = true;
         this._btnCancel.Click += new System.EventHandler(this.CancelLogin);
         // 
         // LoginForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(259, 143);
         this.Controls.Add(this._btnCancel);
         this.Controls.Add(this._btnOk);
         this.Controls.Add(this._txtUserId);
         this.Controls.Add(this._lblUserId);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "LoginForm";
         this.Text = "Login";
         this.TopMost = true;
         this.Load += new System.EventHandler(this.LoadForm);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label _lblUserId;
      private System.Windows.Forms.TextBox _txtUserId;
      private System.Windows.Forms.Button _btnOk;
      private System.Windows.Forms.Button _btnCancel;
   }
}