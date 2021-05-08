using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float BulletSpeed;
    public bool needsColorGreen;
    public bool needsColorRed;
    public bool needsColorWhite;
    public bool isWhite;
    public bool isGreen;
    public bool isRed;
    private float objectHeight;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = new Vector2(0, BulletSpeed);

        AudioManager.instance.Play("Bullet");
    }

    void DestroyBullet()
    {
        Vector2 lastPos = transform.position;
        Destroy(gameObject);
        //Debug.Log(gameObject.name + "destroyed at" + lastPos);
    }

    // Use this for initialization
    void Start()
    { 
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Enemy") && !gameObject.CompareTag("EnemyBullet"))
        {
            if (!other.gameObject.GetComponent<Enemy>().isHit)
            {
                other.gameObject.GetComponent<Enemy>().isHit = true;
                DestroyBullet();
                other.gameObject.GetComponent<Enemy>().KillCharacter();
            }

            other.gameObject.GetComponent<Enemy>().isHit = false;
        }

        if (other.gameObject.CompareTag("Barrier"))
        {
            DestroyBullet();
            other.gameObject.GetComponent<Barrier>().HitBarrier();
        }

        if (other.gameObject.CompareTag("UFO"))
        {
            if (!other.gameObject.GetComponent<UFO>().isHit)
            {
                other.gameObject.GetComponent<UFO>().isHit = true;
                DestroyBullet();
                AudioManager.instance.StopPlaying("UFOhigh");
                other.gameObject.GetComponent<UFO>().KillCharacter();
                AudioManager.instance.Play("UFOlow");
            }

            other.gameObject.GetComponent<UFO>().isHit = false;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<Player>().isHit)
            {
                other.gameObject.GetComponent<Player>().isHit = true;
                DestroyBullet();
                other.gameObject.GetComponent<Player>().KillCharacter();
            }

            other.gameObject.GetComponent<Player>().isHit = false;
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            DestroyBullet();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            DestroyBullet();
            Destroy(other.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (BulletSpeed > 0)
        {
            isGreen = true;
            if (transform.position.y - objectHeight > Confinement.screenBounds.y)
                DestroyBullet();
        }
        
        if (BulletSpeed < 0)
        {
            isWhite = true;
            if (transform.position.y + objectHeight < -Confinement.screenBounds.y)
                DestroyBullet();
        }

        needsColorGreen = (transform.position.y > -6.0 && transform.position.y < -2.5);
        needsColorRed = (transform.position.y < 6.0 && transform.position.y > 2.5);
        needsColorWhite = (!needsColorGreen && !needsColorRed);
    }
}
