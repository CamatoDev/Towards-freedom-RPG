using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCollision : MonoBehaviour
{
    public float spellDamage;
    public float delay = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAi>().ApplyDamage(spellDamage);
        }
        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
