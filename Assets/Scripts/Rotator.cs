using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private readonly Vector3 _rotateVector = new(15, 30, 45);
    private void Update()
    {
        transform.Rotate(_rotateVector * Time.deltaTime);
    }
}
