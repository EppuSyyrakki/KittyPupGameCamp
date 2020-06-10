using UnityEngine;

public class IngredientList : MonoBehaviour
{
    [Header("Increase SIZE if needed and drag ingredient to the new row")]
    public GameObject[] _allIngredients;
    
    private GameObject[] _itemsOnTheBelt;
    private float[] _startTimes;

    public Transform _spawningPos;
    public Transform _destroyPos;
    private float _offsetDestroySpot = 5f;

    public float _speed = 0.008f;
    private float _startTime;
    private float _journeyLength;

    private float _timer = 0;
    private int _itemCountOnBelt;
    private int _maxCountOnBelt = 4;
    private int _indexToDestroy;

    private GameObject _priorOne;

    private bool _isFirst { get; set; }

    private void OnEnable()
    {
        EventManager.onDecrease += Decrease;
        EventManager.onIncrease += Increase;
    }

    private void OnDisable()
    {
        EventManager.onDecrease -= Decrease;
        EventManager.onIncrease -= Increase;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitConveyorBelt();
    }

    private void InitConveyorBelt()
    {
        _itemCountOnBelt = 0;

        // item moving (lerp) stuff
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(_spawningPos.position, _destroyPos.position);

        // init the array for items on the conveyor belt
        _itemsOnTheBelt = new GameObject[_maxCountOnBelt];

        _isFirst = true;
    }

    // Update is called once per frame
    void Update()
    {
        AddNewItemOnBelt();
        MoveItems();
        DestroyItemsAtTheEndOfBelt();
    }

    private void AddNewItemOnBelt()
    {
        _timer += Time.deltaTime;

        // time to spawn next item aka the spawning frequency
        // higher the value the longer it takes to spawn next one
        int _nextUp = 10;

        if (_isFirst)
        {
            // First item is spawn immediately (spawning event invoked).
            EventManager.RaiseOnSpawn();
            _isFirst = false;
            _timer = 0;
        }
        else
        {          
            // The second option for spawning event (the one that works better): 
            // When timer reaches the given moment AND there's room on teh belt, spawning event is invoked.
            if (_timer > _nextUp && _itemCountOnBelt < _maxCountOnBelt) EventManager.RaiseOnSpawn();

            // The first option for spawning event:
            // Would love to make this one work, but as it does not yet, it has been commented out
            //float xPos = 12; // arbitrary init value
            //if (_priorOne != null) xPos = _priorOne.transform.position.x; // helper var is set for clarification

            // When prior item reaches a given spot (here:0) on the conveyor belt AND there is room on the belt, spawning event is invoked
            //if (xPos <= 0 && _itemCountOnBelt < _maxCountOnBelt) EventMngr.RaiseOnSpawn();
        }
    }

    private void MoveItems()
    {
        foreach (GameObject item in _itemsOnTheBelt)
        {
            // Move existing item(s)
            if (item != null) MoveItemOnTheBelt(item);

            //if (item != null) Debug.Log(item.name + " is at: " + item.transform.position.x); 
        }

    }

    private void DestroyItemsAtTheEndOfBelt()
    {
        for (int i = 0; i < _itemsOnTheBelt.Length; i++)
        {
            // if any item on the belt reaches the offset corrected destroy pos...
            if (_itemsOnTheBelt[i] != null && _itemsOnTheBelt[i].transform.position.x <= (_destroyPos.position.x + _offsetDestroySpot))
            {
                // ... the index of such item is recorded...
                _indexToDestroy = i;

                // ...and event to destroy is invoked
                EventManager.RaiseOnDestroy();
            }
        }
    }

    private void Increase()
    {
        GameObject _item = GetRandomItemFromArray();
        SpawnItemOnTheBelt(_item);
    }

    private void Decrease()
    {
        // Destroy the Gameobject
        GameObject.Destroy(_itemsOnTheBelt[_indexToDestroy]);
        _itemCountOnBelt--;
    }

    private void SpawnItemOnTheBelt(GameObject _item)
    {
        // spawn item clone
        GameObject _itemClone = Instantiate(_item, _spawningPos);

        // non-kinematic bodies may be shoved into spinning motion by incoming bodies and then they are beyond control
        // both body types may stop (what seems) randomly, but it seems kinematic bodies are maybe wee bit more inclined to do so

        // make them kinematic so they can't shove each other spinning
        //Rigidbody rb = _itemClone.GetComponent<Rigidbody>();
        //rb.isKinematic = true;

        // used to follow when next spawn should be released
        _priorOne = _itemClone;

        // add the spawn to belt array
        _itemsOnTheBelt[_itemCountOnBelt] = _itemClone;

        // add to the count
        _itemCountOnBelt++;

        // restart the timer 
        _timer = 0;
    }

    private void MoveItemOnTheBelt(GameObject _itemClone)
    {
        // NEED HELP:
        // The _distCovered var keeps increasing little by little for each spawn object (so they "launch" faster each time).
        // Couldn't figure out where to "set it to default". Thought that might help. 
        // Or is there another way to keep the moving pace the same for each object spawned?

        // The lerp helpers
        float _distCovered = (Time.time - _startTime) * _speed;
        float _fractionOfJourney = _distCovered / _journeyLength;

        // Do the lerp
        _itemClone.transform.position = Vector3.Lerp(_itemClone.transform.position, _destroyPos.position, _fractionOfJourney);

        //Debug.Log(_itemClone.name + " dist covered pos: " + _distCovered);

    }

    private GameObject GetRandomItemFromArray()
    {
        // Get a random GameObject from the available objects
        int _index = UnityEngine.Random.Range(0, _allIngredients.Length - 1);
        return _allIngredients[_index];
    }
}