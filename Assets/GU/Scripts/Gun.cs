using UnityEngine;
using System.Collections;


[System.Serializable]
public class GunElement 
{
    [SerializeField] private string _name;
    [SerializeField] private float _damage;
    [SerializeField] private float _shootRange;
    [SerializeField] private float _ammunation; //Municação total da arma, ou seja, a quantidade de munição que o player tem para usar durante o jogo
    [SerializeField] private float _clipSize; //Quantidade de munição que o player tem para usar antes de recarregar a arma, ou seja, a quantidade de munição que o player tem no pente atual
    private float _ammunationClip; //Pente atual sendo utilizado, ou seja, a quantidade de munição que o player tem para usar antes de recarregar a arma

    public GunElement(string name, float damage, float shootRange, float ammunation)
    {
        _name = name;
        _damage = damage;
        _shootRange = shootRange;
        _ammunation = ammunation;
    }

    public void Initialize()
    {
        _ammunationClip = _clipSize;
    }
    public bool UseAmmunation()
    {
        Debug.Log(_ammunationClip);
        if (_ammunationClip <= 0)
            return false; // Se não tiver blas no pente, não é possível atirar

        _ammunationClip--;
        return true;// Retorna true se a munição foi usada com sucesso
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
        _handGun.Initialize();
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
        if (!_handGun.UseAmmunation()) // Se não tiver munição, não é possível atirar
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
