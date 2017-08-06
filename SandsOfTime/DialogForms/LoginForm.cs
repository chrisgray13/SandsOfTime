#region Usings

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion


namespace SandsOfTime.DialogForms
{
   public partial class LoginForm : Form
   {
      #region Fields

      private static string _sUserId;

      #endregion

      #region Properties

      #region UserID

      public static string UserID
      {
         get { return (_sUserId); }
      } // end UserID property

      #endregion

      #endregion

      #region Constructors

      private LoginForm()
      {
         InitializeComponent();
      }

      #endregion

      #region Methods

      #region Show

      public new static DialogResult Show()
      {
         LoginForm frmLogin = new LoginForm();

         frmLogin.DialogResult = frmLogin.ShowDialog();

         return (frmLogin.DialogResult);
      } // end Show

      public static DialogResult Show(bool bUseActiveDirectorySecurity)
      {
         if (bUseActiveDirectorySecurity)
         {
            _sUserId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            return (DialogResult.OK);
         }
         else
         {
            return (Show());
         }
      } // end Show

      public new static DialogResult Show(IWin32Window owner)
      {
         LoginForm frmLogin = new LoginForm();

         frmLogin.DialogResult = frmLogin.ShowDialog(owner);

         return (frmLogin.DialogResult);
      } // end Show

      public static DialogResult Show(IWin32Window owner, bool bUseActiveDirectorySecurity)
      {
         if (bUseActiveDirectorySecurity)
         {
            _sUserId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            return (DialogResult.OK);
         }
         else
         {
            return (Show(owner));
         }
      } // end Show

      #endregion

      #region LoadForm

      private void LoadForm(object sender, EventArgs e)
      {
         _txtUserId.Text = String.Empty;
      } // end LoadForm

      #endregion

      #region LoginUser

      private void LoginUser(object sender, EventArgs e)
      {
         _sUserId = _txtUserId.Text.Trim();

         this.DialogResult = System.Windows.Forms.DialogResult.OK;

         Close();
      } // end LoginUser

      #endregion

      #region CancelLogin

      private void CancelLogin(object sender, EventArgs e)
      {
         this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

         Close();
      } // end CancelLogin

      #endregion

      #region SubmitOnEnter

      private void SubmitOnEnter(object sender, System.Windows.Forms.KeyPressEventArgs e)
      {
         if ((int) e.KeyChar == 13)
         {
            LoginUser(sender, e);
            e.Handled = true;
         } // end if
      } // end SubmitOnEnter

      #endregion

      #endregion
   } // end LoginForm Class
} // end SandsOfTime.DialogForms Namespace