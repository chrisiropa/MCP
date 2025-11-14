using System.Diagnostics;
using System.Runtime.InteropServices;
using System;



namespace MCP
{
   
      
   public class Program
   {
      static void Main(string[] args)
      {
         Boolean oneWay = false;

         //Einmaliger Gebrauch, wenn das Argument direkt übergeben wird
         //Dann beginnt er keine Schleife, sondern beendet sich direkt nach
         //dem Arbarbeiten des übergebenen Befehls.
         //Das dienst dazu das das MCP Programm auch beim PostBuild-Ereignis
         //des DatenClieNT-Projektes verwendet werden kann. (Signer)
         //Sonst landet der da in einer Endlosschleife...
         if(args.Length > 0)  
         {
            oneWay = true;
            Console.WriteLine("Master Control Programm gestartet: Einmaliger Gebrauch ohne Dauerschleife !");
         }
         else
         {
            Console.WriteLine("Master Control Programm gestartet...");

            SetPath();
         }

         CLI cli = new CLI(oneWay, args);

         cli.Run();

         Console.WriteLine("Master Control Programm beendet...");
         Thread.Sleep(1000);
      }

      static void SetPath()
      {
         string newPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');

         // PATH (Machine) lesen
         string currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);

         if (string.IsNullOrWhiteSpace(currentPath))
         {
            currentPath = "";
         }


         // PATH in einzelne Einträge splitten
         var pathEntries = currentPath.Split(';', StringSplitOptions.RemoveEmptyEntries);

         // Exakte Prüfung
         bool alreadyExists = pathEntries.Any(p => string.Equals(p.TrimEnd('\\'), newPath,StringComparison.OrdinalIgnoreCase));

         if (!alreadyExists)
         {
            string updatedPath = currentPath + ";" + newPath;

            Environment.SetEnvironmentVariable("PATH",updatedPath,EnvironmentVariableTarget.Machine);

            Console.WriteLine("Erster Start ! Pfad wurde zu PATH hinzugefügt: " + newPath);
            Console.WriteLine("CMD/Powershell neu starten, damit die Änderungen wirksam werden !");
         }
      }
   }
}
