using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTimer : MonoBehaviour
{
    private float timeLeft;
    private Slider slider;
    private float sliderValue;
    private GameObject reloadTimer;

    private void Awake()
    {
        slider = FindObjectOfType<Slider>();
        reloadTimer = this.gameObject;
        
    }
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            sliderValue += Time.deltaTime;
            slider.value = sliderValue;
        }
        if (slider.value >= slider.maxValue)
        {
            reloadTimer.SetActive(false);
        }
        
    }

    public void StartTimer(float time)
    {
        timeLeft = time;
        sliderValue = 0;
        slider.value = 0;
        slider.maxValue = time;
        reloadTimer.SetActive(true);
    }
}
