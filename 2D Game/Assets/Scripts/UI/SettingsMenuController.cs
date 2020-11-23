using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenuController : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private Slider _volume;
    [SerializeField] private AudioMixer _masterMixer;
    [Space]
    [SerializeField] private Toggle _fullScreen;
    [Space]
    [SerializeField] TMP_Dropdown _resolutionDropDown;
    private Resolution[] _availableResolutions;
    [Space]
    [SerializeField] TMP_Dropdown _qualityDropDown;
    private string[] _qualityLevels;

    
    // Start is called before the first frame update
    void Start()
    {
        _volume.onValueChanged.AddListener(OnVolumeChanged);
        _fullScreen.onValueChanged.AddListener(OnFullScreenChanged);
        _resolutionDropDown.onValueChanged.AddListener(OnResolutionChanged);
        _qualityDropDown.onValueChanged.AddListener(OnQualityChanged);

        _availableResolutions = Screen.resolutions;
        _resolutionDropDown.ClearOptions();
        int currentIndex = 0;

        List<string> options = new List<string>();
        for(int i = 0; i < _availableResolutions.Length; i++)
        {
            if (_availableResolutions[i].width <= 800)
            {
                continue;
            }
            options.Add(_availableResolutions[i].width + "x" + _availableResolutions[i].height);
            if(_availableResolutions[i].width == Screen.currentResolution.width && _availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
            }
        }
        _resolutionDropDown.AddOptions(options);
        _resolutionDropDown.value = currentIndex;
        _resolutionDropDown.RefreshShownValue();

        _qualityLevels = QualitySettings.names;
        _qualityDropDown.ClearOptions();

        _qualityDropDown.AddOptions(_qualityLevels.ToList());
        int qualityLvl = QualitySettings.GetQualityLevel();
        _qualityDropDown.value = qualityLvl;
        _qualityDropDown.RefreshShownValue();
    }

    private void OnDestroy()
    {
        _volume.onValueChanged.RemoveListener(OnVolumeChanged);
        _fullScreen.onValueChanged.RemoveListener(OnFullScreenChanged);
        _resolutionDropDown.onValueChanged.RemoveListener(OnResolutionChanged);
        _qualityDropDown.onValueChanged.RemoveListener(OnQualityChanged);
    }
    private void OnVolumeChanged(float volume)
    {
        _masterMixer.SetFloat("Volume", volume);
    }
    private void OnFullScreenChanged(bool value)
    {
        Screen.fullScreen = value;
    }
    private void OnResolutionChanged(int resolutionindex)
    {
        Resolution resolution = _availableResolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    private void OnQualityChanged(int qualityLvl)
    {
        QualitySettings.SetQualityLevel(qualityLvl,true);
    }


}
