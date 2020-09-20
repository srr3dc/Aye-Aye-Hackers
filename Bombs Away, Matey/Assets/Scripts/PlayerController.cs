using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static int playerHealth = 5;
    public static int playerWealth = 0;
    public static int playerGem = 0;

    public static PlayerController instance;

    public Sprite damaged1;
    public Sprite damaged2;

    public AudioSource boom;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 2)
        {
            SpriteRenderer playerBoat = gameObject.GetComponent<SpriteRenderer>();
            playerBoat.sprite = damaged1;

        }

        if (playerHealth == 1)
        {
            SpriteRenderer playerBoat = gameObject.GetComponent<SpriteRenderer>();
            playerBoat.sprite = damaged2;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyProjectile") 
        { playerHealth--; boom.Play(); }
    }

}
