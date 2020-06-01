using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Student : MonoBehaviour
{
    public Teacher _teacher;

    public SpriteRenderer _sr;

    private bool _isTeacherWatching;
    private bool _isSleeping;

    public int _totalScore;
    public int _currentScore;
    private int _minusScore;

    private float _currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StudentBegin();
    }


    // Update is called once per frame
    void Update()
    {

        SetToSleep();
        ScoreCounting();
    }

    private void ScoreCounting()
    {
        // the score counting does not work as it should
        // Total count is wiped along side with the current count

        _currentTime += Time.deltaTime;

        if (FreeToSleep())
        {

            if (_isSleeping)
            {

                _currentScore = Mathf.RoundToInt(_currentTime);
            }
            else
            {

                _totalScore = _currentScore;
                _currentTime = 0;
            }
        }
        else
        {

            if (_isSleeping)
            {
                _minusScore = -Mathf.RoundToInt(_currentTime);
            }

            _currentScore = 0;
            _currentTime = 0;
        }

        _totalScore = _currentScore + _minusScore;
        _minusScore = 0;

        Debug.Log("current score: " + _currentScore);
        Debug.Log("Total score: " + _totalScore);
    }

    private bool FreeToSleep()
    {
        _isTeacherWatching = _teacher.isWatching;

        if (_isTeacherWatching && _isSleeping)
        {
            return false;
        }

        return true;
    }

    private void SetToSleep()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _sr.color = Color.blue;
            _isSleeping = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _sr.color = Color.white;
            _isSleeping = false;
        }
    }

    private void StudentBegin()
    {
        _sr.color = Color.white;
        _isSleeping = false;
        _currentScore = 0;
        _totalScore = 0;
        _minusScore = 0;
    }

}
