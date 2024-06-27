using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;

    [Range(0,360)]
    public float viewAngle;


    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    void Update()
    {
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }


    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i< targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }

        foreach (Transform target in visibleTargets)
        {
            target.gameObject.GetComponent<Ghost>().setIsSeen();
        }
    }

    public Vector3 DirectionFromAngle(float pAngleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            pAngleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(pAngleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(pAngleInDegrees * Mathf.Deg2Rad));
    }
}
