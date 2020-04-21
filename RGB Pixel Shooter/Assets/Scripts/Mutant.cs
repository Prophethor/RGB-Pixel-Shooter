using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : GenericEnemy
{

    public int laneSetter;
    private Rigidbody2D rb;

    private void Start()
    {
        speed = 1;
        lane = laneSetter;
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0)).x + 1, PlayField.GetLanePosition(lane));
        hpStackList.Add(new HPStack(RGBColor.BLUE, 1));
        rb = GetComponent<Rigidbody2D>();
        Move();
    }

    protected override void Move() {
        rb.velocity = new Vector2(-speed, 0);
    }

    protected override void InitiateShanking() {
        rb.velocity = Vector2.zero;
    }

}
