using System.Collections.Generic;
using System.Data;

namespace ExcelImport.ActionsModels
{
    public class ValidatedData
    {
        public DataTable ValidData { get; set; }
        public List<InValidData> InValidData { get; set; }
    }
}