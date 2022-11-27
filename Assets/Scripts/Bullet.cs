using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _directrion = Vector2.zero;
    private float _speed;

    private Transform _trans;
    private float damage;

    private void Start()
    {
        _trans = GetComponent<Transform>();    
    }

    private void Update()
    {
        if (transform.position.x < -8.6f)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.x > 8.6f)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y > 4.6f)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < -4.6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (_directrion == Vector2.zero) return;
        Vector2 move = _directrion * _speed * Time.deltaTime;
        _trans.position += new Vector3(move.x, move.y, 0);
    }

    public void Init(Vector2 dir, float speed, float damage)
    {
        _directrion = dir;
        _speed = speed;
        this.damage = damage;
        Destroy(this.gameObject, 4);
    }

    public float GetDamage()
    {
        return this.damage;
    }
}
