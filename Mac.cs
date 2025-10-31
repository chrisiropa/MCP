using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP
{
	public class Mac : BaseCommand, ICommand
	{
		private string mac = "";
		
		protected override string Parameters 
      { 
         get
         {
            return "status, encrypt, set, read";
         } 
      }
		public void HandleCommand(string[] commandArgs)
		{
			if(commandArgs.Length == 0) 
			{	
				ShowHelp();
				return;	
			}

			switch(commandArgs[0]) 
			{
				case "set":
					Set(commandArgs);
				break;
				case "read":
					Read();
				break;
				case "status":
					Status();
				break;
				case "encrypt":
					Encrypt(commandArgs);
				break;
				default:
					ShowHelp();
				break;
			}
		}

		private void Set(string[] commandArgs)
		{
			if(commandArgs.Length > 1)
			{
				if(IsValidMac(commandArgs[1]))
				{
					mac = commandArgs[1];
					Console.WriteLine("MAC-Adresse gesetzt = {0}", mac);
				}
				else
				{
					Console.WriteLine("Übergebene MAC-Adresse ungültig = {0}", commandArgs[1]);
				}
			}
		}

		private void Read()
		{
			mac = MachineHelpers.GetPrimaryMacAddress();
			Console.WriteLine("MAC-Adresse gelesen = {0}", mac);
		}

		private void Status()
		{
			Console.WriteLine("Aktuelle MAC-Adresse = {0}", mac);
		}

		private void Encrypt(string[] commandArgs)
		{
			string mac2Encrypt = "";
			if(commandArgs.Length == 1) 
			{
				mac2Encrypt = mac;				
			}
			else if(commandArgs.Length == 2)
			{
				//Zweiter Parameter ist eine (nicht lokale) gültige MAC-Adresse
				if(IsValidMac(commandArgs[1]))
				{					
					mac2Encrypt = commandArgs[1];
				}
				else
				{					
					Console.WriteLine("Übergebene MAC-Adresse ungültig = {0}", commandArgs[1]);
					return;
				}
			}

			if(mac2Encrypt.Length == 0)
			{
				Console.WriteLine("Keine MAC-Adresse zum Encrypten. mac read ausführen");
			}
			else
			{
				string encrypt = CryptoHelper.Encrypt(mac2Encrypt);
				Console.WriteLine("Ermittelte MAC-Adresse encrypt = {0}", encrypt);

				string decrypt = CryptoHelper.Decrypt(encrypt);
				Console.WriteLine("Ermittelte MAC-Adresse decrypt = {0}", decrypt);
			}
		}

		public static bool IsValidMac(string mac)
		{
			if (string.IsNullOrWhiteSpace(mac))
			{
				return false;
			}

			// Trennzeichen entfernen
			string clean = mac.Replace(":", "").Replace("-", "");
    
			if (clean.Length != 12)
			{
				return false;
			}

			foreach (char c in clean)
			{
				if (!Uri.IsHexDigit(c)) // prüft ob Hexadezimal
				{
					return false;
				}
			}

			return true;
		}
	}
}
