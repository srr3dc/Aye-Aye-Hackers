﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) < 1f)
        {
            if (Input.GetKey(KeyCode.W))
            {
                PlayerController.playerGem++;
                GameManager.gemCount--;
                Debug.Log(GameManager.gemCount);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            PlayerController.playerGem++;
            GameManager.gemCount--;
            Debug.Log(GameManager.gemCount);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            PlayerController.playerGem++;
            GameManager.gemCount--;
            Debug.Log(GameManager.gemCount);
            Destroy(gameObject);
        }
    }
}
