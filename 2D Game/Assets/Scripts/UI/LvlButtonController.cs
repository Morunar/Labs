using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LvlButtonController : MonoBehaviour
{
    private Button _button;
    [SerializeField] private Scenes scene;
    void Start()
    {
        _button = GetComponent<Button>();
        
        if (!PlayerPrefs.HasKey(GamePrefs.LvlPlayed.ToString() + ((int)scene).ToString())){
            _button.interactable = false;
            return;
        }
        _button.onClick.AddListener(OnChangeLvlClicked);

        GetComponentInChildren<TMP_Text>().text = ((int)scene).ToString();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    void Update()
    {

    }
    void OnChangeLvlClicked()
    {
        ServiceManager.Instanse.ChangeLvl((int)scene);
    }

}
