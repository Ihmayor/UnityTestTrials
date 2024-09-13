using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNotification : MonoBehaviour
{
    [SerializeField]
    GameObject Notification;

    private void OnTriggerEnter(Collider other)
    {
        if (Notification == null || !other.gameObject.tag.Contains("Player"))
            return;
        Notification.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (Notification == null || !other.gameObject.tag.Contains("Player"))
            return;
        Notification.SetActive(false);
    }
}
