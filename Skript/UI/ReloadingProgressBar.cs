using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ReloadingProgressBar : MonoBehaviour
{
    [SerializeField] private Image reloadProgressBar;
    [SerializeField] private Image backgroundReloadProgressBar;
    [SerializeField] private float fillDuration = 2f;
    [SerializeField] private FirePoint firePoint;

    private Coroutine reloadCoroutine;
    private bool _isReloading = true;

    public bool isReloading()
    {
        return _isReloading;
    }
    
    private void Start()
    {
        firePoint.OnReloadTrigger += StartReload;
    }

    private void StartReload()
    {
        _isReloading = false;
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
        }
        reloadProgressBar.fillAmount = 0;
        reloadCoroutine =  StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        float startReloadTime = 0;
        
        while (startReloadTime < fillDuration)
        {
            startReloadTime += Time.deltaTime;
            reloadProgressBar.fillAmount = Mathf.Clamp01(startReloadTime / fillDuration);
            yield return null;
        }
        reloadProgressBar.fillAmount = 1;
        _isReloading = true;
        reloadCoroutine = null;
    }
}