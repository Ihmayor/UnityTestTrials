using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistController : MonoBehaviour, IClickHandler
{
    public IntVariable TotalScientists;

    public GameObject Model;
    public void OnClick()
    {
        TotalScientists.Value -= 1;
        Model.SetActive(true);
        Destroy(gameObject);
    }

}
