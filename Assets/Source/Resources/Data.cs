using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Source.Unity.Objects;

namespace Source.Resources
{
    public static class Data
    {
        public static void Init()
        {
            LoadData();
            LoadCategories();
        }

        private static List<IDataListener> Listeners { get; } = new List<IDataListener>();

        private static Dictionary<string, DataTable> AllData { get; } = new Dictionary<string, DataTable>();
        private static string[] Headers { get; set; }

        public static string CurrentMonth { get; private set; }
        public static string MaxMonth { get; private set; }
        public static string MinMonth { get; private set; }
        public static DataTable CurrentMonthData => AllData[CurrentMonth].Copy();

        public static string[] Categories { get; private set; }

        public static void AddListener(IDataListener listener) => Listeners.Add(listener);

        private static void AnnouncePropertyChange(string property)
        {
            foreach (var listener in Listeners)
            {
                listener.propertyChange(property);
            }
        }

        public static void SetCurrentMonth(string month)
        {
            CurrentMonth = AllData.ContainsKey(month) ? month : "/ Month Not Found /";
            AnnouncePropertyChange(Constants.DataPropertyCurrentMonth);
        }

        private static void LoadCategories()
        {
            var strReader = new StreamReader(Constants.CategoriesDirectory);

            var dataString = strReader.ReadLine();

            var values = dataString!.Split(Constants.Delimiter);
            Categories = values;
        }

        private static void LoadData()
        {
            var strReader = new StreamReader(Constants.TransactionCsvDirectory);
            var end = false;
            Headers = GetHeaderRow(strReader);

            while (!end)
            {
                var data = new DataTable();
                data.Columns.AddRange(
                    Headers.Select(colName => new DataColumn(colName)).ToArray()
                );
                var monthEnd = false;
                string currentMonth = null;

                while (!monthEnd)
                {
                    var dataString = strReader.ReadLine();
                    if (dataString == null)
                    {
                        end = true;
                        monthEnd = true;
                    }
                    else
                    {
                        var values = dataString.Split(Constants.Delimiter);
                        data.Rows.Add(values);
                        var rowMonth = values[0].Split(new[] {'/'}, 2)[1];
                        currentMonth ??= rowMonth;
                        if (currentMonth != rowMonth) monthEnd = true;
                    }
                }

                AllData[currentMonth!] = data.Copy();
                MinMonth ??= currentMonth;
                CurrentMonth = currentMonth;
                MaxMonth = currentMonth;
            }

        }

        private static string[] GetHeaderRow(TextReader reader)
        {
            return reader.ReadLine()!.Split(Constants.Delimiter);
        }
    }
}
