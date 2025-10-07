using System.Diagnostics;



namespace MCP
{
   
      
   public class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Master Control Programm gestartet...");

         
         string command;
         string[] commandArgs = args.Skip(1).ToArray();
                  

         if(Debugger.IsAttached)
         {
            command = "sign";
            Directory.SetCurrentDirectory(@"C:\Projekte\Iropa\Datenclient TIA\DatenClieNT\bin\Debug");
         }
         else
         {
            if (args.Length == 0)
            {
               ShowHelp();
               return;
            }

            command = args[0].ToLowerInvariant();
            
         }


         switch (command)
         {
            case "sign": 
               string fileToSign = "DatenClieNT_TIA.exe";

               if(!Debugger.IsAttached)
               {
                  fileToSign = Directory.GetCurrentDirectory() + "\\" + commandArgs[0];
               }
               
               //signiert eine Datei (gedacht für DatenClieNT.exe)
               Console.WriteLine("Signiere {0}", fileToSign);

					Signer signer = new Signer(fileToSign);
               
               signer.Start();
               
               
               break;
            case "where":
               Console.WriteLine($"Du befindest dich im Pfad: {Directory.GetCurrentDirectory()}");
               break;
            
            default:
               Console.WriteLine($"Fehler: Unbekannter Befehl '{command}'.");
               ShowHelp();
               break;
         }

         if(Debugger.IsAttached)
         {
            Console.ReadLine();
         }

      }

      private static void ShowHelp()
      {
         Console.WriteLine("\nVerwendung: MCP [Befehl] [Optionen]");
         Console.WriteLine("Befehle:");
         Console.WriteLine("   where                  - Zeigt den aktuellen Pfad an.");
      }
   }
}
