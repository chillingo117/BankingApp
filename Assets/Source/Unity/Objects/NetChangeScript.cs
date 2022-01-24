using System.Data;
using System.Globalization;
using Source.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Unity.Objects
{
    public class NetChangeScript : AbstractWidget
    {
        private decimal _netChange;
        protected override void UpdateMyData()
        {
            _netChange = 0;
            var dataTable = Data.CurrentMonthData;
            foreach (DataRow row in dataTable.Rows)
            {
                _netChange += decimal.Parse(row["Amount"].ToString());
            }
            textObject.color = _netChange == 0 ? Color.cyan :
                _netChange > 0 ? Color.green : Color.red;
            textObject.text = "Net Change: " + _netChange.ToString(CultureInfo.InvariantCulture);
        }
    }
}