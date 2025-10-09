using System.Diagnostics;



namespace MCP
{
   
      
   public class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Master Control Programm gestartet...");

         CLI cli = new CLI();


         cli.Run();


         Console.WriteLine("Master Control Programm beendet...");
         Thread.Sleep(1000);

      }

   }
}
