using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    public bool MovingRight;
    public bool EnemiesCanShoot = true;
    public bool movedDownLast;
    public bool shouldMoveDown;
    public GameObject bullet;
    public float shotCooldown = 0.5f;
    public float repeatRate = 1.0f;
    public static float speedModifier = 1.012f;
    private int soundCounter = 0;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating("Move", 1.0f, repeatRate);
    }

    private void Update()
    {
        if (GameManager.instance.gameOver || GameManager.instance.isReloadingScene)
        {
            Destroy(gameObject);
        }

        else if(EnemyTracker.numEnemies > 0)
        {
            shouldMoveDown = (EnemyTracker.FetchFurthestRight().transform.position.x > 4.5f && MovingRight) ||
                             (EnemyTracker.FetchFurthestLeft().transform.position.x < -4.5f && !MovingRight);
        }

    }
    private void Move()
    {
        if (movedDownLast)
        {
            foreach (Enemy enemy in EnemyTracker.enemyGrid)
            {
                if (enemy)
                {
                    MoveHorizontal(enemy);
                }
            }

            MovingRight = !MovingRight;
            movedDownLast = false;

            Shoot();
            PlaySound();
            return;
        }

        foreach (Enemy enemy in EnemyTracker.enemyGrid)
        {
            if (enemy)
            {
                if (shouldMoveDown)
                {
                    MoveDown(enemy);
                    movedDownLast = true;
                }
                else
                {
                    MoveHorizontal(enemy);
                }

            }
        }

        Shoot();
        PlaySound();

        if (EnemyTracker.numEnemies > 0 && !GameManager.instance.gameOver)
        {
            EnemyTracker.FurthestToLeft = EnemyTracker.FetchFurthestLeft().transform.position;
            EnemyTracker.FurthestToRight = EnemyTracker.FetchFurthestRight().transform.position;
            EnemyTracker.ClosestToPlayer = EnemyTracker.FetchClosestToPlayer().transform.position;
        }
    }

    private void Shoot()
    {
        Enemy shootingEnemy = EnemyTracker.FetchRandFront();
        if (shootingEnemy.canShoot)
        {
            Fire(shootingEnemy);
        }
    }

    private void Fire(Enemy shootingEnemy)
    {
        Instantiate(bullet, shootingEnemy.transform.position + new Vector3(0, -0.5f, 0),
            shootingEnemy.transform.rotation);
    }


    private void MoveDown(Enemy enemy)
    {
        float tempSpeed = enemy.speed;
        enemy.speed = -Mathf.Abs(enemy.speed);
        enemy.transform.position += new Vector3(0, enemy.speed, 0);
        enemy.speed = -tempSpeed;
    }

    private void MoveHorizontal(Enemy enemy)
    {
        enemy.transform.position += new Vector3(enemy.speed, 0, 0);
    }


    public static void AdjustSpeed()
    {
        foreach (Enemy enemy in EnemyTracker.enemyGrid)
        {
            if (enemy)
                enemy.speed *= speedModifier;
        }
    }

    public void PlaySound()
    {
        AudioManager source = FindObjectOfType<AudioManager>();
        if (soundCounter > 3)
            soundCounter = 0;
        
        switch (soundCounter)
        {
            case 0:
                source.Play("Movement1");
                break;
            case 1:
                source.Play("Movement2");
                break;
            case 2:
                source.Play("Movement3");
                break;
            case 3:
                source.Play("Movement4");
                break;
        }

        soundCounter++;
    }

    public void UpdateMovementPitch()
    {
        AudioManager source = FindObjectOfType<AudioManager>();

        source.UpdatePitch("Movement1", .01f);
        source.UpdatePitch("Movement2", .01f);
        source.UpdatePitch("Movement3", .01f);
        source.UpdatePitch("Movement4", .01f);
    }
}

