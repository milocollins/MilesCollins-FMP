using EnemyAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : HealthManager
{
    public GameObject explosionVFX;
    public AudioClip explosionSFX;
    private void Awake()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
    public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart = null, GameObject origin = null)
    {
        StartCoroutine(ExplosiveDamage());
        //Instantiate VFX
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
    }
    public IEnumerator ExplosiveDamage()
    {
        gameObject.GetComponent<SphereCollider>().enabled = true;
        SFXManager.instance.PlaySFX(explosionSFX, transform, new Vector2(6, 150));
        yield return new WaitForSeconds(0.1f);
        GameManager.DestroyObject(gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<MyHealth>().Kill();
        }
        if (collision.transform.root.tag == "Enemy")
        {
            if (!collision.transform.root.GetComponent<EnemyHealth>().dead)
            {
                collision.transform.root.GetComponent<EnemyHealth>().Kill();
            }
        }
    }
}
