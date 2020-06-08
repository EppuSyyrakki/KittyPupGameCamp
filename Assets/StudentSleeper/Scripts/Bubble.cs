using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private enum State
    {
        charging,
        nothing,
        dropped,
        scored
    };
    private State state;

    public GameObject[] positions;
    public Student student;
    public Teacher teacher;
    public SpriteRenderer sr;
    public GameObject z;
    
    private int _pIndex;
    private Color transparent;
    private Color halfTransparent;
    private List<GameObject> allZ;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        transparent = new Color(1, 1, 1, 0);
        halfTransparent = new Color(1, 1, 1, 0.3f);
        sr.color = transparent;
        allZ = new List<GameObject>();
        _pIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (student._isSleeping && teacher.state == TeacherState.notWatching)
        {
            state = State.charging;
            Charging();
        }
        else if (student._isSleeping && teacher.state == TeacherState.raging && state == State.charging)
        {
            state = State.dropped;
            Dropped();
        }
        else if (!student._isSleeping && teacher.state == TeacherState.notWatching && state == State.charging)
        {
            state = State.scored;
            Scored();
        }
        else if (!student._isSleeping && teacher.state == TeacherState.notWatching)
        {
            state = State.nothing;
            sr.color = transparent;
        }
    }

    private void Charging()
    {
        sr.color = Color.white;

        if (_pIndex + 1 == student._currentScore)    // + 1 to not draw Z at 0 score
        {
            GameObject zClone = Instantiate(z, positions[_pIndex].transform);
            allZ.Add(zClone);
            _pIndex++;
        }
    }
    private void Dropped()
    {      
        sr.color = halfTransparent;
        _pIndex = 0;

        foreach (GameObject zTmp in allZ)
        {
            Random.InitState(Random.Range(1, 10));
            Rigidbody rb = zTmp.GetComponent<Rigidbody>();
            SpriteFader sf = zTmp.GetComponent<SpriteFader>();
            rb.useGravity = true;
            rb.AddForce(new Vector3(Random.Range(-50, 50f), Random.Range(10f, 50f), Random.Range(-1, 1)));
            sf.fadingOut = true;
        }
        allZ.Clear();
        state = State.nothing;
    }

    private void Scored()
    {
        state = State.nothing;
        _pIndex = 0;
        sr.color = transparent;

        foreach (GameObject zTmp in allZ)
        {
            Rigidbody rb = zTmp.GetComponent<Rigidbody>();
            SpriteFader sf = zTmp.GetComponent<SpriteFader>();
            rb.useGravity = true;
            rb.AddForce(new Vector3(Random.Range(-3f, 3f), Random.Range(-1f, 0f), Random.Range(-1f, 1f)));
            sf.fadingOut = true;
        }
        allZ.Clear();
        state = State.nothing;
    }
}
