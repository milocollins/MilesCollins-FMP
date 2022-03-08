using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestruct : MonoBehaviour
{
    public float destroyTime;
    private void Awake()
    {
        StartCoroutine(SelfDestruct());
    }
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
