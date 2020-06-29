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
        SpriteRenderer[] childrenderers = new SpriteRenderer[0];
        childrenderers = player.gameObject.GetComponentsInChildren<SpriteRenderer>();
        childrenderers[1].enabled = false;
    }
    public void EnablePlayer()
    {
        player.enabled = true;
        player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        SpriteRenderer[] childrenderers = new SpriteRenderer[0];
        childrenderers = player.gameObject.GetComponentsInChildren<SpriteRenderer>();
        childrenderers[1].enabled = true;
        Destroy(this.gameObject);
    }
}
