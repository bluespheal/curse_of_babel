using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_detector : MonoBehaviour
{
	public Personaje playerScript;

	float timer = 0.0f;
	bool aterrizando = false;
	bool contando = false;
	float seconds;

	//Detecta cuando la parte de las piernas del jugador entra o sale de contacto con algo
	private void OnTriggerEnter(Collider other)
	{
		//Al aplastar un enemigo le reinicia el salto en el aire al jugador
		if (other.CompareTag("enemy"))
		{
			playerScript.canDash = true;
		}
		//Al llegar a una plataforma le indica al jugador que ya no esta en el aire
		if (other.CompareTag("platform"))
		{
			playerScript.isGrounded = true;
		}
	}

	private void OnTriggerStay(Collider other)
	{//Ejecuta el sistema de "aterrizage" mientras estes en contacto con una plataforma
		if (other.CompareTag("platform"))
		{
			playerScript.isGrounded = true;
			if(!aterrizando)
			{
				aterrizando = true;
				contando = true;
			}
		}
    }
	//Le indica al jugador que dejo de estar en contacto con el suelo
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("platform"))
		{
			playerScript.isGrounded = false;
			aterrizando = false;
		}
	}
	//Cuando el jugador aterriza evita que que pueda dashear de inmediato
	private void Update()
	{
		if (aterrizando && contando)
		{
			timer += Time.deltaTime;
			if (seconds < timer + 0.1f)
			{
				seconds += 0.1f;
				if(seconds >= 0.2f)
				{
					//Transcurrido el tiempo de aterrizage, el jugador puede volver a dashear
					playerScript.canDash = true;
					contando = false;
					seconds = 0;
				}
			}
		}
	}
}