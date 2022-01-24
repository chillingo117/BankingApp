using System.IO;
using UnityEngine;

namespace Source.Resources
{
    public static class Constants
    {
        public static string TransactionCsvDirectory => Directory.GetCurrentDirectory() + "\\Assets\\Data\\Everyday.csv";
        public static char[] Delimiter => new[] {','};
        public static string CategoriesDirectory => Directory.GetCurrentDirectory() + "\\Assets\\Data\\Everyday.csv";
        public static string DataPropertyCurrentMonth => "CurrentMonth";

        public static string MyName => "CHEN W";
    }
}