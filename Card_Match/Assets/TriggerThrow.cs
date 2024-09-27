using UnityEngine;

public class TriggerThrow : MonoBehaviour
{
    [SerializeField]
    ThrowType typeOfThrow;
    public enum ThrowType
    {
        Right,
        Left,
        Below,
        Above
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GooseMovement goose = collision.gameObject.GetComponent<GooseMovement>();
        if (goose != null)
        {
            if (typeOfThrow == ThrowType.Right)
               goose.ThrowRight();
            else if (typeOfThrow == ThrowType.Left)
                goose.ThrowLeft();
            else if (typeOfThrow == ThrowType.Below)
                goose.ThrowBelow();
        }
    }
}
