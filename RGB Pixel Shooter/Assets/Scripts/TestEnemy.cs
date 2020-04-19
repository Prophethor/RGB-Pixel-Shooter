using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : GenericEnemy {

    private void OnDrawGizmos () {
        for(int i = 0; i < hpStackList.Count; i++) {
            RGBColor rcolor = hpStackList[i].GetColor();
            int amount = hpStackList[i].GetAmount();

            Color color = Color.white;

            switch(rcolor) {
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

            for (int j = 0; j < amount; j++) {
                Gizmos.color = color;
                Gizmos.DrawCube(new Vector3(transform.position.x - (hpStackList.Count - 1) / 2f + i * 1.1f, transform.position.y + 4f + j * 1.1f),
                    Vector3.one);
            }
        }
    }

    protected override void Move () {
        throw new System.NotImplementedException();
    }

    protected override void InitiateShanking () {
        throw new System.NotImplementedException();
    }
}
