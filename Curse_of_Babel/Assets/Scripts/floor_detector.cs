using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_detector : MonoBehaviour
{
	public Personaje playerScript;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("enemy"))
		{
			playerScript.bounce = true;
			playerScript.canDash = true;
		}
		if (other.CompareTag("platform"))
		{
			playerScript.isGrounded = true;
			playerScript.knight_animation.SetTrigger("landing");
			//playerScript.comportamientos();
		}
		//jump.en_suelo = true;
	}

	private void OnTriggerStay(Collider other)
	{
        playerScript.isGrounded = true;
        //jump.dashed = false;
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("platform"))
		{
			playerScript.isGrounded = false;

		}
	}
}