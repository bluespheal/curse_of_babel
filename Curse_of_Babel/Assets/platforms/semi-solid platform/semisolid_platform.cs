using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semisolid_platform : MonoBehaviour
{
		public Transform player;
		public Transform platform;

		private void OnTriggerEnter(Collider other)// checks if player has landed
		{
			Physics.IgnoreCollision(player.GetComponent<Collider>(), platform.GetComponent<Collider>());
		}

		private void OnTriggerExit(Collider other)// checks if player has landed
		{
			Physics.IgnoreCollision(player.GetComponent<Collider>(), platform.GetComponent<Collider>(), false);
		}
}
