using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MCP
{
	public class Expire : BaseCommand, ICommand
	{
		private string expire = "";

		protected override string Parameters 
      { 
         get
         {
            return "status, encrypt, expire set dd.MM.yyyy HH:mm:ss --(Uhrzeit optional)";
         } 
      }


		public void HandleCommand(string[] commandArgs)
		{
			string combineParameters = "";
			if(commandArgs.Length == 0) 
			{
				ShowHelp();
				return;
			}

			switch(commandArgs[0]) 
			{	
				case "status":
					Console.WriteLine("Aktueller expire = {0}", expire);
				break;
				case "encrypt":
					Encrypt(commandArgs);
				break;
				case "set":
					if(commandArgs.Length < 2)
					{
						Console.WriteLine("Kein Datum angegeben !");
						return;
					}

					combineParameters = commandArgs[1];
					if(commandArgs.Length == 3)
					{
						//Den dritten Parameter kombinieren, da die Uhrzeit vom Datum 
						//durch ein Leerzeichen getrennt ist. Und dies als eigener
						//Parameter gilt.
						combineParameters += commandArgs[2];
					}

					SetExpire(combineParameters);
				break;
				default:
					ShowHelp();
				break;
			}
		}

		private void Encrypt(string[] commandArgs)
		{
			string expireEncrypt = "";
			if(commandArgs.Length == 1) 
			{
				expireEncrypt = expire;				
			}

			if(expireEncrypt.Length == 0)
			{
				Console.WriteLine("Kein Datum zum Encrypten gefunden -> Setzen mit 'expire set dd.MM.yyyy HH:mm:ss --(Uhrzeit optional)'");

			}
			else
			{
				string encrypt = CryptoHelper.Encrypt(expireEncrypt);
				Console.WriteLine("Verschlüsseltes expire Datum = {0}", encrypt);

				string decrypt = CryptoHelper.Decrypt(encrypt);
				Console.WriteLine("Entschlüsseltes expire Datum = {0}", decrypt);
			}
		}
		private void SetExpire(string dateTime)
		{
			dateTime = dateTime.Replace(" ","");
			string[] formats = { "dd.MM.yyyyHH:mm:ss", "dd.MM.yyyy" };
			DateTime result;

			if (DateTime.TryParseExact(dateTime, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
			{
				expire = result.ToString("dd.MM.yyyy HH:mm:ss");
				Console.WriteLine("Datum gesetzt: {0}", expire);
				return;
			}

			Console.WriteLine("Ungültiges Datum -> expire set dd.MM.yyyy HH:mm:ss --(Uhrzeit optional)");
		}
	}
}
