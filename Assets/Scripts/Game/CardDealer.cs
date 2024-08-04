using System.Collections.Generic;
using Game.Models;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;
using Utils.SaveSystems;
using Window;

namespace Game
{
    public class CardDealer : MonoBehaviour
    {
        private const string GameOverMessage = "Restart Game?";
        private const int TimerDuration = 150;
        private const int SameCardsSize = 2;
        private const float ShowHintDuration = 2f;
        private const float StartGameShowDuration = 3f;
        
        [SerializeField] private int deckSize;
        [SerializeField] private TimerView timerView;
        [SerializeField] private Button hintButton;
        [SerializeField] private GameBoard gameBoard;
        [SerializeField] private PopUpWindow popUpWindowPrefab;
        [SerializeField] private Transform windowsRoot;

        private ISavable _saveSystem;
        private readonly List<Card> _cardsList = new();

        private void Start()
        {
            InitSaveSystem();
            InitGame();
            InitListeners();
        }

        private void InitSaveSystem()
        {
#if USE_FILE_SAVES
            _saveSystem = new FileSave();
#elif USE_PREFS_SAVES
            // TODO implement prefs saves
#elif USE_BASE64_SAVES
            // TODO implement base64 saves
#endif
            _saveSystem.Load();
        }

        private void InitGame()
        {
            PrepareDeck();
            ShuffleDeck();
            gameBoard.Init(_cardsList, SameCardsSize);
            gameBoard.PlayStartGame(StartGameShowDuration);
            timerView.StartTimer(TimerDuration, OnTimeExpired); 
        }

        private void ShuffleDeck()
        {
            _cardsList.Shuffle();
        }

        private void PrepareDeck()
        {
            _cardsList.Clear();
            int currentValue = 1; // star from 1, just to show cards with values
 
            for (int id = 0; id < deckSize; id++)
            {
                Card card = new Card(currentValue, id);
                _cardsList.Add(card);
                if (id % SameCardsSize != 0) // increase card value every odd i, so we have 2 cards with same values
                { 
                    currentValue++; 
                } 
            } 
        }

        private void InitListeners()
        {
            hintButton.onClick.AddListener(ShowHint);
            gameBoard.OnAllCardsMatched += ShowGameOverPopUp;
        }

        private void ShowHint()
        {
            gameBoard.PlayHint(ShowHintDuration);
        }

        private void OnTimeExpired()
        {
            timerView.StopTimer();
            OnGameOver();
        }

        private void OnGameOver()
        {
            ShowGameOverPopUp();
        }

        private void ShowGameOverPopUp()
        {
            var popupWindow = Instantiate(popUpWindowPrefab, windowsRoot);
            popupWindow.Init(GameOverMessage, RestartGame);
        }

        private void RestartGame()
        {
            Destroy(windowsRoot.GetChild(0).gameObject);
            gameBoard.ResetGame();
            timerView.StartTimer(TimerDuration, OnTimeExpired);
        }
 
    }
}
