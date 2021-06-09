using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Makezip
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                ZipFile(args[0], args[1]);
                Console.WriteLine("1");
            }
            else { Console.WriteLine("2");}
        }
        public static void CreateZipFile(string filename)
        {
            //Create the header of the Zip File 
            System.Text.ASCIIEncoding Encoder = new System.Text.ASCIIEncoding();
            string sHeader = "PK" + (char)5 + (char)6;
            sHeader = sHeader.PadRight(22, (char)0);
            //Convert to byte array
            byte[] baHeader = System.Text.Encoding.ASCII.GetBytes(sHeader);

            //Save File - Make sure your file ends with .zip!
            FileStream fs = File.Create(filename);
            fs.Write(baHeader, 0, baHeader.Length);
            fs.Flush();
            fs.Close();
            fs = null;
        }
        public static void ZipFile(string Input, string Filename)
        {
            Shell32.Shell Shell = new Shell32.Shell();

            //Create our Zip File
            CreateZipFile(Filename);

            //Copy the file or folder to it
            Shell.NameSpace(Filename).CopyHere(Input, 0);

            //If you can write the code to wait for the code to finish, please let me know
            System.Threading.Thread.Sleep(1000);

        }
    }
}
