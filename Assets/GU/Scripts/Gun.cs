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
        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit target, LayerMask.NameToLayer("Shootable")))
        {

            if (Input.GetButtonDown("Fire1"))
            { 
              Destroy(target.transform.gameObject);
            }

        }

    }
}
