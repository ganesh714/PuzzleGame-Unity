using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle Sounds;
    public Toggle ClickSounds;
    public Toggle AnswerResponse;
    public Toggle BatterySaver;
    public Toggle Animations;

    public AudioClip TgleClickSound;
    private AudioSource audioSource;

    public bool isProgramaticallyUpdating = false;
    public bool isInitialized = false;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        InitializeSettingsVariables();
        isProgramaticallyUpdating = true;

        Sounds.isOn = PlayerPrefs.GetInt("Sounds") == 1;
        ClickSounds.isOn = PlayerPrefs.GetInt("ClickSounds") == 1;
        AnswerResponse.isOn = PlayerPrefs.GetInt("AnswerResponse") == 1;
        BatterySaver.isOn = PlayerPrefs.GetInt("BatterySaver") == 1;
        Animations.isOn = PlayerPrefs.GetInt("Animations") == 1;
        isInitialized = true;

        isProgramaticallyUpdating = false;

    // Attach listeners to handle user interactions
    // Sounds.onValueChanged.AddListener(delegate { ToggleSounds(); });
    // ClickSounds.onValueChanged.AddListener(delegate { ToggleClickSounds(); });
    // AnswerResponse.onValueChanged.AddListener(delegate { ToggleAnswerResponse(); });
    // BatterySaver.onValueChanged.AddListener(delegate { ToggleBatterySaver(); });
    // Animations.onValueChanged.AddListener(delegate { ToggleAnimations(); });
    }
    public void OnAnyToggleClicked(){
        if(!isInitialized  || isProgramaticallyUpdating) return;

        if(TgleClickSound != null){
            audioSource.PlayOneShot(TgleClickSound);
            // Debug.Log("Sound From Toggle Func"+WhichToggle);
        }
    }

    public void ToggleSounds()
    {
        if (isProgramaticallyUpdating) return;

        PlayerPrefs.SetInt("Sounds", Sounds.isOn ? 1 : 0);

        isProgramaticallyUpdating = true;
        UpdateClickSounds(Sounds.isOn);
        UpdateAnswerResponse(Sounds.isOn);
        isProgramaticallyUpdating = false;
    }

    public void ToggleClickSounds()
    {
        if (isProgramaticallyUpdating) return;

        PlayerPrefs.SetInt("ClickSounds", ClickSounds.isOn ? 1 : 0);
    
        isProgramaticallyUpdating = true;
        UpdateSounds(ClickSounds.isOn && AnswerResponse.isOn);
        isProgramaticallyUpdating = false;
    }

public void ToggleAnswerResponse()
{
    if (isProgramaticallyUpdating) return;

    PlayerPrefs.SetInt("AnswerResponse", AnswerResponse.isOn ? 1 : 0);
    
    isProgramaticallyUpdating = true;
    UpdateSounds(ClickSounds.isOn && AnswerResponse.isOn);
    isProgramaticallyUpdating = false;
}

    public void ToggleBatterySaver()
    {
    if (isProgramaticallyUpdating) return;

    PlayerPrefs.SetInt("BatterySaver", BatterySaver.isOn ? 1 : 0);
    
    isProgramaticallyUpdating = true;
    UpdateAnimations(!BatterySaver.isOn);
    isProgramaticallyUpdating = false;
    }

    public void ToggleAnimations()
    {
    if (isProgramaticallyUpdating) return;

    PlayerPrefs.SetInt("Animations", Animations.isOn ? 1 : 0);
    
    FindObjectOfType<MainMenu>()?.UpdateAnimations();
    
    isProgramaticallyUpdating = true;
    UpdateBatterySaver(!Animations.isOn);
    isProgramaticallyUpdating = false;
    }

    void InitializeSettingsVariables(){
        if(!PlayerPrefs.HasKey("Sounds")){
            PlayerPrefs.SetInt("Sounds",1);
        }
        if(!PlayerPrefs.HasKey("ClickSounds")){
            PlayerPrefs.SetInt("ClickSounds",1);
        } 
        if(!PlayerPrefs.HasKey("AnswerResponse")){
            PlayerPrefs.SetInt("AnswerResponse",1);
        }
        if(!PlayerPrefs.HasKey("BatterySaver")){
            PlayerPrefs.SetInt("BatterySaver",0);
        }
        if(!PlayerPrefs.HasKey("Animations")){
            PlayerPrefs.SetInt("Animations",1);
        }
    }

    void UpdateSounds(bool TF){
            PlayerPrefs.SetInt("Sounds",TF ? 1 : 0);
            Sounds.isOn = TF;
    }
    void UpdateClickSounds(bool TF){
        PlayerPrefs.SetInt("ClickSounds",TF ? 1 : 0);
        ClickSounds.isOn = TF;
    }
    void UpdateAnswerResponse(bool TF){
        PlayerPrefs.SetInt("AnswerResponse",TF ? 1 : 0);
        AnswerResponse.isOn = TF;
    }
    void UpdateBatterySaver(bool TF){
        PlayerPrefs.SetInt("BatterySaver",TF ? 1 : 0);
        BatterySaver.isOn = TF;
    }
    void UpdateAnimations(bool TF){
        PlayerPrefs.SetInt("Animations",TF ? 1 : 0);
        Animations.isOn = TF;
        FindObjectOfType<MainMenu>()?.UpdateAnimations();
    }    
}
