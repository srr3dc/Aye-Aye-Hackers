using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    float speed = 0.5f;
    float rotate = 0.7f;
    Vector3 movement;
    Vector3 currentEulerAngles;

    public Transform target;
    public GameObject cannonball;

    public float timeBtwShots;
    float startTime = 3f;

    int health = 2;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBtwShots = startTime;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        float angle2 = Vector2.Angle(Vector2.up, vectorToTarget);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion a = Quaternion.Euler(0, 0, 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, a, Time.deltaTime * rotate);

        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
       
        if (Vector2.Distance(transform.position, target.position) > 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotate);

        } 

        else if (Vector2.Distance(transform.position, target.position) < 5)
        {
            //Vector2 ballPos = new Vector2(transform.position.x - 0.3f, transform.position.y - 0.5f);
            if (timeBtwShots <= 0)
            {
                Instantiate(cannonball, transform.position, Quaternion.identity);
                timeBtwShots = startTime;
            }
            }
        if(health == 0)
        {
            PlayerController.playerWealth = PlayerController.playerWealth + 500;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerProjectile") 
        { health--; }
    }
}
