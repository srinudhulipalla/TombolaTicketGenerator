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

namespace TombolaTicketGenerator
{
    public static class StaticRandom
    {
        private static int seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }

    public partial class TicketGenerator : Form
    {
        //List<int> listAllottedNumbers = new List<int>();
        //List<int> listAllottedColumns = new List<int>();
        Random random = new Random();


        //Dictionary<int, int> dic2 = new Dictionary<int, int>();
        //Dictionary<int, int> dic3 = new Dictionary<int, int>();

        public TicketGenerator()
        {
            InitializeComponent();


        }

        private Dictionary<int, int[]> getTicketNumbersColumnWise()
        {
            List<int> listAllottedNumbers = new List<int>();

            Dictionary<int, int[]> dic = new Dictionary<int, int[]>();

            for (int i = 1; i <= 9; i++)
            {
                int[] nums = new int[]
                {
                    generateNumberForColumn(i, listAllottedNumbers),
                    generateNumberForColumn(i, listAllottedNumbers),
                    generateNumberForColumn(i, listAllottedNumbers)
                };

                Array.Sort(nums);

                dic[i] = nums;
            }

            return dic;
        }

        private Dictionary<int, int[]> getNumberPositionsByRow()
        {
            Dictionary<int, int[]> dicRowNumberPlaces = new Dictionary<int, int[]>();

            List<int> one = getFiveNumberPositionsForRow();
            List<int> two = getFiveNumberPositionsForRow();
            List<int> three = getFiveNumberPositionsForRow();

            List<int> temp = Enumerable.Range(1, 9).ToList();

            List<int> notfound = temp.Except(one).Except(two).Except(three).ToList();

            List<int> duplicates = one.Intersect(two).Intersect(three).ToList();

            foreach (int not in notfound)
            {
                foreach (int d in duplicates)
                {
                    if (one.Contains(d) && !one.Contains(not))
                    {
                        one.Remove(d);
                        one.Add(not);
                        break;
                    }

                    if (two.Contains(d) && !two.Contains(not))
                    {
                        two.Remove(d);
                        two.Add(not);
                        break;
                    }
                }
            }

            one.Sort();
            two.Sort();
            three.Sort();

            dicRowNumberPlaces.Add(1, one.ToArray());
            dicRowNumberPlaces.Add(2, two.ToArray());
            dicRowNumberPlaces.Add(3, three.ToArray());

            return dicRowNumberPlaces;
        }

        private List<int> getFiveNumberPositionsForRow()
        {
            List<int> dic1 = new List<int>();

            for (int i = 1; i <= 5; i++)
            {
                dic1.Add(getNextNumberPosition(dic1));

                dic1.Sort();
            }

            return dic1;
        }

        private int getNextNumberPosition(List<int> dic)
        {
            //int c = random.Next(1, 10);
            int c = StaticRandom.Instance.Next(1, 10);

            if (dic.Contains(c))
            {
                c = getNextNumberPosition(dic);
            }

            return c;
        }

        private int generateNumberForColumn(int c, List<int> listAllottedNumbers)
        {
            int startnum = (c - 1) * 10;

            if (startnum == 0) startnum = 1;

            int endnum = (c * 10) - 1;

            if (endnum == 89) endnum = 90;

            //int num = random.Next(startnum, endnum + 1);
            int num = StaticRandom.Instance.Next(startnum, endnum + 1);

            if (listAllottedNumbers.Contains(num))
            {
                num = generateNumberForColumn(c, listAllottedNumbers);
            }

            if (!listAllottedNumbers.Contains(num))
            {
                listAllottedNumbers.Add(num);
            }

            return num;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string html = generateTicket();

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

            //PdfWriter writer = new PdfWriter("output.pdf");
            //PdfDocument pdfDocument = new PdfDocument(writer);
            //string header = "pdfHtml Header and footer example using page-events";
            ////Header headerHandler = new Header(header);
            ////Footer footerHandler = new Footer();

            ////pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, headerHandler);
            ////pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, footerHandler);

            //// Base URI is required to resolve the path to source files
            //ConverterProperties converterProperties = new ConverterProperties();
            //HtmlConverter.ConvertToDocument(oDoc.DocumentNode.OuterHtml, pdfDocument, converterProperties);

            //// Write the total number of pages to the placeholder
            ////footerHandler.WriteTotal(pdfDocument);
            ////pdfDocument.Close();




        }

        private string generateTicket()
        {
            string ticketHTML = File.ReadAllText("ticket.html");

            HtmlAgilityPack.HtmlDocument oDoc = new HtmlAgilityPack.HtmlDocument();
            oDoc.LoadHtml(ticketHTML);

            HtmlNode table = oDoc.DocumentNode.SelectNodes("table").FirstOrDefault();

            Dictionary<int, int[]> dic = getTicketNumbersColumnWise();
            Dictionary<int, int[]> dic1 = getNumberPositionsByRow();

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




    }
}
