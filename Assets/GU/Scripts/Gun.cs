using UnityEngine;
using System.Collections;


[System.Serializable]
public class GunElement 
{
    [SerializeField] private string _name;
    [SerializeField] private float _damage;
    [SerializeField] private float _shootRange;
    [SerializeField] private float _ammunation;


    public GunElement(string name, float damage, float shootRange, float ammunation)
    {
        _name = name;
        _damage = damage;
        _shootRange = shootRange;
        _ammunation = ammunation;
    }

    public string Name { get => _name; }
    public float Damage { get => _damage; }
    public float ShootRange { get => _shootRange; }
    public float Ammunation { get => _ammunation; }
}

public class Gun : MonoBehaviour
{
    private Transform _camera;
    [SerializeField] private GunElement _handGun;
    private float _shootTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main.transform;
        _shootTimer = _handGun.ShootRange;
    }

    // Update is called once per frame
    void Update()
    {
        _shootTimer += Time.deltaTime;
        if(_shootTimer < _handGun.ShootRange)
            return;
        //Verifica se o player atirou
        if (!Input.GetButtonDown("Fire1"))
            return;
        //Verifica se o player acertou algo
        if (!Physics.Raycast(_camera.position, _camera.forward, out RaycastHit target))
            return;
        //Verifica se o objeto acertado implementa IShootable
        if (!target.collider.TryGetComponent(out IShootable shootable))
            return;

        //Aciona o método do contrato IShootable
        shootable.Hitted(1, target.point);
        _shootTimer = 0;
    }
}
