using UnityEngine;
// Class example copied from unity tutorials video https://www.youtube.com/watch?v=WLDgtRNK2VE
// Upgraded from video https://www.youtube.com/watch?v=tE1qH8OxO2Y
namespace Utils
{
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour {
        protected static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        private void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour {
        protected override void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
            }
            base.Awake();
        }
    }
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour {
        protected override void Awake() {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}