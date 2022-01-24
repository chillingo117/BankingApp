using Source.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Unity.Objects
{
    public class MonthSelectionScript : MonoBehaviour, IDataListener
    {
        // Start is called before the first frame update

        public Button nextButton;
        public Button prevButton;
        public Text monthText;
        void Start()
        {
            nextButton.onClick.AddListener(() => ChangeMonth(1));
            nextButton.interactable = false;
            prevButton.onClick.AddListener(() => ChangeMonth(-1));
            UpdateMyState();
            ListenToData();
        }

        void Update()
        { }

        void ChangeMonth(int increment)
        {
            var month = int.Parse(Data.CurrentMonth.Split('/')[0]);
            var year = int.Parse(Data.CurrentMonth.Split('/')[1]);
            month += increment;
            switch (month)
            {
                case 13:
                    year++;
                    month = 1;
                    break;
                case 0:
                    year--;
                    month = 12;
                    break;
            }

            var monthAsString = month.ToString();
            if (monthAsString.Length == 1)
            {
                monthAsString = "0" + monthAsString;
            }
            Data.SetCurrentMonth(monthAsString+'/'+year);
        }

        private void UpdateMyState()
        {
            nextButton.interactable = !Data.CurrentMonth.Equals(Data.MaxMonth);
            prevButton.interactable = !Data.CurrentMonth.Equals(Data.MinMonth);
            monthText.text = Data.CurrentMonth;
        }

        public void ListenToData()
        {
            Data.AddListener(this);
        }

        public void propertyChange(string property)
        {
            if (property.Equals(Constants.DataPropertyCurrentMonth))
                UpdateMyState();
        }
    }
}
