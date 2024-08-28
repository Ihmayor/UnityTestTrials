using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Card")]

public class CardAsset : ScriptableObject
{
    public Vector3 HoverDestination;
    public Vector3 SelectedPosition;
    public Vector3 OriginalPosition;
    public float OriginalRotateZ;

    public Vector2 OriginalScale;

}
