using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Del : MonoBehaviour
{
    [SerializeField] float lifespan = 0.3f; // Время жизни снаряда
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= 1 * Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }
}
