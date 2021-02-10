using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimer : MonoBehaviour
{
    public float startTime = 0;
    private float endTime = 0;
    public Text countText = null;
    public Text countText2 = null;
    private bool countTime = true;
    void Start()
    {
        countText.text = startTime.ToString("F2");
        countText2.text = startTime.ToString("F2");

    }

    void Update()
    {
        if (countTime == true)
        {
            startTime += Time.deltaTime;
            if (startTime < 60.00f)
            {
                countText.text = startTime.ToString("F2");
                countText2.text = startTime.ToString("F2");
            }
            else if (startTime > 60.00f)
            {
                countText.text = (startTime / 60f).ToString("0") + "." + (startTime % 60f).ToString("F2");
                countText2.text = (startTime / 60f).ToString("0") + "." + (startTime % 60f).ToString("F2");
            }
        }

    }
    public void StopTimer()
    {
        countTime = false;
        endTime = startTime;
    }

    public float getScore()
    {
        float score = endTime;
        return score;
    }
}
