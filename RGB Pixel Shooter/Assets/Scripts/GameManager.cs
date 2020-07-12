using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // TODO: Get loadout from Inventory
    private Loadout loadout;

    public Weapon revolver;
    public Weapon shotgun;

    public TestPlayer player;

    public AudioClip soundtrack;

    private void Awake () {
        loadout = new Loadout();
        loadout.AddWeapon(Instantiate(revolver));
        loadout.AddWeapon(Instantiate(shotgun));

        loadout.GetWeapons()[0].LevelStart();
        loadout.GetWeapons()[1].LevelStart();

        player.equippedWeapon = loadout.GetWeapons()[0];

        if (soundtrack != null) {
            AudioManager.GetInstance().PlaySoundtrack(soundtrack);
        }
    }

    public void SwitchWeapon () {
        if (player.equippedWeapon == loadout.GetWeapons()[0]) {
            player.equippedWeapon = loadout.GetWeapons()[1];
            player.GetComponent<Animator>().runtimeAnimatorController = player.equippedWeapon.controller;
        }
        else {
            player.equippedWeapon = loadout.GetWeapons()[0];
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

    public Loadout GetLoadout () {
        return loadout;
    }
}