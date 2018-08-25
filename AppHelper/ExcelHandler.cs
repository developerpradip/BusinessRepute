using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
//using Microsoft.Office.Interop;

namespace AppHelper
{
    class ExcelHandler
    {
        private Microsoft.Office.Interop.Excel.Application app = null;
        private Microsoft.Office.Interop.Excel.Workbook workbook = null;
        private Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
        private Microsoft.Office.Interop.Excel.Range workSheet_range = null;

        public ExcelHandler(string fileName)
        {
            createDoc(fileName);
        }
        public ExcelHandler()
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            //app.Visible = true;
            workbook = app.Workbooks.Add(1);
            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
        }

        public void createDoc(string fileName)
        {
            try
            {
                app = new Microsoft.Office.Interop.Excel.Application();
                //app.Visible = true;
                workbook = app.Workbooks.Add(1);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
            }
            catch (Exception )
            {
                Console.Write("Error");
            }
            finally
            {
            }
        }
        public bool Visible
        {
            get 
            {
                return app.Visible;
            }
            set 
            {
                app.Visible = value; 
            }
        }

        public void createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)
        {
            worksheet.Cells[row, col] = htext;
            workSheet_range = worksheet.get_Range(cell1, cell2);
            workSheet_range.Merge(mergeColumns);
            switch (b)
            {
                case "YELLOW":
                    workSheet_range.Interior.Color = Color.Yellow.ToArgb();
                    break;
                case "GRAY":
                    workSheet_range.Interior.Color = Color.Gray.ToArgb();
                    break;
                case "GAINSBORO":
                    workSheet_range.Interior.Color = System.Drawing.Color.Gainsboro.ToArgb();
                    break;
                case "Turquoise":
                    workSheet_range.Interior.Color = System.Drawing.Color.Turquoise.ToArgb();
                    break;
                case "PeachPuff":
                    workSheet_range.Interior.Color = System.Drawing.Color.PeachPuff.ToArgb();
                    break;
                default:
                    //  workSheet_range.Interior.Color = System.Drawing.Color..ToArgb();
                    break;
            }

            
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.Font.Bold = font;
            workSheet_range.ColumnWidth = size;
            if (fcolor.Equals(""))
            {
                workSheet_range.Font.Color = System.Drawing.Color.White.ToArgb();
            }
            else
            {
                workSheet_range.Font.Color = System.Drawing.Color.Black.ToArgb();
            }
        }

        public void addData(int row, int col, string data, string cell1, string cell2, string format)
        {
            worksheet.Cells[row, col] = data;
            workSheet_range = worksheet.get_Range(cell1, cell2);
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.NumberFormat = format;
        }

        public void addData(int row, int col, string data)
        {
            string[] dataCols = data.Split('|');
            for (int nCounter = 0; nCounter < dataCols.Length; nCounter++)
            {
                worksheet.Cells[row, col++] = dataCols[nCounter];
            }
        }


        public void createHeaders(int row, int col, string htext, string hcolor)
        {
            string[] headerCols = htext.Split('|');
            for (int nCounter = 0; nCounter < headerCols.Length ; nCounter++ )
            {   
                worksheet.Cells[row, col++] = headerCols[nCounter];
            }
        }

        
        public  void AddWorksheet(string worksheetName)
        {
            if (app != null && workbook != null)
            {
                bool bfound = false;
                foreach (Worksheet tempworksheet in workbook.Sheets)
                {
                    if (tempworksheet.Name == worksheetName)
                    {
                        worksheet = tempworksheet;
                        bfound = true;
                        break;
                    }
                }
                if (!bfound)
                {
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets.Add();
                    if (worksheet != null)
                    {
                        worksheet.Name = worksheetName;
                    }
                }

                // workbook.ActiveSheet = worksheet;
            }
        }
        public void Save(string folderName, string fileName)
        {
            string strFilePath = string.Empty;
            if (string.IsNullOrEmpty(folderName))
            {
                strFilePath = fileName;
            }
            else
            {
                strFilePath = folderName + "\\" + fileName;
            }
            object misValue = System.Reflection.Missing.Value;


            workbook.SaveAs(strFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            workbook.Close(true, misValue, misValue);
            app.Quit();

            
        }
        
    }
}
   