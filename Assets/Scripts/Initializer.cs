using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Initializer : MonoBehaviour
{
    private void Start()
    {
        Initialize();
        SceneManager.LoadScene(Constants.GameScene);
    }

    private void Initialize()
    {
        // TODO initialize configs
        // TODO load saves
        // TODO initialize something more ..
    } 
}
