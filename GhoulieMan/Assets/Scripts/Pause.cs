using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject audioManager;
    private AudioSource audioSource;
    private AudioSource pauseAudio;
    public AudioClip pause;

    void Start()
    {
        audioSource = audioManager.GetComponent<AudioSource>();
        pauseAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseAudio.PlayOneShot(pause);
            if (GameIsPaused) Resume();
            else Paused();
        }
    }
    private void Resume()
    {
        //timescale en 1 reproduce normal.
        Time.timeScale = 1f;
        GameIsPaused = false;
        audioSource.Play();
    }
    private void Paused()
    {
        //timescale en 0 se detiene.
        Time.timeScale = 0f;
        GameIsPaused = true;
        audioSource.Pause();
    }
}
