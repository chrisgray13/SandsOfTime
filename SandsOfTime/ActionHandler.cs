#region Usings

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using GraySystem.Data;

using SandsOfTime.Actions;

#endregion

namespace SandsOfTime
{
   /// <summary>
   /// Start - stops any started tasks, which should be at most one, and starts the specified
   /// Stop  - stops any started tasks, which should be at most one
   /// Resume - stops any started tasks, which should be at most one, and starts the specified task,
   ///          which should exist in the database.  If it does not, throw an error with the option to
   ///          view the last x started tasks and rollback stopped tasks.  If no task is specified, the
   ///          last stopped task will be started.
   /// Rename - renames the currently started task.  If no tasks are started, then it throws an error.
   /// List - lists the most recent tasks.
   /// Report - displays a grouping of time with total elapsed tme per day given the date range.  If a
   ///          task is specified, only that task will be displayed; otherwise, all tasks for the date
   ///          range.
   /// </summary>
   public class ActionHandler
   {
      public ActionHandler()
      {
      } // end ActionHandler

      #region Methods

      #region HandleAction

      public static bool HandleAction(string sUserId, ActionTypes actionType)
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (HandleAction(connection, sUserId, actionType, null));
         } // end using
      } // end HandleAction

      public static bool HandleAction(Connection connection, string sUserId, ActionTypes actionType)
      {
         return (HandleAction(connection, sUserId, actionType, null));
      } // end HandleAction

      public static bool HandleAction(string sUserId, ActionTypes actionType, string sArg3)
      {
         using (Connection connection = new Connection(ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
         {
            return (HandleAction(connection, sUserId, actionType, sArg3));
         } // end using
      } // end HandleAction

      public static bool HandleAction(Connection connection, string sUserId, ActionTypes actionType, string sArg3)
      {
         int iArg3;

         switch (actionType)
         {
            case SandsOfTime.Actions.ActionTypes.Start:
               return (Start.Execute(connection, sUserId, sArg3) > 0);
            case SandsOfTime.Actions.ActionTypes.Stop:
               return (Stop.Execute(connection, sUserId) > 0);
            case SandsOfTime.Actions.ActionTypes.Resume:
               return (Resume.Execute(connection, sUserId, sArg3) > 0);
            case SandsOfTime.Actions.ActionTypes.Rename:
               return (Rename.Execute(connection, sUserId, sArg3) > 0);
            case SandsOfTime.Actions.ActionTypes.List:
               return (List.Execute(connection, sUserId, int.TryParse(sArg3, out iArg3) ? iArg3 : 0) > 0);
            default:
               return (false);
         } // end switch
      } // end HandleAction

      #endregion

      #endregion
   } // end ActionHandler Class
} // SandsOfTime Namespace