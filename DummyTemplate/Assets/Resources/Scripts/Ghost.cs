using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    //Data Script Variables
    public BoolVariable   isGameWon;

    //Scene Variables
    public Transform playerTarget;
    public float     coolDownDurationInSeconds = 1;
    public float     movementSpeed             = 0.0004f;

    //Private Settings
    private Renderer ghostRenderer;
    private bool isSeen = false;

    public void setIsSeen()
    {
        isSeen = true;
    }

    void Start()
    {
        ghostRenderer = gameObject.GetComponent<Renderer>();
        StartCoroutine("seenCoolDown", coolDownDurationInSeconds);
    }

    void Update()
    {
        if (isGameWon.Value)
        {
            StopAllCoroutines();
            return;
        }

        if (isSeen)
        {
            foreach(Material m in ghostRenderer.materials)
            {
                m.SetColor("_BaseColor", Color.red);
            }
        }
        else
        {
            foreach (Material m in ghostRenderer.materials)
            {
                m.SetColor("_BaseColor", Color.blue);
            }
            MoveTowards();
        }

    }


    private IEnumerator seenCoolDown(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            isSeen = false;
        }
    }

    private void MoveTowards()
    {
        if (transform.position != playerTarget.position)
        {

            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, movementSpeed);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Player") || other.gameObject.name.Contains("Ghost"))
            transform.position += new Vector3(UnityEngine.Random.Range(-2f, 1f), 0, UnityEngine.Random.Range(-0.3f, 0.4f));

    }

}
