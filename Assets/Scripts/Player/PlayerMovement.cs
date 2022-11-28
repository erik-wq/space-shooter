using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI currentDamageTxt;
    public TextMeshProUGUI currentHealthTxt;
    public TextMeshProUGUI currentFireSpeedTxt;

    public TextMeshProUGUI damageAfterUpdate;
    public TextMeshProUGUI healthAfterUpdate;
    public TextMeshProUGUI fireSpeedAfterUpdate;

    [SerializeField] private AudioSource playerShoot;


    public GameObject bullets;
    public float speed;

    public float baseDamage = 10;
    public float baseFireSpeed = 2f;
    public float baseHealth = 100;
    public float currentDamage;
    public float currentHealth = 100;
    public float currentFireSpeed;

    public float maxHealth= 100;

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
       
        
        currentFireSpeed = baseFireSpeed / stats.fsMult * 2;
        
        currentDamageTxt.text = ((int)currentDamage).ToString();
        damageAfterUpdate.text = ((int)(currentDamage * 1.2)).ToString();

        currentFireSpeedTxt.text = Mathf.Round((currentFireSpeed*100f)/10f).ToString();
        fireSpeedAfterUpdate.text = Mathf.Round(((currentFireSpeed/ 1.2f) * 100.0f) /10f).ToString();

        GetInputs();
        if (transform.position.x < -8.5f)
        {
            transform.position = new Vector3(-8.5f, transform.position.y,transform.position.z);
        }
        if (transform.position.x > 8.5f)
        {
            transform.position = new Vector3(8.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.y > 4.5f)
        {
            transform.position = new Vector3(transform.position.x, 4.5f, transform.position.z);
        }
        if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);
        }


        currentHealth = maxHealth + ((baseHealth * stats.hpMult) - baseHealth);
        print(currentHealth);

        currentHealthTxt.text = ((int)currentHealth).ToString();

        healthAfterUpdate.text = ((int)currentHealth * 1.2).ToString();

    }

    private IEnumerator Shooting()
    {
        if(stats == null)
        {
            stats = PlayerStats._instance;
        }
        while(true)
        {
            yield return new WaitForSeconds(currentFireSpeed);
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
    public void Shoot()
    {
        foreach (var trans in buletPos)
        {
            currentDamage = baseDamage + ((baseDamage* stats.dmgMult)-baseDamage);
            var bulet = Instantiate(bullet);
            bulet.Init((mousePos - rb.position).normalized, 5f , currentDamage);
            bulet.transform.position = trans.position;
            bulet.transform.rotation = rb.transform.rotation;
            bulet.transform.SetParent(bullets.transform);
            playerShoot.Play();
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
                if (_gameControl == null)
                {
                    _gameControl = GetComponentInParent<GameControl>();
                }
                _gameControl.ShowLoss();
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
