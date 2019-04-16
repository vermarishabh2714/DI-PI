using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace PrivateLocker
{
    public static class EncryptionUtils
    {
        #region Variables

        static List<FileInfo> files = new List<FileInfo>();  // List that will hold the files and sub files in path
        static List<DirectoryInfo> folders = new List<DirectoryInfo>(); // List that hold directories that cannot be accessed

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a random Salt
        /// </summary>
        /// <param name="length">Length of Salt</param>
        /// <returns>Random Salt of given length</returns>
        public static string CreateRandomSalt(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(38 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates a random Salt
        /// </summary>
        /// <param name="length">Length of Salt</param>
        /// <param name="strong">Use Strong (Special) characters</param>
        /// <returns>Random Salt of given length</returns>
        public static string CreateRandomSalt(int length, bool strong)
        {
            Random random = new Random();
            int seed = random.Next(1, int.MaxValue);
            const string AllowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            const string SpecialCharacters = @"!#$%&'()*+,-./:;<=>?@[\]_";

            var chars = new char[length];
            var rd = new Random(seed);

            for (var i = 0; i < length; i++)
            {
                // If we are to use special characters
                if (strong && i % random.Next(3, length) == 0)
                {
                    chars[i] = SpecialCharacters[rd.Next(0, SpecialCharacters.Length)];
                }
                else
                {
                    chars[i] = AllowedChars[rd.Next(0, AllowedChars.Length)];
                }
            }

            return new string(chars);
        }

        /// <summary>
        /// Encrypt String
        /// </summary>
        /// <param name="clearText">Clear Text to be Encrypted</param>
        /// <param name="password">Password to use during encryption</param>
        /// <param name="salt">Salt to use during Encryption</param>
        /// <returns></returns>
        public static byte[] EncryptPassword(string clearText, string password, string salt)
        {
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, saltBytes);

            byte[] encryptedData = EncryptPW(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            //return Convert.ToBase64String(encryptedData); //For returning string instead
            return encryptedData;
        }

        /// <summary>
        /// Decrypt String
        /// </summary>
        /// <param name="cipherText">Encrypted Text to be decrypted</param>
        /// <param name="password">Password to use during decryption</param>
        /// <param name="salt">Salt to use during Encryption</param>
        /// <returns></returns>
        public static byte[] DecryptPassword(string cipherText, string password, string salt)
        {
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, saltBytes);
            byte[] decryptedData = DecryptPW(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            //return System.Text.Encoding.Unicode.GetString(decryptedData); //For returning string instead
            return decryptedData;
        }

        public static bool EncryptedFolder(string folderDirectory, string pword,bool compress)
        {
            bool status = false;
            string fileLocation = "";
            string salt = "";
            byte[] encPW = null;

            try
            {
                status = Directory.Exists(folderDirectory);

                if (status)
                {
                    DirectoryInfo di = new DirectoryInfo(folderDirectory);

                    //Clear Folder and File list
                    folders = new List<DirectoryInfo>();
                    files = new List<FileInfo>();

                    //Build new Folder and File list
                    GetAllFilesInDir(di, "*");

                    
                    foreach (FileInfo fi in files)
                    {
                        fileLocation = fi.FullName;

                        if (compress)
                        {
                            fileLocation += ".zip";
                        }

                        if (compress)
                        {
                            //Compress the file using 7Zip's LZMA compression
                            CompressFileLZMA(fi.FullName, fileLocation);

                            //Delete the original file
                            if (File.Exists(fi.FullName))
                            {
                                File.Delete(fi.FullName);
                            }
                        }

                        //Build the Encrypted Password with a unique salt based on the file's info
                        string fileData = string.Format("{0}", fi.Name.Substring(0, fi.Name.IndexOf(".")));
                        salt = Convert.ToBase64String(GetBytes(fileData));
                        encPW = EncryptPassword(pword, "!PrivateLocker-2013", salt);
                        string strPW = Convert.ToBase64String(encPW);

                        EncryptFile(fileLocation, encPW);

                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = false;
            }

            return status;
        }

        public static bool DecryptFolder(string folderDirectory, string pword,bool compressed)
        {
            bool status = false;
            string fileLocation = "";
            string salt = "";
            byte[] encPW = null;

            try
            {
                status = Directory.Exists(folderDirectory);

                if (status)
                {
                    DirectoryInfo di = new DirectoryInfo(folderDirectory);

                    //Clear Folder and File list
                    folders = new List<DirectoryInfo>();
                    files = new List<FileInfo>();
                    //Build new Folder and File list
                    GetAllFilesInDir(di, "*");

                    foreach (FileInfo fi in files)
                    {
                        fileLocation = fi.FullName;

                        //Build the Encrypted Password with a unique salt based on the file's info
                        string fileData = string.Format("{0}", fi.Name.Substring(0,fi.Name.IndexOf(".")));
                        salt = Convert.ToBase64String(GetBytes(fileData));
                        encPW = EncryptPassword(pword, "!PrivateLocker-2013", salt);
                        string strPW = Convert.ToBase64String(encPW);

                        DecryptFile(fileLocation, encPW);

                        if (compressed)
                        {
                            DecompressFileLZMA(fi.FullName, fi.FullName.Replace(".zip", ""));

                            //Delete the original file
                            if (File.Exists(fi.FullName))
                            {
                                File.Delete(fi.FullName);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = false;
            }

            return status;
        }

        #endregion

        #region Private Methods

        private static void GetAllFilesInDir(DirectoryInfo dir, string searchPattern)
        {
            // list the files
            try
            {
                foreach (FileInfo f in dir.GetFiles(searchPattern))
                {
                    //Console.WriteLine("File {0}", f.FullName);
                    files.Add(f);
                }
            }
            catch
            {
                Console.WriteLine("Directory {0}  \n could not be accessed!!!!", dir.FullName);
                return;  // We already got an error trying to access dir so don't try to access it again
            }

            // process each directory
            // If I have been able to see the files in the directory I should also be able 
            // to look at its directories so I don't think I should place this in a try catch block
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                folders.Add(d);
                GetAllFilesInDir(d, searchPattern);
            }
        }

        private static byte[] EncryptPW(byte[] clearText, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearText, 0, clearText.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        private static byte[] DecryptPW(byte[] cipherData, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        private static void EncryptFile(string inputFile, byte[] key)
        {
            try
            {
                string ext = Path.GetExtension(inputFile);
                string outputFile = inputFile.Replace(ext, "_enc" + ext);

                //Prepare the file for encryption by getting it into a stream
                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                //Setup the Encryption Standard using Write mode
                RijndaelManaged rijndaelCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fsCrypt, rijndaelCrypto.CreateEncryptor(key, key), CryptoStreamMode.Write);

                //Write the encrypted file stream
                FileStream fsIn = new FileStream(inputFile, FileMode.Open);
                int data;
                while ((data = fsIn.ReadByte()) != -1)
                {
                    cs.WriteByte((byte)data);
                }

                //Close all the Writers
                fsIn.Close();
                cs.Close();
                fsCrypt.Close();

                //Delete the original file
                File.Delete(inputFile);
                //Rename the encrypted file to that of the original
                File.Copy(outputFile, inputFile);
                File.Delete(outputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DecryptFile(string inputFile, byte[] key)
        {
            string ext = Path.GetExtension(inputFile);
            string outputFile = inputFile.Replace(ext, "_enc" + ext);

            //Prepare the file for decryption by getting it into a stream
            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            //Setup the Decryption Standard using Read mode
            RijndaelManaged rijndaelCrypto = new RijndaelManaged();
            CryptoStream cs = new CryptoStream(fsCrypt, rijndaelCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read);

            //Write the decrypted file stream
            FileStream fsOut = new FileStream(outputFile, FileMode.Create);
            try
            {
                int data;
                while ((data = cs.ReadByte()) != -1)
                { fsOut.WriteByte((byte)data); }

                //Close all the Writers
                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

                //Delete the original file
                File.Delete(inputFile);
                //Rename the encrypted file to that of the original
                File.Copy(outputFile, inputFile);
                File.Delete(outputFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fsOut = null;
                cs = null;
                fsCrypt = null;
            }
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static void CompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Write the encoder properties
            coder.WriteCoderProperties(output);

            // Write the decompressed file size.
            output.Write(BitConverter.GetBytes(input.Length), 0, 8);

            // Encode the file.
            coder.Code(input, output, input.Length, -1, null);

            //Cleanup
            input.Close();
            output.Flush();
            output.Close();
            coder = null;
        }

        private static void DecompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Read the decoder properties
            byte[] properties = new byte[5];
            input.Read(properties, 0, 5);

            // Read in the decompress file size.
            byte[] fileLengthBytes = new byte[8];
            input.Read(fileLengthBytes, 0, 8);
            long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            coder.SetDecoderProperties(properties);
            coder.Code(input, output, input.Length, fileLength, null);

            //Cleanup
            input.Close();
            output.Flush();
            output.Close();
            coder = null;
        }
       
        #endregion
    }
}
