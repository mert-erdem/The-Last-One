using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetles : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    [Range(0, 100)]
    private int speed = 5;

    [SerializeField]
    [Range(0, 100)]
    private int startingHealth = 100, currentHealth;

    private bool gameOver = false;

    [Header("Power-Ups & Gold Gains")]
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] [Range(700, 1000)] [Tooltip("Lower, better")] private int powerUpLuck = 950;//lower, better

    private void OnEnable() => currentHealth = startingHealth;

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        GameManager.actGameOver += this.GameOver;
    }

    private void FixedUpdate()
    {
        if (!gameOver) Track(player);
    }

    public void Die() => Destroy(transform.gameObject);

    private void Track(GameObject player)
    {
        this.transform.LookAt(player.transform.position);
        this.transform.Rotate(new Vector3(0, 0, 0));
        Vector2 lookDir = transform.position - player.transform.position;

        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 270f));
        this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;

        if (this.currentHealth <= 0)
        {
            this.Die();
            if (Random.Range(0, 1000) > powerUpLuck) this.DropPowerUp();
        }
            
    }

    private void DropPowerUp()
    {
        if(this!=null)
        {
            var powerUp = powerUps[Random.Range(0, powerUps.Length)];
            Instantiate(powerUp, this.transform.position, powerUp.transform.rotation);
        }        
    }

    private void GameOver()
    {
        if(this!=null)
        {
            transform.GetComponent<Animator>().enabled = false;
            gameOver = true;
        }        
    }
}
