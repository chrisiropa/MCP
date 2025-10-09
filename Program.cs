using System.Diagnostics;



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
         }

         

         CLI cli = new CLI(oneWay, args);


         cli.Run();


         Console.WriteLine("Master Control Programm beendet...");
         Thread.Sleep(1000);

      }

   }
}
