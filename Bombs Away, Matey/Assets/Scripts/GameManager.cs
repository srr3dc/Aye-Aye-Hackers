using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyShips;
    public float delay;

    public GameObject[] gemPoints;
    public float spawnRate;
    public GameObject gem;
    public static int gemCount = 0;
    private bool canSpawn;

    public Image[] hearts;
    public int numOfHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Text wealthText;
    public Text gemText;

    public GameObject introPanel;
    public GameObject losePanel;
    public static bool lost;

    private Transform playerTrans;
    Quaternion currentRotation;

    private float maxX;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnGems());
        InvokeRepeating("SpawnShips", 10f, delay);
        lost = false;
        losePanel.SetActive(false);
        introPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        wealthText.text = PlayerController.playerWealth.ToString();
        gemText.text = PlayerController.playerGem.ToString();

        if (PlayerController.playerHealth > numOfHearts)
        {
            PlayerController.playerHealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < PlayerController.playerHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (PlayerController.playerHealth <= 0)
        {
            lost = true;
            losePanel.SetActive(true);
        } else { losePanel.SetActive(false); }

    }


    public void SpawnShips()
    {
        if (lost == false)
        {
            int rand = Random.Range(0, enemyShips.Length);

            maxX = playerTrans.position.x - 5f;
            maxY = playerTrans.position.y - 5f;
            float randX = Random.Range(-maxX, maxX);
            float randY = Random.Range(-maxY, maxY);
            Vector3 pos = new Vector3(randX, randY, 0);
            Quaternion a = Quaternion.Euler(0, 0, 90);

            Vector3 directionToTarget = (playerTrans.position - pos).normalized;
            currentRotation.eulerAngles = directionToTarget;
            var targetRotation = Quaternion.LookRotation(directionToTarget);
            RaycastHit hit;
            Physics.SphereCast(pos, 2f, Vector3.zero, out hit, 8f);

            if (Vector2.Distance(playerTrans.position, pos) > 3f && hit.collider==null)
            {
                Instantiate(enemyShips[rand], pos, currentRotation);
            }
        }

    }


    IEnumerator SpawnGems()
    {

        while (true)
        {
            while (gemCount > 10)
                yield return null;
            while (gemCount <= 10)
            {
                int rand = Random.Range(0, gemPoints.Length);
                Vector2 randPoint = new Vector2(gemPoints[rand].transform.position.x, gemPoints[rand].transform.position.y);
                Instantiate(gem, randPoint, Quaternion.identity);
                gemCount++;
                Debug.Log(gemCount);
                yield return new WaitForSeconds(2f);
            }
        }
     

    }
      bool CanSpawn()
    {
        if (gemCount > 11)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Close()
    {
        introPanel.SetActive(false);
    }

    public void Redo()
    {
        PlayerController.playerHealth = 4;
        PlayerController.playerWealth = 0;
        PlayerController.playerGem = 0;

        SceneManager.LoadScene("Game");
    }

    public void Stop()
    {
        Application.Quit();
    }
}
