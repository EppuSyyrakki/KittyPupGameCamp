using UnityEngine;

public class IngredientList : MonoBehaviour
{
    [Header("Increase SIZE if needed and drag ingredient to the new row")]
    public GameObject[] _allIngredients;    
    private GameObject[] _itemsOnTheBelt;

    public Transform _spawningPos;
    public Transform _spawnNextPos;
    public Transform _destroyPos;
    private float _offsetDestroySpot = 5f;

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

        // init the array for items on the conveyor belt
        _itemsOnTheBelt = new GameObject[_maxCountOnBelt];

        _isFirst = true;
    }

    // Update is called once per frame
    void Update()
    {
        AddNewItemOnBelt();
        DestroyItemsAtTheEndOfBelt();
    }

    private void AddNewItemOnBelt()
    {
        _timer += Time.deltaTime;

        int _nextUp = 10;

        if (_isFirst)
        {
            // First item is spawn immediately (spawning event invoked).
            EventManager.RaiseOnSpawn();
            _isFirst = false;
            _timer = 0;
        }
        else if (_priorOne)
        {
            float _xPos = _priorOne.transform.position.x;

            // When prior item reaches a given spot (here:0) on the conveyor belt AND there is room on the belt, spawning event is invoked
            if (_xPos <= _spawnNextPos.position.x && _itemCountOnBelt < _maxCountOnBelt) EventManager.RaiseOnSpawn();
        } 
        else
        {

            // When timer reaches the given moment AND there's room on teh belt, spawning event is invoked.
            if (_timer > _nextUp && _itemCountOnBelt < _maxCountOnBelt) EventManager.RaiseOnSpawn();

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
   
    public void DestroyItemsWhenHit(GameObject _item)
    {
        if (_item && _item.name != "Ingredients") GameObject.Destroy(_item);
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
        _itemClone.AddComponent<ItemTimer>();
        _itemClone.GetComponent<ItemTimer>()._belt = this;
        _itemClone.transform.parent = null;
        float zScaler = _itemClone.transform.localScale.z;
        _itemClone.transform.localScale = new Vector3(100, 100, zScaler);

        // used to follow when next spawn should be released
        _priorOne = _itemClone;

        // add the spawn to belt array
        _itemsOnTheBelt[_itemCountOnBelt] = _itemClone;

        // add to the count
        _itemCountOnBelt++;

        // restart the timer 
        _timer = 0;
    }

    private GameObject GetRandomItemFromArray()
    {
        // Get a random GameObject from the available objects
        int _index = UnityEngine.Random.Range(0, _allIngredients.Length - 1);
        return _allIngredients[_index];
    }
}