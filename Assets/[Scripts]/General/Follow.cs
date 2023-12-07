using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    GameObject _followingObject;

    [SerializeField]
    Vector2 _offset;

   
    void Update()
    {
        transform.position = new Vector3(_followingObject.transform.position.x + _offset.x, _followingObject.transform.position.y + _offset.y, transform.position.z  );


    }
}
