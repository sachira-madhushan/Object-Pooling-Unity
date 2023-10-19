using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigi;
    public GameObject fireLocation;
    public Missile missile;
    private IObjectPool<Missile> missilePool;

    private void Awake()
    {
        missilePool = new ObjectPool<Missile>(createMissile,OnGet,OnRelease,OnKill,maxSize:10);
    }
    
    private void OnGet(Missile miss)
    {
        miss.gameObject.SetActive(true);
        miss.transform.position = fireLocation.transform.position;
    }

    private void OnRelease(Missile miss)
    {
        miss.gameObject.SetActive(false);
    }
    private Missile createMissile()
    {
        Missile miss= Instantiate(missile, fireLocation.transform.position, Quaternion.identity);
        miss.SetPool(missilePool);
        return miss;
    }
    
    private void OnKill(Missile miss)
    {
        Destroy(miss.gameObject);
    }

    void Start()
    {
        _rigi = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        float mov = Input.GetAxis("Horizontal");
        _rigi.transform.position = new Vector3(transform.position.x+mov*Time.timeScale*0.5f,transform.position.y,0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            missilePool.Get();
        }
    }
}
