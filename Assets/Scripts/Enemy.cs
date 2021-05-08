using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;


public class Enemy : Character
{
    public bool isFront;
    public bool canShoot;
    public Sprite greenSprite;
    private bool needsGreen = true;
    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isDying && !GameManager.instance.isPaused)
        {
            EnemyTracker.UpdateEnemyFrontStatus(this);
            Destroy(gameObject);
            GameManager.instance.UpdateScore(this);
            EnemyTracker.UpdateNumEnemies();
            FindObjectOfType<EnemyController>().UpdateMovementPitch();
        }

        if(GameManager.instance.gameOver)
            Destroy(gameObject);

        canShoot = (isFront && !GameManager.instance.isPaused && !isDying && (GameObject.FindGameObjectsWithTag("EnemyBullet").Length < 3));

        if (transform.position.y < -2.5f && needsGreen)
        {
            rend.sprite = greenSprite;
            needsGreen = false;
        }

    }

    public void KillCharacter()
    {
        EnemyController.AdjustSpeed();
        Explode(.25f);
        AudioManager.instance.Play("InvaderDeath");
    }
}       
    