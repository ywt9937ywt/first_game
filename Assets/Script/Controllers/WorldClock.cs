using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldClock : MonoBehaviour
{
    //public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static int Minute { get; private set; }
    public static int hour { get; private set; }

    //private int segmentPerGameHour = 2;
    private int currentSegment;

    private float RealTimeSecToGameh = 30f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentSegment = 0;
        Minute = 0;
        hour = 0;
        timer = RealTimeSecToGameh;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        /*if(timer <= RealTimeSecToGameh/2 && currentSegment == 0)
        {
            currentSegment++;
            OnHourChanged.Invoke();
        }
        if (timer <= 0 && currentSegment == 1)
        {
            hour++;
            currentSegment++;
            OnHourChanged.Invoke();
        }*/
        if (timer <= 0)
        {
            hour++;
            //OnHourChanged.Invoke();
        }
    }

}
