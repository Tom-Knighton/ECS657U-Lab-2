using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;

    private Rigidbody _rigidbody;
    private int _count;

    private const string PickUpTag = "PickUp";

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _count = 0;
    }

    void OnMove(InputValue val)
    {
        moveValue = val.Get<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PickUpTag))
        {
            other.gameObject.SetActive(false);
            _count += 1;
        }
    }

    private void FixedUpdate()
    {
        var movement = new Vector3(moveValue.x, 0, moveValue.y);
        
       _rigidbody.AddForce(movement * (speed * Time.fixedDeltaTime));
    }
}
