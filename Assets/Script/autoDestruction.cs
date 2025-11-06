using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestruction : MonoBehaviour
{
    // Temps avant l'auto destruction
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
