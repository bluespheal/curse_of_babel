using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
  public Animator transitionAnim;

    void Start()
    {
        StartCoroutine(LoadScene()); //starts the transitions after a few seconds
    }

    IEnumerator LoadScene(){
      transitionAnim.SetTrigger("start"); // plays the star animation after 1 and a half seconds
      yield return new WaitForSeconds(1.5f);
    }
}
