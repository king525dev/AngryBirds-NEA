using UnityEngine;

public class Piggie : MonoBehaviour
{
     // Maximum health of the pig (can be adjusted in the Inspector)
     [SerializeField] private float _maxHealth = 3f;

     // Threshold for the damage to register when the pig collides with an object
     [SerializeField] private float _damageThreshold = 0.2f;

     // Particle effect to show when the pig is popped
     [SerializeField] private GameObject _piggiePoppedParticle;

     // Current health of the pig (starts at _maxHealth)
     private float _currentHealth;

     private void Awake()
     {
          // Initialize the pig's health to the maximum value when it spawns
          _currentHealth = _maxHealth;
     }

     /// <summary>
     /// Reduces the pig's health by the given damage amount.
     /// If health falls to zero or below, the pig dies.
     /// </summary>
     /// <param name="damageAmount">The amount of damage to apply to the pig.</param>
     public void DamagePiggie(float damageAmount)
     {
          _currentHealth -= damageAmount;

          // If health reaches zero or below, call Die() method
          if (_currentHealth <= 0f)
          {
               Die();
          }
     }

     /// <summary>
     /// Handles the death of the pig. It removes the pig from the game, instantiates the "popped" particle effect,
     /// and destroys the pig game object.
     /// </summary>
     private void Die()
     {
          // Remove the pig from the game (called by GameManager)
          GameManager.instance.RemovePiggie(this);

          // Instantiate the particle effect at the pig's position
          Instantiate(_piggiePoppedParticle, transform.position, Quaternion.identity);

          // Destroy the pig's game object after death
          Destroy(gameObject);
     }

     /// <summary>
     /// Detects collisions with other objects and applies damage if the impact velocity exceeds the damage threshold.
     /// </summary>
     /// <param name="collision">The collision data of the object the pig collides with.</param>
     private void OnCollisionEnter2D(Collision2D collision)
     {
          // Calculate the velocity at which the pig collided with an object
          float impactVelocity = collision.relativeVelocity.magnitude;

          // If the collision impact is above the threshold, apply damage
          if (impactVelocity > _damageThreshold)
          {
               DamagePiggie(impactVelocity);
          }
     }
}
