using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool paused=false;
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pause() {
        if (paused)
        {
            Time.timeScale = 1f;
            paused = false;
            PauseMenu.SetActive(false);
            PauseButton.GetComponent<Image>().sprite = pause_image;
        }
        else
        {
            PauseButton.GetComponent<Image>().sprite = play_image;
            PauseMenu.SetActive(true);
            paused = true;
            Time.timeScale = 0f;
        }
    }
}
