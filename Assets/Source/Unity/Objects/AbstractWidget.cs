using System.Data;
using System.Globalization;
using Source.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Unity.Objects
{
    public abstract class AbstractWidget : MonoBehaviour, IDataListener
    {
        public Text textObject;
        void Start()
        {
            UpdateMyData();
            ListenToData();
        }

        void Update()
        { }

        public void ListenToData()
        {
            Data.AddListener(this);
        }

        public void propertyChange(string property)
        {
            if (property.Equals(Constants.DataPropertyCurrentMonth)) UpdateMyData();
        }

        protected abstract void UpdateMyData();
    }
}