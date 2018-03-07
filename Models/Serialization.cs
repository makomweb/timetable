using Newtonsoft.Json;

namespace Timetable.Models
{
    public static class Serialization
    {
        public static string ToJson(Table table)
        {
            return JsonConvert.SerializeObject(table);
        }

        public static Table FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Table>(json);
        }
    }
}
