using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleHealth : HealthManager
{
    public GameObject barrier;
    public GameObject consoleVFX;
    public AudioClip consoleSFX;
    public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart = null, GameObject origin = null)
    {
        if (!dead)
        {
            dead = true;
            if (barrier != null)
            {
                StartCoroutine(CreateVFX(location));
                Destroy(barrier);
                SFXManager.instance.PlaySFX(consoleSFX, transform, new Vector2(20, 150), 1f);
            }
        }
    }
    private IEnumerator CreateVFX(Vector3 location)
    {
        GameObject VFX = Instantiate(consoleVFX, location, Quaternion.Euler(-90f, 0f, 0f));
        yield return new WaitForSeconds(30f);
        Destroy(VFX);
    }
}
