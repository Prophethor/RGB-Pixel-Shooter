using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // TODO: Get loadout from Inventory
    private Loadout loadout;
    private List<Weapon> weapons;

    public Weapon revolver;
    public Weapon shotgun;

    public PlayerController player;

    public AudioClip soundtrack;

    public AudioClip swapWeapon;

    private UIManager UIManager;

    private void Awake () {
        weapons = new List<Weapon>();

        // In case loadout/inventory/inventoryManager doesn't exist
        try {
            loadout = InventoryManager.GetInstance().GetInventory().GetLoadout();

            foreach (Weapon weaponToken in loadout.GetWeapons()) {
                weapons.Add(weaponToken);
            }
        }
        catch (Exception e) {
            Debug.LogError(e.Message + "\n" + e.StackTrace);

        }

        //if (weapons.Count < 1) {
        //    GenerateLoadout();
        //}

        GenerateLoadout();

        player.equippedWeapon = weapons[0];

        foreach (Weapon weapon in weapons) {
            weapon.LevelStart();
        }

        if (soundtrack != null) {
            AudioManager.GetInstance().PlaySoundtrack(soundtrack);
        }

        UIManager = FindObjectOfType<UIManager>();
    }

    private void GenerateLoadout () {
        weapons.Add(new Revolver());
        weapons.Add(new DBShotgun());
    }

    public void SwitchWeapon () {
        if (weapons.Count < 2) {
            return;
        }

        AudioManager.GetInstance().PlaySound(swapWeapon, true);

        if (player.equippedWeapon == weapons[0]) {
            player.equippedWeapon = weapons[1];
            player.GetComponent<Animator>().runtimeAnimatorController = player.equippedWeapon.Controller;
        }
        else {
            player.equippedWeapon = weapons[0];
            player.GetComponent<Animator>().runtimeAnimatorController = player.equippedWeapon.Controller;
        }
    }

    public void WinGame () {
        UIManager.ShowWinScreen();
    }

    public void LoseGame () {
        UIManager.ShowLoseScreen();
    }

    public void RestartLevel () {
        SceneLoader.GetInstance().LoadScene("LevelScene");
    }

    public void GoToMenu () {
        SceneLoader.GetInstance().FinishLevel("LevelSelect");
    }

    IEnumerator Reset () {
        while (true) {
            if (Input.touchCount <= 0 && !Input.GetKeyDown(KeyCode.Space)) {
                yield return null;
            }
            else {
                Tweener.Invoke(1f, () => {
                    FindObjectOfType<UIManager>().UnhookWeapons();
                    SceneManager.LoadScene(0);
                    Time.timeScale = 1f;
                }, true);
                break;
            }
        }
    }

    public List<Weapon> GetWeapons () {
        return weapons;
    }
}