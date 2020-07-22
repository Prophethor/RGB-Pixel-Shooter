using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader instance;
    private LevelInfo level;
    public GameObject blackoutScreen;
    private GameObject blackoutScreenObject;

    public static SceneLoader GetInstance () {
        if (instance == null) {
            GameObject sceneLoaderObject = new GameObject("SceneLoader");
            sceneLoaderObject.AddComponent<SceneLoader>();
        }
        return instance;
    }

    private void Awake () {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        blackoutScreen = Resources.Load<GameObject>("Prefabs\\UI\\BlackoutScreen");
        SceneManager.sceneLoaded += (x, y) => {
            FadeIn();
        };
        DontDestroyOnLoad(gameObject);
    }

    public void SetLevel(LevelInfo level) {
        this.level = level;
    }

    void LoadLevelMethod (Scene scene, LoadSceneMode mode) {
        if (scene == SceneManager.GetSceneByName("LevelScene")) {
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SetLevelInfo(level);
        }
    }

    public void LoadLevel () {
        SceneManager.sceneLoaded += LoadLevelMethod;
        LoadScene("LevelScene");
    }

    public void FinishLevel (string scene) {
        SceneManager.sceneLoaded -= LoadLevelMethod;
        LoadScene(scene);
    }

    private void FadeIn() {
        if (blackoutScreenObject == null) blackoutScreenObject = Instantiate(blackoutScreen);
        Image blackout = blackoutScreenObject.GetComponentInChildren<Image>();
        Tweener.AddTween(() => blackout.color.a, (x) => { blackout.color = new Color(0, 0, 0, x); }, 0, 0.5f, TweenMethods.Linear, ()=> {
            blackoutScreenObject.SetActive(false);
        }, true);
    }

    private void FadeOut() {
        if (blackoutScreenObject == null) blackoutScreenObject = Instantiate(blackoutScreen);
        Image blackout = blackoutScreenObject.GetComponentInChildren<Image>();
        blackoutScreenObject.SetActive(true);
        Tweener.AddTween(() => blackout.color.a, (x) => { blackout.color = new Color(0, 0, 0, x); }, 255, 0.5f, TweenMethods.Linear, true);
    }

    public void LoadScene (string scene) {
        FadeOut();
        SceneManager.LoadScene(scene);
    }
}
