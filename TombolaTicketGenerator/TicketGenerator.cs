using HtmlAgilityPack;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Environment;

namespace TombolaTicketGenerator
{
    public partial class TicketGenerator : Form
    {
        public TicketGenerator()
        {
            InitializeComponent();

            saveTicketsDialog.InitialDirectory = GetFolderPath(SpecialFolder.UserProfile) + @"\Downloads";
            saveTicketsDialog.RestoreDirectory = true;
            saveTicketsDialog.Title = "Choose location to save tickets file";
            saveTicketsDialog.DefaultExt = "pdf";
            saveTicketsDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveTicketsDialog.CheckPathExists = true;
            saveTicketsDialog.FileName = "output.pdf";
            saveTicketsDialog.FilterIndex = 2;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string outputFilePath = string.Empty;

            DialogResult saveDialogResult = saveTicketsDialog.ShowDialog();

            if (saveDialogResult == DialogResult.Cancel)
            {
                MessageBox.Show("Ticket Generation was cancelled!", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (saveDialogResult == DialogResult.OK)
            {
                outputFilePath = saveTicketsDialog.FileName;
            }

            try
            {
                string ticketStyles = File.ReadAllText("styles.html");

                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(ticketStyles + Environment.NewLine + "<table class='page' />");

                HtmlNode table = htmlDocument.DocumentNode.SelectNodes("table").FirstOrDefault();

                int ticketNumber = 1;

                for (int i = 0; i < 50; i += 2)
                {
                    HtmlNode row = HtmlNode.CreateNode("<tr/>");

                    HtmlNode column1 = HtmlNode.CreateNode("<td/>");
                    column1.InnerHtml = generateTicket().Replace("#", "#" + ticketNumber++);
                    row.AppendChild(column1);

                    HtmlNode column2 = HtmlNode.CreateNode("<td/>");
                    column2.InnerHtml = generateTicket().Replace("#", "#" + ticketNumber++);
                    row.AppendChild(column2);

                    table.AppendChild(row);
                }

                SaveTicketsFile(outputFilePath, htmlDocument.DocumentNode.OuterHtml);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string generateTicket()
        {
            string ticketHTML = File.ReadAllText("ticket.html");

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(ticketHTML);

            HtmlNode table = htmlDocument.DocumentNode.SelectNodes("table").FirstOrDefault();

            Dictionary<int, int[]> columnNumbers = clsTicketGenerator.getTicketNumbersColumnWise();
            Dictionary<int, int[]> numberPositions = clsTicketGenerator.getNumberPositionsByRow();

            HtmlNodeCollection rows = table.SelectNodes("tr");

            for (int r = 1; r <= rows.Count; r++)
            {
                HtmlNode row = rows[r - 1];

                HtmlNodeCollection columns = row.SelectNodes("td");

                int[] colPositinos = numberPositions[r];

                foreach (int c in colPositinos)
                {
                    int[] nums = columnNumbers[c];

                    int number = nums[r - 1];

                    columns[c - 1].InnerHtml = number.ToString();
                }
            }

            string html = htmlDocument.DocumentNode.OuterHtml;

            return html;
        }

        void SaveTicketsFile(string outputFilePath, string html)
        {
            //File.WriteAllText("output.html", html);

            using (FileStream pdfDestination = File.Open(outputFilePath, FileMode.OpenOrCreate))
            {
                ConverterProperties converterProperties = new ConverterProperties();

                HtmlConverter.ConvertToPdf(html, pdfDestination, converterProperties);
            }

            MessageBox.Show("Tickets file is saved at " + outputFilePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
            this.Dispose();
        }

    }
}
