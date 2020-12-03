using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
    public Rigidbody2D bullet;
    public int damage = 1;
    public float bulletSpeed = 500f;

    public const int bulletCount = 3;
    public const int spread = 1;
    private float[] range = {-30f, 0f, 30f };

    private PlayerController player;

    void Start () {
        player = GetComponent<PlayerController> ();
    }

    public void BasicShoot (int direction) {
        Rigidbody2D bulletClone = (Rigidbody2D) Instantiate (bullet, transform.position, transform.rotation);
        bulletClone.gameObject.tag = "Player";
        bulletClone.GetComponent<Bullet> ().damage = damage;

        switch (direction) {
            case 0:
                bulletClone.AddForce (bulletClone.transform.up * bulletSpeed);
                StandShootAnimation (direction);
                break;
            case 1:
                bulletClone.AddForce (-bulletClone.transform.up * bulletSpeed);
                StandShootAnimation (direction);
                break;
            case 2:
                bulletClone.AddForce (-bulletClone.transform.right * bulletSpeed);
                StandShootAnimation (direction);
                break;
            case 3:
                bulletClone.AddForce (bulletClone.transform.right * bulletSpeed);
                StandShootAnimation (direction);
                break;
        }
    }

    public void SpreadShoot (int direction) {
        Quaternion newRotation = transform.rotation;
        for (int i = 0; i < bulletCount; i++) {
            float offSet = range[i];
            newRotation = Quaternion.Euler (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + offSet);

            Rigidbody2D bulletClone = (Rigidbody2D) Instantiate (bullet, transform.position, newRotation);
            bulletClone.gameObject.tag = "Player";
            bulletClone.GetComponent<Bullet> ().damage = damage;
            switch (direction) {
                case 0:
                    bulletClone.AddForce (bulletClone.transform.up * bulletSpeed);
                    StandShootAnimation (direction);
                    break;
                case 1:
                    bulletClone.AddForce (-bulletClone.transform.up * bulletSpeed);
                    StandShootAnimation (direction);
                    break;
                case 2:
                    bulletClone.AddForce (-bulletClone.transform.right * bulletSpeed);
                    StandShootAnimation (direction);
                    break;
                case 3:
                    bulletClone.AddForce (bulletClone.transform.right * bulletSpeed);
                    StandShootAnimation (direction);
                    break;
            }
        }
    }

    public void BurstShoot (int direction) {
        StartCoroutine(BurstWait(direction));
    }

    private void StandShootAnimation (int direction) {
        if (player._body.velocity == Vector2.zero) {
            switch (direction) {
                case 0:
                    player._animator.SetInteger ("direction", 0);
                    break;
                case 1:
                    player._animator.SetInteger ("direction", 1);
                    break;
                case 2:
                    player._animator.SetInteger ("direction", 2);
                    break;
                case 3:
                    player._animator.SetInteger ("direction", 3);
                    break;
            }
        } else {
            player._animator.SetInteger ("direction", -1);
        }
    }

    private IEnumerator BurstWait (int direction) {
        for(int i = 0; i < bulletCount; i++)
        {
            Rigidbody2D bulletClone = (Rigidbody2D) Instantiate (bullet, transform.position, transform.rotation);
            bulletClone.gameObject.tag = "Player";
            bulletClone.GetComponent<Bullet>().damage = damage;
            switch (direction) {
                case 0:
                    bulletClone.AddForce (bulletClone.transform.up * bulletSpeed);
                    StandShootAnimation (direction);
                    break;
                case 1:
                    bulletClone.AddForce (-bulletClone.transform.up * bulletSpeed);
                    StandShootAnimation (direction);
                    break;
                case 2:
                    bulletClone.AddForce (-bulletClone.transform.right * bulletSpeed);
                    StandShootAnimation (direction);
                    break;
                case 3:
                    bulletClone.AddForce (bulletClone.transform.right * bulletSpeed);
                    StandShootAnimation (direction);
                    break;
            }
            yield return new WaitForSeconds (0.05f);
        }
    }
}