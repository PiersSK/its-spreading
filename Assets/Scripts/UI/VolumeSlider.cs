using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum VolumeChannel
    {
        Master,
        Music,
        SFX
    }

    private const string MASTERVOL = "masterVol";
    private const string MUSICVOL = "musicVol";
    private const string SFXVOL = "sfxVol";
    private List<string> CHANNELVOLPARAM = new()
    {
        MASTERVOL,
        MUSICVOL,
        SFXVOL
    };

    private const float MUTEVOL = -80f;
    private const float MINVOL = -20f;
    private const float MAXVOL = 0f;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private VolumeChannel targetChannel;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI label;

    private void Start()
    {
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        if (volume == slider.minValue)
            volume = MUTEVOL;
        else
            volume = MINVOL + (MAXVOL - MINVOL) * volume / slider.maxValue;

        audioMixer.SetFloat(CHANNELVOLPARAM[(int)targetChannel], volume);

        label.text = slider.value.ToString() + "%";
    }
}
