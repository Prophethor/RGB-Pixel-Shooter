using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

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
            FadeIn(1.5f);
        };
        DontDestroyOnLoad(gameObject);
    }

    public void SetLevel (LevelInfo level) {
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

    private void FadeIn (float duration) {
        if (blackoutScreenObject == null) blackoutScreenObject = Instantiate(blackoutScreen);
        Image blackout = blackoutScreenObject.GetComponentInChildren<Image>();
        blackout.color = new Color(0, 0, 0, 1f);
        Tweener.AddTween(() => blackout.color.a, (x) => { blackout.color = new Color(0f, 0f, 0f, x); }, 0f, duration, TweenMethods.Quadratic, () => {
            blackoutScreenObject.SetActive(false);
        }, true);
    }

    private void FadeOut (float duration) {
        if (blackoutScreenObject == null) blackoutScreenObject = Instantiate(blackoutScreen);
        Image blackout = blackoutScreenObject.GetComponentInChildren<Image>();
        blackout.color = new Color(0f, 0f, 0f, 0f);
        blackoutScreenObject.SetActive(true);
        Tweener.AddTween(() => blackout.color.a, (x) => { blackout.color = new Color(0f, 0f, 0f, x); }, 1f, duration, TweenMethods.Linear, true);
    }

    public void LoadScene (string scene) {
        FadeOut(1f);
        Tweener.Invoke(1f, () => {
            SceneManager.LoadScene(scene);
            Time.timeScale = 1f;
        }, true);
    }
}
