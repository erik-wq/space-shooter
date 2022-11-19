using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public float baseDamage = 10;
    public float baseFireSpeed = 1.5f;
    public float baseHealth = 100;

    private Vector2 movement;
    private Vector2 mousePos;

    public Camera cam;

    public Rigidbody2D rb;

    public Bullet bullet;
    public Transform[] buletPos;

    private float Health = 1000;
    private PlayerStats stats;

    private void Start()
    {
        stats = PlayerStats._instance;
        StartCoroutine(Shooting());
    }

    void Update()
    {
        GetInputs();
    }

    private IEnumerator Shooting()
    {
        while(true)
        {
            yield return new WaitForSeconds(baseFireSpeed / stats.fireSpeedMult);
            Shoot();
        }
    }

    private void GetInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * speed * Time.fixedDeltaTime;

        Rotate();
    }
    private void Rotate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
    private void Shoot()
    {
        foreach (var trans in buletPos)
        {
            var bulet = Instantiate(bullet);
            bulet.Init((mousePos - rb.position).normalized, 5f , baseDamage * stats.damageMult);
            bulet.transform.position = trans.position;
            bulet.transform.rotation = rb.transform.rotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            Bullet objekt = collision.gameObject.GetComponent<Bullet>();
            this.Health -= objekt.GetDamage();
            Destroy(objekt.gameObject);
            if (this.Health <= 0)
            {
                print("GameOver");
            }
        }
    }
}
