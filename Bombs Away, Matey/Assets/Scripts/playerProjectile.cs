using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour
{
    float speed = 2.5f;
    Transform player;
    Vector2 playerPos;
    Vector2 currentPos;

    public bool impact = false;

    Animator anim;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = new Vector2(player.position.x, player.position.y);
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, playerPos) > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            impact = true;
            //currentPos = transform.position;
            StartCoroutine("Explode");
        } else { impact = false; anim.SetBool("impact", false); }
    }

    IEnumerator Explode()
    {
        rb.velocity = new Vector2(0, 0);
        impact = true;
        //transform.position = currentPos;
        anim.SetBool("impact", true);
       yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        impact = false;
    }
}
