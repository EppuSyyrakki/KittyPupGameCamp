using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrestling;

public class DrawTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject textObj;

    private List<int> scoresBegin;
    private List<int> scoresEnd;
    private int scoreOne;
    private int scoreTwo;
    private bool _isTapOut { get; set; }

    private void Start()
    {
        
        InitTrigger();
    }

    private void InitTrigger()
    {
        textObj.SetActive(false);
        scoresBegin = new List<int>();
        scoresEnd = new List<int>();
        UpdateScores(scoresBegin);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {

            InitScoresEnd();
            GetIsTapOut();

            if (!_isTapOut) textObj.SetActive(true);
        }
    }

    private void GetIsTapOut()
    {
        for (int i = 0; i < 2; i++)
        {
            if (scoresBegin[i] != scoresEnd[i]) _isTapOut = true;
        }
    }

    private void InitScoresEnd()
    {
        UpdateScores(scoresEnd);
    }

    private void UpdateScores(List<int> list)
    {
        scoreOne = ScoreKeeper._playerOneScore;
        scoreTwo = ScoreKeeper._playerTwoScore;
        list.Add(scoreOne);
        list.Add(scoreTwo);
    }
}
