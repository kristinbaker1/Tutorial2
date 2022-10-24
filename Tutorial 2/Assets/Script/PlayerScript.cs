using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI score;
    private int scoreValue = 0;
    public GameObject win;
    private float lives;
    public TextMeshProUGUI livesText;
    public GameObject loseGame;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        win.SetActive(false);
        lives = 3;
        SetLivesText();
        loseGame.SetActive(false);
    }

   
    void Update()

    {
        if(scoreValue == 4)
        {win.SetActive(true);}

    
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }


    public void LoseGame()
    {
        loseGame.SetActive(true);

        Destroy(Player);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    if(collision.gameObject.CompareTag("Coin"))
    {
        scoreValue += 1;
        score.text = scoreValue.ToString();
        Destroy(collision.collider.gameObject);
        collision.gameObject.SetActive(false);
    }
    else if (collision.gameObject.CompareTag("Enemy"))
    {
       collision.gameObject.SetActive(false);
        lives = lives - 1;
        SetLivesText();
    }
    if (lives <= 0)
    {
        LoseGame();
    }

    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

   private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }


}