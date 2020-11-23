using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuController : BaseGameMenuController
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _mainMenu;
   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //ayerPrefs.DeleteAll();
        _play.onClick.AddListener(OnMenuClicked);
        _restart.onClick.AddListener(OnRestartClicked);
        _mainMenu.onClick.AddListener(OnMainMenuClicked);
    }
    private void Update()
    {
       
        if (Input.GetKeyUp(KeyCode.Escape)){
            OnMenuClicked();
        }
    }
    protected override void OnDestroy()
    {
        _play.onClick.RemoveListener(OnMenuClicked);
        _restart.onClick.RemoveListener(OnRestartClicked);
        _mainMenu.onClick.RemoveListener(OnMainMenuClicked);
    }
    protected override void OnMenuClicked()
    {
        base.OnMenuClicked();
        
        Time.timeScale = _menu.activeInHierarchy ? 0 : 1;
    }
    protected void OnRestartClicked()
    {
        _serviceManager.Restart();
        _audioManager.Play(UIClipNames.Restart);
    }
    public void OnMainMenuClicked()
    {
        _serviceManager.ChangeLvl((int)Scenes.MainMenu);
        _audioManager.Play(UIClipNames.MainMenu);
    }
}

