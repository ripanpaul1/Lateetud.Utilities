using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Lateetud.Utilities.ExcelManager
{
    public class ExcelManagerByNetClasses : BaseExcelManager
    {
        public override DataTable ReadExcel(string file)
        {
            return this.ReadfromExcel(file, "Sheet1");
        }
        public override DataTable ReadExcel(string file, string sheetname)
        {
            return this.ReadfromExcel(file, sheetname);
        }
        private DataTable ReadfromExcel(string file, string sheetname)
        {
            try
            {
                string strsheetname = "Sheet1";
                if (!string.IsNullOrWhiteSpace(sheetname)) strsheetname = sheetname;
                DataTable dtexcel = ConfigueConnection(file, sheetname);
                if (dtexcel == null) return null;
                return dtexcel;
            }
            catch
            {
                return null;
            }
        }
    }
}