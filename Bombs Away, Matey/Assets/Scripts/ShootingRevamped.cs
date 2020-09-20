using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRevamped : MonoBehaviour
{
    public Vector3 bulletOffset = new Vector3(0f, 0.5f, 0);
    public GameObject playerBullet;
    public Transform firePoint;
    public Transform firePoint2;

    public float cannonForce = 0.1f;

    public float fireDelay = 1f;
    float cooldownTimer = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (GameManager.lost == false)
        {
            if (Input.GetKey(KeyCode.E) && cooldownTimer <= 0)
            {
                cooldownTimer = fireDelay;
                ShootRight();
                //Vector3 offset = transform.rotation * bulletOffset;
                //Vector2 bulletPos = new Vector2(transform.position.x, transform.position.y - 0.5f);
            }
            else if (Input.GetKey(KeyCode.Q) && cooldownTimer <= 0)
            {
                cooldownTimer = fireDelay;
                ShootLeft();
            }
        }
    }

    void ShootRight()
    {
       GameObject playerCannon = Instantiate(playerBullet, firePoint.position, firePoint.rotation);
       Rigidbody2D rb = playerCannon.GetComponent<Rigidbody2D>();
       rb.AddForce(firePoint.up * cannonForce, ForceMode2D.Impulse);
    }

    void ShootLeft()
    {
        GameObject playerCannon = Instantiate(playerBullet, firePoint2.position, firePoint2.rotation);
        Rigidbody2D rb = playerCannon.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint2.up * cannonForce, ForceMode2D.Impulse);
    }
}
