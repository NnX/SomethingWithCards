using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
namespace UI
{ 
    public class TimerView : MonoBehaviour
    {
        private const float UpdateTimerDelay = 0.5f;
        [SerializeField] private TextMeshProUGUI timeView;
        private Action _timeExpiredCallback;
        private DateTime _endDateTime;
        private TimeSpan _timeLeft;
        private bool _isStopped;
        
        public void StartTimer(int secondsDuration, Action timeExpiredCallback)
        {
            _isStopped = false;
            _endDateTime = DateTime.Now.AddSeconds(secondsDuration + UpdateTimerDelay);  
            
            _timeExpiredCallback = timeExpiredCallback;
            
            UpdateTimerText();
            UpdateTimerTask().Forget();
        }

        private async UniTask UpdateTimerTask()
        {
            while (!_isStopped)
            {
                await UniTask.Delay(UpdateTimerDelay);
                _timeLeft = _endDateTime - DateTime.Now;
                _isStopped = TimeSpan.Zero > _timeLeft;
                if (_isStopped)
                {
                    _timeExpiredCallback.Invoke();
                    break;
                }

                UpdateTimerText();
            }
        }

        private void UpdateTimerText()
        {
            _timeLeft = _endDateTime - DateTime.Now;
            timeView.text = _timeLeft.ToString(@"mm\:ss");
        }

        public void StopTimer()
        {
            _isStopped = true;
            timeView.text = "Time's up, over";
        }
        
    }
}
