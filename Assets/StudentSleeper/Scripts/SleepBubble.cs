using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SleepBubble : MonoBehaviour
{
    public SleepBubble _bubble;
    public GameObject _bubbleObject;
    public Student _student;

    public SpriteRenderer _sr;

    private bool _isVisible;

    public MyIntEvent _asleepEvent;
    public MyIntEvent _awakeEvent;

    // Start is called before the first frame update
    void Start()
    {
        _isVisible = false;

        // MyIntEvent class is found inside Student script
        if (_asleepEvent == null) _asleepEvent = new MyIntEvent();
        _asleepEvent.AddListener(SetVisible);

        if (_awakeEvent == null) _awakeEvent = new MyIntEvent();
        _awakeEvent.AddListener(SetInvisible);
    }

    private void SetInvisible(int _timesToExecute)
    {
        _isVisible = false;
    }

    private void SetVisible(int _timesToExecute)
    {
        _isVisible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_student._isSleeping) _asleepEvent.Invoke(1);

        if (!_student._isSleeping) _awakeEvent.Invoke(1);

        showAndHideBubble();
    }

    private void showAndHideBubble()
    {
        // render game object if visible, and not if not
        if (!_isVisible)
        {
            _bubbleObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            _bubbleObject.GetComponent<Renderer>().enabled = true;
        }

    }
}
