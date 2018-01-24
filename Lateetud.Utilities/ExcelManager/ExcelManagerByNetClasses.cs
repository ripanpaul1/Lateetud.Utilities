using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Lateetud.Utilities.ExcelManager
{
    public class ExcelManagerByNetClasses : BaseExcelManager
    {
        public override string ReadExcel(string file)
        {
            return this.ReadfromExcel(file, "Sheet1");
        }
        public override string ReadExcel(string file, string sheetname)
        {
            return this.ReadfromExcel(file, sheetname);
        }
        private string ReadfromExcel(string file, string sheetname)
        {
            string strdata = "error";
            DataTable dtexcel = new DataTable();
            try
            {
                dtexcel = ConfigueConnection(file, sheetname);
                if (dtexcel == null) return strdata;
                if (dtexcel.Rows.Count == 1) return "Nothing in the excel sheet";
                strdata = "";
                for (int row = 1; row < dtexcel.Rows.Count; row++)
                {
                    foreach (DataColumn col in dtexcel.Columns)
                    {
                        strdata += "[[[" + dtexcel.Rows[0][col].ToString().Replace("\"", "") + ":::" + dtexcel.Rows[row][col] + "]]]";
                    }
                }
            }
            catch{}
            finally
            {
                dtexcel.Dispose();
            }
            return strdata;
        }
    }
}