using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    private Player player;

    public Text livesText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<Player>();
        livesText.text = player.lives.ToString("0");
    }
}
