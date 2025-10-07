using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MCP
{
	public class Signer
	{	
		public static class EmbeddedSignature
      {         
         public static readonly byte[] Marker = new byte[20] 
         {
            //Die ersten 20 Nachkommastellen von PI sind der Marker
            (byte)'1',(byte)'4',(byte)'1',(byte)'5',(byte)'9',(byte)'2',(byte)'6',(byte)'5',(byte)'3',(byte)'5',
            (byte)'8',(byte)'9',(byte)'7',(byte)'9',(byte)'3',(byte)'2',(byte)'3',(byte)'8',(byte)'4',(byte)'6'
         };
      }


		private string fileToSign;
		public Signer(string fileToSign)
		{
			this.fileToSign = fileToSign;
		}

		static int IndexOf(byte[] haystack, byte[] needle)
         {
            if (needle.Length == 0) return 0;
            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
               bool ok = true;
               for (int j = 0; j < needle.Length; j++)
               {
                     if (haystack[i + j] != needle[j]) { ok = false; break; }
               }
               if (ok) return i;
            }
            return -1;
         }

		public void Start()
		{
			Console.WriteLine("Öffne Datei {0}...", this.fileToSign);

			if (!File.Exists(this.fileToSign)) 
			{ 
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Datei nicht gefunden !"); 
				Console.ResetColor();
				return; 
			}			
			
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Datei gefunden !"); 
			Console.ResetColor();


         var bin = File.ReadAllBytes(this.fileToSign);

         Console.WriteLine("Platzhalter in der Datei suchen..."); 


         var idx = IndexOf(bin, EmbeddedSignature.Marker);
         if (idx < 0) 
         { 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Platzhalter nicht gefunden ! Abbruch !"); 
            Console.ResetColor();
            return; 
         }

         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine("Platzhalter gefunden an folgendem Offset: 0x{0:x}", idx);
         Console.ResetColor();


         var hashBuffer = new byte[bin.Length];
         Array.Copy(bin, hashBuffer, bin.Length);
         
         int payloadOffset = idx + EmbeddedSignature.Marker.Length; // Payload direkt nach Marker
         int payloadLength = 32; // z.B. SHA-256

         // Vor der Hash-Berechnung Payload nullen
         for (int i = 0; i < payloadLength; i++)
         {
            hashBuffer[payloadOffset + i] = 0;
         }

         Console.WriteLine("payloadOffset = 0x{0:x}", payloadOffset);

         // Compute SHA256 over the modified buffer
         byte[] digest;
         using (var sha = SHA256.Create())
         {
            digest = sha.ComputeHash(hashBuffer);
         }

         //digest[0] = 5;
         //digest[1] = 6;
         //digest[2] = 7;
         //digest[3] = 8;

         Console.WriteLine("SHA256: " + BitConverter.ToString(digest).ToLowerInvariant());

         Array.Copy(digest, 0, bin, payloadOffset, digest.Length);
         


         //BIN-File überschreiben mit eingebautem SHA256-Hash-Code
         File.WriteAllBytes(this.fileToSign, bin);


         //SelfVerifier sv = new SelfVerifier();
         //sv.VerifySelf(this.fileToSign);



         File.WriteAllBytes(this.fileToSign.Replace("exe", "bin"), bin);

         Console.WriteLine($"Signed file written to {this.fileToSign}");



         
		}
	}
}
