using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _selectableObjects = new List<GameObject>();

    [SerializeField]
    GameObject _selectionIconGameObject;

    internal GameObject _selectedObject;

    int _selectedIndex = 0;
    bool _isSelecting = false;

    float _prevHorizontalValue = 0;

    private void Awake()
    {
       if (_selectableObjects.Count > 0)
            _selectedObject = _selectableObjects[0];
    }


    void Update()
    {
        float horizontalAxisValue = Input.GetAxis("Horizontal");
        Debug.Log(horizontalAxisValue);
        float diff = Mathf.Abs(horizontalAxisValue - _prevHorizontalValue);
        if (horizontalAxisValue == 0)
        {
            return;
        }

        if (Mathf.Abs(horizontalAxisValue) > 0.6f && !_isSelecting)
        {
            StartCoroutine(SwitchSelection(1));
        }
        else if (Mathf.Abs(horizontalAxisValue) > 0)
        {
            StopCoroutine(SwitchSelection(1));
            StartCoroutine(SwitchSelection(0));
        }

        _prevHorizontalValue = horizontalAxisValue;
    }

    IEnumerator SwitchSelection(float seconds)
    {
        _isSelecting = true;
        _selectedIndex = (_selectedIndex + 1) % (_selectableObjects.Count);

        _selectedObject = _selectableObjects[_selectedIndex];
        _selectionIconGameObject.transform.SetParent(_selectedObject.transform);
        _selectionIconGameObject.transform.localPosition = new Vector3(0, -0.8f);

        yield return new WaitForSeconds(seconds);
        _isSelecting = false;
    }
}
