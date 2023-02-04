using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnCicle;

    [SerializeField] private float minuteToRealTime;
    private float timer;
    public static int randSeed;

    //[SerializeField] private NavMeshBuilder navigation;

    private void Start()
    {
        timer = minuteToRealTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            randSeed = UnityEngine.Random.Range(1, 5);
            OnCicle?.Invoke();

            //navigation.bui
            
            timer = minuteToRealTime;
        }
    }
}
