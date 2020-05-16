using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    // Besto testo
    public Weapon equippedWeapon;

    private int lane = 1;

    private void Start () {
        equippedWeapon.LevelStart();
    }

    private void Update () {
        if (Input.GetKeyDown(KeyCode.S)) {
            if (lane > 0) {
                lane--;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            if (lane < 2) {
                lane++;
            }
        }
        UpdatePosition();

        
        if (Input.GetKeyDown(KeyCode.J)) {
            ((Revolver) equippedWeapon).Load(RGBColor.RED);
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            ((Revolver) equippedWeapon).Load(RGBColor.GREEN);
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            ((Revolver) equippedWeapon).Load(RGBColor.BLUE);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            equippedWeapon.Shoot(transform.position);
        }
    }

    private void UpdatePosition () {
        float yPos = PlayField.GetSpacePosition(lane, 0).y;
        transform.position = new Vector3(transform.position.x, yPos, yPos);
    }
}
