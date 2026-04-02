using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    public void Hitted(float damage)
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
