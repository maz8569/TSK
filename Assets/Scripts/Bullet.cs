using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{

    private UnityAction<object> onShoot;

    private void Awake()
    {
        onShoot = new UnityAction<object>(OnShoot);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Shoot", onShoot);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Shoot", onShoot);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnShoot(object data)
    {
        Debug.Log("Shoot");
    }

}
