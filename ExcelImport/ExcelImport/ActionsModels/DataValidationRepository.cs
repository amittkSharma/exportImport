using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;

namespace ExcelImport.ActionsModels
{
    public class DataValidationRepository
    {
        public ValidatedData StartValidatingData(string[] csvData)
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new[]
            {
                new DataColumn("Account", typeof (string)),
                new DataColumn("Description", typeof (string)),
                new DataColumn("CountryCode", typeof (string)),
                new DataColumn("Value", typeof (int))
            });
            
            var lstObjectsNotAddes = new List<InValidData>();
            foreach (string row in csvData)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    var cell = row.Split(',');

                    int n;
                    bool isNumeric = int.TryParse(cell[3], out n);

                    if (!string.IsNullOrEmpty(cell[0]) && !string.IsNullOrEmpty(cell[1]) &&
                        !string.IsNullOrEmpty(cell[2]) && cell[2].Length == 3 &&
                        isNumeric)
                    {
                        dt.Rows.Add(cell[0], cell[1], cell[2], cell[3]);
                    }
                    else
                    {
                        lstObjectsNotAddes.Add(new InValidData
                        {
                            Account = cell[0],
                            Description = cell[1],
                            CountryCode = cell[2],
                            Value = cell[3]
                        });
                    }
                }
            }

            return new ValidatedData
            {
                ValidData = dt,
                InValidData = lstObjectsNotAddes
            };
        }
    }
}
