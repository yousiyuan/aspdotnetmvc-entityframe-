using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Common.Tools.Extension
{
    public static class DataTableExtention
    {
        public static DataTable ToMSSwapRowColumn(this DataTable SourseTable)
        {
            if (SourseTable == null) return null;
            DataTable dt = new DataTable();
            dt.TableName = SourseTable.TableName;
            dt.Columns.Add(SourseTable.Columns[0].ColumnName);
            foreach (DataRow row in SourseTable.Rows)
            {
                dt.Columns.Add(row[0].ToString(), row[0].GetType());
            }
            for (int i = 1; i < SourseTable.Columns.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[SourseTable.Columns[0].ColumnName] = SourseTable.Columns[i].ColumnName;
                foreach(DataRow row in SourseTable.Rows)
                {
                    dr[row[0].ToString()] = row[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}