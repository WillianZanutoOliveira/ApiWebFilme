using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace ApiWebFilme.Model
{
    public class DynamicClassMap<T> : ClassMap<T>
    {
        public DynamicClassMap()
        {
            var properties = typeof(T).GetProperties();
            string caminhoArquivo = Path.Combine("Content", "movielist.csv");

            using (var reader = new StreamReader(caminhoArquivo))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                var headerRecord = csv.HeaderRecord;

                foreach (var header in headerRecord)
                {
                    var property = properties.FirstOrDefault(p => p.Name.Equals(header, StringComparison.OrdinalIgnoreCase));

                    if (property != null)
                    {
                        var map = Map(typeof(T), property);
                        map.Name(header);
                    }
                }
            }
        }
    }
}
