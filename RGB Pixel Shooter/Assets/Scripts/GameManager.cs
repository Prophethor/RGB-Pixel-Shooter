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
    public Animator UIAnimator;

    private void Awake () {
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
        Tweener.Invoke(2f, () => {
            StartCoroutine(Reset());
        }, true);
    }

    public void LoseGame () {
        UIAnimator.SetBool("Lose", true);
        Time.timeScale = 0;
        Debug.Log("Run.");
        Tweener.Invoke(0.5f, () => {
            StartCoroutine(Reset());
        }, true);
    }

    IEnumerator Reset () {
        while (true) {
            if (Input.touchCount <= 0 && !Input.GetKeyDown(KeyCode.Space)) {
                yield return null;
            }
            else {
                UIAnimator.SetTrigger("Reset");
                Tweener.Invoke(0.5f, () => {
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