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

    public TestPlayer player;

    public AudioClip soundtrack;

    private void Awake () {
        weapons = new List<Weapon>();

        // In case loadout/inventory/inventoryManager doesn't exist
        try {
            loadout = InventoryManager.GetInstance().GetInventory().GetLoadout();

            foreach (ItemToken weaponToken in loadout.GetWeapons()) {
                weapons.Add(weaponToken.Instantiate() as Weapon);
            }
        }
        catch (Exception e) {
            Debug.LogError(e.Message + "\n" + e.StackTrace);

        }

        if (weapons.Count < 1) {
            GenerateLoadout();
        }

        player.equippedWeapon = weapons[0];

        foreach (Weapon weapon in weapons) {
            weapon.LevelStart();
        }

        if (soundtrack != null) {
            AudioManager.GetInstance().PlaySoundtrack(soundtrack);
        }
    }

    private void GenerateLoadout () {
        weapons.Add(Instantiate(revolver));
        weapons.Add(Instantiate(shotgun));
    }

    public void SwitchWeapon () {
        if (weapons.Count < 2) {
            return;
        }

        if (player.equippedWeapon == weapons[0]) {
            player.equippedWeapon = weapons[1];
            player.GetComponent<Animator>().runtimeAnimatorController = player.equippedWeapon.controller;
        }
        else {
            player.equippedWeapon = weapons[0];
            player.GetComponent<Animator>().runtimeAnimatorController = player.equippedWeapon.controller;
        }
    }

    public void WinGame () {
        Tweener.Invoke(1f, () => {
            Time.timeScale = 0;
        });
        Tweener.Invoke(0.5f, () => {
            StartCoroutine(Reset());
        }, true);
    }

    public void LoseGame () {
        Time.timeScale = 0;
        StartCoroutine(Reset());
    }

    IEnumerator Reset () {
        while (true) {
            if (Input.touchCount <= 0 && !Input.GetKeyDown(KeyCode.Space)) {
                yield return null;
            }
            else {
                Tweener.Invoke(1f, () => {
                    FindObjectOfType<UITest>().UnhookWeapons();
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