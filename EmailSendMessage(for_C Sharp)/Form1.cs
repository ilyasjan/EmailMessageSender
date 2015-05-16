using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net.Mime;
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
            Properties.Settings.Default.sender.Add(comFrom.Text);
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
            if( MessageBox.Show("Really want to quit it？","Seriously ?",MessageBoxButtons.YesNo ,MessageBoxIcon.Question )==DialogResult.Yes  )
            Application.Exit();
        }
        private void SendEmail(string strFrom, string strTo, string strFileName, string strHost, string strID, string strPass, string strTitle, string strContet)
        {
            MailAddress @from = new MailAddress(strFrom);
            MailAddress total = new MailAddress(strTo);
            MailMessage message = new MailMessage(@from, total);
            //有没有附件
            if (strFileName != "")
            {
                Attachment myAtt = new Attachment(strFileName, MediaTypeNames.Application.Octet);
                ContentDisposition dispos = new ContentDisposition();
                dispos = myAtt.ContentDisposition;
                dispos.CreationDate = File.GetCreationTime(strFileName);
                dispos.ModificationDate = File.GetLastWriteTime(strFileName);
                dispos.ReadDate = File.GetLastAccessTime(strFileName);
                message.Attachments.Add(myAtt);
            }
            SmtpClient clint = new SmtpClient(strHost);
            clint.Credentials = new System.Net.NetworkCredential(strID, strPass);
            message.Subject = strTitle;
            message.Body = strContet;
            try
            {
                clint.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,MessageBoxIcon.Hand  );
                isError = true;
                return;
            }
            MessageBox.Show("Sent successfully!", "Surprise", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void Send_Click(object sender, EventArgs e)
        {
            SendEmail(comFrom.Text,comTo.Text,txtFileName.Text,txtServer.Text, txtID.Text, txtPass.Text,txtTitle.Text,richTextBox1.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void AddTo_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.receiver.Add(comTo.Text);
        }

        private void Frm_ESG_Load(object sender, EventArgs e)
        {
         
            foreach (string st in Properties.Settings.Default.sender)
            { 
                comFrom.Items.Add(st);
            }
            
            foreach (string st in Properties.Settings.Default.receiver)
            {
                comTo.Items.Add(st);
            }
        }
    }
}
