using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Character
{
    // Update is called once per frame
    void Update()
    {
        if (isDying && !GameManager.instance.isPaused)
        {
            GameManager.instance.UpdateScore(this);
            Destroy(gameObject);
        }

        if (speed > 0 && transform.position.x > 6.5)
        {
            Destroy(gameObject);
            AudioManager.instance.StopPlaying("UFOhigh");
        }

        if (speed < 0 && transform.position.x < -6.5)
        {
            Destroy(gameObject);
            AudioManager.instance.StopPlaying("UFOhigh");
        }


    }

    void FixedUpdate()
    {
        if (canMove)
            transform.position = transform.position + new Vector3(speed* Time.deltaTime, 0, 0);
    }

    public void Awake()
    {
        AudioManager.instance.Play("UFOhigh");
        if (transform.position.x > 6.0f)
        {
            speed = -speed;
        }
    }
    public void KillCharacter()
    {
        Explode(2.0f);
    }
}
