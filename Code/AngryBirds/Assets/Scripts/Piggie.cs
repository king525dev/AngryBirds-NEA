using UnityEngine;

public class Piggie : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private float _damageThreshold = 0.2f;
    [SerializeField] private GameObject _piggiePoppedParticle;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void DamagePiggie(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if(_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.instance.RemovePiggie(this);

        Instantiate(_piggiePoppedParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if(impactVelocity > _damageThreshold)
        {
            DamagePiggie(impactVelocity);
        }
    }
}
