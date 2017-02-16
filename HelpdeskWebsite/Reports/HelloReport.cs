using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;
using HelpdeskViewModels;
using System.Collections.Generic;

namespace ExercisesWebsite.Reports
{
    public class HelloReport
    {
        static string mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        static Font title_font = new Font(Font.FontFamily.COURIER, 20, Font.BOLDITALIC);
        static Font column_font = new Font(Font.FontFamily.COURIER, 15, Font.BOLDITALIC + Font.UNDERLINE);
        static Font list_font = new Font(Font.FontFamily.COURIER, 14, Font.BOLDITALIC);
        static Font date_created_font = new Font(Font.FontFamily.COURIER, 14, Font.BOLDITALIC);
        static string logo = "Helpdesk.png";

        public void do_it()
        {
            try
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(mappedPath + "pdfs/HelloWorld.pdf", FileMode.Create));
                document.Open();
                Image image = Image.GetInstance(mappedPath + logo);
                image.ScaleAbsolute(150, 125);
                Paragraph para = new Paragraph("Employees", title_font);
                para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                image.SetAbsolutePosition(115f, 710f);
                para.Add(image);
                 
               
                PdfPTable table = new PdfPTable(3);
                table.WidthPercentage = 75.00F;
                PdfPCell cell = new PdfPCell(new Phrase("Title", column_font));
                cell.Border = Rectangle.NO_BORDER;
                table.SpacingBefore = 100f;
                table.AddCell(cell);
                PdfPCell cell2 = new PdfPCell(new Phrase("FirstName", column_font));
                cell2.Border = Rectangle.NO_BORDER;
                table.AddCell(cell2);
                PdfPCell cell3 = new PdfPCell(new Phrase("LastName", column_font));
                cell3.Border = Rectangle.NO_BORDER;
                table.AddCell(cell3);


                EmployeeViewModel emp = new EmployeeViewModel();
                List<EmployeeViewModel> emp_list = emp.GetAll();

                foreach(EmployeeViewModel e in emp_list)
                {
                    table.AddCell(new PdfPCell(new Phrase(e.Title, list_font))).Border = Rectangle.NO_BORDER;
                    table.AddCell(new PdfPCell(new Phrase(e.Firstname, list_font))).Border = Rectangle.NO_BORDER;
                    table.AddCell(new PdfPCell(new Phrase(e.Lastname, list_font))).Border = Rectangle.NO_BORDER;
                }

                Paragraph date = new Paragraph("Employee report written on - " + DateTime.Now.ToString(), date_created_font);
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
