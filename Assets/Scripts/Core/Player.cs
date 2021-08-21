using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public Camera cam;
    Vector2 joystickDir;
    [Range(0, 100)] public int lookingSpeed;
    [SerializeField] private Joystick joystick;

    public Weapon weapon;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject muzzle;

    private List<Weapon> weapons;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite spriteDead;

    private Animator animator;

    private AudioSource audioSource;

    private bool fireOn = false;

    private void Awake()
    {
        weapons = new List<Weapon>
        {
            new Pistol(muzzle),
            new RayGun(muzzle, lineRenderer),
            new Shotgun(muzzle),
            new AutoRifle(muzzle)
        };

        weapon = weapons[0];
    }

    void Start()
    {
        CanvasController.ChangeNameText(weapon.weaponName);
        CanvasController.ChangeAmmoText(weapon.totalAmmo);

        rb = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        animator = transform.GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.clip = weapon.soundEffect;

        CheckSound();
    }

    void Update()
    {
        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        joystickDir = joystick.Direction;
    }

    public void Shoot()
    {
        if (weapon.ready && weapon.totalAmmo != 0)//depends on weapon's fire rate and remaining ammo amount
        {
            audioSource.volume = 0.5f;
            audioSource.Play();
            StartCoroutine(weapon.Fire());
            StartCoroutine(AnimateFire());
        }
        else if (weapon.totalAmmo == 0) return;
    }

    private IEnumerator AnimateFire()
    {
        animator.SetBool("Fire", true);

        yield return new WaitForSeconds(weapon.fireRate);

        animator.SetBool("Fire", false);
    }

    private void FixedUpdate()
    {
        if(fireOn && weapon.weaponName=="Auto") Shoot();
        Aim();
    }

    private void Aim()
    {
        /* with mouse;
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle * lookingSpeed * Time.fixedDeltaTime;
        */
        if(joystickDir != Vector2.zero)//to prevent locking in joystick's origin
        {
            float angle = Mathf.Atan2(joystickDir.y, joystickDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle * lookingSpeed * Time.fixedDeltaTime;
        }      
    }

    private void Die()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Handheld.Vibrate();
#endif
        
        animator.enabled = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        spriteRenderer.sprite = spriteDead;
        GameManager.actGameOver?.Invoke();
        this.enabled = false;
    }

    public void ChangeWeapon()
    {
        if(GameManager.GetGold()>=100)
        {
            GameManager.DecreaseGold(100);

            var used = weapon;
            weapons.Remove(weapon);

            Weapon newWeapon = weapons[Random.Range(0, weapons.Count)];

            weapons.Add(used);

            #region Setting the new weapon
            weapon = newWeapon;
            weapon.Reload();
            audioSource.clip = weapon.soundEffect;
            #endregion
        }
    }

    //check if sound on/off;
    private void CheckSound()
    {
        if (PlayerPrefs.GetString("SOUND", "on").Equals("off"))
            audioSource.enabled = false;
        else
            audioSource.enabled = true;
    }
    
    public void OnPointerDown() => fireOn = true;

    public void OnPointerUp() => fireOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") Die();
    }
}
