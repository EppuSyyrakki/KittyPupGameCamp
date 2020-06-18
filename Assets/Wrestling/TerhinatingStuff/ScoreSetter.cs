using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Wrestling;

public class ScoreSetter : MonoBehaviour
{
    [Header("Input suitable text object.")]
    [SerializeField]
    private TextMeshProUGUI _scoreTextOne = null;

    [SerializeField]
    private TextMeshProUGUI _scoreTextTwo = null;

    [SerializeField]
    private TextMeshProUGUI _winnerText = null;


    [Header("These values need not to be filled.")]
    [SerializeField]
    private string _scoreFormatOne = "{0}";

    [SerializeField]
    private string _scoreFormatTwo = "{0}";

    [SerializeField]
    private string _winnerFormat = "Player {0}";

    [SerializeField]
    private int _scoreOne = 0;

    [SerializeField]
    private int _scoreTwo = 0;

    [SerializeField]
    private int _winnerNumber = 0;

    private bool _isTie { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScores();
        OutputScores();

        UpdateWinner();
        OutputWinner();
    }

    private void OutputWinner()
    {
        if(_isTie) _winnerText.text = _winnerFormat;
        else _winnerText.text = string.Format(format: _winnerFormat, arg0: _winnerNumber);

    }

    private void UpdateWinner()
    {
        if(_winnerText)
        {
            if (_scoreOne > _scoreTwo) _winnerNumber = 1;
            else if (_scoreTwo > _scoreOne) _winnerNumber = 2;
            else
            {
                _winnerFormat = "It's a tie!";
            }
        }
    }

    private void UpdateScores()
    {
        _scoreOne = ScoreKeeper._playerOneScore;
        _scoreTwo = ScoreKeeper._playerTwoScore;
    }

    private void OutputScores()
    {
        if (_scoreTextOne && _scoreTextTwo)
        {
            _scoreTextOne.text = string.Format(format: _scoreFormatOne, arg0: _scoreOne);
            _scoreTextTwo.text = string.Format(format: _scoreFormatTwo, arg0: _scoreTwo);
        }
    }
}
