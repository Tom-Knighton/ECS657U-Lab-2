using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    void Start()
    {
        offset = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}