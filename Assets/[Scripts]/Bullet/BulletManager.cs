using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BulletManager : MonoBehaviour
{
    /// <summary>
    /// ----------------------SINGLETON SECTION-----------------------
    /// </summary>
    private static BulletManager instance;

    private BulletManager()
    {
        //Something that you setup when you create an instance 
        Initialize();
    }

    public static BulletManager Instance()
    {

        return instance ??= new BulletManager(); 
    }

    /// <summary>
    /// ----------------------SINGLETON SECTION-----------------------
    /// </summary>

    private Queue<GameObject> _bulletPool;
    int _poolSize; // define how many bullet we create at the beginning
    GameObject _bulletPrefab;
    Transform _bulletParent;

    // Start is called before the first frame update
    void Initialize()
    {
        _poolSize = 30;
        _bulletPool = new Queue<GameObject>();
        _bulletPrefab = Resources.Load<GameObject>("Prefabs/CherryBomb");
    }

    public void BuildPool()
    {
        _bulletParent = GameObject.Find("[BULLETS]").transform;

        for(int i = 0; i < _poolSize; i++)
        {
            
            _bulletPool.Enqueue(CreateBullet());
        }
    }

    GameObject CreateBullet()
    {
        GameObject temp = GameObject.Instantiate(_bulletPrefab, _bulletParent);
        temp.SetActive(false);
        return temp;
    }

    public GameObject GetBullet(Vector3 spawnPoint)
    {
        if(_bulletPool.Count < 0)
        {
            _bulletPool.Enqueue(CreateBullet());
        }

        GameObject bullet;
        bullet = _bulletPool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = spawnPoint;

        // StartCoroutine(bullet.GetComponent<BulletBehavior>().ThrowBulletRoutine());
        bullet.GetComponent<BulletBehavior>().Activate();
        return bullet;

    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
