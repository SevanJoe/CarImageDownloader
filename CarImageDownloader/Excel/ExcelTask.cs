using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Excel;

namespace CarImageDownloader.Excel
{
    class ExcelTask
    {
        public ExcelTask()
        {
            createExcelFile();
        }

        private void createExcelFile()
        {
            Application application = new Application();
            application.Visible = false;
            Workbook workBook = application.Workbooks.Add();
            Worksheet workSheet = workBook.Sheets[1];
            workSheet.Name = "Logo";
            workSheet.SaveAs("test.xlsx");
            workBook.Close(true);
            application.Quit();
        }
    }
}
