using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Excel;

using CarImageDownloader.CarData;

namespace CarImageDownloader.Excel
{
    class ExcelTask
    {
        private Application mApplication;
        private Workbook mWorkbook;
        private Worksheet mWorksheet;
        private Range mRange;

        public ExcelTask()
        {
            createExcelFile();
        }

        private void createExcelFile()
        {
            mApplication = new Application();
            mApplication.Visible = false;
            mWorkbook = mApplication.Workbooks.Add();
            mWorksheet = mWorkbook.Sheets[1];
            mWorksheet.Name = ExcelConstants.SHEET_NAME;

            // init header
            for (int i = 1; i <= ExcelConstants.SHEET_HEADERS.Length; ++i)
            {
                mWorksheet.Cells[1, i] = ExcelConstants.SHEET_HEADERS[i - 1];
                mRange = mWorksheet.Cells[1, i];
                mRange.Font.Bold = true;
                mRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                mRange.EntireColumn.AutoFit();
            }
        }

        public void WriteCarInfo(List<CarBrand> carBrandList)
        {
            int index = 1;
            foreach (CarBrand carBrand in carBrandList)
            {
                foreach (CarFactory carFactory in carBrand.CarFactoryList)
                {
                    foreach (CarType carType in carFactory.CarTypeList)
                    {
                        ++index;
                        
                        mWorksheet.Cells[index, 1] = carBrand.Alpha.ToString();
                        mWorksheet.Cells[index, 2] = carBrand.Name;
                        mWorksheet.Cells[index, 3] = carBrand.OfficialSite;
                        mWorksheet.Cells[index, 4] = carBrand.Country.Name;
                        mWorksheet.Cells[index, 5] = carFactory.Name;
                        mWorksheet.Cells[index, 6] = carFactory.OfficialSite;
                        mWorksheet.Cells[index, 7] = carType.Name;
                        mWorksheet.Cells[index, 8] = carType.PriceMin;
                        mWorksheet.Cells[index, 9] = carType.PriceMax;

                        for (int i = 1; i <= ExcelConstants.SHEET_HEADERS.Length; ++i)
                        {
                            mRange = mWorksheet.Cells[index, i];
                            mRange.EntireColumn.AutoFit();
                        }
                    }
                }
            }

            saveExcelFile();
        }

        private void saveExcelFile()
        {
            mWorksheet.SaveAs(Path.Combine(Environment.CurrentDirectory, ExcelConstants.FILE_NAME));
            mWorkbook.Close(true);
            mApplication.Quit();

            releaseObject(mWorksheet);
            releaseObject(mWorkbook);
            releaseObject(mApplication);
        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception e)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
