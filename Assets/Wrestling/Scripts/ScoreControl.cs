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

    public static bool _isOneFall { get; set; }


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
        InitScores();
        InitFallBools();
        UpdateScoreOutput();
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

    private void AddScoreForOne()
    { 
        if (!_isOneFall)
        {
            ScoreKeeper._playerOneScore++;
            _controlsTwo.Dislocate();
            _isOneFall = true;
            UpdateScoreOutput();
        }
    }

    private void AddScoreForTwo()
    {
        if (!_isOneFall)
        {
            ScoreKeeper._playerTwoScore++;
            _controlsOne.Dislocate();
            _isOneFall = true;
            UpdateScoreOutput();
        }
    }

    private void UpdateScoreOutput()
    {
        _playerOneScore = ScoreKeeper._playerOneScore;
        _playerTwoScore = ScoreKeeper._playerTwoScore;
        _scoreTextOne.text = string.Format(format: _scoreFormatOne, arg0: _playerOneScore);
        _scoreTextTwo.text = string.Format(format: _scoreFormatTwo, arg0: _playerTwoScore);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            Wrestling.EventManager.RaiseOnPointForTwo();
        }

        if (collision.gameObject.tag == "Player2")
        {
            Wrestling.EventManager.RaiseOnPointForOne();
        }
    }
}
