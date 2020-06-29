using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public BloodSplatterTestScript[] explosionArray;
    [HideInInspector]
    public Queue<BloodSplatterTestScript> explosionQueue = new Queue<BloodSplatterTestScript>();
    [Range(0, 5)]
    public float emmisionValue;
    [Range(1f, 5)]
    public float scale;
    private float initialScale;

    // Start is called before the first frame update
    void Start()
    { // here we initialize the queue of explosions
        initialScale = scale;
        explosionArray = FindObjectsOfType<BloodSplatterTestScript>();
        foreach (BloodSplatterTestScript explosion in explosionArray)
        {
            explosionQueue.Enqueue(explosion);
            explosion.gameObject.transform.localScale = new Vector3(scale, scale, 0);
            
        }
    }

    public void Update()
    {
        if (scale != initialScale )
        {
            initialScale = scale;
            foreach (BloodSplatterTestScript explosion in explosionArray)
            {
                explosion.gameObject.transform.localScale = new Vector3(scale, scale, 0);
            }
        }
    }
    public void SpawnExplosion(RGBColor color, Collision2D col, HitStatus hitData)
    {
        explosionQueue.Peek().Initialize(color, hitData, col, emmisionValue);
        explosionQueue.Dequeue();
    }

    public void ReturnToQueue(BloodSplatterTestScript explosionObject)
    {
        explosionQueue.Enqueue(explosionObject);
    }
}
