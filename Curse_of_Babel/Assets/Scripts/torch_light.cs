using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch_light : MonoBehaviour
{
    Light fireLight;
    float light_Int;
    float new_Int;
    float nsteps;
    float vstep;
    float tstep;
    bool ascedente = true;
    public float minInt, maxInt;
    public float minT, maxT;
    float timer = 0.0f;
    float seconds;
    void Start()
    {
        fireLight = GetComponent<Light>();
        light_Int = minInt;
        fireLight.intensity = light_Int;
        calculos();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (seconds < timer + 0.1f)
        {
            seconds += 0.1f;
            if (ascedente)
            {
                light_Int += vstep;
                fireLight.intensity = light_Int;
                if (light_Int > new_Int)
                {
                    ascedente = !ascedente;
                    calculos();
                }
            }
            else
            {
                light_Int -= vstep;
                fireLight.intensity = light_Int;
                if (light_Int < new_Int)
                {
                    ascedente = !ascedente;
                    calculos();
                }
            }
        }
    }
    void calculos()
    {
        new_Int = Random.Range(minInt, maxInt);
        tstep = Random.Range(minT, maxT);
        //print(tstep);
        nsteps = tstep;
        if (new_Int > light_Int)
        {
            vstep = (new_Int - light_Int) / nsteps;
            timer = 0;
            seconds = 0;
        }
        else if (new_Int < light_Int)
        {
            vstep = (light_Int - new_Int) / nsteps;
            timer = 0;
            seconds = 0;
        }
        else
        {
            calculos();
        }
    }
}
