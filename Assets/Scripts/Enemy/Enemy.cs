using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
    public float ofset;
    public Bullet bulet;
    public Transform[] firePositions;

    private Transform playerTrans;
    private Rigidbody2D _rb;
    private Collider2D _col;
    
    private float _timer = 0;
    private Vector3 _dir;

    [SerializeField] private AudioSource enemyShoot;


    void Start()
    {
        _col = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate()
    {
        if (playerTrans == null)
        {
            return;
        }
        RotateEnemy();
        CheckShoot();
        Move();
    }
    private void RotateEnemy()
    {
        _dir = (playerTrans.position - transform.position).normalized;
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }
    private void Move()
    {
        if (Vector3.Distance(transform.position, playerTrans.position) <= ofset)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position + _dir, _col.bounds.extents * 1.15f, 0);
        if (cols.Length > 1)
        {
            return;
        }
        _rb.velocity = _dir * Speed * Time.fixedDeltaTime;
    }
    private void CheckShoot()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer >= 5 / firringSpeed)
        {
            Shoot();
        }
    }
    public void Init(Transform player, EnemySpawner spawner, Vector3 position,float speed, float health, float damage, float firringSpeed ,float bulletSpeed, float money)
    {
        this.Speed = speed;
        this.Health = health;
        this.Damage = damage;
        this.firringSpeed = firringSpeed;
        this.bulletSpeed = bulletSpeed;
        this.money = money;

        transform.position = position;
        this.playerTrans = player;
        this.spawner = spawner;
    }
    private void Shoot()
    {
        if (_rb.position.x <9 && _rb.position.x>-9 && _rb.position.y>-5 && _rb.position.y < 5)
        {
            foreach (var pos in firePositions)
            {
                Bullet bul = Instantiate(bulet);
                bul.transform.position = pos.position;
                bul.transform.rotation = transform.rotation;
                bul.Init(_dir, this.bulletSpeed, this.Damage);
                bul.transform.SetParent(transform.parent);
                enemyShoot.Play();
            }
            _timer = 0;
        }
        
    }
    private void OnDestroy()
    {
        if (money != 0)
        {
            
            var stats = PlayerStats._instance;
            stats.AddMoney(money);
        }
    }
}
