using System;
using System.IO;
using System.Linq;

namespace MCP
{
   public class McpInstaller : ICommand
   {
      public void HandleCommand(string[] commandArgs)
      {
         InstallMcpInPath();
      }
      private void InstallMcpInPath(bool systemWide = false)
      {
         try
         {
            string installPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
            Console.WriteLine($"Installationspfad: {installPath}");

            var target = systemWide ? EnvironmentVariableTarget.Machine : EnvironmentVariableTarget.User;
            string scope = systemWide ? "Systemweit" : "Benutzerspezifisch";

            string existingPath = Environment.GetEnvironmentVariable("PATH", target) ?? "";
            var pathEntries = existingPath.Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (pathEntries.Any(p => string.Equals(p, installPath, StringComparison.OrdinalIgnoreCase)))
            {
                  Console.WriteLine($"PATH enthält '{installPath}' bereits. Keine Änderung nötig.");
                  return;
            }

            string updatedPath = existingPath.TrimEnd(';') + ";" + installPath;
            Environment.SetEnvironmentVariable("PATH", updatedPath, target);

            Console.WriteLine($"✅ Pfad erfolgreich hinzugefügt ({scope}).");
            Console.WriteLine("Sie können 'mcp' nun aus jeder Kommandozeile starten.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("WICHTIG: CONSOLE neu starten um damit die Änderungen wirksam werden !");
         }
         catch (Exception ex)
         {
            Console.WriteLine($"❌ Fehler bei der PATH-Installation: {ex.Message}");
         }
      }
   }
}
