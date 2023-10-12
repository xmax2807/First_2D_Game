using UnityEngine;
using TMPro;
using System;

public class FPSCalculator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    int frameCount = 0;
    float dt = 0f;
    int fps = 0;
    [SerializeField]private float updateRate = 4f;  // 4 updates per sec.
    float invertRate;
    private void Start(){
        invertRate = 1f/updateRate;
    }
    private void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > invertRate)
        {
            fps =(int) (frameCount / dt);
            text.SetText(fps.ToString());
            frameCount = 0;
            dt -= invertRate;
        }
    }
}