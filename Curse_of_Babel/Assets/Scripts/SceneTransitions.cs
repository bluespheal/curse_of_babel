using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
  public Animator transitionAnim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    IEnumerator LoadScene(){
      transitionAnim.SetTrigger("start");
      yield return new WaitForSeconds(1.5f);
    }
}
