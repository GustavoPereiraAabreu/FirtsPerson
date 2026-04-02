using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    private Transform _camera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
         //Verifica se o player atirou
        if (!Input.GetButtonDown("Fire1"))
            return;
        //Verifica se o player acertou algo na layer Shootable
        if (!Physics.Raycast(_camera.position, _camera.forward, out RaycastHit target))
            return;
        //Verifica se o objeto acertado implementa IShootable
        if (!target.collider.TryGetComponent(out IShootable shootable))
            return;

        //Aciona o método do contrato IShootable
        shootable.Hitted(1);

    }
}
