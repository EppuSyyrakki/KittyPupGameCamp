﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{
}

public class Student : MonoBehaviour
{
    public Teacher _teacher;
    public SpriteRenderer _sr;

    private bool _isSleeping;
    private bool _isTimeToIncrementTotal;

    public int _totalScore;
    public int _currentScore;

    private int _plusScore;
    private int _minusScore;

    private float _currentTime = 0f;

    public MyIntEvent _scoreCountingEvent;
    public MyIntEvent _totalScoreCountingEvent;
    

    // Start is called before the first frame update
    void Start()
    {
        if (_scoreCountingEvent == null) _scoreCountingEvent = new MyIntEvent();

        if (_totalScoreCountingEvent == null) _totalScoreCountingEvent = new MyIntEvent();

        _scoreCountingEvent.AddListener(CountCurrentScore);

        _totalScoreCountingEvent.AddListener(CountTotalScore);

        StudentBegin();
        SetScoresToDefault();
    }

    // Update is called once per frame
    void Update()
    {
        // user input (mouse button 0) sets student to sleep (down) or awake (up)
        SetToSleep();

        if (_isSleeping && _scoreCountingEvent != null) _scoreCountingEvent.Invoke(1);

        if (!_isSleeping)
        {
            if (_totalScoreCountingEvent != null) _totalScoreCountingEvent.Invoke(1);

            SetMinorScoresToZero();
        }
    }

    private void CountCurrentScore(int timesToAdd)
    {
        _currentTime += Time.deltaTime;

        if (_teacher.isWatching) _currentScore = -Mathf.FloorToInt(_currentTime);

        if (!_teacher.isWatching) _currentScore = Mathf.FloorToInt(_currentTime);

        // Debug.Log("The score to add is: " + _currentScore);
    }

    private void CountTotalScore(int timesToAdd)
    {
        // Debug.Log("DO THE ADDITION! " + _totalScore + " + " + _currentScore);

        _totalScore += _currentScore;

        _currentScore = 0;
        _currentTime = 0;

        // Debug.Log("Total Score: " + _totalScore + "(" + _currentScore + ")");

    }


    private bool FreeToSleep()
    {
        if (_teacher.isWatching) 
        {
            return false; 
        }
        return true;
    }

    private void SetToSleep()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetMinorScoresToZero();
            _sr.color = Color.blue;
            _isSleeping = true;

            _scoreCountingEvent.Invoke(1);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _sr.color = Color.white;
            _isSleeping = false;

        }
    }

    private void SetMinorScoresToZero()
    {
        _currentScore = 0;
        _minusScore = 0;
        _plusScore = 0;
    }

    private void SetScoresToDefault()
    {
        SetMinorScoresToZero();
        _totalScore = 0;
    }

    private void StudentBegin()
    {
        _sr.color = Color.white;
        _isSleeping = false;
    }

}