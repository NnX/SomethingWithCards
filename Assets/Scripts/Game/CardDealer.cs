using UI;
using UnityEngine;

namespace Game
{
    public class CardDealer : MonoBehaviour
    {
        [SerializeField] private TimerView timerView;

        private void Start()
        {
            timerView.StartTimer(5, OnTimeExpired);
        }

        private void OnTimeExpired()
        {
            timerView.StopTimer();
        }
 
    }
}
