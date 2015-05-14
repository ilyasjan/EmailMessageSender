using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace EmailSendMessage_for_C_Sharp_
{
    public partial class Frm_ESG : Form
    {
        private Boolean isError = false;
        public Frm_ESG()
        {
            InitializeComponent();
        }

        private void AddFrom_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select the attached file";
            dlg.Filter = "AllFileFormat|*.*";
            dlg.FileName = "";
            dlg.ShowDialog();
            if (dlg.FileName != "")
            txtFileName.Text = dlg.FileName;
            dlg.Dispose();
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            if( MessageBox.Show("Really want to quit it？","Prompted",MessageBoxButtons.YesNo ,MessageBoxIcon.Question )==DialogResult.Yes  )
            Application.Exit();
        }
        private void SendEmail(string strFrom, string strTo, string strFileName, string strHost, string strID, string strPass, string strTitle, string strContet)
        {
            System.Net.Mail.MailAddress @from = new System.Net.Mail.MailAddress(strFrom);
            System.Net.Mail.MailAddress total = new System.Net.Mail.MailAddress(strTo);
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(@from, total);
            if (strFileName != "")
            {
                System.Net.Mail.Attachment myAtt = new System.Net.Mail.Attachment(strFileName, System.Net.Mime.MediaTypeNames.Application.Octet);
                System.Net.Mime.ContentDisposition dispos = new System.Net.Mime.ContentDisposition();
                dispos = myAtt.ContentDisposition;
                dispos.CreationDate = File.GetCreationTime(strFileName);
                dispos.ModificationDate = File.GetLastWriteTime(strFileName);
                dispos.ReadDate = File.GetLastAccessTime(strFileName);
                message.Attachments.Add(myAtt);
            }
            System.Net.Mail.SmtpClient clint = new System.Net.Mail.SmtpClient(strHost);
            clint.Credentials = new System.Net.NetworkCredential(strID, strPass);
            message.Subject = strTitle;
            message.Body = strContet;
            try
            {
                clint.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prompted", MessageBoxButtons.OK,MessageBoxIcon.Hand  );
                isError = true;
                return;
            }
            MessageBox.Show("Sent successfully!", "Prompted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void Send_Click(object sender, EventArgs e)
        {
            SendEmail("291519876@qq.com", "ilyas5137@163.com", "", "smtp.qq.com", "291519876", "recent1a", "this is a new test", "jsdlkfjlksdjflksdj");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }
    }
}
