using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Resources;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine(assembly.GetManifestResourceNames().Length);



            try
            {
                string gzipFileName = assembly.GetManifestResourceNames()[0];
                Console.WriteLine(gzipFileName);
                Stream compressedStream = assembly.GetManifestResourceStream(gzipFileName);

                string encFileName = assembly.GetManifestResourceNames()[1];
                Console.WriteLine(gzipFileName);
                Stream encStream = assembly.GetManifestResourceStream(encFileName);
                byte[] v= new byte[encStream.Length];
                encStream.Read(v,0,v.Length);
                File.WriteAllBytes("xx.xml", v);
                string pwd = "";
                string hash = "";
                string InputPwd = Console.ReadLine();
                //   string infoFileName = assembly.GetManifestResourceNames()[1];

                ResourceReader res = new ResourceReader(compressedStream);
                IDictionaryEnumerator dict = res.GetEnumerator();
                Stream sss = null;
                while (dict.MoveNext())
                {

                    if (dict.Key.ToString().ToUpper() == "CCD_PASS")
                    {
                        pwd = dict.Value.ToString();
                        Console.WriteLine(pwd);
                    }
                    //else if (dict.Key.ToString().ToUpper() == "CCD_FILES")
                    //{
                    //    byte[] file = (byte[])dict.Value;
                    //    sss = new MemoryStream(file);
                    //    Console.WriteLine(file.Length);
                    //}
                    else if (dict.Key.ToString().ToUpper() == "CCD_HASH")
                    {
                        hash = dict.Value.ToString();
                        Console.WriteLine(hash);
                    }
                }

                pwd = VICEncryption.Decrypt(pwd, hash);
                if (InputPwd == pwd)
                {
                    //StreamReader sr = new StreamReader(assembly.GetManifestResourceStream(infoFileName));
                    Console.WriteLine(pwd);
                    //string pwd = sr.ReadLine();
                   // Stream ss = VICEncryption.Decrypt(encStream,@"D:\asd.xml", pwd);
                    VICEncryption.Decrypt("xx.xml", @"D:\asd.xml", pwd);
                    //ss.Seek(0, 0);
                    ////sr.Close();
                    //Console.WriteLine("output:" + ss.Length.ToString());
                    //byte[] buffer = new byte[1024 * 1024]; // 1 MB say
                    //int bytesRead;
                    //using (FileStream decompressedStream = new FileStream(@"D:\asd.xml", FileMode.Create, FileAccess.Write, FileShare.None))
                    //{
                    //    Console.WriteLine("Start file");
                    //    while ((bytesRead = ss.Read(buffer, 0, buffer.Length)) > 0)
                    //    {
                    //        decompressedStream.Write(buffer, 0, bytesRead);
                    //        Console.WriteLine(bytesRead.ToString());
                    //    }
                    //    ss.Seek(0, 0);
                    //    Console.WriteLine("\n\n hash  g  :" + VICEncryption.GenerateHash(ss));
                    //    Console.WriteLine("\n\n hash  b  :" + VICEncryption.GenerateHash(decompressedStream));
                    //}
                    //ss.Close();

                    //Console.WriteLine("\n\n hash:" + hash);

                    //if (hash == VICEncryption.GenerateHash(ss)) { Console.WriteLine("File is intact"); }
                    //else { Console.WriteLine("File integrity faild!!"); }
                }
                else
                {
                    Console.WriteLine("Wrong Password");
                }
                //  res.Close();
                //compressedStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("pop" + e.Message + "\n" + e.StackTrace + "\n");
            }

            //byte[] buffer = new byte[1024 * 1024]; // 1 MB say
            //int bytesRead;
            //using (FileStream decompressedStream = new FileStream(@"D:\aa.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    while ((bytesRead = decompressedStream.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        decompressedStream.Write(buffer, 0, bytesRead);
            //    }
            //}
            //Console.WriteLine("helo pankaj");
            Console.ReadKey();
        }
    }
    public class VICEncryption
    {
        public static string GenerateHash(Stream file)
        {
            try
            {
                string key = "";
                using (SHA1 shaCripto = new SHA1CryptoServiceProvider())
                {
                    byte[] hashKey = shaCripto.ComputeHash(file);
                    key = BitConverter.ToString(hashKey).Replace("-", string.Empty);
                    // file.Close();
                    // System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                }
                return key;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        // Decrypt a byte array into a byte array using a key and an IV
        public static byte[] Decrypt(byte[] cipherData,
                                    byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the
            // decrypted bytes
            MemoryStream ms = new MemoryStream();
            // Create a symmetric algorithm.
            // We are going to use Rijndael because it is strong and
            // available on all platforms.
            // You can use other algorithms, to do so substitute the next
            // line with something like
            //     TripleDES alg = TripleDES.Create();
            Rijndael alg = Rijndael.Create();
            // Now set the key and the IV.
            // We need the IV (Initialization Vector) because the algorithm
            // is operating in its default
            // mode called CBC (Cipher Block Chaining). The IV is XORed with
            // the first block (8 byte)
            // of the data after it is decrypted, and then each decrypted
            // block is XORed with the previous
            // cipher block. This is done to make encryption more secure.
            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure.
            alg.Key = Key;
            alg.IV = IV;
            // Create a CryptoStream through which we are going to be
            // pumping our data.
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream
            // and the output will be written in the MemoryStream
            // we have provided.
            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);
            // Write the data and make it do the decryption
            cs.Write(cipherData, 0, cipherData.Length);
            // Close the crypto stream (or do FlushFinalBlock).
            // This will tell it that we have done our decryption
            // and there is no more data coming in,
            // and it is now a good time to remove the padding
            // and finalize the decryption process.
            cs.Close();
            // Now get the decrypted data from the MemoryStream.
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way.
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        // Decrypt a string into a string using a password
        //    Uses Decrypt(byte[], byte[], byte[])
        public static string Decrypt(string cipherText, string Password)
        {
            // First we need to turn the input string into a byte array.
            // We presume that Base64 encoding was used
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            // Then, we need to turn the password into Key and IV
            // We are using salt to make it harder to guess our key
            // using a dictionary attack -
            // trying to guess a password by enumerating all possible words.
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            // Now get the key/IV and do the decryption using
            // the function that accepts byte arrays.
            // Using PasswordDeriveBytes object we are first
            // getting 32 bytes for the Key
            // (the default Rijndael key length is 256bit = 32bytes)
            // and then 16 bytes for the IV.
            // IV should always be the block size, which is by
            // default 16 bytes (128 bit) for Rijndael.
            // If you are using DES/TripleDES/RC2 the block size is
            // 8 bytes and so should be the IV size.
            // You can also read KeySize/BlockSize properties off
            // the algorithm to find out the sizes.
            byte[] decryptedData = Decrypt(cipherBytes,
               pdb.GetBytes(32), pdb.GetBytes(16));
            // Now we need to turn the resulting byte array into a string.
            // A common mistake would be to use an Encoding class for that.
            // It does not work
            // because not all byte values can be represented by characters.
            // We are going to be using Base64 encoding that is
            // designed exactly for what we are trying to do.
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        // Decrypt bytes into bytes using a password
        //    Uses Decrypt(byte[], byte[], byte[])
        public static byte[] Decrypt(byte[] cipherData, string Password)
        {
            // We need to turn the password into Key and IV.
            // We are using salt to make it harder to guess our key
            // using a dictionary attack -
            // trying to guess a password by enumerating all possible words.
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            // Now get the key/IV and do the Decryption using the
            //function that accepts byte arrays.
            // Using PasswordDeriveBytes object we are first getting
            // 32 bytes for the Key
            // (the default Rijndael key length is 256bit = 32bytes)
            // and then 16 bytes for the IV.
            // IV should always be the block size, which is by default
            // 16 bytes (128 bit) for Rijndael.
            // If you are using DES/TripleDES/RC2 the block size is
            // 8 bytes and so should be the IV size.
            // You can also read KeySize/BlockSize properties off the
            // algorithm to find out the sizes.
            return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        // Decrypt a file into another file using a password
        public static void Decrypt(string fileIn,
                    string fileOut, string Password)
        {
            // First we are going to open the file streams
            FileStream fsIn = new FileStream(fileIn,
                        FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut,
                        FileMode.OpenOrCreate, FileAccess.Write);
            // Then we are going to derive a Key and an IV from
            // the Password and create an algorithm
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            Aes alg = new AesCryptoServiceProvider();
            //  Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
            // Now create a crypto stream through which we are going
            // to be pumping data.
            // Our fileOut is going to be receiving the Decrypted bytes.
            CryptoStream cs = new CryptoStream(fsOut,
                alg.CreateDecryptor(), CryptoStreamMode.Write);
            // Now will will initialize a buffer and will be
            // processing the input file in chunks.
            // This is done to avoid reading the whole file (which can be
            // huge) into memory.
            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;
            do
            {
                // read a chunk of data from the input file
                bytesRead = fsIn.Read(buffer, 0, bufferLen);
                // Decrypt it
                cs.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
            // close everything
            cs.Close(); // this will also close the unrelying fsOut stream
            fsIn.Close();
        }

        // Decrypt a file into another file using a password
        public static Stream Decrypt(Stream fsIn,
                   string Password)
        {
            // First we are going to open the file streams
            Stream s = new MemoryStream();

            // Then we are going to derive a Key and an IV from
            // the Password and create an algorithm
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            Aes alg = new AesCryptoServiceProvider();
            //  Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
            // Now create a crypto stream through which we are going
            // to be pumping data.
            // Our fileOut is going to be receiving the Decrypted bytes.
            CryptoStream cs = new CryptoStream(s,
                alg.CreateDecryptor(), CryptoStreamMode.Write);
            // Now will will initialize a buffer and will be
            // processing the input file in chunks.
            // This is done to avoid reading the whole file (which can be
            // huge) into memory.
            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;
            fsIn.CopyTo(cs);
            //do
            //{
            //    // read a chunk of data from the input file
            //    bytesRead = fsIn.Read(buffer, 0, bufferLen);
            //    // Decrypt it
            //    cs.Write(buffer, 0, bytesRead);
            //} while (bytesRead != 0);
            
            //  close everything
            // cs.Close(); // this will also close the unrelying fsOut stream
            fsIn.Close();
            return s;
        }

        // Decrypt a file into another file using a password
        public static void Decrypt(Stream fsIn, string fileOut,
                   string Password)
        {
            FileStream fsOut = new FileStream(fileOut,
                           FileMode.OpenOrCreate, FileAccess.Write);
            // First we are going to open the file streams
           
            // Then we are going to derive a Key and an IV from
            // the Password and create an algorithm
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            Aes alg = new AesCryptoServiceProvider();
            //  Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
            // Now create a crypto stream through which we are going
            // to be pumping data.
            // Our fileOut is going to be receiving the Decrypted bytes.
            CryptoStream cs = new CryptoStream(fsOut,
                alg.CreateDecryptor(), CryptoStreamMode.Write);
            // Now will will initialize a buffer and will be
            // processing the input file in chunks.
            // This is done to avoid reading the whole file (which can be
            // huge) into memory.
            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;
            fsIn.CopyTo(cs);
            //do
            //{
            //    // read a chunk of data from the input file
            //    bytesRead = fsIn.Read(buffer, 0, bufferLen);
            //    // Decrypt it
            //    cs.Write(buffer, 0, bytesRead);
            //} while (bytesRead != 0);

            //  close everything
            // cs.Close(); // this will also close the unrelying fsOut stream
            fsIn.Close();
            
        }
        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
