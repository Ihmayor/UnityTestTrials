using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public float angleRange;
    public float speed;

    public GameState gameState;
    private bool hasStartedScan = false;

    private EyeFOV FOVScript;

    private void Start()
    {
        FOVScript = GetComponent<EyeFOV>();
    }


    private void Update()
    {
        if (FOVScript != null)
        {         
            FOVScript.enabled = gameState.IsEyeVisible;
        }

        if (gameState != null && gameState.IsEyeVisible && !hasStartedScan)
        {
            StartCoroutine("Scan", .02f);
        }
    }

    private IEnumerator Scan(float delay)
    {
        hasStartedScan = true;
        while (gameState.IsEyeVisible)
        {
            yield return new WaitForSeconds(delay);
            float angle = Mathf.PingPong(Time.time * speed, angleRange*2) - angleRange;
            transform.rotation = Quaternion.Euler(transform.localRotation.x, -angle + -180, transform.localRotation.z);

            if (FOVScript.visibleTargets.Count > 0)
            {
                gameState.SpotCount++;
                //if (gameState.SpotCount > gameState.SpotLimit)
                //{
                //    gameState.IsFullySpotted = true;
                //}
                FOVScript.visibleTargets.Clear();
            }
        }
        hasStartedScan = false;
    }

}
