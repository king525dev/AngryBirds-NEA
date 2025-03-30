using UnityEngine;

public class AngryBird : MonoBehaviour
{
     // Rigidbody component for physics interactions
     private Rigidbody2D _rb;

     // Circle collider component to detect collisions
     private CircleCollider2D _circleCollider;

     // Flags to track if the bird has been launched and if it should face its velocity direction
     private bool _hasBeenLaunched;
     private bool _shouldFaceVelocityDirection;

     private void Awake()
     {
          // Get Rigidbody2D and CircleCollider2D components attached to the bird
          _rb = GetComponent<Rigidbody2D>();
          _circleCollider = GetComponent<CircleCollider2D>();

          // Disable gravity initially to keep the bird stationary on the slingshot
          _rb.bodyType = RigidbodyType2D.Kinematic;

          // Disable collider so the bird doesn't interact with other objects before launch
          _circleCollider.enabled = false;
     }

     private void FixedUpdate()
     {
          // If the bird has been launched, update its rotation to face the direction of its velocity
          if (_hasBeenLaunched && _shouldFaceVelocityDirection)
          {
               transform.right = _rb.linearVelocity;
          }
     }

     public void LaunchBird(Vector2 direction, float force)
     {
          // Enable gravity once the bird is launched
          _rb.bodyType = RigidbodyType2D.Dynamic;

          // Enable collider so the bird can now interact with other objects
          _circleCollider.enabled = true;

          // Apply force to launch the bird in the specified direction
          _rb.AddForce(direction * force, ForceMode2D.Impulse);

          // Set flags to track the bird's launch state
          _hasBeenLaunched = true;
          _shouldFaceVelocityDirection = true;
     }

     private void OnCollisionEnter2D(Collision2D collision)
     {
          // Stop the bird from continuously facing the velocity direction after a collision
          _shouldFaceVelocityDirection = false;
     }
}
