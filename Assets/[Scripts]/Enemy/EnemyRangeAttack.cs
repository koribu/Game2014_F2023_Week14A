using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField]
    Transform _bulletSpawnPoint;

    PlayerDetection _playerDetection;

    bool _LOS, _isAttacking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerDetection = GetComponentInChildren<PlayerDetection>();


    }

    // Update is called once per frame
    void Update()
    {
        _LOS = _playerDetection.GetLOS();
    }

    private void FixedUpdate()
    {
        if(_LOS)
        {
            if(!_isAttacking)
                StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        _isAttacking = true;

        BulletManager.Instance().GetBullet(_bulletSpawnPoint.position);
        yield return new WaitForSeconds(1);

        _isAttacking = false;
    }
}
