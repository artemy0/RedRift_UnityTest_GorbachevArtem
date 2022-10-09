using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    [SerializeField] private float upOffset = 0.001f;

    public Vector3 HitToDropAreaPoin(Vector3 position)
    {
        position.z = transform.position.z;
        position += upOffset * Vector3.up;

        return position;
    }
}
