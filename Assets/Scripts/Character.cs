using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool canMove = true;
    public bool isDying;
    public bool isHit;
    public int lives;
    public int score;
    public static Sprite explosion;
    public static Sprite playerSprite;
    public SpriteRenderer rend;

    public enum CharacterCategory
    {
        Player,
        Enemy,
        UFO
    }

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Collider2D col = GetComponent<Collider2D>();
        rb.isKinematic = true;
        col.isTrigger = false;
    }
    private void Update()
    {
        if(this.lives < 0 && !GameManager.instance.isPaused)
            Destroy(this);
        canMove = !isDying && !GameManager.instance.isPaused && !GameManager.instance.isReloadingScene;
    }
    public void Explode(float duration)
    {
        ColorChanger.AdjustExplosionColor(this);
        GameManager.instance.StartPause(duration);
        isDying = true;
    }

    public CharacterCategory characterCategory;
}
