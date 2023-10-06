using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue val)
    {
        moveValue = val.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        var movement = new Vector3(moveValue.x, 0, moveValue.y);
        
       _rigidbody.AddForce(movement * (speed * Time.fixedDeltaTime));
    }
}
