using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Reflection;
using System.IO;

namespace PrivateLocker
{
    public partial class Form1 : Form
    {
        string dir = "";
        string pw = "";
        string statusMsg = "";
        bool status = false;

        public Form1()
        {
            InitializeComponent();
            dir = Application.StartupPath;
        }

        #region Form Events

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            pnlBtns.Enabled = false;
            pbar.Visible = true;
            lblStatus.Text = "Waiting.";
            Application.DoEvents();

            if (fbd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pw = Interaction.InputBox("Please Enter Password", "Password Required", "", btnEncrypt.Location.X, btnEncrypt.Location.Y);
                bgw.RunWorkerAsync();
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            pnlBtns.Enabled = false;
            pbar.Visible = true;
            lblStatus.Text = "Waiting.";
            Application.DoEvents();

            if (fbd1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pw = Interaction.InputBox("Please Enter Password", "Password Required", "", btnEncrypt.Location.X, btnEncrypt.Location.Y);
                //EncryptFolder();
                bgw2.RunWorkerAsync();
            }

        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            EncryptFolder();
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlBtns.Enabled = true;
            pbar.Visible = false;
            lblStatus.Text = statusMsg;
            Application.DoEvents();
        }

        private void bgw2_DoWork(object sender, DoWorkEventArgs e)
        {
            DecryptFolder();
        }

        private void bgw2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlBtns.Enabled = true;
            pbar.Visible = false;
            lblStatus.Text = statusMsg;
            Application.DoEvents();
        }

        #endregion

        #region Methods

        private void EncryptFolder()
        {
            status = false;
            if (pw != string.Empty)
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    lblStatus.Text = "Encrypting.";
                    Application.DoEvents();
                }));
                status = EncryptionUtils.EncryptedFolder(fbd1.SelectedPath, pw, chkCompress.Checked);
            }
            else
            {
                statusMsg = "Password cannot be empty";
            }

            if (status)
            {

                if (chkStandalone.Checked)
                {
                    CreateStandaloneEncryption(fbd1.SelectedPath);
                }

                statusMsg = "Encryption Done";
            }
            else
            {
                statusMsg = "Encryption Failed";
            }
        }

        private void DecryptFolder()
        {
            status = false;
            if (pw != string.Empty)
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    lblStatus.Text = "Decrypting.";
                    Application.DoEvents();
                }));
                status = EncryptionUtils.DecryptFolder(fbd1.SelectedPath, pw, chkCompress.Checked);
            }
            else
            {
                statusMsg = "Password cannot be empty";
            }

            if (status)
            {
                statusMsg = "Decryption Done";
            }
            else
            {
                statusMsg = "Decryption Failed";
            }
        }

        private void CreateStandaloneEncryption(string path)
        {
            StandaloneCompiler.ExtractSourceCode(path);

            StandaloneCompiler.CompileExecutable(path + @"\res\standalone.cs", path);

            if(Directory.Exists(path + "\\res\\"))
            {
                Directory.Delete(path + "\\res\\",true);
            }
        }

        //private void ExtractSourceCode(string path)
        //{
        //    string fInfo = "";
        //    Assembly asm = Assembly.GetExecutingAssembly();
        //    Stream fstr = null;
        //    string asmName = asm.GetName().Name;

        //    //Loop thru all the resources and Extract them
        //    foreach (string resourceName in asm.GetManifestResourceNames())
        //    {
        //        fInfo = path + "\\res\\" + resourceName.Replace(asmName + ".", "").Replace("Resources.", "");
        //        fstr = asm.GetManifestResourceStream(resourceName);

        //        if (fstr != null && fInfo.Contains("standalone.cs"))
        //        {
        //            SaveStreamToFile(fInfo, fstr);
        //        }

        //        if (fstr != null && fInfo.Contains("Lzma.dll"))
        //        {
        //            SaveStreamToFile(fInfo, fstr);
        //        }

        //        if (fstr != null && fInfo.Contains("key2.ico"))
        //        {
        //            SaveStreamToFile(fInfo, fstr);
        //        }
        //    }
        //}

        //private static void SaveStreamToFile(string fileFullPath, Stream stream)
        //{
        //    if (stream.Length == 0) return;

        //    if (!Directory.Exists(Path.GetDirectoryName(fileFullPath)))
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(fileFullPath));
        //    }

        //    // Create a FileStream object to write a stream to a file
        //    using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
        //    {

        //        // Fill the bytes[] array with the stream data
        //        byte[] bytesInStream = new byte[stream.Length];
        //        stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

        //        // Use FileStream object to write to the specified file
        //        fileStream.Write(bytesInStream, 0, bytesInStream.Length);
        //    }
        //}

        #endregion

        private void pnlBtns_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkCompress_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
