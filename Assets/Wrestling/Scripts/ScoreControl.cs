using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public PlayerOne _playerOne;
    public PlayerTwo _playerTwo;
    public Controls _controlsOne;
    public Controls _controlsTwo;

    [SerializeField]
    private float _scoreTriggerPos;

    public Transform _tappingOutPos;

    [SerializeField]
    private int _playerOneScore;

    [SerializeField]
    private int _playerTwoScore;

    [Header("Insert correct text including objects here.")]
    public GameObject _scoreTextPlayerOne;
    public GameObject _scoreTextPlayerTwo;

    [SerializeField]
    [Header("Insert the same text including objects here as well.")]
    private TextMeshProUGUI _scoreTextOne = null;

    [SerializeField]
    private TextMeshProUGUI _scoreTextTwo = null;


    [Header("Nothing needs to be inserted in here.")]
    [SerializeField]
    private string _scoreFormatOne = "{0}";

    [SerializeField]
    private string _scoreFormatTwo = "{0}";

    private bool _isOneFall { get; set; }


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
        _scoreTriggerPos = _tappingOutPos.position.y;
        InitScores();
        InitFallBools();
    }

    private void InitFallBools()
    {
        _isOneFall = false;
    }

    private void InitScores()
    {
        _playerOneScore = ScoreKeeper._playerOneScore;
        _playerTwoScore = ScoreKeeper._playerTwoScore;
    }

    // Update is called once per frame
    void Update()
    {
        ListenOneFalls();
        UpdateScoreOutput();
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
            ScoreKeeper._playerOneScore++;
            _controlsTwo.Dislocate();
            _isOneFall = true;
        }
    }

    private void AddScoreForTwo()
    {

        if (!_isOneFall)
        {
            ScoreKeeper._playerTwoScore++;
            _controlsOne.Dislocate();
            _isOneFall = true;
        }
    }

    public int GetPlayerOneScore()
    {
        return ScoreKeeper._playerOneScore;
    }

    public int GetPlayerTwoScore()
    {
        return ScoreKeeper._playerTwoScore;
    }

    private void UpdateScoreOutput()
    {
        _playerOneScore = GetPlayerOneScore();
        _playerTwoScore = GetPlayerTwoScore();
        _scoreTextOne.text = string.Format(format: _scoreFormatOne, arg0: _playerOneScore);
        _scoreTextTwo.text = string.Format(format: _scoreFormatTwo, arg0: _playerTwoScore);
    }
}
