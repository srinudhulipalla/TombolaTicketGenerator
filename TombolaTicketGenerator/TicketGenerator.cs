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

            saveTickets.InitialDirectory = GetFolderPath(SpecialFolder.UserProfile) + @"\Downloads";
            saveTickets.RestoreDirectory = true;
            saveTickets.Title = "Choose location to save tickets file";
            saveTickets.DefaultExt = "pdf";
            saveTickets.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveTickets.CheckFileExists = true;
            saveTickets.CheckPathExists = true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //string html = generateTicket();

            string styles = File.ReadAllText("styles.html");



            HtmlAgilityPack.HtmlDocument oDoc = new HtmlAgilityPack.HtmlDocument();
            oDoc.LoadHtml(styles + Environment.NewLine + "<table class='page' />");

            var table = oDoc.DocumentNode.SelectNodes("table").FirstOrDefault();
            // var node = HtmlNode.CreateNode(rowToAppend); rowtoapend is row html
            //table.AppendChild(node);

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

            File.WriteAllText("output.html", oDoc.DocumentNode.OuterHtml);

            using (FileStream pdfDest = File.Open("output.pdf", FileMode.OpenOrCreate))
            {
                ConverterProperties converterProperties = new ConverterProperties();

                HtmlConverter.ConvertToPdf(oDoc.DocumentNode.OuterHtml, pdfDest, converterProperties);
            }

            DialogResult result = saveTickets.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = saveTickets.FileName;
            }

        }

        private string generateTicket()
        {
            string ticketHTML = File.ReadAllText("ticket.html");

            HtmlAgilityPack.HtmlDocument oDoc = new HtmlAgilityPack.HtmlDocument();
            oDoc.LoadHtml(ticketHTML);

            HtmlNode table = oDoc.DocumentNode.SelectNodes("table").FirstOrDefault();

            Dictionary<int, int[]> dic = clsTicketGenerator.getTicketNumbersColumnWise();
            Dictionary<int, int[]> dic1 = clsTicketGenerator.getNumberPositionsByRow();

            HtmlNodeCollection rows = table.SelectNodes("tr");

            for (int r = 1; r <= rows.Count; r++)
            {
                HtmlNode row = rows[r - 1];

                HtmlNodeCollection columns = row.SelectNodes("td");

                int[] cols = dic1[r];

                foreach (int col in cols)
                {
                    //int column = getNextColumn();
                    int[] nums = dic[col];

                    int number = nums[r - 1];

                    columns[col - 1].InnerHtml = number.ToString();
                }
            }

            string html = oDoc.DocumentNode.OuterHtml;

            return html;
        }

        private void saveTickets_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
