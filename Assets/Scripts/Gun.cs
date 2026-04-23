using System.Collections;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class GunElement
{
    public UnityEvent OnReload;
    [SerializeField]private string _name;
    [SerializeField]private float _damage;
    [SerializeField] private float _shootRate;
    [SerializeField] private float _ammunation;//Munição total da arma para referência pro jogo
    [SerializeField]private float _clipSize;//Quantidade de balas que o pente suporta
    [SerializeField]private float _reloadTime;//Tempo que leva para recarregar a arma
    private float _ammunationClip;//Pente atual sendo utilizado até ter que puxar mais

    public GunElement(string name, float damage, float shootRate, float ammunation, float reloadTime)
    {
        _name = name;
        _damage = damage;
        _shootRate = shootRate;
        _ammunation = ammunation;
        _reloadTime = reloadTime;
    }
    public void Initialize()
    {
        _ammunationClip = _clipSize;
    }
    public bool UseAmmunation()
    {
        Debug.Log(_ammunationClip);
        if (_ammunationClip <= 0)
        {
            if(_ammunation > 0)
            {
                OnReload.Invoke();
            }

            return false;
        }

        _ammunationClip--;
        return true;//Retorna true se a bala foi utilizada com sucesso
    }
    public void Reload()
    {
        if (_ammunation <= 0)
            return;
        float ammunationToReload = _clipSize - _ammunationClip;
        if (ammunationToReload <= 0)
            return;
        if(_ammunation < ammunationToReload)
        {
            ammunationToReload = _ammunation;
        }
        _ammunationClip += ammunationToReload;
        _ammunation -= ammunationToReload;
    }
    public string Name { get => _name;}
    public float Damage { get => _damage;}
    public float ShootRate { get => _shootRate;}
    public float Ammunation { get => _ammunation;}
    public float ReloadTime { get => _reloadTime;}
}
public class Gun : MonoBehaviour
{
    private Transform _camera;
    [SerializeField]private GunElement _handGun;
    private float _shootTimer;
    private bool _isReloading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main.transform;
        _handGun.Initialize();
        _shootTimer = _handGun.ShootRate;
        _handGun.OnReload.AddListener(() => StartCoroutine(Reload()));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Reload"))
        {
            if (_handGun.Ammunation <= 0)
                return;

            _handGun.OnReload.Invoke();
        }

        _shootTimer += Time.deltaTime;
        if (_isReloading)
            return;
        if (_shootTimer < _handGun.ShootRate)
            return;
        //Verifica se o player atirou
        if (!Input.GetButtonDown("Fire1"))
            return;
        if (!_handGun.UseAmmunation())//Se não tiver munição, não é possível atirar
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
    IEnumerator Reload()
    {
        _isReloading = true;
        //Trava até ser verdadeiro
        //yield return new WaitUntil(() => _handGun.Ammunation > 0);
        //Trava enquanto for verdadeiro
        //yield return new WaitWhile(() => _handGun.Ammunation <= 0);
        yield return new WaitForSeconds(_handGun.ReloadTime);
        _handGun.Reload();
        _shootTimer = _handGun.ShootRate;//Deixa a arma já pronta para atirar
        _isReloading = false;
    }
}
