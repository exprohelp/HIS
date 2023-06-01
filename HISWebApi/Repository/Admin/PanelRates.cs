using HISWebApi.Models;
using HISWebApi.Repository.IPD;
using HISWebApi.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HISWebApi.Repository.Admin
{
    public class PanelRates
    {
        ExcelGenerator exclGen = new ExcelGenerator();
        public string UploadPanelRates(ipPanelRates ipObj)
        {
            string _result = string.Empty;
            DataTable dtPivot = exclGen.GetDataTableByExcel(out _result, ipObj);
            DataTable dtUnPivot= UnpivotDataTable(dtPivot);
            return _result;
        }
        public static DataTable UnpivotDataTable(DataTable dt)
        {
            DataTable dt2 = new DataTable();
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dt2.Columns.Add();
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt2.Rows.Add();
                dt2.Rows[i][0] = dt.Columns[i].ColumnName;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dt2.Rows[i][j + 1] = dt.Rows[j][i];
                }
            }
            return dt2;
        }
    }
}