using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UITest : MonoBehaviour {

    public Transform weapon1Panel;
    public Transform weapon2Panel;
    public Transform barrelPanel;
    public Image currentWeaponPanel;
    public Image otherWeaponPanel;

    public TestPlayer player;
    public GameManager gameManager;
    public GameObject swipeDetector;

    private bool weapon1Selected = true;
    private GameObject weapon1Barrel, weapon2Barrel;
    private Sprite weapon1Sprite, weapon2Sprite;


    public GameObject HUDCanvas, YouWin, YouLose, PausePanel, BlackScreen, SFXButton, MusicButton, SFXSlider, MusicSlider;

    private void Start () {
        List<Weapon> weapons = gameManager.GetWeapons();

        weapons[0].HookUI(weapon1Panel);
        weapons[1].HookUI(weapon2Panel);
        weapon1Barrel = Instantiate(weapons[0].UIbarrel, barrelPanel);
        weapon2Barrel = Instantiate(weapons[1].UIbarrel, barrelPanel);
        weapon1Sprite = weapons[0].weaponSprite;
        weapon2Sprite = weapons[1].weaponSprite;
        currentWeaponPanel.color = Color.white;
        otherWeaponPanel.color = Color.white;
        currentWeaponPanel.sprite = weapon1Sprite;
        otherWeaponPanel.sprite = weapon2Sprite;

        SFXSlider.GetComponent<Slider>().value = AudioManager.GetInstance().sfxVolumeValue;
        MusicSlider.GetComponent<Slider>().value = AudioManager.GetInstance().musicVolumeValue;
    }

    public void PauseGame () {
        Time.timeScale = 0;
        Image blackScreen = BlackScreen.GetComponent<Image>();
        CanvasGroup pausePanel = PausePanel.GetComponent<CanvasGroup>();
        BlackScreen.SetActive(true);
        Tweener.AddTween(() => pausePanel.alpha, (x) => {pausePanel.alpha = x; }, 1, 0.1f,()=> {
            pausePanel.blocksRaycasts = true;
            pausePanel.interactable = true;

        }, true);
        Tweener.AddTween(() => blackScreen.color.a, (x) => { blackScreen.color = new Color(0, 0, 0, x); }, 0.5f, 0.1f, true);
    }

    public void ResumeGame() {
        CanvasGroup pausePanel = PausePanel.GetComponent<CanvasGroup>();
        Image blackScreen = BlackScreen.GetComponent<Image>();
        pausePanel.blocksRaycasts = false;
        pausePanel.interactable = false;
        Tweener.AddTween(() => pausePanel.alpha, (x) => { pausePanel.alpha = x; }, 0, 0.1f, () => {
            Time.timeScale = 1;
        }, true);
        Tweener.AddTween(() => blackScreen.color.a, (x) => { blackScreen.color = new Color(0, 0, 0, x); }, 0, 0.1f, () => {
            BlackScreen.SetActive(false);
        },true);
    }

    public void RestartLevel() {
        gameManager.RestartLevel();
    }

    public void GoToMenu() {
        gameManager.GoToMenu();
    }

    public void ShowWinScreen () {
        Image blackScreen = BlackScreen.GetComponent<Image>();
        BlackScreen.SetActive(true);
        Tweener.AddTween(() => blackScreen.color.a, (x) => { blackScreen.color = new Color(0, 0, 0, x); }, 0.5f, 0.5f, true);

        Tweener.AddTween(() => YouWin.transform.position.y,(x)=> { YouWin.transform.position = new Vector3(YouWin.transform.position.x, x); },
             Camera.main.WorldToScreenPoint(new Vector3(0,5.5f)).y,0.5f,()=> {
                 Time.timeScale = 0;
             },true);
    }

    public void ShowLoseScreen () {
        Time.timeScale = 0;
        Image blackScreen = BlackScreen.GetComponent<Image>();
        BlackScreen.SetActive(true);
        Tweener.AddTween(() => blackScreen.color.a, (x) => { blackScreen.color = new Color(0, 0, 0, x); }, 0.5f, 0.5f, true);

        Tweener.AddTween(() => YouLose.transform.position.y, (x) => { YouLose.transform.position = new Vector3(YouLose.transform.position.x, x); },
            Camera.main.WorldToScreenPoint(new Vector3(0, 5.5f)).y, 0.5f, () => {
                Time.timeScale = 0;
            }, true);
    }

    public void SetMusicVolume(float value) {
        AudioManager.GetInstance().musicVolumeValue = value;
        if(value==0) MusicButton.GetComponent<MuteButton>().SetMuted();
        else MusicButton.GetComponent<MuteButton>().SetUnmuted();
    }

    public void SetSFXVolume (float value) {
        AudioManager.GetInstance().sfxVolumeValue = value;
        if (value == 0) SFXButton.GetComponent<MuteButton>().SetMuted();
        else SFXButton.GetComponent<MuteButton>().SetUnmuted();
    }

    public void SwitchWeapon () {

        if (weapon1Selected) {
            weapon1Panel.gameObject.SetActive(false);
            weapon2Panel.gameObject.SetActive(true);
            weapon1Barrel.gameObject.SetActive(false);
            weapon2Barrel.gameObject.SetActive(true);
            currentWeaponPanel.sprite = weapon2Sprite;
            otherWeaponPanel.sprite = weapon1Sprite;
            weapon1Selected = false;
        }
        else {
            weapon1Panel.gameObject.SetActive(true);
            weapon2Panel.gameObject.SetActive(false);
            weapon1Barrel.gameObject.SetActive(true);
            weapon2Barrel.gameObject.SetActive(false);
            currentWeaponPanel.sprite = weapon1Sprite;
            otherWeaponPanel.sprite = weapon2Sprite;
            weapon1Selected = true;
        }
        gameManager.SwitchWeapon();
        player.GetComponent<Animator>().SetTrigger("weaponSwitch");
    }



    public void UnhookWeapons () {
        gameManager.GetWeapons()[0].UnhookUI(weapon1Panel);
        gameManager.GetWeapons()[1].UnhookUI(weapon2Panel);
    }

    private void OnDestroy () {
        //UnhookWeapons();
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(UITest))]
public class UITestEditor : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (GUILayout.Button("Switch weapon")) {
            ((UITest) target).SwitchWeapon();
        }
    }
}

#endif