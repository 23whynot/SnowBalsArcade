using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void RestartLevel(int ammount)
    {
        SceneManager.LoadScene(ammount);
    }
}