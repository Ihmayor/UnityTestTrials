using UnityEngine;

public class GiantController : MonoBehaviour
{
    private const float AwakeDuration = 7f;
    public GameState GameState;

    public GameObject Hole;
    public GameObject Eye;

    private Vector3 OriginalHolePosition;

    private Vector3 OriginalEyePosition;

    private bool IsEyeOut = false;

    private readonly float AwakeFactor = 0.5f;

    private int trackSpotCount;

    // Start is called before the first frame update
    void Start()
    {
        OriginalHolePosition = Hole.transform.position;
        OriginalEyePosition = Eye.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsNoisy() && !GameState.IsEyeVisible)
        {
            IsEyeOut = true;

            //Reset Noise level now that you're awake.
            GameState.NoiseLevel = 0;
        }

        if (GameState.IsEyeVisible != IsEyeOut)
        {
            Swap();
        }
    }

    private bool IsNoisy()
    {
        return (GameState.NoiseLevel > GameState.NoiseAcceptabilityLevel);
    }

    void SleepEye()
    {
        //Check if noise level or were spotted is still unacceptable
        if ((IsNoisy() || trackSpotCount < GameState.SpotCount) && IsEyeOut)
        {
            Debug.Log(trackSpotCount);
            //Reset Noise level && tracked spot count now that you're awake.
            trackSpotCount = GameState.SpotCount;
            GameState.NoiseLevel = 0;
            Invoke("SleepEye", AwakeDuration);
            return;
        }
        else
        {
            IsEyeOut = false;
        } 

    }

    private void Swap()
    {
        Vector3 currentEyePosition = Eye.transform.position; 
        Vector3 currentHolePosition = Hole.transform.position;

        //Awaken Eye
        if (IsEyeOut)
        {
            Eye.transform.position = Vector3.MoveTowards(currentEyePosition, OriginalHolePosition, Time.deltaTime);
            Hole.transform.position = Vector3.MoveTowards(currentHolePosition, OriginalEyePosition, Time.deltaTime);

            if (currentEyePosition == OriginalHolePosition &&
                currentHolePosition == OriginalEyePosition && 
                !GameState.IsEyeVisible)
            {
                trackSpotCount = GameState.SpotCount;
                Invoke("SleepEye", AwakeDuration);
                GameState.IsEyeVisible = true;
            }
        }
        //Close Eye
        else
        {
            Eye.transform.position = Vector3.MoveTowards(currentEyePosition, OriginalEyePosition, Time.deltaTime);
            Hole.transform.position = Vector3.MoveTowards(currentHolePosition, OriginalHolePosition, Time.deltaTime);

            if (currentEyePosition == OriginalEyePosition && 
                currentHolePosition == OriginalHolePosition &&
                GameState.IsEyeVisible)
            {
                GameState.IsEyeVisible = false;
            }
        }
    }



    private void OnApplicationQuit()
    {
        GameState.Reset();
    }
}
