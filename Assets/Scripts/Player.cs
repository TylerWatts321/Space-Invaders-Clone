using UnityEngine;

public class Player : Character
{
    private Vector3 startPos;
    public bool canRespawn;

    private void Awake()
    {
        startPos = transform.position;
        rend = GetComponent<SpriteRenderer>();
        playerSprite = rend.sprite;
        lives = GameManager.instance.LIVES;
        score = GameManager.instance.SCORE;
    }

    private void Update()
    {
        canRespawn = isDying && !GameManager.instance.isPaused && (lives > 0);

        if(canRespawn)
            ResetCharacter();
    }

    public void KillCharacter()
    {
        if(!isDying)
        {
            AudioManager.instance.Play("Explosion");
            Explode(2.0f);
            lives--;
        }

        if (lives == 0)
            GameManager.instance.GameOver();
    }

    public void ResetCharacter()
    {
        transform.position = startPos;
        rend.sprite = playerSprite;
        isDying = false;
    }

    public void SavePlayer()
    {
        GameManager.instance.LIVES = lives;
        GameManager.instance.SCORE = score;
    }
}
