using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP
{
	public abstract class BaseCommand
	{
		protected abstract string Parameters { get; }

		public virtual void ShowHelp()
		{
			Console.WriteLine("Mögliche Parameter = {0}", Parameters);
		}
	}
}
