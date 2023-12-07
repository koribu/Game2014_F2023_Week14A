using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BulletBehavior : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    PlayerBehavior _player;
    Vector2 _direction;
    [SerializeField]
    float _throwPower;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction = (_player.transform.position - transform.position).normalized;
    }

    public void Activate()
    {
        StartCoroutine(ThrowBulletRoutine());
    }

     IEnumerator ThrowBulletRoutine()
    {
        yield return new WaitForSeconds(.1f);
        //Throw bullet
        Throw();

        //Wait for awhile
        yield return new WaitForSeconds(1f);
        Debug.LogError("ReadyToDestroy");
        //Destroy it
        BulletManager.Instance().ReturnBullet(transform.gameObject);
    }

     void Throw()
    {
        _rigidBody.AddForce(_direction * _throwPower, ForceMode2D.Impulse);
    }
}
