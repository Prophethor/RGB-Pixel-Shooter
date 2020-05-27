using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // TODO: Get loadout from Inventory
    private Loadout loadout;

    public Weapon revolver;
    public Weapon shotgun;

    public TestPlayer player;
    public Animator UIAnimator;

    private void Start () {
        loadout = new Loadout();
        loadout.AddWeapon(Instantiate(revolver));
        loadout.AddWeapon(Instantiate(shotgun));

        loadout.GetWeapons()[0].LevelStart();
        loadout.GetWeapons()[1].LevelStart();

        player.equippedWeapon = loadout.GetWeapons()[0];
    }

    public void SwitchWeapon () {
        if (player.equippedWeapon == loadout.GetWeapons()[0]) {
            player.equippedWeapon = loadout.GetWeapons()[1];
        }
        else {
            player.equippedWeapon = loadout.GetWeapons()[0];
        }
        Debug.Log("Switched. Weapon is now " + player.equippedWeapon.GetName());
    }

    public void WinGame () {
        UIAnimator.SetBool("Win", true);
        Time.timeScale = 0;
        Debug.Log("You win!");
    }

    public void LoseGame () {
        UIAnimator.SetBool("Lose", true);
        Time.timeScale = 0;
        Debug.Log("Run.");
    }

    public Loadout GetLoadout () {
        return loadout;
    }
}