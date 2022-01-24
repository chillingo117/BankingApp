using System.Data;
using System.Globalization;
using Source.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Unity.Objects
{
    public class TrueChangeScript : AbstractWidget
    {
        private decimal _trueChange;
        protected override void UpdateMyData()
        {
            _trueChange = 0;
            var dataTable = Data.CurrentMonthData;
            foreach (DataRow row in dataTable.Rows)
            {
                if(!row["Payee"].Equals(Constants.MyName))
                    _trueChange += decimal.Parse(row["Amount"].ToString());
            }
            textObject.color = _trueChange == 0 ? Color.cyan :
                _trueChange > 0 ? Color.green : Color.red;
            textObject.text = "Net Change: " + _trueChange.ToString(CultureInfo.InvariantCulture);
        }
    }
}