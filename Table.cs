using Newtonsoft.Json;
using System.Data;

namespace Timetable
{
    public class Table
    {
        private DataTable _table;
        
        public Table(DataTable table)
        {
            _table = table;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(_table);
        }
    }
}
