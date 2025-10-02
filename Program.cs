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
        }

        // Der erste Eintrag ist der Befehl (z.B. "connect")
        string command = args[0].ToLowerInvariant();

        // 2. Das Switch-Statement (Die Dispatch-Tabelle)
        switch (command)
        {
            case "connect":
                Console.WriteLine("Master Control Programm gestartet...");   
                break;
            default:
                Console.WriteLine($"Fehler: Unbekannter Befehl '{command}'.");
                return;
        }
        }
    }
}
