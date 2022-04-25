using UnityEngine;

[CreateAssetMenu(menuName = "Custom/FollowProtocol")]
public class FollowProtocol : ScriptableObject
{
    public float Neighbour = 5f;
    public float maxForce = 3f;
    public float maxVelocity = 2f;
}