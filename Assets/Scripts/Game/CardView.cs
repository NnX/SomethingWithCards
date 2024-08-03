using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Models;
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

        private bool IsFaceShowed => cardBack.color.a == 0;
        private bool _isMatched;
        
        private Button _cardButton;
        private Action<Card> _onShowCallback;
        private Card _card;
        
        private void Start()
        {
            _cardButton = GetComponent<Button>();
            _cardButton.onClick.AddListener(OnCardClick);
        }

        private void OnCardClick()
        {
            if (_isMatched)
            {
                return;
            }
            
            ShowFace();
            _onShowCallback?.Invoke(_card); 
        }

        public void Init(Card card, bool showFace, Action<Card> onShowCallback)
        {
            _onShowCallback = onShowCallback;
            _card = card; 
            
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
            if (IsFaceShowed)
            {
                return;
            } 
            cardBack.DOFade(0, FadeDuration);
        }
        
        public void HideFace(bool skipMatched = false, bool isForceHide = false)
        {
            Debug.Log($"--- Card ID = {_card.Id}, value = {_card.GetValue()}---");
            Debug.Log($"IsFaceShowed = {IsFaceShowed}, IsMatched = {_isMatched}");
            if ( _isMatched && skipMatched && !isForceHide)
            {
                Debug.Log($" Skip DOFade");
                return;
            }
            
            bool isAlreadyHidden = Math.Abs(cardBack.color.a - 1) < 0.01f; 
            if (isAlreadyHidden)
            {
                Debug.Log($" Skip DOFade");
                return;
            }
            Debug.Log($"!!!!!DOFade");
            cardBack.DOFade(1, FadeDuration); 
        }

        private void InitLabel()
        {
            labelView.text = _card.GetValue().ToString();
        }

        public bool IsSame(int value)
        {
            return _card.IsMatches(value);
        }

        public async void TrySetMatched(int value)
        {
            if (_card.GetValue() == value)
            {
                matchedIndicator.DOFade(0.3f, FadeDuration);
                _isMatched = true;
            }
            else
            {
                if (IsFaceShowed && !_isMatched)
                { 
                    errorIndicator.DOFade(0.7f, FadeDuration);
                    await UniTask.Delay(FadeDuration);
                    errorIndicator.DOFade(0, FadeDuration);
                }
            }
        }
    }
}
