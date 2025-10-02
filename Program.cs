namespace MCP
{
   internal class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Master Control Programm gestartet...");

         // 1. Prüfen, ob überhaupt Argumente übergeben wurden
         if (args.Length == 0)
         {
            Console.WriteLine("Kein Befehl angegeben. Verwenden Sie 'where' für den aktuellen Pfad.");
            return;
         }

         // Der erste Eintrag ist der Befehl (z.B. "connect")
         string command = args[0].ToLowerInvariant();

         // 2. Das Switch-Statement (Die Dispatch-Tabelle)
         switch (command)
         {
            case "where":
               // Abfrage des aktuellen Arbeitsverzeichnisses
               string currentPath = Directory.GetCurrentDirectory(); 
         
               // Ausgabe des Pfades
               Console.WriteLine($"Du befindest dich im Pfad: {currentPath}");
               break;
         
            default:
               Console.WriteLine($"Fehler: Unbekannter Befehl '{command}'.");
               return;
         }
      }      
   }
}
