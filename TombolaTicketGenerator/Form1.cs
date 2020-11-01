using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TombolaTicketGenerator
{
    public partial class Form1 : Form
    {
        List<int> listAllottedNumbers = new List<int>();
        List<int> listAllottedColumns = new List<int>();
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string ticketHTML = File.ReadAllText("ticket.html");

            HtmlAgilityPack.HtmlDocument oDoc = new HtmlAgilityPack.HtmlDocument();
            oDoc.LoadHtml(ticketHTML);

            //var table = html.DocumentNode.SelectNodes("table").FirstOrDefault();
            //var node = HtmlNode.CreateNode(rowToAppend);rowtoapend is row html 
            //table.AppendChild(node);
            //table.AppendChild(HtmlNode.CreateNode("<tr></tr>"));

            HtmlNode table = oDoc.DocumentNode.SelectNodes("table").FirstOrDefault();
            listAllottedNumbers.Clear();


            HtmlNodeCollection rows = table.SelectNodes("tr");

            foreach (HtmlNode row in rows)
            {
                listAllottedColumns.Clear();
                HtmlNodeCollection columns = row.SelectNodes("td");

                for (int index = 0; index < 5; index++)
                {
                    int column = getNextColumn();
                    int number = getNextNumber();

                    columns[column - 1].InnerHtml = number.ToString();
                }
            }

            string html = oDoc.DocumentNode.OuterHtml;

        }

        private int getNextNumber()
        {            
            int number = random.Next(1, 99);

            if (listAllottedNumbers.Contains(number))
            {
                number = getNextNumber();
            }

            if (!listAllottedNumbers.Contains(number))
            {
                listAllottedNumbers.Add(number);
            }

            return number;
        }

        private int getNextColumn()
        {
            int column = random.Next(1, 9);

            if (listAllottedColumns.Contains(column))
            {
                column = getNextColumn();
            }

            if (!listAllottedColumns.Contains(column))
            {
                listAllottedColumns.Add(column);
            }

            return column;
        }

    }
}
