using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dashboard.Module.Helpers
{
    public class AWSDataSource
    {
        public AWSDataSource(string url)
        {
            Url = url;
        }

        public XafApplication Application
        {
            get; set;
        }

        public string Url
        {
            get;
        }

        public object GetData(IDictionary<string, object> parameters, string worksheetIndex)
        {
            Workbook workbook = new DevExpress.Spreadsheet.Workbook();
            byte[] imageData = null;
            using (var wc = new System.Net.WebClient())
            {
                imageData = wc.DownloadData(Url);
            }
            var ms = new MemoryStream(imageData);
            ms.Position = 0;
            workbook.LoadDocument(ms);
            WorksheetCollection worksheetsCollection = workbook.Worksheets;
            Worksheet worksheet = workbook.Worksheets[int.Parse(worksheetIndex)];
            CellRange rangeA1G44 = worksheet.GetUsedRange();
            //CellRange rangeA1G44 = worksheet["A1:G44"];					
            DataTable table = worksheet.CreateDataTable(rangeA1G44, true);
            DataTableExporter exporter = worksheet.CreateDataTableExporter(rangeA1G44, table, true);
            // Handle value conversion errors.
            //exporter.CellValueConversionError += exporter_CellValueConversionError;
            // Perform the export.
            exporter.Export();

            return table.DefaultView;
        }
        object GetPropertyValue(Cell obj)
        {
            if (obj.Value.IsText)
            {
                return (object)obj.Value.TextValue;
            }
            else if (obj.Value.IsDateTime)
            {
                return (object)obj.Value.DateTimeValue;
            }
            else if (obj.Value.IsBoolean)
            {
                return (object)obj.Value.BooleanValue;
            }
            else if (obj.Value.IsNumeric)
            {
                return (object)obj.Value.NumericValue;
            }
            else
            {
                return (object)obj.Value;
            }


        }
    }
}
