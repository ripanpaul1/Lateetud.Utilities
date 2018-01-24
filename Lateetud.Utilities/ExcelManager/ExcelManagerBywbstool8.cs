
namespace Lateetud.Utilities.ExcelManager
{
    public class ExcelManagerBywbstool8 : BaseExcelManager
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
            string strdata = "";
            foreach (var worksheet in wbstool8.ExcelReader.Workbook.Worksheets(file))
            {
                foreach (var row in worksheet.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        strdata += "[[[" + cell.Text[0] + ":::" + cell.Text + "]]]";
                    }
                }
            }
            return strdata;
        }
    }
}