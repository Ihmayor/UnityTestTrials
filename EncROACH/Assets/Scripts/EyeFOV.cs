using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EyeFOV : MonoBehaviour
{
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;


    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    private float scanDelay = 0.08f;

    private readonly float validYAxis = 0.7f;

    void Start()
    {
        StartCoroutine(nameof(FindTargetsWithDelay), scanDelay);
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(FindTargetsWithDelay), scanDelay);
    }
    private void OnDisable()
    {
        visibleTargets.Clear();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true && enabled)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }


    void FindVisibleTargets()
    {
        visibleTargets.Clear();

        if (!enabled || transform.position.y < validYAxis)
            return;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
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
