using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Window
{
    public class PopUpWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;
        
        public void Init(string messageText, Action onOkCallback = null, Action onCancelCallback = null)
        {
            message.text = messageText;

            InitButtonListener(okButton, onOkCallback);
            InitButtonListener(cancelButton, onCancelCallback);
        }

        private void InitButtonListener(Button button, Action buttonCallback)
        {
            button.gameObject.SetActive(buttonCallback != null);
            if (buttonCallback != null)
            {
                button.onClick.AddListener(buttonCallback.Invoke);
            }
        }
    }
}
