using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnDecreaseMoment();              //Declare a Delegate
    public static event OnDecreaseMoment onDecrease;         //Create an Event

    public delegate void OnIncreaseMoment();           //Declare a Delegate
    public static event OnIncreaseMoment onIncrease;    //Create an Event

    public static void RaiseOnDestroy()
    {
        if (onDecrease != null)
        {
            onDecrease();                          //Invoke an Event
        }
    }
    public static void RaiseOnSpawn()
    {
        if (onIncrease != null)
        {
            onIncrease();                       //Invoke an Event
        }
    }
}
