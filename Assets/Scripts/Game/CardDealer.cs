using UI;
using UnityEngine;

namespace Game
{
    public class CardDealer : MonoBehaviour
    {
        private const int TimerDuration = 5;
        [SerializeField] private TimerView timerView;

        private void Start()
        {
            timerView.StartTimer(TimerDuration, OnTimeExpired);
        }

        private void OnTimeExpired()
        {
            timerView.StopTimer();
        }
 
    }
}
