using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader instance; 

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

        DontDestroyOnLoad(gameObject);
        
    }
    public void LoadLevel (LevelInfo level) {
        SceneManager.sceneLoaded += (scene, mode) => {
            if (scene == SceneManager.GetSceneByName("LevelScene")) {
                GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SetLevelInfo(level);
                Debug.Log("Level loaded: " + level);
            }
        };
        SceneManager.LoadScene("LevelScene");
    }
}
