using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    //TODO:
    //1. Judge character whether into the ending area, trigger game ending operation
    //2. write ending logic
    //  2.1 call ending UI page, display victory canvas(changing UI transparency)
    //  2.2 set timer to exit game when achieve certain time
    //step 1: write in trigger event
    //step 2: write a customized method to end the game
    //step 3: using switch to judge step 2 called in Update

    //declare a swith
    bool m_IsPlayerExit;
    //declare a public object to get player character
    public GameObject m_player;

    //change transparent time
    public float fadeDuration = 2.0f;
    //timer
    float m_Timer;
    //display UI ending time
    public float endDuration = 2.0f;
    //declare a canvas group to get and set the instance transparent of UI
    public CanvasGroup m_ExitCanvasGroup;
    //display ending image
    public Image image;

    //using bool to represent whether player is caught
    bool m_IsPlayerCaught;
    Sprite spriteCaught;
    Sprite SpriteWon;

    //declare audio source for victory and be caught
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    //set switch to check audio played
    bool m_HasAudioPlayed;
    private void Start()
    {
        //read file in Asset/Resources
        spriteCaught = Resources.Load<Sprite>("Caught");
        SpriteWon = Resources.Load<Sprite>("Won");
    }
    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerExit)
        {
            EndLevel(SpriteWon, false,exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(spriteCaught, true,caughtAudio);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if trigger collide is player
        if (other.gameObject == m_player)
        {
            //set switch to true
            m_IsPlayerExit = true;
        }
    }
    //end this level
    void EndLevel(Sprite sprite,bool doRestart,AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        //only activate after published
        //Application.Quit();

        //timer increase
        m_Timer += Time.deltaTime;
        //image source
        image.sprite = sprite;
        //gradually change tranparency
        m_ExitCanvasGroup.alpha = m_Timer / fadeDuration;
        //when timer greater than total time
        if (m_Timer > fadeDuration + endDuration)
        {
            //set Audio switch to false
            m_HasAudioPlayed = false;
            if (doRestart)
            {
                //reload current scene
                SceneManager.LoadScene(0);
            }
            else {
                //stop the game in editting
                //UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
            
        }
        
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
}
