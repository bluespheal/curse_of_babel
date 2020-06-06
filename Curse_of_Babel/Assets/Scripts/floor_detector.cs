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


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("enemy"))
		{
			playerScript.canDash = true;
		}
		if (other.CompareTag("platform"))
		{
			playerScript.isGrounded = true;
			//playerScript.comportamientos();
		}
		//jump.en_suelo = true;
	}

	private void OnTriggerStay(Collider other)
	{
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

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("platform"))
		{
			playerScript.isGrounded = false;
			aterrizando = false;
		}
	}
	private void Update()
	{
		if (aterrizando && contando)
		{
			timer += Time.deltaTime;
			if (seconds < timer + 0.1f)
			{
				seconds += 0.1f;
				if(seconds >= 0.5f)
				{
					playerScript.canDash = true;
					contando = false;
					seconds = 0;
				}
			}
		}
	}
}