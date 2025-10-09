using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MCP
{
	public class MachineHelpers
	{
		public static string GetPrimaryMacAddress()
      {
         var ifaces = NetworkInterface.GetAllNetworkInterfaces()
            .Where(n => n.OperationalStatus == OperationalStatus.Up)
            .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Where(n => !n.Description.ToLowerInvariant().Contains("virtual") && !n.Name.ToLowerInvariant().Contains("virtual"))
            .OrderByDescending(n => n.Speed) // pref. schnellste NIC
            .ToArray();

         var nic = ifaces.FirstOrDefault();
         if (nic == null)
         {
            // fallback: erste non-loopback
            nic = NetworkInterface.GetAllNetworkInterfaces()
                  .FirstOrDefault(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback);
            if (nic == null) return string.Empty;
         }

         var mac = nic.GetPhysicalAddress().GetAddressBytes();
         return BitConverter.ToString(mac).Replace("-", "").ToLowerInvariant();
      }
	}
}
