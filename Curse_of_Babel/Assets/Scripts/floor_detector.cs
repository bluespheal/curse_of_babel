using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor_detector : MonoBehaviour
{
	public Personaje playerScript;

	private void OnTriggerEnter(Collider other)
	{
		print(other.tag);
		if (other.CompareTag("enemy"))
		{
			print("bounce");
			playerScript.bounce = true;
			playerScript.canDash = true;

		}
		if (other.CompareTag("platform"))
		{
			playerScript.isGrounded = true;

		}
		//jump.en_suelo = true;
	}

	private void OnTriggerStay(Collider other)
	{
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