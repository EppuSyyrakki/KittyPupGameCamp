using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTimer : MonoBehaviour
{
    public float _startTime;
    private float _journeyLength;
    private float _speed = 0.08f;

    public IngredientList _belt;

    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(_belt._spawningPos.position, _belt._destroyPos.position);
    }

    // Update is called once per frame
    void Update()
    {

        // The lerp helpers
        float _distCovered = (Time.time - _startTime) * _speed;
        float _fractionOfJourney = _distCovered / _journeyLength;

        // Do the lerp
        transform.position = Vector3.Lerp(transform.position, _belt._destroyPos.position, _fractionOfJourney);

        Debug.Log(name + " pos: " + transform.position.x);
    }

    public float GetJourneyLength()
    {
        return _journeyLength;
    }

    public void DestroyThis()
    {
        Destroy(this);
    }
}
