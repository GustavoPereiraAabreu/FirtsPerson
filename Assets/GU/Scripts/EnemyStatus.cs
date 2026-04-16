using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] float _health = 2;
    private float _currentHealth;

    public void Hitted(float damage, Vector3 shootPoint)
    {
        _currentHealth -= damage;
        
        GameObject blood = Instantiate(_bloodEffect, shootPoint, Quaternion.identity);
        if (_currentHealth > 0)
            return;

        Destroy(blood);
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _health;
    }

}
