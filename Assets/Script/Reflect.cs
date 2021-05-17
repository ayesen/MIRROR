using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemeyBullet;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void ReflectBullet()
    {
        bool isReflected = false;

        if (!isReflected)
        {
            GameObject EB = GameObject.Instantiate(EnemeyBullet, this.transform.position, Quaternion.identity);
            EB.gameObject.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity * -1;
            Destroy(this.gameObject);
            isReflected = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall1")
        {
            ReflectBullet();
        }
    }


}
