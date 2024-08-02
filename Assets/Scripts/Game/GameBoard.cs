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

        private List<CardView> _cardsDeckList = new();
        public void Init(List<Card> cardDeck)
        {
            _deckSize = cardDeck.Count;
            _cardsDeckList.Clear();
            for (int i = 0; i < _deckSize; i++)
            {
                Card card = cardDeck[i];
                var cardView = Instantiate(cardPrefab, transform);
                cardView.Init(card.GetValue(), false);
                _cardsDeckList.Add(cardView);
            }
        }

        public async void PlayHint(float secondsShowTime)
        {
            ShowFaces();
            await UniTask.Delay(secondsShowTime);
            HideFaces();
        }

        public void ShowFaces()
        {
            foreach (var cardView in _cardsDeckList)
            {
                cardView.ShowFace();
            }
        }

        public void HideFaces()
        {
            foreach (var cardView in _cardsDeckList)
            {
                cardView.HideFace();
            }
        }
        
    }
}
