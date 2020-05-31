using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public TestPlayer player;

    public void Start()
    {
        player.enabled = false;
        player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void EnablePlayer()
    {
        player.enabled = true;
        player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(this.gameObject);
    }
}
