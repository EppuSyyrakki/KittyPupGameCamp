using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public PlayerOne _playerOne;
    public PlayerTwo _playerTwo;

    [SerializeField]
    private float _scoreTriggerPos;

    [SerializeField]
    private int _playerOneScore;

    [SerializeField]
    private int _playerTwoScore;

    private bool _isOneFall { get; set; }

    public GameObject _scoreTextPlayerOne;
    public GameObject _scoreTextPlayerTwo;

    [SerializeField]
    private TextMeshProUGUI _scoreTextOne = null;

    [SerializeField]
    private TextMeshProUGUI _scoreTextTwo = null;

    [SerializeField]
    private string _scoreFormatOne = "{ }";
    
    [SerializeField]
    private string _scoreFormatTwo = "{ }";

    private void OnEnable()
    {
        Wrestling.EventManager.onPlayerOneTappedOut += AddScoreForTwo;
        Wrestling.EventManager.onPlayerTwoTappedOut += AddScoreForOne;
    }

    private void OnDisable()
    {
        Wrestling.EventManager.onPlayerOneTappedOut -= AddScoreForTwo;
        Wrestling.EventManager.onPlayerTwoTappedOut -= AddScoreForOne;
    }

    // Start is called before the first frame update
    void Start()
    {
        _scoreTriggerPos = -8.1f;
        InitScores();
        InitFallBools();
    }

    private void InitFallBools()
    {
        _isOneFall = false;
    }

    private void InitScores()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

        ListenOneFalls();
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreTextOne.text = string.Format(format: _scoreFormatOne, arg0: _playerOneScore);
        _scoreTextTwo.text = string.Format(format: _scoreFormatTwo, arg0: _playerTwoScore);
    }

    private void ListenOneFalls()
    {
        if (_playerOne.transform.position.y <= _scoreTriggerPos) Wrestling.EventManager.RaiseOnPointForTwo();
        if (_playerTwo.transform.position.y <= _scoreTriggerPos) Wrestling.EventManager.RaiseOnPointForOne();
    }

    private void AddScoreForOne()
    { 
        if (!_isOneFall)
        {
            _playerOneScore++;
            _isOneFall = true;
        }
    }

    private void AddScoreForTwo()
    {

        if (!_isOneFall)
        {
            _playerTwoScore++;
            _isOneFall = true;
        }
    }

    public int GetPlayerOneScore()
    {
        return _playerOneScore;
    }

    public int GetPlayerTwoScore()
    {
        return _playerTwoScore;
    }
}
