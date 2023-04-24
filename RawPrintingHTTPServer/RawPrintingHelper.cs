using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace RawPrintingHTTPServer
{
    class RawPrintingHelper {

        static string textToPrint = "";

        public static bool sendStringToPrinter(string printerName, string text, string jobName)
        {
            RawPrintingHelper.textToPrint = text;
            var printerSettings = new PrinterSettings
            {
                PrinterName = printerName,
                Copies = 1,
            };

            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings = printerSettings;
            pd.PrintPage += new PrintPageEventHandler(handlePrintPage);
            pd.Print();
            return true;
        }

        public static void handlePrintPage(object sender, PrintPageEventArgs ev)
        {
            Font printFont = new Font("Arial", 10);
            int yPos = 0;
            do
            {
                string text = textToPrint.Length > 48 ? textToPrint.Substring(0, 48) : textToPrint;
                ev.Graphics.DrawString(text, printFont, Brushes.Black, 0, yPos, new StringFormat());
                yPos += 20;
                textToPrint = textToPrint.Length > 48 ? textToPrint.Substring(48) : "";
            } while(textToPrint.Length > 0);
            ev.HasMorePages = false;
        }
    }
}
