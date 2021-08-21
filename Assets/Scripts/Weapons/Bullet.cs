using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0, 1000)]
    public int speed;

    [Range(0, 100)]
    public int damage;


    void FixedUpdate() => transform.position += transform.up * speed * Time.fixedDeltaTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<Beetles>().TakeDamage(damage);
                Destroy(this.transform.gameObject);
                PopupController.CreateDamagePopup(collision.transform.position, this.damage);
                break;

            case "PowerUp":
                collision.gameObject.GetComponent<IDropable>().GivePower();
                Destroy(this.transform.gameObject);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
            Destroy(this.transform.gameObject);
    }

}
