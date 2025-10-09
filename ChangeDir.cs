using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP
{
	public class ChangeDir : ICommand
	{
      private string currentDirectory = Directory.GetCurrentDirectory();

      public string CurrDir
      {
         get { return currentDirectory; } 
      }
		public void HandleCommand(string[] commandArgs)
      {
          if (commandArgs.Length == 0)
              return;

          string targetPath = commandArgs[0].Trim('"'); // ← WICHTIG!

          // Sonderfall: cd ..
          if (targetPath == "..")
          {
              var parentInfo = Directory.GetParent(currentDirectory);
              if (parentInfo != null)
              {
                  string parent = parentInfo.FullName;
                  Directory.SetCurrentDirectory(parent);
                  currentDirectory = parent;
              }
              else
              {
                  Console.WriteLine("Bereits im Stammverzeichnis.");
              }
              return;
          }

          // Sonderfall: cd .
          if (targetPath == ".")
              return;

          string newPath;

          // Prüfen ob absoluter Pfad mit Laufwerksbuchstabe
          string root = Path.GetPathRoot(targetPath);
          bool isAbsoluteWithDrive = !string.IsNullOrEmpty(root) && root.Contains(':');

          if (isAbsoluteWithDrive)
          {
              newPath = targetPath;
          }
          else if (Path.IsPathRooted(targetPath))
          {
              string drive = Path.GetPathRoot(currentDirectory);
              newPath = Path.Combine(drive, targetPath.TrimStart(Path.DirectorySeparatorChar));
          }
          else
          {
              newPath = Path.Combine(currentDirectory, targetPath);
          }

          newPath = Path.GetFullPath(newPath);

          if (Directory.Exists(newPath))
          {
              Directory.SetCurrentDirectory(newPath);
              currentDirectory = newPath;
          }
          else
          {
              Console.WriteLine($"Das Verzeichnis \"{newPath}\" existiert nicht.");
          }
      }
	}
}
