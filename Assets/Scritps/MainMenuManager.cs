using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject tutorialMenu;
    public GameObject confirmQuitMenu;

    [Header("Options References")]
    public Slider soundFxSlider;
    public Slider musicSlider;
    public AudioMixer gameVolume;

    [Header("Options Values")]
    public float soundFxVolume;
    public float musicVolume;

    private void Awake()
    {
        SetActiveUIOnAwake();

    }
    void Start()
    {
        SetSavedSettings();
        
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButton();
                return;
            }
        }
    }

    public void SetActiveUIOnAwake()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        confirmQuitMenu.SetActive(false);
    }

    public void SetSavedSettings()
    {
        //Volume
        soundFxVolume = PlayerPrefs.GetFloat("FxVolume");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        SetSoundFxVolume(soundFxVolume);
        SetMusicVolume(musicVolume);
        soundFxSlider.value = soundFxVolume;
        musicSlider.value= musicVolume;

        //
    }

    #region Buttons

    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ConfirmQuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        if(settingsMenu.activeSelf == true)
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
            return;
        }
        else if(tutorialMenu.activeSelf == true)
        {
            tutorialMenu.SetActive(false);
            mainMenu.SetActive(true);
            return;
        }
        else if(confirmQuitMenu.activeSelf == true)
        {
            confirmQuitMenu.SetActive(false);
            mainMenu.SetActive(true);
            return;
        }
        else
        {
            confirmQuitMenu.SetActive(true);
            mainMenu.SetActive(false);
        }

    }
    #endregion

    #region Sliders

    public void SetSoundFxVolume(float sliderValue)
    {
        gameVolume.SetFloat("SoundFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("FxVolume", (float)sliderValue);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float sliderValue)
    {
        gameVolume.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", (float)sliderValue);
        PlayerPrefs.Save();
    }

    #endregion
}
