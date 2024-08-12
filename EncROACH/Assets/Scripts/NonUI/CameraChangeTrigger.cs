using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour
{
    public GameObject cameraTarget = null;
    public GameObject cameraUI = null;

    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CloseCamera();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            OpenCamera();
        }
    }

    void OpenCamera()
    {

        if (cameraTarget != null)
        {
            cameraTarget.SetActive(true);
        }

        if (cameraUI != null)
        {
            cameraUI.SetActive(true);
        }
    }

    void CloseCamera()
    {
        if (cameraTarget != null)
        {
            cameraTarget.SetActive(false);
        }

        if (cameraUI != null)
        {
            cameraUI.SetActive(false);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            CloseCamera();  
        }

    }

}
