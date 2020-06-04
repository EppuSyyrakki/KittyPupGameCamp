using System;
using UnityEditor;
using UnityEngine;


public class SleepBubble : MonoBehaviour
{
    public GameObject _bubbleObject;
    public GameObject _zMark;
    public Student _student;

    public GameObject[] _bubblePositions;

    private int _bubblePosIndex;

    private Vector3 _lastMarkPos;
    private Vector3 _nextMarkPos;
    public float _fallingSpeed;
    private float _lerpT;

    public SpriteRenderer _sr;

    private bool _isVisible { get; set; }
    private bool _isTeacherWatching { get; set; }

    private bool _isFallOk { get; set; }

    public MyIntEvent _asleepEvent;
    public MyIntEvent _awakeEvent;
    public MyIntEvent _fallOkEvent;

    // Start is called before the first frame update
    void Start()
    {
        InitEventHandling();
        InitZMarkRendering();
    }

    private void InitZMarkRendering()
    {
        _bubblePosIndex = 0;
        _isFallOk = false;
    }

    private void InitEventHandling()
    {
        // MyIntEvent class is found inside Student script
        if (_asleepEvent == null) _asleepEvent = new MyIntEvent();
        _asleepEvent.AddListener(SetVisible);

        if (_awakeEvent == null) _awakeEvent = new MyIntEvent();
        _awakeEvent.AddListener(SetInvisible);

        if (_fallOkEvent == null) _fallOkEvent = new MyIntEvent();
        _fallOkEvent.AddListener(SetFallOk);
    }

    // not working at the moment
    private void SetFallOk(int _timesToExecute)
    {
        Debug.Log("is Teacher WATCHING? " + _isTeacherWatching + "is fall ok? " + _isFallOk);
        if(!_isFallOk)
        {
            _isFallOk = true;
            Debug.Log("Is fall ok? " + _isFallOk);
        }
    }

    private void SetInvisible(int _timesToExecute)
    {
        _isVisible = false;
    }

    private void SetVisible(int _timesToExecute)
    {
        _isVisible = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEvents();
        UpdateTeacherWatchingBool();        
        ShowAndHideBubble();
        SetBubbleColor();
        UpdateZMarks();
    }

    private void UpdateZMarks()
    {
        int _score = _student._currentScore;

        // Spawn new marks when score increases
        if (_score == _bubblePosIndex + 1 && _score < _bubblePositions.Length) SpawnNewZ();

        // Destroy all gained marks, if score turns to minus
        if (_score < 0) DestroyZMarks();

        if (!_isVisible) DestroyZMarks();

        // Does not work as it should. 
        // Object end up in the ground at the wrong moment and in way too big size.
        if (!_isVisible && _isFallOk) Fall();
    }

    // not working at the moment
    private void Fall()
    {
        foreach (UnityEngine.GameObject obj in _bubblePositions)
        {
            float _objX = obj.transform.position.x;
            float _step = 0.05f;
            float x = UnityEngine.Random.Range(_objX - _step, _objX + _step);

            Vector3 _newPos = new Vector3(x, 2.2f, obj.transform.position.z);

            Debug.Log("new pos: " + _newPos);

            GameObject _zMarkClone = Instantiate(_zMark, _newPos, Quaternion.identity);

            SpriteFader sf = _zMarkClone.GetComponent<SpriteFader>();
            SpriteRenderer sr = _zMarkClone.GetComponent<SpriteRenderer>();

            sf.fadingIn = true;
        }
    }

    private void DestroyZMarks()
    {

        for (int i = 0; i < _bubblePositions.Length; i++)
        {
            GameObject obj = _bubblePositions[i];
            obj.SetActive(false); 
        }

        _bubblePosIndex = 0;
    }

    private void SpawnNewZ()
    {
        if (_bubblePosIndex < _bubblePositions.Length)
        {
            GameObject _zMarkClone = Instantiate(_zMark, _bubblePositions[_bubblePosIndex].transform);
            
            if(!_bubblePositions[_bubblePosIndex].activeSelf) _bubblePositions[_bubblePosIndex].SetActive(true);

            SpriteFader sf = _zMarkClone.GetComponent<SpriteFader>();
            SpriteRenderer sr = _zMarkClone.GetComponent<SpriteRenderer>();

            sf.fadingIn = true;
            _bubblePosIndex++;
        }
    }

    private void UpdateTeacherWatchingBool()
    {
        _isTeacherWatching = _student._isTeacherWatching;
    }

    private void SetBubbleColor()
    {
        Color _defaultColor = new Color(0.981f, 0.963f, 0.963f, 0.61f);
        Color _burned = new Color(1f, 0.033f, 0.033f, 0.61f);

        if (_isVisible && _isTeacherWatching) _sr.color = _burned;
        if (_isVisible && !_isTeacherWatching) _sr.color = _defaultColor;
    }

    private void HandleEvents()
    {
        float _currentTime = 0f;
        _currentTime += Time.deltaTime;

        if (_student._isSleeping) _asleepEvent.Invoke(1);

        if (!_student._isSleeping) _awakeEvent.Invoke(1);


        Debug.Log("Event handler: is Teacher WATCHING? " + _isTeacherWatching);
        // doesn't work as the teacher is watching even if not
        if (!_isTeacherWatching) _fallOkEvent.Invoke(1);
    }

    private void ShowAndHideBubble()
    {

        // render game object if visible, and not if not
        if (!_isVisible)
        {
            _bubbleObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            _bubbleObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
