using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    private Renderer ghostRenderer;

    private bool isSeen = false;

    public float coolDownDurationInSeconds = 1;

    public Transform playerTarget;

    public float movementSpeed = 0.0004f;

    public BoolVariable isGameWon;

    void Start()
    {
        ghostRenderer = gameObject.GetComponent<Renderer>();
        StartCoroutine("seenCoolDown", coolDownDurationInSeconds);
    }

    public void setIsSeen()
    {
        isSeen = true;
    }

    IEnumerator seenCoolDown(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            isSeen = false;
        }
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
            Console.WriteLine("testing seen");

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


    private void MoveTowards()
    {
        if (transform.position != playerTarget.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, movementSpeed);
        }
    }

}
