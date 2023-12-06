using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Dropdown dropdown;

    Resolution[] resolutions;

    public Slider musicSlider;
    public Slider soundSlider;

    public void Start()
    {
        audiomixer.GetFloat("Music", out float musicValueForSlider);
        soundSlider.value = musicValueForSlider;

        audiomixer.GetFloat("Sound", out float soundValueForSlider);
        musicSlider.value = soundValueForSlider;


        resolutions = Screen.resolutions;
        dropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropdown.AddOptions(options);
        dropdown.value = currentResolutionIndex;
        dropdown.RefreshShownValue();

    }
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Music", volume);
    }

    public void SetSound(float volume)
    {
        audiomixer.SetFloat("Sound", volume);
    }


    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
