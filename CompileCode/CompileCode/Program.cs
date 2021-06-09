using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Security.Cryptography;
using System.CodeDom.Compiler;
using System.IO.Compression;
using System.Diagnostics;
namespace CompileCode
{
    class Program
    {
        static void Main(string[] args)
        {

           GenerateCCDtransmit();

            
            

            // Path to directory of files to compress and decompress.
            //string dirpath = @"D:\CCD\Encrypt";

            //DirectoryInfo di = new DirectoryInfo(dirpath);
            //int i=0;
            // Compress the directory's files.
            //foreach (FileInfo fi in di.GetFiles())
            //{
            //   // Compress(fi);
                
            //}
            //ZIP(@"D:\CCD\Encrypt", @"D:\CCD\Encrypt\a.zip");
           // CreateZipFile(@"D:\CCD\Encrypt\a.zip");

            //ZipFile(@"D:\CCD\testFile\1097421_JOHN_30-01-12_05-50-13.xml", @"D:\CCD\Encrypt\a.zip");
        }

       

        public  static void CreateZipFile(string filename)
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
      
        public static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName)
                    & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream outFile =
                                File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream Compress =
                            new GZipStream(outFile,
                            CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);

                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                                fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }
        public static string GenerateCCDtransmit()
        {
            string InputFileName = @"D:\CCD\testFile\1097421_JOHN_30-01-12_05-50-13.xml";
            string TempFileName = "enc_using_stream_13";
            string Password = VICEncryption.CreateRandomPassword(12);
            string OutputFile = TempFileName + ".xml";
            string hashKey = VICEncryption.GenerateHash(File.ReadAllText(InputFileName));
            string SourceFileName = @"D:\PROJECTS\Tests\R_N_D\CompileCode\skeleton\skeleton.cs";
            string OutputAsmblyName = @"D:\ccd\VIC.com";

            FileStream fs = new FileStream(InputFileName, FileMode.Open);
            
            
            VICEncryption.Encrypt(fs, OutputFile, Password);
         Console.WriteLine(   VICEncryption.GenerateHash(VICEncryption.Decrypt(File.OpenRead(OutputFile), Password)));
            string resourceFileName = TempFileName + ".resource";
            StreamWriter wr = File.CreateText(resourceFileName);
            wr.Close();

            IResourceWriter writer = new ResourceWriter(resourceFileName);

            Console.WriteLine(Password);
            writer.AddResource("CCD_PASS", VICEncryption.Encrypt(Password, hashKey));
            writer.AddResource("CCD_HASH", hashKey);
            // Writes the resources to the file or stream, and closes it.
            writer.Close();

            List<string> filesToEmbed = new List<string>();
            filesToEmbed.Add(resourceFileName);
            filesToEmbed.Add(OutputFile);
            CompileCode(CodeDomProvider.CreateProvider("CSharp"), SourceFileName, filesToEmbed, OutputAsmblyName);
            string exeFile = @"D:\CCD\Encrypt\Makezip.exe";
            //Process p = new Process();
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.FileName = exeFile;// +" " + OutputAsmblyName + " " + @"D:\CCD\Encrypt\a.zip";
            
            //p.Start();
            Process.Start(exeFile, OutputAsmblyName + " " + @"D:\CCD\Encrypt\a.zip");
           // ZipFile(OutputAsmblyName, @"D:\CCD\Encrypt\a.zip");
            return Password;
        }

        public static bool CompileCode(CodeDomProvider provider,    String sourceFile,List<string> filesToEmbed,    String exeFile)
        {

            CompilerParameters cp = new CompilerParameters();

            // Generate an executable instead of 
            // a class library.
            cp.GenerateExecutable = true;

            // Set the assembly file name to generate.
            cp.OutputAssembly = exeFile;

            // Generate debug information.
            cp.IncludeDebugInformation = true;

            // Add an assembly reference.
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Core.dll");
            cp.ReferencedAssemblies.Add("mscorlib.dll");
            cp.ReferencedAssemblies.Add("System.Drawing.dll");
            cp.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            
            // Save the assembly as a physical file.
            cp.GenerateInMemory = false;

            // Set the level at which the compiler 
            // should start displaying warnings.
            cp.WarningLevel = 3;

            // Set whether to treat all warnings as errors.
            cp.TreatWarningsAsErrors = false;

            // Set compiler argument to optimize output.
            cp.CompilerOptions = "/debug-  /optimize+ /target:winexe";// "/optimize";

            // Set a temporary files collection.
            // The TempFileCollection stores the temporary files
            // generated during a build in the current directory,
            // and does not delete them after compilation.
            cp.TempFiles = new TempFileCollection(".", true);

            if (provider.Supports(GeneratorSupport.EntryPointMethod))
            {
                // Specify the class that contains 
                // the main method of the executable.
                cp.MainClass = "skeleton.Program";
            }

            //if (Directory.Exists("Resources"))
            //{
                if (provider.Supports(GeneratorSupport.Resources))
                {
                    // Set the embedded resource file of the assembly.
                    // This is useful for culture-neutral resources,
                    // or default (fallback) resources.

                   // cp.EmbeddedResources.Add(@"D:\PROJECTS\Tests\R_N_D\CompileCode\CompileCode\Extractor\CCD.resource");

                    cp.EmbeddedResources.AddRange(filesToEmbed.ToArray());
                  //  cp.EmbeddedResources.Add("enc_1097421_JOHN_30-01-12_05-50-13.xml"); 
                    // Set the linked resource reference files of the assembly.
                    // These resources are included in separate assembly files,
                    // typically localized for a specific language and culture.
                   // cp.LinkedResources.Add("Resources\\nb-no.resources");
                }
           // }

            // Invoke compilation.
            CompilerResults cr = provider.CompileAssemblyFromFile(cp, sourceFile);

            if (cr.Errors.Count > 0)
            {
                // Display compilation errors.
                //Console.WriteLine("Errors building {0} into {1}",
                //    sourceFile, cr.PathToAssembly);
                //foreach (CompilerError ce in cr.Errors)
                //{
                //    Console.WriteLine("  {0}", ce.ToString());
                //    Console.WriteLine();
                //}
            }
            else
            {
                //Console.WriteLine("Source {0} built into {1} successfully.",
                //    sourceFile, cr.PathToAssembly);
                //Console.WriteLine("{0} temporary files created during the compilation.",
                //    cp.TempFiles.Count.ToString());

            }

            // Return the results of compilation.
            if (cr.Errors.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
                    key=BitConverter.ToString(hashKey).Replace("-", string.Empty);
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
        public static string GenerateHash(byte[] file)
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

        public static string GenerateHash(string str)
        {
            try
            {
                string key = "";
                using (SHA1 shaCripto = new SHA1CryptoServiceProvider())
                {
                    System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
                    byte[] combined = encoder.GetBytes(str);
                   
                    byte[] hashKey = shaCripto.ComputeHash(combined);
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
        // Encrypt a byte array into a byte array using a key and an IV
        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream to accept the encrypted bytes
            MemoryStream ms = new MemoryStream();
            // Create a symmetric algorithm.
            // We are going to use Rijndael because it is strong and
            // available on all platforms.
            // You can use other algorithms, to do so substitute the
            // next line with something like
            //      TripleDES alg = TripleDES.Create();
            Rijndael alg = Rijndael.Create();
            // Now set the key and the IV.
            // We need the IV (Initialization Vector) because
            // the algorithm is operating in its default
            // mode called CBC (Cipher Block Chaining).
            // The IV is XORed with the first block (8 byte)
            // of the data before it is encrypted, and then each
            // encrypted block is XORed with the
            // following block of plaintext.
            // This is done to make encryption more secure.
            // There is also a mode called ECB which does not need an IV,
            // but it is much less secure.
            alg.Key = Key;
            alg.IV = IV;
            // Create a CryptoStream through which we are going to be
            // pumping our data.
            // CryptoStreamMode.Write means that we are going to be
            // writing data to the stream and the output will be written
            // in the MemoryStream we have provided.
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            // Write the data and make it do the encryption
            cs.Write(clearData, 0, clearData.Length);
            // Close the crypto stream (or do FlushFinalBlock).
            // This will tell it that we have done our encryption and
            // there is no more data coming in,
            // and it is now a good time to apply the padding and
            // finalize the encryption process.
            cs.Close();
            // Now get the encrypted data from the MemoryStream.
            // Some people make a mistake of using GetBuffer() here,
            // which is not the right way.
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        // Encrypt a string into a string using a password
        //    Uses Encrypt(byte[], byte[], byte[])
        public static string Encrypt(string clearText, string Password)
        {
            // First we need to turn the input string into a byte array.
            byte[] clearBytes =
              System.Text.Encoding.Unicode.GetBytes(clearText);
            // Then, we need to turn the password into Key and IV
            // We are using salt to make it harder to guess our key
            // using a dictionary attack -
            // trying to guess a password by enumerating all possible words.
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            // Now get the key/IV and do the encryption using the
            // function that accepts byte arrays.
            // Using PasswordDeriveBytes object we are first getting
            // 32 bytes for the Key
            // (the default Rijndael key length is 256bit = 32bytes)
            // and then 16 bytes for the IV.
            // IV should always be the block size, which is by default
            // 16 bytes (128 bit) for Rijndael.
            // If you are using DES/TripleDES/RC2 the block size is
            // 8 bytes and so should be the IV size.
            // You can also read KeySize/BlockSize properties off
            // the algorithm to find out the sizes.
            byte[] encryptedData = Encrypt(clearBytes,
                     pdb.GetBytes(32), pdb.GetBytes(16));

            // Now we need to turn the resulting byte array into a string.
            // A common mistake would be to use an Encoding class for that.
            //It does not work because not all byte values can be
            // represented by characters.
            // We are going to be using Base64 encoding that is designed
            //exactly for what we are trying to do.
            return Convert.ToBase64String(encryptedData);
        }

        // Encrypt bytes into bytes using a password
        //    Uses Encrypt(byte[], byte[], byte[])
        public static byte[] Encrypt(byte[] clearData, string Password)
        {
            // We need to turn the password into Key and IV.
            // We are using salt to make it harder to guess our key
            // using a dictionary attack -
            // trying to guess a password by enumerating all possible words.
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            // Now get the key/IV and do the encryption using the function
            // that accepts byte arrays.
            // Using PasswordDeriveBytes object we are first getting
            // 32 bytes for the Key
            // (the default Rijndael key length is 256bit = 32bytes)
            // and then 16 bytes for the IV.
            // IV should always be the block size, which is by default
            // 16 bytes (128 bit) for Rijndael.
            // If you are using DES/TripleDES/RC2 the block size is 8
            // bytes and so should be the IV size.
            // You can also read KeySize/BlockSize properties off the
            // algorithm to find out the sizes.
            return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        // Encrypt a file into another file using a password
        public static void Encrypt(string fileIn,
                    string fileOut, string Password)
        {
            // First we are going to open the file streams
            FileStream fsIn = new FileStream(fileIn,
                FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut,
                FileMode.OpenOrCreate, FileAccess.Write);
            // Then we are going to derive a Key and an IV from the
            // Password and create an algorithm
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            Aes alg = new AesCryptoServiceProvider();
            // Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
            // Now create a crypto stream through which we are going
            // to be pumping data.
            // Our fileOut is going to be receiving the encrypted bytes.
            CryptoStream cs = new CryptoStream(fsOut,
                alg.CreateEncryptor(), CryptoStreamMode.Write);

            // Now will will initialize a buffer and will be processing
            // the input file in chunks.
            // This is done to avoid reading the whole file (which can
            // be huge) into memory.
            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;
            do
            {
                // read a chunk of data from the input file
                bytesRead = fsIn.Read(buffer, 0, bufferLen);
                // encrypt it
                cs.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
            // close everything
            // this will also close the unrelying fsOut stream
            cs.Close();
            fsIn.Close();
        }
        // Encrypt a file into another file using a password
        public static Stream Encrypt(FileStream fsIn,
                     string Password)
        {
            // First we are going to open the file streams
            //FileStream fsIn = new FileStream(fileIn,
            //    FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream("xx.xml",
                FileMode.OpenOrCreate, FileAccess.Write);
            // Then we are going to derive a Key and an IV from the
            // Password and create an algorithm
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            Aes alg = new AesCryptoServiceProvider();
            
            // Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
            // Now create a crypto stream through which we are going
            // to be pumping data.
            // Our fileOut is going to be receiving the encrypted bytes.
            MemoryStream s = new MemoryStream();
            CryptoStream cs = new CryptoStream(s, alg.CreateEncryptor(), CryptoStreamMode.Write);

            // Now will will initialize a buffer and will be processing
            // the input file in chunks.
            // This is done to avoid reading the whole file (which can
            // be huge) into memory.
            int bufferLen = 1024;
            byte[] buffer = new byte[bufferLen];
            int bytesRead;
            do
            {
                // read a chunk of data from the input file
                bytesRead = fsIn.Read(buffer, 0, bufferLen);
                
                cs.Write(buffer, 0, bytesRead);
                
            } while (bytesRead != 0);
            // close everything
            // this will also close the unrelying fsOut stream
            //cs.Close();
            //fsIn.Close();
            return s;
        }
        public static void Encrypt(FileStream fsIn,string fileOut,
                    string Password)
        {
            // First we are going to open the file streams
            //FileStream fsIn = new FileStream(fileIn,
            //    FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut,
                FileMode.Create, FileAccess.Write);
            // Then we are going to derive a Key and an IV from the
            // Password and create an algorithm
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            Aes alg = new AesCryptoServiceProvider();
            // Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
            // Now create a crypto stream through which we are going
            // to be pumping data.
            // Our fileOut is going to be receiving the encrypted bytes.
            //Stream s = new MemoryStream();
            CryptoStream cs = new CryptoStream(fsOut, alg.CreateEncryptor(), CryptoStreamMode.Write);

            // Now will will initialize a buffer and will be processing
            // the input file in chunks.
            // This is done to avoid reading the whole file (which can
            // be huge) into memory.
            int bufferLen = 4096;
            byte[] buffer = new byte[bufferLen];
            int bytesRead; 
            
            do
            {
                // read a chunk of data from the input file

                // encrypt it
                //cs.Write(buffer, 0, bytesRead);
                bytesRead = fsIn.Read(buffer, 0, bufferLen);
                if (bytesRead  == 0)
                {

                }
                else
                {
                    cs.Write(buffer, 0, bytesRead);
                }
                cs.Flush();
            } while (bytesRead != 0);
            // close everything
            // this will also close the unrelying fsOut stream
            cs.Close();
            fsIn.Close();
            //return s;
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
                if (bytesRead == 0)
                { break; }
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
            fsIn.Seek(0, 0);
            do
            {
                // read a chunk of data from the input file
                bytesRead = fsIn.Read(buffer, 0, bufferLen);
                // Decrypt it
               
                cs.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
            //  close everything
            // cs.Close(); // this will also close the unrelying fsOut stream
            fsIn.Close();
            return s;
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
