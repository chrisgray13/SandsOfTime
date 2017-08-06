using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SandsOfTime.Actions;

namespace SandsOfTime
{
   static class Program
   {
      [System.Runtime.InteropServices.DllImport( "kernel32.dll" )]
      static extern bool AttachConsole( int dwProcessId );
      private const int ATTACH_PARENT_PROCESS = -1;

      #region DisplayUsage

      private static void DisplayUsage()
      {
         MessageBox.Show("Usage:\n" +
                            "    SandsOfTime [UserId] [ActionTypeID] [Task | Number of tasks]\n\n" +
                            "    UserId - Id of the user for which the ActionTypeID is being logged\n" +
                            "    ActionTypeID - ActionTypeID to be logged.\n" +
                            "                0 - Start;\n" +
                            "                1 - Stop;\n" +
                            "                2 - Resume previous;\n" +
                            "                3 - Rename current;\n" +
                            "                4 - List tasks;\n" +
                            "               99 - Report\n" +
                            "    Task - Optional.  Task related to the ActionTypeID.\n" +
                            "           Required for ActionTypeID 3.\n" +
                            "    Number of Tasks - Optional.  Number of last tasks to display.\n" +
                            "                      This is to be used with ActionTypeID 4.",
                         "Usage",
                         MessageBoxButtons.OK);
      } // end DisplayUsage

      #endregion

      /// <summary>
      /// The main entry point for the application.  This accepts no arguments, which will launch
      /// the GUI form of the application or two or three arguments, which will perform an ActionTypeID
      /// in the background.  Anything else will display a usage popup.
      /// </summary>
      [STAThread]
      static void Main(string[] sArguments)
      {
         ActionTypes actionType;

         if (sArguments.Length == 0)
         {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SandsOfTimeForm());
         } // end if
         else if ((sArguments.Length == 1) && (sArguments[0] == "-i"))
         {
            InstallDatabase();
         }
         else if (sArguments.Length == 2)
         {
            actionType = Enum.IsDefined(typeof(ActionTypes), sArguments[1]) ?
                            (ActionTypes) Enum.Parse(typeof(ActionTypes), sArguments[1]) : ActionTypes.Unknown;
            if (((actionType == ActionTypes.Stop) || (actionType == ActionTypes.Resume) || (actionType == ActionTypes.List)))
            {
               // redirect console output to parent process;
               // must be before any calls to Console.WriteLine()
               AttachConsole(ATTACH_PARENT_PROCESS);
               Console.WriteLine(ActionHandler.HandleAction(sArguments[0], actionType));
            } // end if
            else
            {
               DisplayUsage();
            } // end else
         } // end if
         else if (sArguments.Length == 3)
         {
            actionType = Enum.IsDefined(typeof(ActionTypes), sArguments[1]) ?
                            (ActionTypes)Enum.Parse(typeof(ActionTypes), sArguments[1]) : ActionTypes.Unknown;
            if (((actionType == ActionTypes.Start) || (actionType == ActionTypes.Resume) || (actionType == ActionTypes.Rename)) &&
                (sArguments[2].Trim().Length > 0))
            {
               // redirect console output to parent process;
               // must be before any calls to Console.WriteLine()
               AttachConsole(ATTACH_PARENT_PROCESS);
               Console.WriteLine(ActionHandler.HandleAction(sArguments[0], actionType, sArguments[2].Trim()));
            } // end if
            else
            {
               DisplayUsage();
            } // end else
         } // end if
         else
         {
            DisplayUsage();
         } // end else
      } // end Main

      #region InstallDatabase

      private static void InstallDatabase()
      {
         System.IO.StreamReader sr;

         try
         {
            using (GraySystem.Data.Connection connection =
                      new GraySystem.Data.Connection(System.Configuration.ConfigurationManager.ConnectionStrings["SandsOfTime"].ConnectionString))
            {
               using (sr = new System.IO.StreamReader("TimeLog.TAB"))
               {
                  connection.ExecuteNonQuery(sr.ReadToEnd());
               } // end using

               using (sr = new System.IO.StreamReader("TaskLog.TAB"))
               {
                  connection.ExecuteNonQuery(sr.ReadToEnd());
               } // end using

               using (sr = new System.IO.StreamReader("Tasks.TAB"))
               {
                  connection.ExecuteNonQuery(sr.ReadToEnd());
               } // end using

               using (sr = new System.IO.StreamReader("TaskStatuses.TAB"))
               {
                  connection.ExecuteNonQuery(sr.ReadToEnd());
               } // end using
            } // end using
         } // end try
         catch (Exception ex)
         {
            AttachConsole(ATTACH_PARENT_PROCESS);
            Console.WriteLine(ex.ToString());
         } // end catch
      }

      #endregion
   }
}