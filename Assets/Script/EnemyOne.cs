using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyOne : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float up = 4.5f;
    [SerializeField]
    private float left = -7.5f;
    [SerializeField]
    private float down = -2.5f;
    [SerializeField]
    private float right = 7.5f;

    private Rigidbody2D thisRB;
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Image EnemyHealthBar;
    [SerializeField]
    private Image EnemyCharge;
    private float enemyHealth = 500f;
    private float maxEHealth = 500f;

    private float E1Timer;

    private int BulletNum = 0;
    private float maxBulletHit = 10;

    [SerializeField]
    private GameObject E2Bullet;

    private int Face = 0;


    private void Awake()
    {
        thisRB = GetComponent<Rigidbody2D>(); //get the rigibody of this enemy
        int randomPos = Random.Range(0, 4);  //use random number to determine its start position
        if (randomPos == 0)
        {
            transform.position = new Vector3(left, up);
        }
        if (randomPos == 1)
        {
            transform.position = new Vector3(right, up);
        }
        if (randomPos == 2)
        {
            transform.position = new Vector3(left, down);
        }
        if (randomPos == 3)
        {
            transform.position = new Vector3(right, down);
        }
    }
    private void Update()
    {
        HealthBar();
        EnemyDie();

        if (this.gameObject.name == "Enemy1")
        {
            E1Timer += Time.deltaTime;
            EnemyCharge.fillAmount = E1Timer / 10f;
            if (E1Timer >= 10f)
            {
                AddHealth();
            }
        }

        if (this.gameObject.name == "Enemy2")
        {
            EnemyCharge.fillAmount = BulletNum / maxBulletHit;
            if (BulletNum >= 10)
            {
                BulletBurst();
            }
        }
    }
    void FixedUpdate()
    {
        RandomMovement();
    }
    private void HealthBar()
    {
        EnemyHealthBar.fillAmount = enemyHealth / maxEHealth;
    }
    private void RandomMovement()   //whenever the enemy move to the corner, it changes its direction of movement.
    {
        if (transform.position == new Vector3(left, up))
        {
            int randomDir = Random.Range(0,2);
            if(randomDir == 0)
            {
                thisRB.velocity = new Vector2(speed,0);
                Face = 1;
            }
            else
            {
                thisRB.velocity = new Vector2(0, -speed);
                Face = 2;
            }
        }
        else if (transform.position == new Vector3(right, up))
        {
            int randomDir = Random.Range(0, 2);
            if (randomDir == 0)
            {
                thisRB.velocity = new Vector2(-speed, 0);
                Face = 1;
            }
            else
            {
                thisRB.velocity = new Vector2(0, -speed);
                Face = 4;
            }
        }
        else if( transform.position == new Vector3(left, down))
        {
            int randomDir = Random.Range(0, 2);
            if (randomDir == 0)
            {
                thisRB.velocity = new Vector2(speed, 0);
                Face = 3;
            }
            else
            {
                thisRB.velocity = new Vector2(0, speed);
                Face = 2;
            }
        }
         else if(transform.position == new Vector3(right, down))
        {
            int randomDir = Random.Range(0, 2);
            if (randomDir == 0)
            {
                thisRB.velocity = new Vector2(-speed, 0);
                Face = 3;
            }
            else
            {
                thisRB.velocity = new Vector2(0, speed);
                Face = 4;
            }
        }
    }

    private void EnemyDie()
    {
        if(enemyHealth <= 0)
        {
            Player.GetComponent<Movement>().winState = 1;
            Destroy(this.gameObject);
        }
    }

    private void AddHealth()
    {

            enemyHealth += 10f;
            E1Timer = 0;

    }


    private void BulletBurst()
    {

        Debug.Log("Burst");
        if(Face == 1)
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject E2B = GameObject.Instantiate(E2Bullet, new Vector2(left + 1 + 2*i, up), Quaternion.identity);
                E2B.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10f);

            }
        }

        else if (Face == 3)
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject E2B = GameObject.Instantiate(E2Bullet, new Vector2(left + 1 + 2 * i, down), Quaternion.identity);
                E2B.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10f);

            }
        }

        else if(Face == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject E2B = GameObject.Instantiate(E2Bullet, new Vector2(right, down + 1 + 1.5f * i), Quaternion.identity);
                E2B.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0);

            }
        }
        else if(Face == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject E2B = GameObject.Instantiate(E2Bullet, new Vector2(left, down + 1 + 1.5f * i), Quaternion.identity);
                E2B.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0f);

            }
        }

        BulletNum = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PBullet")
        {
            enemyHealth -= 5;
            BulletNum++;
        }
    }

}
