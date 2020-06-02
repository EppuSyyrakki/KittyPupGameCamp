using UnityEngine;

public class Teacher : MonoBehaviour
{
    public SpriteRenderer sr;
    public GameObject[] boardPositions;
    public GameObject writing;
    public float _walkSpeed;
    public float _writingTimer;   
    public float _watchingTimer;
    public float _difficultyMultiplier;
    public float _minimumTime;
    private float _currentDifficultyMultiplier;
    private float _actualWatchingTimer;
    private float _actualWritingTimer;
    private float _currentTime;
    private float _lerpT;
    private int _boardIndex;
    public bool isWatching { get; set; }
    public bool isReady { get; set; }
    private Vector3 lastPosition;
    private Vector3 nextPosition;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        nextPosition = transform.position;
        _boardIndex = 0;
        Enter();     
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("boardIndex is " + _boardIndex);

        if (_currentTime >= _actualWritingTimer * 0.9f && !isWatching) WatchingSoon();

        if (_currentTime >= _actualWritingTimer && !isWatching) Watching();

        if (_currentTime >= _actualWatchingTimer && isWatching) NotWatching();

        _currentTime += Time.deltaTime;

        if (transform.position.x != nextPosition.x) Walk();
        else _lerpT = 0;
    }

    private void Enter()
    {
        // teacher moves in from the left, watching and talking
        NotWatching();
    }

    private void Walk()
    {
        transform.position = Vector3.Lerp(lastPosition, nextPosition, _lerpT);
        _lerpT += Time.deltaTime * _walkSpeed;
    }

    private void Watching()
    {       
        _actualWatchingTimer = RandomizeTime(_watchingTimer - _currentDifficultyMultiplier) ;

        if (_actualWatchingTimer < _minimumTime)
        {
            _actualWatchingTimer = _minimumTime;
            Debug.Log("minimum time reached.");
        }

        _currentDifficultyMultiplier += _difficultyMultiplier;
        Debug.Log("Watching time is " + _actualWatchingTimer);
        Debug.Log("current difficultyMultiplier is " + _currentDifficultyMultiplier);
        sr.color = Color.red;   // TODO actual turning, warning sign, talking sound
        isWatching = true;
        _currentTime = 0;
    }

    private void WatchingSoon()
    {
        sr.color = Color.yellow;
    }

    private void NotWatching()
    {
        DefineVectors();
        _actualWritingTimer = RandomizeTime(_writingTimer - _currentDifficultyMultiplier);

        if (_actualWritingTimer < _minimumTime) _actualWritingTimer = _minimumTime;

        Debug.Log("Writing time is " + _actualWritingTimer);
        SpawnWriting();
        sr.color = Color.white; // TODO actual turning, writing, talking & writing sounds
        isWatching = false;
        _currentTime = 0;
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
            GameObject[] allWritings = GameObject.FindGameObjectsWithTag("Writing");

            for (int i = 0; i < allWritings.Length; i++)
            {
                SpriteFader sf = allWritings[i].GetComponent<SpriteFader>();
                sf.fadingOut = true;
            }
            
            // TODO game ends
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
