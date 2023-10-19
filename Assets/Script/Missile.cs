using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Missile : MonoBehaviour
{
    private IObjectPool<Missile> missilePool;

    public void SetPool(IObjectPool<Missile> pool)
    {
        missilePool = pool;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y+0.1f);
    }
    private void OnBecameInvisible()
    {
        missilePool.Release(this);
    }
}
