using UnityEngine;

namespace Utils
{
    public class TargetFrameRate : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = 15; 
        }
    }
}
