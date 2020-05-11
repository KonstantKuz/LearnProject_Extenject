using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    void Update()
    {
        transform.position += transform.forward * moveSpeed;
    }
}
