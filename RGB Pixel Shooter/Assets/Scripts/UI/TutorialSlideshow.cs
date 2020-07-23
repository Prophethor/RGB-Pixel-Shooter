using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSlideshow : MonoBehaviour
{

    public GameObject
        panel1, button1,
        panel2, button2,
        panel3, button3,
        panel4, button4,
        panel5, button5,
        panel6, button6,
        panel7, button7,
        arrows1, arrows2,
        arrows4, arrows5;

    private void Start () {
        Tweener.Invoke(1f, () => {
            Tweener.AddTween(
                () => panel1.transform.position.y,
                (x) => { panel1.transform.position = new Vector3(panel1.transform.position.x, x, panel1.transform.position.z); },
                Camera.main.WorldToScreenPoint(new Vector3(0, 5f, 0)).y,
                0.3f,
                () => ShowArrows(arrows1),
                true
            );
        });
        
        button1.GetComponent<Button>().onClick.AddListener(() => { HideArrows(arrows1); SwitchPanel(panel1, panel2); });
        button2.GetComponent<Button>().onClick.AddListener(() => { SwitchPanel(panel2, panel3); });
        button3.GetComponent<Button>().onClick.AddListener(() => { SwitchPanel(panel3, panel4); ShowArrows(arrows2); });
        button4.GetComponent<Button>().onClick.AddListener(() => { HideArrows(arrows2); SwitchPanel(panel4, panel5); });
        button5.GetComponent<Button>().onClick.AddListener(() => { SwitchPanel(panel5, panel6); ShowArrows(arrows4); });
        button6.GetComponent<Button>().onClick.AddListener(() => { HideArrows(arrows4); SwitchPanel(panel6, panel7); ShowArrows(arrows5); });
        button7.GetComponent<Button>().onClick.AddListener(() => { SceneLoader.GetInstance().LoadScene("LevelSelect"); });
    }

    public void ShowArrows(GameObject arrows) {
        Tweener.AddTween(
                        () => arrows.GetComponent<CanvasGroup>().alpha,
                        (x) => { arrows.GetComponent<CanvasGroup>().alpha = x; },
                        1,
                        0.3f,
                        true
        );
    }

    public void HideArrows(GameObject arrows) {
        Tweener.AddTween(
                        () => arrows.GetComponent<CanvasGroup>().alpha,
                        (x) => { arrows.GetComponent<CanvasGroup>().alpha = x; },
                        0,
                        0.3f,
                        true
        );
    }

    public void SwitchPanel(GameObject panel1, GameObject panel2) {
        Tweener.AddTween(
            ()=>panel1.transform.position.y, 
            (x) => { panel1.transform.position = new Vector3(panel1.transform.position.x, x, panel1.transform.position.z); },
            Camera.main.WorldToScreenPoint(new Vector3(0, 20f, 0)).y, 
            0.5f,
            TweenMethods.Ease,
            ()=> {
                Tweener.AddTween(
                    ()=>panel2.transform.position.y, 
                    (x) => { panel2.transform.position = new Vector3(panel2.transform.position.x, x, panel1.transform.position.z); },
                    Camera.main.WorldToScreenPoint(new Vector3(0, 5f, 0)).y, 
                    0.5f, 
                    TweenMethods.Ease,
                    true);
            },
            true);
    }
}
