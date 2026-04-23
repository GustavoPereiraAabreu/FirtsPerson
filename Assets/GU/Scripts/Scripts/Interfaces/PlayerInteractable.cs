using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private GunSystem _gunSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _gunSystem = GetComponentInParent<GunSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out IColectable colectable))
            return;

       switch (collision.gameObject.tag)
       {
            case "Gun":
                _gunSystem.AddNewGun((GunElement)colectable.Colect());
                break;
            case "Ammo":
               
                break;
            case "Armor":
                break;
            default:
                break;
       }

    }
}
