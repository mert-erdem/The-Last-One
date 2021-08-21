using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    private int ammo = 25;
    private float fireRatio = 0.3f;
    private GameObject muzzleFlash;

    public Shotgun(GameObject muzzle)
    {
        #region Setting The Weapon

        weaponName = "Shotgun";
        base.muzzle = muzzle;
        totalAmmo = ammo;
        bulletType = Resources.Load("Shotgun_Bullet") as GameObject;
        muzzleFlash = Resources.Load("Muzzle_Flash") as GameObject;
        fireRate = fireRatio;
        ready = true;
        soundEffect = Resources.Load("Shotgun_Sound") as AudioClip;

        #endregion

        GameManager.actGameOver += this.GameOver;
    }

    public override IEnumerator Fire()
    {
        if (ready && totalAmmo != 0)
        {
            totalAmmo--;
            CanvasController.ChangeAmmoText(totalAmmo);

            #region Sprey Effect
            for (int i = -20; i <= 20; i+=10)
            {
                MonoBehaviour.Instantiate(bulletType, muzzle.transform.position + (muzzle.transform.up - muzzle.transform.right),
                    Quaternion.Euler(new Vector3(muzzle.transform.eulerAngles.x, muzzle.transform.eulerAngles.y, muzzle.transform.eulerAngles.z - i)));
            }
            #endregion

            muzzle.GetComponent<SpriteRenderer>().enabled = true;

            ready = false;

            yield return new WaitForSeconds(fireRate);

            muzzle.GetComponent<SpriteRenderer>().enabled = false;

            ready = true;
        }
    }

    public override void Reload()
    {
        totalAmmo = ammo;
        CanvasController.ChangeNameText(this.weaponName);
        CanvasController.ChangeAmmoText(this.totalAmmo);
    }
}
