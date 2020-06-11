using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public Transform holdPos;
    public float _lerpSpeed;
    private Vector3 startPos;
    private Quaternion startRot;
    private GameObject selected;
    private Camera cam;
    private bool _itemSelected = false;
    private float _lerpT;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_itemSelected) GetNewItem();
        if (_itemSelected) LerpSelected();
    }

    private void GetNewItem()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))  // get object that was hit with hit.collider.gameObject                      
            if (hit.collider.gameObject.GetComponent<ItemTimer>() != null) AssignSelected(hit);
    }

    private void AssignSelected(RaycastHit hit)
    {
        Destroy(selected.GetComponent<ItemTimer>());
        selected = hit.collider.gameObject;
        startPos = new Vector3(
            selected.transform.position.x,
            selected.transform.position.y,
            selected.transform.position.z
            );
        startRot = Quaternion.identity;      
        _itemSelected = true;
    }

    private void LerpSelected()
    {
        if (_lerpT < 1)
        {
            selected.transform.position = Vector3.Lerp(startPos, holdPos.position, _lerpT);
            selected.transform.rotation = Quaternion.Lerp(startRot, new Quaternion(0, 0, 0, 0), _lerpT);
            _lerpT += Time.deltaTime * _lerpSpeed;
        }
        else
        {
            _lerpT = 0;
            _itemSelected = false;
        }
    }
}
