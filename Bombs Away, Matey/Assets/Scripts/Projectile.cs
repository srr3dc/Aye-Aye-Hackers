using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 2.5f;
    Transform target;
    Vector2 targetPos;
    Vector2 currentPos;

    public bool impact = false;

    Animator anim;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        targetPos = new Vector2 (target.position.x, target.position.y);
        anim = gameObject.GetComponent<Animator>();
        currentPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, currentPos) > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.velocity = new Vector2(0, 0);
            impact = true;
            currentPos = transform.position;
            StartCoroutine("Explode");
        }
        else { impact = false; anim.SetBool("impact", false); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.velocity = new Vector2(0, 0);
            impact = true;
            //currentPos = transform.position;
            StartCoroutine("Explode");
        }
        else { impact = false; anim.SetBool("impact", false); }
    }

    IEnumerator Explode()
    {
        //transform.position = currentPos;
        anim.SetBool("impact", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        impact = false;
    }
}
