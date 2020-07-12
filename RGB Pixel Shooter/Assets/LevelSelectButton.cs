using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{

    public LevelInfo level;

    private void Awake () {
        GetComponent<Button>().onClick.AddListener(() =>{
            SceneLoader.GetInstance().LoadLevel(level);
        });
    }
}
