using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch_light : MonoBehaviour
{
    Light fireLight;
    float light_Int;
    public float minInt = 0.5f, maxInt = 1f;
    void Start()
    {
        fireLight = GetComponent<Light>();
    }

    void Update()
    {
        light_Int = Random.Range(minInt, maxInt);
        fireLight.intensity = light_Int;
    }
}
