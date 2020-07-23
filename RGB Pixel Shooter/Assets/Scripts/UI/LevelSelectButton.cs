using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{

    public LevelInfo level;

    private void Awake () {
        if (level != null) {
            GetComponent<Button>().onClick.AddListener(() => {
                SceneLoader.GetInstance().SetLevel(level);
                SceneLoader.GetInstance().LoadLevel();
            });
        }
        else {
            GetComponent<Button>().onClick.AddListener(() => {
                SceneLoader.GetInstance().LoadScene("Tutorial");
            });
        }
    }
}
