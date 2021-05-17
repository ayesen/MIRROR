using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 3f)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Movement>().Health -= 5;
            Destroy(this.gameObject);
        }
    }

}
