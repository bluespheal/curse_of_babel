using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class semisolid_platform : MonoBehaviour
{
		//public Transform player;
		public Transform platform;

		private void OnTriggerStay(Collider other)// checks if player has landed
		{
      platform.transform.GetComponent<Collider>().isTrigger = true;
			//Physics.IgnoreCollision(player.GetComponent<Collider>(), platform.GetComponent<Collider>());
		}

		private void OnTriggerExit(Collider other)// checks if player has landed
		{
      platform.transform.GetComponent<Collider>().isTrigger = false;
        //Physics.IgnoreCollision(player.GetComponent<Collider>(), platform.GetComponent<Collider>(), false);
		}
}
