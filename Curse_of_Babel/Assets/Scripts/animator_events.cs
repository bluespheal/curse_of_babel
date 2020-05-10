using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animator_events : MonoBehaviour
{
    public Personaje personaje;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void idle_alt()
    {
        personaje.idle_alts();
    }
}
