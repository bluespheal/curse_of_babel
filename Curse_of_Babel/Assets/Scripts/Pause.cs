using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool paused=false;
    public GameObject PauseMenu;
    public GameObject PauseButton;
    public Sprite play_image;
    public Sprite pause_image;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.SetActive(false);
        PauseButton.GetComponent<Image>().sprite = pause_image;
    }

    public void unpause() {
        paused = false;
    }

    // Update is called once per frame
    public void pause() {
        if (paused)
        {
            Time.timeScale = 1f;
            PauseMenu.SetActive(false);
            PauseButton.GetComponent<Image>().sprite = pause_image;
            Invoke("unpause", 0.2f);
        }
        if(!paused)
        {
            Time.timeScale = 0f;
            PauseButton.GetComponent<Image>().sprite = play_image;
            PauseMenu.SetActive(true);
            paused = true;
        }
    }
}
