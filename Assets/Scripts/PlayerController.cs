using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;

    private Rigidbody _rigidbody;
    private int _count;

    private const int TotalPickUps = 9;
    private const string PickUpTag = "PickUp";

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _count = 0;
        winText.text = string.Empty;
    }

    void OnMove(InputValue val)
    {
        moveValue = val.Get<Vector2>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(PickUpTag)) return;
        
        other.gameObject.SetActive(false);
        _count += 1;
        SetCountText();
    }

    private void FixedUpdate()
    {
        var movement = new Vector3(moveValue.x, 0, moveValue.y);
        
       _rigidbody.AddForce(movement * (speed * Time.fixedDeltaTime));
    }

    private void SetCountText()
    {
        scoreText.text = $"Score: {_count}";
        if (_count >= TotalPickUps)
        {
            winText.text = "You Win!";
        }
    }
}
