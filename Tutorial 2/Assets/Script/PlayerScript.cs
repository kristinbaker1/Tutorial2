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
    private int lives;
    public TextMeshProUGUI livesText;
    public GameObject loseGame;
    public GameObject Player;
    public int level;
    private float movementX;
    private float movementY;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public bool gameDone; 
    Animator anim;
    private bool facingRight;
    
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        win.SetActive(false);
        lives = 3;
        SetLivesText();
        loseGame.SetActive(false);
        level = 1;
        gameDone = false;
        facingRight = true;

        
        
        musicSource.clip = musicClipOne;
        musicSource.loop = true;
    
       
        
        musicSource.clip = musicClipOne;
        musicSource.Play();

     
        anim = GetComponent<Animator>();
        
        
        
    }

   
    void Update()

    {

      
        
    
        if (Input.GetKeyDown(KeyCode.W))
        {
             anim.SetInteger("State", 2);
        }
            

         if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0); 
        }
    
        if (Input.GetKeyDown(KeyCode.A))
        {
           
            anim.SetInteger("State", 2);
        }
        
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 2);
        }

        {
            anim.SetInteger("State", 0);
            
        }
        
        if((scoreValue == 8) && (gameDone = true))
        {
        WinGame();

        }
       
        
        if (lives == 0)
        {
        LoseGame();
        }
        


        if (facingRight == false && movementX > 0)
        {
            Flip();
        }
        else if (facingRight == true && movementX < 0)
        {
        Flip();
        }
    }


    
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }




    public void PlayWin()
   {
    
    {   musicSource.Stop();
        musicSource.PlayOneShot(musicClipTwo);  
    }
   }



    public void livesReset()
    {  lives = 3; 
        SetLivesText();
    }
   
   


    void FixedUpdate()
   { 
    {   float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    {
         Vector3 movement = new Vector3(movementX, 0.0f, movementY);
         rd2d.AddForce(movement * speed);
         anim.SetInteger("State", 1);
            Debug.Log("hey");
    }


        if (scoreValue >= 4 && level == 1)
    {
        transform.position = new Vector3(40.0f, 0.0f, 0.0f);
        level = 2;
        livesReset();
        
    }

          
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
            Debug.Log("hey");
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0); 
            Debug.Log("hey hi");
        }
    }
   


    void LateUpdate()
    {
        
       
       
    }


    public void LoseGame()
    {
        loseGame.SetActive(true);

        Destroy(Player);

        gameDone = true;
    }

    public void WinGame()
    {   
       
        { win.SetActive(true);

        gameDone = true;  


        musicSource.clip = musicClipOne;
        musicSource.loop = false;

        PlayWin();
        }
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
        Destroy(collision.collider.gameObject);
        lives = lives - 1;
        SetLivesText();
        collision.gameObject.SetActive(false);
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