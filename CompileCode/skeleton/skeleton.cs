using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;

using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
namespace skeleton
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new skeleton());
        }
    }
    public partial class skeleton : Form
    {
        string fileName = string.Empty;
        public skeleton()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                lblError.Text = "";
                DeCryptAndCheck(fileName, txtPassword.Text);
                Application.Exit();
            }
        }
        private bool validate()
        {
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Paswword can't be Empty";
                return false;
            }
            if (string.IsNullOrWhiteSpace(fileName))
            {
                lblError.Text = "FileName can't be Empty";
                return false;
            }
            return true;
        }
        private void btnCancle_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            saveFileCCD.ShowDialog();
            fileName = saveFileCCD.FileName;
            txtFileName.Text = fileName;
            lblError.Text = "";
        }
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPwd = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.saveFileCCD = new System.Windows.Forms.SaveFileDialog();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Location = new System.Drawing.Point(12, 34);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(81, 13);
            this.lblPwd.TabIndex = 0;
            this.lblPwd.Text = "Enter Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(131, 31);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(165, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 64);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(112, 13);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "Select Destination File";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(267, 60);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(29, 23);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "--";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(199, 101);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(62, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(267, 101);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(65, 23);
            this.btnCancle.TabIndex = 5;
            this.btnCancle.Text = "Cancel";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // saveFileCCD
            // 
            this.saveFileCCD.DefaultExt = "xml";
            this.saveFileCCD.Filter = "XML file|*.xml";
            this.saveFileCCD.Title = "Save CCD File";
            // 
            // txtFileName
            // 
            this.txtFileName.Enabled = false;
            this.txtFileName.Location = new System.Drawing.Point(131, 61);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(130, 20);
            this.txtFileName.TabIndex = 2;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(114, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 137);
            this.ControlBox = false;
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblPwd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "VIC_CCD";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private  void DeCryptAndCheck(string OutputPath, string InputPwd)
        {
            

            Assembly assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine(assembly.GetManifestResourceNames().Length);



            try
            {
                string gzipFileName = assembly.GetManifestResourceNames()[0];
               // Console.WriteLine(gzipFileName);
                Stream compressedStream = assembly.GetManifestResourceStream(gzipFileName);

                string encFileName = assembly.GetManifestResourceNames()[1];
               // Console.WriteLine(gzipFileName);
                Stream encStream = assembly.GetManifestResourceStream(encFileName);
                string pwd = "";
                string hash = "";

                ResourceReader res = new ResourceReader(compressedStream);
                IDictionaryEnumerator dict = res.GetEnumerator();
                //Stream sss = null;
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

                    byte[] v = new byte[encStream.Length];
                    encStream.Read(v, 0, v.Length);
                    File.WriteAllBytes("temp.xml", v);//temp file

                    Console.WriteLine(pwd);
                    VICEncryption.Decrypt("temp.xml", OutputPath, pwd);
                   // Console.WriteLine("\n\n hash  b  :" + VICEncryption.GenerateHash(File.ReadAllText(OutputPath)));

                    if(VICEncryption.GenerateHash(File.ReadAllText(OutputPath)) == hash)
                    {
                        MessageBox.Show("Successfully Saved to " + OutputPath);
                    }
                    compressedStream.Close();
                    encStream.Close();
                    if (File.Exists("temp.xml"))
                    {
                        try
                        {
                            File.Delete("temp.xml");
                        }
                        catch { }/// need to implement
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Password");
                  //  Console.WriteLine("Wrong Password");
                }
                //  res.Close();
                //compressedStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message + "\n" + e.StackTrace + "\n");
            }
        }
        #endregion
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.SaveFileDialog saveFileCCD;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblError;
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
