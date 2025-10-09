using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MCP
{
	public class CLI
	{
      private Boolean running = true;
		private Mac mac = new Mac();
      private ChangeDir cd = new ChangeDir(); 
      private Dir dir = new Dir();
      private Expire exp = new Expire();
      private Signer signer = new Signer();
      private Decrypt dec = new Decrypt();

      private Boolean oneWay = false;
      private string[] args;

      public CLI(Boolean oneWay, string[] args)
      {
         this.oneWay = oneWay;
         this.args = args;
      }

      private void WritePrompt()
      {
         Console.ForegroundColor = ConsoleColor.Cyan;
         Console.Write(cd.CurrDir);
         Console.ResetColor();

         Console.Write(" ");
         Console.ForegroundColor = ConsoleColor.Green;
         Console.Write("MCP> ");
         Console.ResetColor();
      }
      public void Run()
      {
         if(oneWay)
         {
            string command = "";
            foreach(string arg in args) 
            {
               command += arg;
               command += " ";
            }


            HandleCommand(command.Trim());

            return;
         }
         while (running)
         {
            WritePrompt();

            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) 
            {
               continue;
            }

            HandleCommand(input.Trim());
         }
      }

      private void HandleCommand(string input)
      {
         string[] parts = SplitArgs(input);
         string cmd = parts[0].ToLower();
         string[] args = parts.Skip(1).ToArray();


         switch (cmd)
         {
            case "exit":
            case "quit":
               running = false;
            break;
            case "decrypt":
               Handle(Command.DEC, args);
            break;
            case "expire":
               Handle(Command.EXP, args);
            break;
            case "cd":
               Handle(Command.CD, args);
            break;
            case "cd..":
               if (args.Length == 0)
               {
                  args = new string[] { ".." };
               }
               else
               {
                  args[0] = "..";
               }
               Handle(Command.CD, args);
            break;
            case "dir":
               Handle(Command.DIR, args);
            break;
            case "mac":
               Handle(Command.MAC, args);
            break;
            case "sign": 
               Handle(Command.SIGN, args);               
            break;
            case "where":
               Console.WriteLine($"Du befindest dich im Pfad: {Directory.GetCurrentDirectory()}");
               break;
            
            default:
               Console.WriteLine($"Fehler: Unbekannter Befehl '{cmd}'.");
               break;
         }

         if(Debugger.IsAttached)
         {
            //Console.ReadLine();
         }
      }
		public void Handle(Command cmd, string[] commandArgs)
		{
			switch(cmd) 
			{
				case Command.MAC:
					mac.HandleCommand(commandArgs);
				break;
            case Command.CD:
               cd.HandleCommand(commandArgs);      
            break;
            case Command.DIR:
               dir.HandleCommand(commandArgs);      
            break;
            case Command.EXP:
               exp.HandleCommand(commandArgs);      
            break;
            case Command.SIGN:
               signer.HandleCommand(commandArgs);      
            break;
            case Command.DEC:
               dec.HandleCommand(commandArgs);
            break;
			}
		}


      

      public static string[] SplitArgs(string input)
      {
          var matches = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+");
          var args = new List<string>();
          foreach (Match match in matches)
          {
              string value = match.Value.Trim();
              if (value.StartsWith("\"") && value.EndsWith("\""))
                  value = value.Substring(1, value.Length - 2); // Anführungszeichen entfernen
              args.Add(value);
          }
          return args.ToArray();
      }
	}
}
