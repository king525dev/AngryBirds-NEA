using UnityEngine;

public class AngryBird : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;

    private bool _hasBeenLaunched;
    private bool _shouldFaceVelocityDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        //Bird disobeys Gravity on Slingshot
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _circleCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_hasBeenLaunched && _shouldFaceVelocityDirection)
        {
        //Bird Looks inthe diretion of Flight
        transform.right = _rb.linearVelocity;
        }
    }

    public void LaunchBird(Vector2 direction, float force)
    {
        //Apply Gravity to Bird
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _circleCollider.enabled = true;

        //Apply the force
        _rb.AddForce(direction * force, ForceMode2D.Impulse);

        _hasBeenLaunched = true;
        _shouldFaceVelocityDirection = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _shouldFaceVelocityDirection = false;
    }
}
