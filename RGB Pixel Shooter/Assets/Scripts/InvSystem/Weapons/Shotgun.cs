using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Weapons/Shotgun")]
public class Shotgun : Weapon {

    public float range = 5f;
    [Range(0f, 90f)]
    public float spreadAngle = 0f;
    public int shotNum = 1;

    public int damageAmount;

    public Material lineMaterial;

    [HideInInspector]
    public List<Vector3> startVertices;
    [HideInInspector]
    public List<Vector3> endVertices;

    private RGBDamage[] barrels = new RGBDamage[2];
    private bool[] barrelLoaded = new bool[2];
    private int selectedBarrel = 0;



    public override string GetName () {
        return "Shotgun";
    }

    public override void LevelStart () {
        startVertices = new List<Vector3>();
        endVertices = new List<Vector3>();

        barrels = new RGBDamage[2];
        barrelLoaded = new bool[2];
        selectedBarrel = 0;
    }

    public void InitUI () {

    }

    public void Load (RGBColor rgbColor) {
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
        barrels[i] = new RGBDamage(rgbColor, damageAmount);
    }

    public void Shoot (int barrelIndex, Vector3 position) {
        if (barrelLoaded[barrelIndex]) {
            selectedBarrel = barrelIndex;
            Shoot(position);
        }
    }

    public override void Shoot (Vector3 position) {
        if (barrelLoaded[selectedBarrel]) {
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
