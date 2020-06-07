using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Models.Datatable
{
    public class DataTableReqest
    {
        public string DocumentTemplateName { get; set; }
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<Column> Columns { get; set; }
        public Search Search { get; set; }
        public List<Order> Order { get; set; }
    }
}