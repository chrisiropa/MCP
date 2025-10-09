using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP
{
	public class Decrypt : ICommand
	{
		public void HandleCommand(string[] commandArgs)
		{
			if(commandArgs.Length > 0) 
			{
				Console.WriteLine("Decrypt {0} = {1}",commandArgs[0], CryptoHelper.Decrypt(commandArgs[0]));
			}
		}
	}
}
