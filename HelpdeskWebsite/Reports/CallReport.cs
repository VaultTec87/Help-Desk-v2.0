using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;
using HelpdeskViewModels;
using System.Collections.Generic;

namespace ExercisesWebsite.Reports
{
    public class CallReport
    {
        static string mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        static Font title_font = new Font(Font.FontFamily.COURIER, 22, Font.BOLDITALIC);
        static Font column_font = new Font(Font.FontFamily.COURIER, 15, Font.BOLDITALIC + Font.UNDERLINE);
        static Font list_font = new Font(Font.FontFamily.COURIER, 12, Font.BOLDITALIC);
        static Font date_created_font = new Font(Font.FontFamily.COURIER, 14, Font.BOLDITALIC);
        static string logo = "Helpdesk.png";
        

        public void do_it()
        {
            try
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(mappedPath + "pdfs/CallPdf.pdf", FileMode.Create));
                document.Open();
                Image image = Image.GetInstance(mappedPath + logo);
                image.ScaleAbsolute(150, 125);
                Paragraph para = new Paragraph("Calls", title_font);
                para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                image.SetAbsolutePosition(125f, 710f);
                para.Add(image);


                PdfPTable table = new PdfPTable(6);
                table.WidthPercentage = 100.00F;
                PdfPCell cell = new PdfPCell(new Phrase("Opened", column_font));
                cell.Border = Rectangle.NO_BORDER;
                table.SpacingBefore = 100f;
                table.AddCell(cell);
                PdfPCell cell2 = new PdfPCell(new Phrase("Lastname", column_font));
                cell2.Border = Rectangle.NO_BORDER;
                table.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("Tech", column_font));
                cell3.Border = Rectangle.NO_BORDER;
                table.AddCell(cell3);
                PdfPCell cell4 = new PdfPCell(new Phrase("Problem", column_font));
                cell4.Border = Rectangle.NO_BORDER;
                table.AddCell(cell4);
                PdfPCell cell5 = new PdfPCell(new Phrase("Status", column_font));
                cell5.Border = Rectangle.NO_BORDER;
                table.AddCell(cell5);
                PdfPCell cell6 = new PdfPCell(new Phrase("Closed", column_font));
                cell6.Border = Rectangle.NO_BORDER;
                table.AddCell(cell6);

                CallViewModel call = new CallViewModel();
                List<CallViewModel> call_list = call.GetAll();

                foreach (CallViewModel c in call_list)
                {
                    table.AddCell(new PdfPCell(new Phrase(c.DateOpened.ToShortDateString(), list_font))).Border = Rectangle.NO_BORDER;
                    table.AddCell(new PdfPCell(new Phrase(c.employee_last_name, list_font))).Border = Rectangle.NO_BORDER;
                    table.AddCell(new PdfPCell(new Phrase(c.tech_name, list_font))).Border = Rectangle.NO_BORDER;
                    table.AddCell(new PdfPCell(new Phrase(c.problem_description, list_font))).Border = Rectangle.NO_BORDER;
                    if (!(c.OpenStatus))
                    {
                        table.AddCell(new PdfPCell(new Phrase("Closed", list_font))).Border = Rectangle.NO_BORDER;
                        table.AddCell(new PdfPCell(new Phrase(c.DateClosed?.ToShortDateString(), list_font))).Border = Rectangle.NO_BORDER;
                    }
                    else
                    {
                        table.AddCell(new PdfPCell(new Phrase("Open", list_font))).Border = Rectangle.NO_BORDER;
                        table.AddCell(new PdfPCell(new Phrase(""))).Border = Rectangle.NO_BORDER;
                    }
                    
                }

                Paragraph date = new Paragraph("Call report written on - " + DateTime.Now.ToString(), date_created_font);
                date.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                document.Add(para);
                document.Add(table);
                document.Add(date);
                document.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error " + ex.Message);
            }
        }
    }
}
