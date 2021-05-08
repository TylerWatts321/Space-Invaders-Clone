using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] player_bullets;
    private GameObject[] enemy_bullets;
    public Sprite playerRed;
    public Sprite playerGreen;
    public Sprite playerWhite;
    public Sprite invaderRed;
    public Sprite invaderGreen;
    public Sprite invaderWhite;

    public static Sprite explosionPlayer;
    public static Sprite explosion;
    public static Sprite explosionRed;
    public static Sprite explosionGreen;

    void Start()
    {
        playerRed = Resources.Load<Sprite>("Player_Bullet_red");
        playerGreen = Resources.Load<Sprite>("Player_Bullet_green");
        playerWhite = Resources.Load<Sprite>("Player_Bullet");
        invaderRed = Resources.Load<Sprite>("Invader_Bullet_red");
        invaderGreen = Resources.Load<Sprite>("Invader_Bullet_green");
        invaderWhite = Resources.Load<Sprite>("Player_Bullet");

        explosionPlayer = Resources.Load<Sprite>("Explosion_player");
        explosionRed = Resources.Load<Sprite>("Explosion_red");
        explosionGreen = Resources.Load<Sprite>("Explosion_green");
        explosion = Resources.Load<Sprite>("Explosion");
    }
    // Update is called once per frame
    void Update()
    {
        player_bullets = GameObject.FindGameObjectsWithTag("Bullet");
        enemy_bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        AdjustBulletColor(player_bullets);
        AdjustBulletColor(enemy_bullets);
    }

    void AdjustBulletColor(GameObject[] bullets)
    {
        foreach (GameObject bullet in bullets)
        {
            if(bullet)
            {
                if (bullet.GetComponent<Bullet>().BulletSpeed > 0)
                {
                    if (bullet.GetComponent<Bullet>().needsColorGreen && !bullet.GetComponent<Bullet>().isGreen)
                    {
                        bullet.GetComponent<SpriteRenderer>().sprite = playerGreen;
                        bullet.GetComponent<Bullet>().isGreen = true;
                        bullet.GetComponent<Bullet>().isRed = false;
                        bullet.GetComponent<Bullet>().isWhite = false;
                    }
                    if (bullet.GetComponent<Bullet>().needsColorRed && !bullet.GetComponent<Bullet>().isRed)
                    {
                        bullet.GetComponent<SpriteRenderer>().sprite = playerRed;
                        bullet.GetComponent<Bullet>().isGreen = false;
                        bullet.GetComponent<Bullet>().isRed = true;
                        bullet.GetComponent<Bullet>().isWhite = false;
                    }
                    if (bullet.GetComponent<Bullet>().needsColorWhite && !bullet.GetComponent<Bullet>().isWhite)
                    {
                        bullet.GetComponent<SpriteRenderer>().sprite = playerWhite;
                        bullet.GetComponent<Bullet>().isGreen = false;
                        bullet.GetComponent<Bullet>().isRed = false;
                        bullet.GetComponent<Bullet>().isWhite = true;
                    }
                }

                if (bullet.GetComponent<Bullet>().BulletSpeed < 0)
                {
                    if (bullet.GetComponent<Bullet>().needsColorGreen && !bullet.GetComponent<Bullet>().isGreen)
                    {
                        bullet.GetComponent<SpriteRenderer>().sprite = invaderGreen;
                        bullet.GetComponent<Bullet>().isGreen = true;
                        bullet.GetComponent<Bullet>().isRed = false;
                        bullet.GetComponent<Bullet>().isWhite = false;
                    }
                    if (bullet.GetComponent<Bullet>().needsColorRed && !bullet.GetComponent<Bullet>().isRed)
                    {
                        bullet.GetComponent<SpriteRenderer>().sprite = invaderRed;
                        bullet.GetComponent<Bullet>().isGreen = false;
                        bullet.GetComponent<Bullet>().isRed = true;
                        bullet.GetComponent<Bullet>().isWhite = false;
                    }
                    if (bullet.GetComponent<Bullet>().needsColorWhite && !bullet.GetComponent<Bullet>().isWhite)
                    {
                        bullet.GetComponent<SpriteRenderer>().sprite = invaderWhite;
                        bullet.GetComponent<Bullet>().isGreen = false;
                        bullet.GetComponent<Bullet>().isRed = false;
                        bullet.GetComponent<Bullet>().isWhite = true;
                    }
                }
            }
        }
    }

    public static void AdjustExplosionColor(Character character)
    {
        if (character.CompareTag("Player"))
        {
            character.GetComponent<SpriteRenderer>().sprite = explosionPlayer;
            return;
        }

        if (character.transform.position.y < 6.0 && character.transform.position.y > 2.5)
        {
            character.GetComponent<SpriteRenderer>().sprite = explosionRed;
        }

        else if (character.transform.position.y > -6.0 && character.transform.position.y < -2.5f)
        {
            character.GetComponent<SpriteRenderer>().sprite = explosionGreen;
        }

        else
        {
            character.GetComponent<SpriteRenderer>().sprite = explosion;
        }
    }
}
