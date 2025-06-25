using System;
using TMPro;
using UnityEngine;

namespace U1W
{
    public class CurrentTimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float offsetSecond;

        public float OffsetSecond
        {
            get => offsetSecond;
            set => offsetSecond = value;
        }

        private void Update()
        {
            DateTime dateTime = DateTime.Now;
            dateTime += TimeSpan.FromSeconds(offsetSecond);
            string dateText = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
            text.text = dateText;
        }
    }
}
