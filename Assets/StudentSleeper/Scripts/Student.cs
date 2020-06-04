using System;
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
    public AudioManager _audioManager;
    public UISystem _ui;
    public Teacher _teacher;
    public SpriteRenderer _sr;

    public bool _isSleeping { get; set; }

    public bool _isTeacherWatching { get; set; }

    public int _totalScore;
    public int _currentScore;

    private float _currentTime = 0f;

    public MyIntEvent _scoreCountingEvent;
    public MyIntEvent _totalScoreCountingEvent;

    public Transform _startPosition;
    public Transform _gamePosition;
    public float _walkSpeed;
    private float _lerpT = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        InitEventHandlers();
        StudentBegin();
        SetScoresToDefault();
    }

    private void InitEventHandlers()
    {

        if (_scoreCountingEvent == null) _scoreCountingEvent = new MyIntEvent();

        if (_totalScoreCountingEvent == null) _totalScoreCountingEvent = new MyIntEvent();

        _scoreCountingEvent.AddListener(CountCurrentScore);

        _totalScoreCountingEvent.AddListener(CountTotalScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (_ui.gameStarted) { 
            UpdateTeacherWatchingBool();

            // user input (mouse button 0) sets student to sleep (down) or awake (up)
            SetToSleep();

            HandleCurrentScoreEvent();
            HandleTotalScoreEvent();

            if (transform.position != _gamePosition.position && !_teacher.isDone) Walk(_startPosition, _gamePosition);
        }
    }

    private void UpdateTeacherWatchingBool()
    {
        _isTeacherWatching = _teacher.isWatching;
    }

    private void HandleTotalScoreEvent()
    {
        if (!_isSleeping)
        {
            if (_totalScoreCountingEvent != null) _totalScoreCountingEvent.Invoke(1);
            _currentScore = 0;
        }
    }

    private void HandleCurrentScoreEvent()
    {

        if (_isSleeping && _scoreCountingEvent != null) _scoreCountingEvent.Invoke(1);
    }

    private void CountCurrentScore(int timesToAdd)
    {
        _currentTime += Time.deltaTime;

        if (_isTeacherWatching) _currentScore = -Mathf.FloorToInt(_currentTime);
        if (!_isTeacherWatching) _currentScore = Mathf.FloorToInt(_currentTime);
    }

    private void CountTotalScore(int timesToAdd)
    {
        _totalScore += _currentScore;

        _currentScore = 0;
        _currentTime = 0;
    }

    private void SetToSleep()
    {
        if (Input.GetMouseButtonDown(0) && !_teacher.isDone)
        {
            _currentScore = 0;
            _sr.color = Color.blue;
            _isSleeping = true;

            _scoreCountingEvent.Invoke(1);
            _audioManager.Effect(true, 0.001f);
        }

        if (Input.GetMouseButtonUp(0) && !_teacher.isDone)
        {
            _sr.color = Color.white;
            _isSleeping = false;
            _audioManager.Effect(false, 2);
        }

        if (_teacher.isDone)
        {
            _sr.flipX = true;
            Walk(_gamePosition, _startPosition);
        }
    }

    private void SetScoresToDefault()
    {
        _currentScore = 0;
        _totalScore = 0;
    }

    private void StudentBegin()
    {
        _sr.color = Color.white;
        _isSleeping = false;
    }

    private void Walk(Transform from, Transform to)
    {
        transform.position = Vector3.Lerp(from.position, to.position, _lerpT);
        _lerpT += Time.deltaTime * _walkSpeed;

        if (transform.position == to.position && !_teacher.isDone) _lerpT = 0;
    }
}
