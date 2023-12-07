using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField]
    bool _isSensingPlayer = false;
    [SerializeField]
    bool LOS = false;

    PlayerBehavior _player;

    [SerializeField]
    LayerMask _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isSensingPlayer)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, _player.transform.position, _layerMask);

            Vector2 playerDirectionVector = _player.transform.position - transform.position;
            float playerDirection = (playerDirectionVector.x > 0) ? 1 : -1; // +1 that means player at left of our enemy else it is at right
            float enemyDirection = transform.parent.transform.localScale.x;  // +1 that means your enemy looks to right else it looks to left

            string colliderName = hit.collider.name;

            LOS = (hit.collider.gameObject.name == "Player") && (playerDirection == enemyDirection);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _isSensingPlayer = true;
        }
    }

    private void OnDrawGizmos()
    {
       

        if(_isSensingPlayer)
        {
            Color mycolor = (LOS) ? Color.green : Color.red;
            Debug.DrawLine(transform.position, _player.transform.position, mycolor);
        }
            


    }

}
