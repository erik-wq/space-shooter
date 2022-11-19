using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected float Speed;
    protected float Health;
    protected float Damage;
    protected float firringSpeed;
    protected float bulletSpeed;
    protected float money;
    protected EnemySpawner spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Bullet objekt = collision.gameObject.GetComponent<Bullet>();
            this.Health -= objekt.GetDamage();
            Destroy(objekt.gameObject);
            if (this.Health <= 0)
            {
                PlayerStats._instance.AddMoney(money);
                this.spawner.DestroyEnemy();
                Destroy(this.gameObject);
            }
        }
    }
}
