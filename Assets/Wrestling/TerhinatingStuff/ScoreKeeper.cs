using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]
    private int _playerOneScore;

    [SerializeField]
    private int _playerTwoScore;

    public ScoreControl _scoreControl;

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

    private void AddScoreForOne()
    {
        if (!_isScoreUpdated)
        {
            _playerOneScore += _scoreControl.GetPlayerOneScore();
            _isScoreUpdated = true;
        }
    }

    private void AddScoreForTwo()
    {
        if (!_isScoreUpdated)
        {
            _playerTwoScore += _scoreControl.GetPlayerTwoScore();
            _isScoreUpdated = true;
        }
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
        Debug.LogWarning("ScoreKeeper created!!");

    }

    private void InitScores()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_scoreControl) UpdateScores();
    }

    private void UpdateScores()
    {
        if (_scoreControl.GetPlayerOneScore() > _playerOneScore) Wrestling.EventManager.RaiseOnPointForOne();
        if (_scoreControl.GetPlayerTwoScore() > _playerTwoScore) Wrestling.EventManager.RaiseOnPointForTwo();
        Debug.Log("Player 1 updated: " + _playerOneScore);
        Debug.Log("Player 2 updated: " + _playerTwoScore);
    }

    public void SetScoreControl(ScoreControl scoreControl)
    {
        _scoreControl = scoreControl;
        Debug.Log("scores are: ");
        Debug.Log("Player 1: " + _playerOneScore);
        Debug.Log("Player 2: " + _playerTwoScore);
    }

    public int GetInitScorePlayerOne()
    {
        return _playerOneScore;
    }

    public int GetInitScorePlayerTwo()
    {
        return _playerTwoScore;
    }
}
