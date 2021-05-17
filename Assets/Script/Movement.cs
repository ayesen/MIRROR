using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    [SerializeField]
    private float moveSpeed = 3f;

    public GameObject bullet;
    [SerializeField]
    private float shootSpeed = 10f;
    private int face = 0;

    [SerializeField]
    private Image PlayerHealthBar;
    private float maxHealth = 100;
    private float health = 100;

    [SerializeField]
    private TextMeshProUGUI instructionTxt;

    public int winState = 0;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();  
    }


    void Update()
    {
        EndLevel();
        HealthBar();
        if (winState == 0)
        {
            PlayerMovement();
        }
    }
    private void FixedUpdate()
    {
        if (winState == 0)
        {
            Shoot();
        }
    }


    private void HealthBar()
    {
        PlayerHealthBar.fillAmount = health / maxHealth;
        if(health <= 0 )
        {
            winState = 2;
        }
    }

    public void EndLevel()
    {
        if (winState == 1)
        {
            instructionTxt.color = Color.cyan;
            instructionTxt.text = "You Win!\nPress N to Next Level ";
            if (Input.GetKeyDown(KeyCode.N))
            {
                SceneManager.LoadScene(1);
            }
        }
        else if (winState == 2)
        {
            instructionTxt.color = Color.red;
            instructionTxt.text = "You Lose!\nPress R to Restart Level ";
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }


    private void PlayerMovement()
    { 
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
            face = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
            face = 2;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
            face = 3;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
            face = 4;
        }
    }

    private void Shoot()
    {
        if (face > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject blt = GameObject.Instantiate(bullet, this.transform.position, Quaternion.identity);
                if (face == 1)
                {
                    blt.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shootSpeed);
                }
                else if (face == 2)
                {
                    blt.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed, 0);
                }
                else if (face == 3)
                {
                    blt.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shootSpeed);
                }
                else if (face == 4)
                {
                    blt.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed, 0);
                }
            }
        }

    }

}
