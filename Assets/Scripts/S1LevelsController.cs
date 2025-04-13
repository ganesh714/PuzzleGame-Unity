using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S1LevelsController : MonoBehaviour
{
    public AudioClip btnClickSound;
    private AudioSource audioSource;
    void Start(){
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadSceneAsync(0);
        }
    }
    public void OnAnyButtonClicked(){
        audioSource.PlayOneShot(btnClickSound);
    }
    public void exit(){
        SceneManager.LoadSceneAsync(0);
    }
    
}