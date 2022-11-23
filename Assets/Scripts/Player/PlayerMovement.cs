using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bullets;
    public float speed;

    public float baseDamage = 10;
    public float baseFireSpeed = 1.5f;
    public float baseHealth = 100;
    public Slider healthSlider;

    private Vector2 movement;
    private Vector2 mousePos;

    public Camera cam;

    public Rigidbody2D rb;

    public Bullet bullet;
    public Transform[] buletPos;

    private PlayerStats stats;
    private Vector2 _startPos;
    private GameControl _gameControl;

    private void Awake()
    {
        _gameControl = GetComponentInParent<GameControl>();
        _startPos = transform.position;
        stats = PlayerStats._instance;
    }

    void Update()
    {
        GetInputs();
    }

    private IEnumerator Shooting()
    {
        if(stats == null)
        {
            stats = PlayerStats._instance;
        }
        while(true)
        {
            print(baseFireSpeed + " " + stats.fireSpeedMult);
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
            bulet.transform.SetParent(bullets.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name + collision.gameObject.tag);
        if(collision.gameObject.tag == "bullet")
        {
            Bullet objekt = collision.gameObject.GetComponent<Bullet>();
            this.baseHealth -= objekt.GetDamage();
            print(baseHealth);
            Destroy(objekt.gameObject);
            healthSlider.value = baseHealth / 100;
            if (this.baseHealth <= 0)
            {
                if(_gameControl == null)
                {
                    _gameControl = GetComponentInParent<GameControl>();
                }
                _gameControl.ShowMenu();
            }
        }
    }
    public void ResetToStart()
    {
        baseHealth = 100;
        healthSlider.value = 1;
        transform.position = _startPos;
        StopAllCoroutines();
        StartCoroutine(Shooting());
    }
    public void DestroyBullets()
    {
        var bullet = bullets.GetComponentsInChildren<Bullet>();
        foreach(var x in bullet)
        {
            Destroy(x.gameObject);
        }
    }
}
