using EnemyAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : HealthManager
{
    public GameObject explosionVFX;
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
        yield return new WaitForSeconds(0.1f);
        GameManager.DestroyObject(gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Boom");
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Hit the Player");
            collision.gameObject.GetComponent<MyHealth>().Kill();
        }
        if (collision.transform.root.tag == "Enemy")
        {
            //DID NOT LIKE THIS
            Debug.Log(collision.transform);
            if (!collision.transform.root.GetComponent<EnemyHealth>().dead)
            {
                Debug.Log("Hit the Enemy");
                collision.transform.root.GetComponent<EnemyHealth>().Kill();
            }
        }
    }
}
