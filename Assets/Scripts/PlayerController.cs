using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    public Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        MovementController();
    }

    void MovementController()
    {
        // Determine horizontal movement.
        if (player.canMove)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            player.transform.position = player.transform.position + new Vector3(horizontal * player.speed * Time.deltaTime, 0, 0);
        }

        // Handle Shooting
        if (GameObject.FindGameObjectsWithTag("Bullet").Length < 1)
        {
            if (Input.GetKeyDown("space") && !GameManager.instance.isPaused)
            {
                Instantiate(bullet, player.transform.position + new Vector3(0, 0.5f, 0), player.transform.rotation);
            }
        }
    }
}
