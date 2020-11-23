using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private ServiceManager _serviceManager;
    private void Start()
    {
        _serviceManager = ServiceManager.Instanse;
    }
    private void OnTriggerEnter2D(Collider2D info)
    {
        _serviceManager.Restart();
    }
}

