using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    [SerializeField] float _health = 2;
    private float _currentHealth;

    public void Hitted(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth > 0)
            return;
            
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _health;
    }

}
