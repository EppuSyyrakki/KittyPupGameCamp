using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalScore : MonoBehaviour
{
    public GameObject _textObj;
    public Student _student;

    [SerializeField]
    private TextMeshProUGUI _scoreText = null;

    [SerializeField]
    private string _scoreFormat = "Total score: {0}";

    [SerializeField]
    private int _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        _textObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (_scoreText)
        {
            _score = _student._totalScore;
            _scoreText.text = string.Format(format: _scoreFormat, arg0: _score);
        }
    }
}
