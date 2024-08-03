using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Models;
using UnityEngine;

namespace Game
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private CardView cardPrefab;

        private int _deckSize;

        private readonly List<CardView> _cardsDeckList = new();
        private readonly List<Card> _cardsToMatch = new();

        private int _cardsToMatchCount;
        
        public void Init(List<Card> cardDeck, int cardsToMatchCount)
        {
            _cardsToMatchCount = cardsToMatchCount;
            _deckSize = cardDeck.Count;
            _cardsDeckList.Clear();
            for (int i = 0; i < _deckSize; i++)
            {
                Card card = cardDeck[i];
                var cardView = Instantiate(cardPrefab, transform);
                cardView.Init(card, false, OnCardClick);
                _cardsDeckList.Add(cardView);
            }
        }

        private void OnCardClick(Card card)
        {
            _cardsToMatch.Add(card);
            if (_cardsToMatch.Count == _cardsToMatchCount)
            {
                TryToMatchCards();
            } 
            
        }

        private async void TryToMatchCards()
        {
            int matchesFound = 0;
            for (int i = 0; i < _cardsToMatch.Count; i++)
            {
                if(i == 0) continue;
                
                if (_cardsToMatch[i].IsMatches(_cardsToMatch[i - 1].GetValue()))
                {
                    matchesFound++;
                }
            }

            if (matchesFound >= _cardsToMatchCount - 1)
            {
                int value = _cardsToMatch[0].GetValue();
                foreach (var cardView in _cardsDeckList)
                {
                    cardView.TrySetMatched(value);
                }
            }
            else
            {
                await UniTask.Delay(0.5f); // wait for card show animation finished
                HideFaces(skipMatched:true); 
            }
            _cardsToMatch.Clear();
        }

        public async void PlayHint(float secondsShowTime)
        {
            ShowFaces();
            await UniTask.Delay(secondsShowTime);
            HideFaces();
        }

        private void ShowFaces()
        {
            foreach (var cardView in _cardsDeckList)
            {
                cardView.ShowFace();
            }
        }

        private void HideFaces(bool skipMatched = false, bool isForceHide = false)
        {
            foreach (var cardView in _cardsDeckList)
            {
                cardView.HideFace(skipMatched, isForceHide);
            }
        }
        
    }
}
