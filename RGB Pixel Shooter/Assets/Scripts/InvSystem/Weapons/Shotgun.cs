using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Weapons/Shotgun")]
public class Shotgun : Weapon {

    public float range = 5f;
    [Range(0f, 90f)]
    public float spreadAngle = 0f;
    public int shotNum = 1;

    public float fireRate = 1f;
    public float loadTime = 1f;

    public int damageAmount;

    private WeaponState state;
    private Queue<RGBColor> loadQueue;

    private RGBDamage[] barrels = new RGBDamage[2];
    private bool[] barrelLoaded = new bool[2];
    private int selectedBarrel = 0;

    public Material lineMaterial;

    [HideInInspector]
    public List<Vector3> startVertices;
    [HideInInspector]
    public List<Vector3> endVertices;

    private static Dictionary<string, UnityEngine.Events.UnityAction> UIHooks;

    public override string GetName () {
        return "Shotgun";
    }

    private void InitHooks () {
        UIHooks = new Dictionary<string, UnityEngine.Events.UnityAction>();
        UIHooks.Add("LoadRed", () => Load(RGBColor.RED));
        UIHooks.Add("LoadGreen", () => Load(RGBColor.GREEN));
        UIHooks.Add("LoadBlue", () => Load(RGBColor.BLUE));
        UIHooks.Add("ShootL", () => Shoot(0, GetPlayerPos()));
        UIHooks.Add("ShootR", () => Shoot(1, GetPlayerPos()));
    }

    public Vector3 GetPlayerPos () {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public override void HookUI (Transform parentPanel) {
        InitHooks();

        foreach (Button button in parentPanel.GetComponentsInChildren<Button>()) {
            if (UIHooks.ContainsKey(button.tag)) {
                button.onClick.AddListener(UIHooks[button.tag]);
            }
        }
    }

    public override void LevelStart () {
        loadQueue = new Queue<RGBColor>();
        startVertices = new List<Vector3>();
        endVertices = new List<Vector3>();

        barrels = new RGBDamage[2];
        barrelLoaded = new bool[2];
        selectedBarrel = 0;
    }

    public void Load (RGBColor rgbColor) {
        if (state != WeaponState.READY) {
            if (!(barrelLoaded[0] && barrelLoaded[1])) {
                loadQueue.Enqueue(rgbColor);
            }
            return;
        }

        int i;
        for (i = 0; i < 2; i++) {
            if (!barrelLoaded[i]) {
                break;
            }
        }

        if (i == 2) {
            return;
        }

        barrelLoaded[i] = true;
        SetState(WeaponState.LOADING);
        Tweener.Invoke(loadTime, () => {
            barrels[i] = new RGBDamage(rgbColor, damageAmount);
            SetState(WeaponState.READY);
        });
    }

    public void Shoot (int barrelIndex, Vector3 position) {
        if (barrelLoaded[barrelIndex]) {
            selectedBarrel = barrelIndex;
            Shoot(position);
        }
    }

    public override void Shoot (Vector3 position) {
        if (state == WeaponState.READY && barrelLoaded[selectedBarrel]) {
            if (startVertices == null) {
                startVertices = new List<Vector3>();
                endVertices = new List<Vector3>();
            }

            Vector3 startVertex, endVertex;

            for (int i = 0; i < shotNum; i++) {
                startVertex = position + (Vector3) deltaPosition;

                Vector2 direction = Quaternion.AngleAxis(Random.Range(-spreadAngle, spreadAngle), Vector3.forward) * Vector2.right;

                RaycastHit2D hit;
                hit = Physics2D.Raycast((Vector2) position + deltaPosition, direction, range, LayerMask.GetMask("Enemies"));

                //Debug.DrawLine(position + (Vector3) deltaPosition, position + (Vector3) deltaPosition + (Vector3) direction * range);

                if (hit.collider != null) {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
                        GenericEnemy enemy = hit.collider.GetComponent<GenericEnemy>();

                        enemy.TakeDamage(barrels[selectedBarrel]);
                    }
                    endVertex = hit.point;
                }
                else {
                    endVertex = position + (Vector3) deltaPosition + (Vector3) direction * range;
                }

                GenerateLine(startVertex, endVertex, barrels[selectedBarrel].color);
            }

            barrelLoaded[selectedBarrel] = false;

            SetState(WeaponState.COOLDOWN);
            Tweener.Invoke(1f / fireRate, () => {
                SetState(WeaponState.READY);
            });
        }
    }

    private void SetState (WeaponState newState) {
        state = newState;

        if (newState == WeaponState.READY && loadQueue.Count > 0) {
            Load(loadQueue.Dequeue());
        }
    }

    private void GenerateLine (Vector3 start, Vector3 end, RGBColor rgbColor) {
        GameObject lineRendererObject = new GameObject("LineRenderer");
        //lineRendererObject.transform.parent;
        LineRenderer lr = lineRendererObject.AddComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.startWidth = lr.endWidth = 0.05f;
        lr.SetPositions(new Vector3[2] { start, end });

        Color color = Color.white;
        switch (rgbColor) {
            case RGBColor.RED:
                color = Color.red;
                break;
            case RGBColor.GREEN:
                color = Color.green;
                break;
            case RGBColor.BLUE:
                color = Color.blue;
                break;
        }

        lr.startColor = lr.endColor = color;

        Tweener.AddTween(() => lr.startColor.a, (x) => { lr.startColor = lr.endColor = new Color(lr.startColor.r, lr.startColor.g, lr.startColor.b, x); }, 0f, 1f, TweenMethods.HardLog, () => {
            Destroy(lineRendererObject);
        });
    }
}
