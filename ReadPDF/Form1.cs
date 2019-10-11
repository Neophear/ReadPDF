using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ReadPDF
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            ofdPDF.ShowDialog();
        }

        private void ofdPDF_FileOk(object sender, CancelEventArgs e)
        {
            txtFilePath.Text = ofdPDF.FileName;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";

            if (File.Exists(txtFilePath.Text))
            {
                try
                {
                    StringBuilder text = new StringBuilder();
                    Regex re = new Regex(txtRegEx.Text);

                    using (PdfReader reader = new PdfReader(File.OpenRead(txtFilePath.Text)))
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                            foreach (Match m in re.Matches(PdfTextExtractor.GetTextFromPage(reader, i)))
                                text.Append(m.Value + Environment.NewLine);

                    txtOutput.Text = text.ToString();
                }
                catch (BadPdfFormatException)
                {
                    MessageBox.Show("PDF-filen kunne ikke læses");
                }
            }
            else
                MessageBox.Show("Filen findes ikke");
        }

        private void lnklblRegexPal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(lnklblRegexPal.Text);
        }
    }
}
