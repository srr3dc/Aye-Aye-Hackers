using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public Transform target;
    Rigidbody2D rb;
    public GameObject cannonball;
    public Transform firePoint;

    float speed = 1.2f;
    float cannonForce = 10f;

    public float timeBtwShots;
    float startTime = 3f;

    int health = 3;

    private AudioSource boom;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        timeBtwShots = startTime;
        boom = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, target.position) > 3)
        {
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90f;
            rb.rotation = angle;

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }
        else if (Vector2.Distance(transform.position, target.position) < 3)
        {
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 30f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

            //Vector2 ballPos = new Vector2(transform.position.x - 0.3f, transform.position.y - 0.5f);
            if (timeBtwShots <= 0)
            {
                GameObject fireBall = Instantiate(cannonball, firePoint.position, firePoint.rotation);
                Rigidbody2D rb2d = fireBall.GetComponent<Rigidbody2D>();
                rb2d.AddForce(firePoint.up * cannonForce, ForceMode2D.Impulse);
                timeBtwShots = startTime;
            }
        }

        if (health == 0)
        {
            StartCoroutine(Death());
        }

    }

    private void FixedUpdate()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerProjectile")
        { boom.Play(); health--; }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.4f);
        PlayerController.playerWealth = PlayerController.playerWealth + 500;
        Destroy(gameObject);
    }
}
