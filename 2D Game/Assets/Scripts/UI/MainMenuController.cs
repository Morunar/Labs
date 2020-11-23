using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : BaseGameMenuController
{

    [SerializeField] private Button _chooseLvl;
    [SerializeField] private Button _reset;

    [SerializeField] private GameObject _lvlMenu;
    [SerializeField] private Button _closeLvlMenu;
    private int _lvl = 1;
    protected override void Start()
    {
        base.Start();
        
        _chooseLvl.onClick.AddListener(OnMenuLvlClicked);
        _closeLvlMenu.onClick.AddListener(OnMenuLvlClicked);
       
        if (PlayerPrefs.HasKey(GamePrefs.LastPlayedLvl.ToString()))
        {
            _play.GetComponentInChildren<TMP_Text>().text = "Resume";
            _lvl = PlayerPrefs.GetInt(GamePrefs.LastPlayedLvl.ToString());
        }

        _play.onClick.AddListener(OnPlayClicked);
        _reset.onClick.AddListener(OnResetClicked);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        _chooseLvl.onClick.RemoveListener(OnMenuLvlClicked);
        _closeLvlMenu.onClick.RemoveListener(OnMenuLvlClicked);
        _play.onClick.RemoveListener(OnPlayClicked);
        _reset.onClick.RemoveListener(OnResetClicked);
    }
    private void OnMenuLvlClicked()
    {
        _lvlMenu.SetActive(!_lvlMenu.activeInHierarchy);
        OnMenuClicked();
        _audioManager.Play(UIClipNames.MenuLvl);
    }

    private void OnPlayClicked()
    {
        _serviceManager.ChangeLvl(_lvl);
        _audioManager.Play(UIClipNames.Play);
    }

    private void OnResetClicked()
    { 
        _play.GetComponentInChildren<TMP_Text>().text = "Play";
        _audioManager.Play(UIClipNames.Reset);
        _serviceManager.ResetProgres();
        _lvl = 1;
       
    }
}
