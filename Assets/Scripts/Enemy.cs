using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IMove, IShoot
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private Rifle enemyGun = null;

    private float firePeriod = 1f;
    private float fireTimer = 0f;
    private Player player = null;
    
    public IGun gun { get { return enemyGun; } }

    [Inject]
    public void Construct(Player player)
    {
        this.player = player;
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
        if (Time.time > fireTimer)
        {
            fireTimer += firePeriod;
            Fire();
        }
    }

    public void Fire()
    {
        enemyGun.Fire();
    }

    public void Move()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * rotationSpeed);
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime*moveSpeed);
    }
}
