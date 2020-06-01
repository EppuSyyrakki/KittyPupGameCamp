using UnityEngine;

public class Teacher : MonoBehaviour
{
    public SpriteRenderer sr;
    public GameObject[] boardPositions;
    public float _writingTimer;
    private float _actualWritingTimer;
    public float _watchingTimer;
    private float _actualWatchingTimer;
    public bool isWatching { get; set; }
    private float _currentTime;


    // Start is called before the first frame update
    void Start()
    {
        TeacherEnter();     
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _actualWritingTimer && !isWatching) TeacherWatching();

        if (_currentTime >= _actualWatchingTimer && isWatching) TeacherNotWatching(); 

        // Debug.Log("Timer is at " + _currentTime);
    }

    private void TeacherWatching()
    {
        _actualWatchingTimer = RandomizeTime(_watchingTimer);
        sr.color = Color.red;   // TODO actual turning, warning sign, talking sound
        isWatching = true;
        _currentTime = 0;
    }

    private void TeacherNotWatching()
    {
        _actualWritingTimer = RandomizeTime(_writingTimer);
        sr.color = Color.white; // TODO actual turning, writing, talking & writing sounds
        isWatching = false;
        _currentTime = 0;
    }

    private void TeacherEnter()
    {
        isWatching = true;
        _currentTime = 0;
        // teacher moves in from the left, watching and talking
    }

    private float RandomizeTime(float time)
    {
        return time;
        // TODO randomization +-20%
    }
}
