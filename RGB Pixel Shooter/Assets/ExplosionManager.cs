using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public BloodSplatterTestScript[] explosionArray;
    [HideInInspector]
    public Queue<BloodSplatterTestScript> explosionQueue = new Queue<BloodSplatterTestScript>();

    // Start is called before the first frame update
    void Start()
    { // here we initialize the queue of explosions
        explosionArray = FindObjectsOfType<BloodSplatterTestScript>();
        foreach (BloodSplatterTestScript explosion in explosionArray)
        {
            explosionQueue.Enqueue(explosion);
        }
    }

    public void SpawnExplosion(RGBColor color, Collision2D col, HitStatus hitData)
    {
        explosionQueue.Peek().Initialize(color, hitData, col);
        explosionQueue.Dequeue();
    }

    public void ReturnToQueue(BloodSplatterTestScript explosionObject)
    {
        explosionQueue.Enqueue(explosionObject);
    }
}
