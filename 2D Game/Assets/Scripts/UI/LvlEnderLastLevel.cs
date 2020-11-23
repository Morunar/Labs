using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlEnderLastLevel : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    private ServiceManager _serviceManager;
    void Start()
    {
        _serviceManager = ServiceManager.Instanse;
    }

    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            _serviceManager.ChangeLvl(0);
        }
    }
}
