using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closingDoor : MonoBehaviour
{
    private bool isDown;
    private int randInt;

    private void OnEnable()
    {
        TimeManager.OnCicle += CloseDoor;
        isDown = false;
    }

    private void OnDisable()
    {
        TimeManager.OnCicle -= CloseDoor;
    }

    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void CloseDoor()
    {

        if (isDown)
        {
            transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z);
            isDown = false;
        }

        else
        { 
            randInt = Random.Range(1, 5);

            if (TimeManager.randSeed == randInt)
            {
                transform.position = new Vector3(transform.position.x, 4, transform.position.z);
                isDown = true;
            }
        }
    }
}
