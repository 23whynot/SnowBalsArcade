using UnityEngine;
using UnityEngine.UI;

public class Slieder : MonoBehaviour
{
    [SerializeField] private Slider powerSlider;
    [SerializeField] private float powerSpeed;
    [SerializeField] private float minPower;
    [SerializeField] private float maxPower;

    private float currentPower;
    private bool increasing = true;

    public float GetPowerValue()
    {
        return currentPower;
    }

    private void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
    }

    private void Update()
    {
        if (increasing)
        {
            currentPower += powerSpeed * Time.deltaTime;
            if (currentPower >= powerSlider.maxValue)
            {
                currentPower = powerSlider.maxValue;
                increasing = false;
            }
        }
        else
        {
            currentPower -= powerSpeed * Time.deltaTime;
            if (currentPower <= powerSlider.minValue)
            {
                currentPower = powerSlider.minValue;
                increasing = true;
            }
        }

        powerSlider.value = currentPower;
    }
}