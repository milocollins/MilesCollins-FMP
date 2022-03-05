using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHealth : HealthManager
{
    public float health = 100f;
    public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart = null, GameObject origin = null)
    {
        health -= damage;
        if (health < 0)
        {
                dead = true;
                health = 0;
                Debug.Log("Dead");
            GameManager.instance.ChangeScene("Prototype");
        }
        else
        {
            Debug.Log(health);
        }
    }
}
