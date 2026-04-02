using UnityEngine;

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
        if (Physics.Raycast(_camera.position, _camera.forward))
        {
            print("Hit");

        }

    }
}
