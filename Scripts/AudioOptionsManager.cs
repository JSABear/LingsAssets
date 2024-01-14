using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    public OptionsDataManager optionsDataManager;

    public static float musicVolume {  get; private set; }
    public static float soundEffectsVolume { get; private set; }

    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider; 

    [SerializeField] private Text musicSliderText;
    [SerializeField] private Text soundEffectsSliderText;



    private void Start()
    {
        if (SceneManager.GetActiveScene() != null && SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "OptionsMenu")
        {
            optionsDataManager.LoadFromFile();
            (float v, float e) = optionsDataManager.LoadVolume();

            musicVolume = v;
            soundEffectsVolume = e;

            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = musicVolume;
                soundVolumeSlider.value = soundEffectsVolume;
            }

            OnMusicSliderValueChange(v);
            OnSoundEffectSliderValueChange(e);
        }
    }

    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;

        //musicSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.instance.UpdateMixerVolume();

    }

    public void OnSoundEffectSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        //soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.instance.UpdateMixerVolume();

    }

    public (float, float) ReturnVolume()
    {
        return (musicVolume, soundEffectsVolume);
    }


}
