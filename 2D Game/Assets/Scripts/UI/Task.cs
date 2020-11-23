using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Task : MonoBehaviour
{
    private TMP_Text _text;
    [SerializeField] Image _image;
    public static int _shard;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _shard = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shard < 3)
        {
            _text.text = _shard + "/3";
        }
        else
        {
            _text.text = "Portal to next level open";
            _image.enabled = false;
        }
    }
}
