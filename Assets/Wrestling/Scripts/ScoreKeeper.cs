using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static int _playerOneScore;
    public static int _playerTwoScore;

    private bool _isScoreUpdated { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _isScoreUpdated = false;
    }

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
    }

    private void InitScores()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;
    }

    private void AddScoreForOne()
    {
        if (!_isScoreUpdated)
        {
            // _playerOneScore += _scoreControl.GetPlayerOneScore();
            _isScoreUpdated = true;
        }
    }

    private void AddScoreForTwo()
    {
        if (!_isScoreUpdated)
        {
            //_playerTwoScore += _scoreControl.GetPlayerTwoScore();
            _isScoreUpdated = true;
        }
    }
}
