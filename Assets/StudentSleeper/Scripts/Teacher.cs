﻿using UnityEngine;

public class Teacher : MonoBehaviour
{
    public TeacherState state;
    public bool isWatching { get; set; }
    public UISystem ui;
    public SpriteRenderer sr;
    public GameObject[] boardPositions;
    public GameObject writing;
    public Sprite watching;
    public Sprite notWatching;
    public Sprite rage;
    public Student student;
    public float _walkSpeed;
    public float _writingTimer;   
    public float _watchingTimer;
    public float _difficultyMultiplier;
    public float _minimumTime;
    public float _rageTime;
    private float _currentDifficultyMultiplier;
    private float _actualWatchingTimer;
    private float _actualWritingTimer;
    private float _currentTime;
    private float _lerpT;
    private int _boardIndex; 
    private Vector3 lastPosition;
    private Vector3 nextPosition;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        nextPosition = new Vector3(boardPositions[_boardIndex].transform.position.x, transform.position.y, transform.position.z);
        _boardIndex = 0;
        Enter();     
    }

    // Update is called once per frame
    void Update()
    {
        if (ui.gameStarted) { 
            if (_currentTime >= _actualWritingTimer * 0.9f && !isWatching) WatchingSoon();

            if (_currentTime >= _actualWritingTimer && !isWatching) Watching();

            if (isWatching && student._isSleeping) Raging();

            if (_currentTime >= _actualWatchingTimer && isWatching) NotWatching();

            if (state != TeacherState.done) _currentTime += Time.deltaTime;
            else EndLecture();

            if (transform.position.x != nextPosition.x) Walk();
            else _lerpT = 0;
        }
    }

    private void Enter()
    {
        // teacher starts by watching and talking
        Watching();
    }

    private void Walk()
    {
        transform.position = Vector3.Lerp(lastPosition, nextPosition, _lerpT);
        _lerpT += Time.deltaTime * _walkSpeed;
    }

    private void Watching()
    {
        state = TeacherState.watching;
        _actualWatchingTimer = RandomizeTime(_watchingTimer - _currentDifficultyMultiplier) ;

        _currentDifficultyMultiplier += _difficultyMultiplier;
        sr.sprite = watching;
        isWatching = true;
        _currentTime = 0;

        sr.color = Color.white; 
    }

    private void WatchingSoon()
    {
        sr.color = Color.yellow; // DEBUG
    }

    private void Raging()
    {
        state = TeacherState.raging;
        sr.sprite = rage;
    }

    private void NotWatching()
    {
        state = TeacherState.notWatching;
        DefineVectors();
        _actualWritingTimer = RandomizeTime(_writingTimer - _currentDifficultyMultiplier);

        if (_actualWritingTimer < _minimumTime) _actualWritingTimer = _minimumTime;

        SpawnWriting();
        sr.sprite = notWatching; ;
        isWatching = false;
        _currentTime = 0;

    }

    private void EndLecture()
    {
        sr.sprite = watching; 
    }

    private float RandomizeTime(float time)
    {
        float newTime = Random.Range(0.8f * time, 1.2f * time);
        return newTime;
    }

    private void SpawnWriting()
    {
        if (_boardIndex < 15)
        {
            GameObject writingClone = Instantiate(writing, boardPositions[_boardIndex].transform);
            SpriteFader sf = writingClone.GetComponent<SpriteFader>();
            SpriteRenderer sr = writingClone.GetComponent<SpriteRenderer>();
            sr.flipY = RandomFlip();
            sr.flipX = RandomFlip();
            sf.fadingIn = true;
            _boardIndex++;
        } 
        else
        {
            state = TeacherState.done;
            GameObject[] allWritings = GameObject.FindGameObjectsWithTag("Writing");

            for (int i = 0; i < allWritings.Length; i++)
            {
                SpriteFader sf = allWritings[i].GetComponent<SpriteFader>();
                sf.fadingOut = true;
            } 
        }
    }

    private bool RandomFlip()
    {
        float number = Random.Range(0f, 1f);
        if (number >= 0.49999) return true;
        else return false;
    }

    private void DefineVectors()
    {
        float lastX;
        float nextX;

        if (_boardIndex + 1 < boardPositions.Length && _boardIndex > 0) 
        { 
            lastX = boardPositions[_boardIndex - 1].transform.position.x;
            nextX = boardPositions[_boardIndex].transform.position.x;
        }
        else
        {
            lastX = boardPositions[boardPositions.Length - 1].transform.position.x;
            nextX = boardPositions[0].transform.position.x;
        }

        lastPosition = new Vector3(lastX, transform.position.y, transform.position.z);
        nextPosition = new Vector3(nextX, transform.position.y, transform.position.z);
    }
}
