using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerIndicator : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value += Time.deltaTime;
    }

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = 0f;
    }
}
