using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMove, IShoot
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private Pistol playerGun = null;

    public IGun gun { get { return playerGun; } }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        Move();
    }

    public void Fire()
    {
        playerGun.Fire();
    }

    public void Move()
    {
        transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, transform.up);
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed;
        }
    }
}
