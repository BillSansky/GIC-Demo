using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Samples.GIC_Pres.Text_Example
{
    public class BadScriptExample : MonoBehaviour
    {
        public TMP_Text AmountClicked;
        public int amountOfClickNeeded = 10;

        public GameObject Button;

        private int currentClicks = 0;
        public float duration;
        public float elasticity;

        public float scaleAmount;
        public int vibrato;

        public GameObject WinningScreen;

        public void OnEnable()
        {
            AmountClicked.text = currentClicks.ToString();
        }

        public void RaiseClick()
        {
            currentClicks++;
            AmountClicked.text = currentClicks.ToString();
            Button.transform.DOPunchScale(Vector3.one * scaleAmount, duration, vibrato, elasticity);
            if (currentClicks >= amountOfClickNeeded)
            {
                WinningScreen.SetActive(true);
            }
        }
    }
}