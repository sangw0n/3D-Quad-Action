using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; // 보정값

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
