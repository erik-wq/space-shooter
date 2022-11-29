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
    public float currentHealth = 100;
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

    public SpriteRenderer spriteRenderer2;
    // public ChangeColor changeColor;

    private TemporalyUpgrades levelUp;
    public GameObject upgradeMenu;


    private void Awake()
    {   
        _gameControl = GetComponentInParent<GameControl>();
        _startPos = transform.position;
        stats = PlayerStats._instance;
    }


    void Update()
    {

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
    }

    private IEnumerator Shooting()
    {
        if(stats == null)
        {
            stats = PlayerStats._instance;
        }
        while(true)
        {
            yield return new WaitForSeconds(stats.currentFireSpeed);
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
        rb.velocity = movement * (speed * (1 + levelUp.speed))* Time.fixedDeltaTime;

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
            var bulet = Instantiate(bullet);
            bulet.Init((mousePos - rb.position).normalized, 5f , stats.currentDamage + levelUp.dmg);
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
            this.currentHealth -= objekt.GetDamage();
            Destroy(objekt.gameObject);
            healthSlider.value = (currentHealth) / (stats.currentHealth*(1 +levelUp.hp));
            if (this.currentHealth <= 0)
            {
                if (_gameControl == null)
                {
                    _gameControl = GetComponentInParent<GameControl>();
                }
                _gameControl.ShowLoss();
                upgradeMenu.SetActive(false);
            }
        }
    }
    public void ResetToStart()
    {
        levelUp = EnemySpawner.instance.levelUps;
        currentHealth = stats.currentHealth;
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
    public void UpgradeHP()
    {
        if(levelUp == null) return;
        levelUp.HpUp();
        currentHealth *= (1 + levelUp.hp);
        AfterUpgrade();
    }
    public void UpgradeDmg()
    {
        if(levelUp == null) return;
        levelUp.DmgUp();
        AfterUpgrade();
    }
    public void UpgradeSpeed()
    {
        if(levelUp == null) return;
        levelUp.SpeedUp();
        AfterUpgrade();
    }
    private void AfterUpgrade()
    {
        EnemySpawner.instance.ResumeSpawning();
        Time.timeScale = 1;
        upgradeMenu.SetActive(false);
    }
}
