using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedCounter : MonoBehaviour
{
    public Player player;
    public Text speedText;
    // Start is called before the first frame update

    void Start()
    {
        player = (Player)GameObject.FindGameObjectsWithTag("Player")[0].GetComponent("Player"); // Get a list of players in scene, get the Player script of Player 0
        speedText = (Text)GetComponent("Text");
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = player.GetSpeed().ToString(); // Set text to player's speed
    }
}
