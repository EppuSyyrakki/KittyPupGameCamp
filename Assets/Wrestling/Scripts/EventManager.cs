using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wrestling
{
    public class EventManager : MonoBehaviour
    {
        public delegate void OnPlayerOneTappedOut();                        //Declare a Delegate
        public static event OnPlayerOneTappedOut onPlayerOneTappedOut;      //Create an Event

        public delegate void OnPlayerTwoTappedOut();           //Declare a Delegate
        public static event OnPlayerTwoTappedOut onPlayerTwoTappedOut;    //Create an Event

        public static void RaiseOnPointForTwo()
        {
            if (onPlayerOneTappedOut != null)
            {
                onPlayerOneTappedOut();                          //Invoke an Event
            }
        }
        public static void RaiseOnPointForOne()
        {
            if (onPlayerTwoTappedOut != null)
            {
                onPlayerTwoTappedOut();                       //Invoke an Event
            }
        }
    }
}
