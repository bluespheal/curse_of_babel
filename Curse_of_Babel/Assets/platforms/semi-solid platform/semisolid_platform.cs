using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semisolid_platform : MonoBehaviour
{
		//public Transform player;
		public Transform platform;

		private void OnTriggerStay(Collider other)// Revisa si el jugador ya aterrizó
		{
      platform.transform.GetComponent<Collider>().isTrigger = true;
		}

		private void OnTriggerExit(Collider other)// Revisa si el jugador ya aterrizó
		{
      platform.transform.GetComponent<Collider>().isTrigger = false;
		}
}
