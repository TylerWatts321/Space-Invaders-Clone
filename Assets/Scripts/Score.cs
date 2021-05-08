using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Player player;

    public Text scoreText;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<Player>();
        scoreText.text = player.score.ToString("0");
    }
}
