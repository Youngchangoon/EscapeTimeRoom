using System.Collections.Generic;
using UnityEngine;

public class Player_BlockMovement : MonoBehaviour
{
    [SerializeField] private float _minimumValue = 0.1f;

    private HashSet<Collision> _collisionList;

    private void Start()
    {
        _collisionList = new HashSet<Collision>();
    }

    public Vector2 ExcludeDirection(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            return Vector2.zero;
        }

        foreach (Collision collision in _collisionList)
        {
            Vector2 contactDirection = GetContactDirection2D(collision);

            if (IsSameDirection(input, contactDirection))
            {
                input.x = contactDirection.x != 0.0f && input.x != 0.0f ? 0.0f : input.x;
                input.y = contactDirection.y != 0.0f && input.y != 0.0f ? 0.0f : input.y;

                if (input == Vector2.zero)
                {
                    return Vector2.zero;
                }
            }
        }

        return input;
    }

    private Vector2 GetContactDirection2D(Collision collision)
    {
        Vector3 direction = ConvertMinimumValueToZero(GetLookAtCollisionDirection(collision).normalized);

        return new Vector2(direction.x, direction.z);
    }

    private Vector3 GetLookAtCollisionDirection(Collision collision)
    {
        return collision.gameObject.transform.position - transform.position;
    }

    private Vector3 ConvertMinimumValueToZero(Vector3 direction)
    {
        if (Mathf.Abs(direction.x) <= _minimumValue)
            direction.x = 0.0f;
        if (Mathf.Abs(direction.y) <= _minimumValue)
            direction.y = 0.0f;
        if (Mathf.Abs(direction.z) <= _minimumValue)
            direction.z = 0.0f;

        return direction;
    }

    private bool IsSameDirection(Vector2 lhs, Vector2 rhs)
    {
        return Vector2.Dot(lhs, rhs) > 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ExcludeObject"))
        {
            return;
        }

        _collisionList.Add(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ExcludeObject"))
        {
            return;
        }

        _collisionList.Remove(collision);
    }
}