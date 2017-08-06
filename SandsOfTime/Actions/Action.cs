#region Usings

using System;
using System.Collections.Generic;
using System.Text;

using GraySystem.Data;

#endregion


namespace SandsOfTime.Actions
{
   interface IAction
   {
      #region Methods

      bool Execute(string sUserId);
      bool Execute(Connection connection, string sUserId);
      bool Execute(string sUserId, string sTask);
      bool Execute(Connection connection, string sUserId, string sTask);

      #endregion
   }
}
