using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLandingBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

            collision.gameObject.transform.parent = transform;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

            collision.gameObject.transform.SetParent(null);
        
    }
}
