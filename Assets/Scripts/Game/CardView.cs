using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CardView : MonoBehaviour
    {
        private const float FadeDuration = 0.3f;
        [SerializeField] private Image cardBack;
        [SerializeField] private Image errorIndicator;
        [SerializeField] private Image matchedIndicator;
        [SerializeField] private TextMeshProUGUI labelView;

        private int _cardValue;
        private Button _cardButton;
        private void Start()
        {
            _cardButton = GetComponent<Button>();
            _cardButton.onClick.AddListener(ShowFace);
        }

        public void Init(int cardValue, bool showFace)
        {
            _cardValue = cardValue;
            InitLabel();

            if (showFace)
            {
                ShowFace();
            }
            else
            {
                HideFace();
            }
        }

        public void ShowFace()
        {
            bool isAlreadyShowed = cardBack.color.a == 0;
            if (isAlreadyShowed)
            {
                return;
            }
            cardBack.DOFade(0, FadeDuration);
        }
        
        public void HideFace()
        {
            bool isAlreadyHidden = Math.Abs(cardBack.color.a - 1) < 0.01f; 
            if (isAlreadyHidden)
            {
                return;
            }
            cardBack.DOFade(1, FadeDuration);
        }

        private void InitLabel()
        {
            labelView.text = _cardValue.ToString();
        }

        public bool IsSame(int value)
        {
            return _cardValue == value;
        }
    }
}
