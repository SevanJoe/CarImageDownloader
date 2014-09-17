using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Core;
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
                        
                        mWorksheet.Cells[index, 1] = carBrand.Alpha;
                        mWorksheet.Cells[index, 2] = carBrand.Name;

                        // brand logo
                        mRange = mWorksheet.Cells[index, 3];
                        mRange.Select();
                        if (carBrand.LogoUrl.Length > 0)
                        {
                            mWorksheet.Shapes.AddPicture(carBrand.LogoPath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0,
                            ExcelConstants.BRAND_LOGO_SIZE, ExcelConstants.BRAND_LOGO_SIZE);
                        }
                        else
                        {
                            mWorksheet.Cells[index, 3] = ExcelConstants.NO_IMAGE;
                        }

                        mWorksheet.Cells[index, 4] = carBrand.OfficialSite;
                        mWorksheet.Cells[index, 5] = carBrand.Country.Name;

                        // country logo
                        mRange = mWorksheet.Cells[index, 6];
                        mRange.Select();
                        mWorksheet.Shapes.AddPicture(carBrand.Country.LogoPath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0,
                            ExcelConstants.COUNTRY_LOGO_WIDTH, ExcelConstants.COUNTRY_LOGO_HEIGHT);

                        mWorksheet.Cells[index, 7] = carFactory.Name;

                        // factory logo
                        mRange = mWorksheet.Cells[index, 8];
                        if (carFactory.LogoUrl.Length > 0)
                        {
                            mWorksheet.Shapes.AddPicture(carFactory.LogoPath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0,
                            ExcelConstants.BRAND_LOGO_SIZE, ExcelConstants.BRAND_LOGO_SIZE);
                        }
                        else
                        {
                            mWorksheet.Cells[index, 8] = ExcelConstants.NO_IMAGE;
                        }

                        mWorksheet.Cells[index, 9] = carFactory.OfficialSite;
                        mWorksheet.Cells[index, 10] = carType.Name;

                        // car image
                        mRange = mWorksheet.Cells[index, 11];
                        if (carType.ImageUrl.Length > 0)
                        {
                            mWorksheet.Shapes.AddPicture(carType.ImagePath, MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 0,
                            ExcelConstants.CAR_IMAGE_WIDTH, ExcelConstants.CAR_IMAGE_HEIGHT);
                        }
                        else
                        {
                            mWorksheet.Cells[index, 11] = ExcelConstants.NO_IMAGE;
                        }

                        for (int i = 1; i <= ExcelConstants.SHEET_HEADERS.Length; ++i)
                        {
                            mRange = mWorksheet.Cells[index, i];
                            mRange.EntireColumn.AutoFit();
                        }

                        break;
                    }
                }
            }

            saveExcelFile();
        }

        private void saveExcelFile()
        {
            mWorksheet.SaveAs(ExcelConstants.FILE_NAME);
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
