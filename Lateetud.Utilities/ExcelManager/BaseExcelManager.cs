using System.Data;
using System.Data.OleDb;
using System.IO;
namespace Lateetud.Utilities.ExcelManager
{
    public abstract class BaseExcelManager
    {
        public abstract DataTable ReadExcel(string file);
        public abstract DataTable ReadExcel(string file, string sheetname);
        public virtual string ConfigueConnectionString(string file)
        {
            if (Path.GetExtension(file).CompareTo(".xls") == 0)
                return @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007 
        }
        public virtual DataTable ConfigueConnection(string file, string sheetname)
        {
            using (OleDbConnection con = new OleDbConnection(this.ConfigueConnectionString(file)))
            {
                try
                {
                    using (DataTable dtexcel = new DataTable())
                    {
                        new OleDbDataAdapter("select * from [" + sheetname + "$]", con).Fill(dtexcel);
                        return dtexcel;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}