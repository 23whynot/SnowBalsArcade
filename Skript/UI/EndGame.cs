using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image[] image;
    
    public void UpdateStarsImage()
    {
        for (int i = 0; i < health.GetHealth(); i++)
        {
            image[i].gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < image.Length; i++)
        {
            image[i].gameObject.SetActive(false);
        }
    }
}
