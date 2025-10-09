using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP
{
	public class Dir : ICommand
	{
      public void HandleCommand(string[] commandArgs)
		{
         string path = Directory.GetCurrentDirectory();
 
         try
         {
         Console.WriteLine();
         Console.WriteLine($" Verzeichnis von {path}");
         Console.WriteLine();

         string[] dirs = Directory.GetDirectories(path);
         string[] files = Directory.GetFiles(path);

         foreach (var dir in dirs)
         {
            var info = new DirectoryInfo(dir);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{info.LastWriteTime:dd.MM.yyyy HH:mm}    <DIR>          {info.Name}");
            Console.ResetColor();
         }

         foreach (var file in files)
         {
            var info = new FileInfo(file);
            Console.WriteLine($"{info.LastWriteTime:dd.MM.yyyy HH:mm}                 {info.Length,10:N0} {info.Name}");
         }

         Console.WriteLine();
         Console.WriteLine($"    {dirs.Length} Verzeichnis(se), {files.Length} Datei(en)");
         }
         catch (Exception ex)
         {
            Console.WriteLine($"Fehler beim Zugriff auf {path}: {ex.Message}");
         }
      }
	}
}
