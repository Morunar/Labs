using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PCInputController
{
    private ServiceManager _serviceManager;

    private Camera _cam;
    private bool _leftPointerClicked;

    public Action<Vector3, Collider> LeftPointerClickHandler = delegate { };
    public Action CharacterWindowClicked = delegate { };
    
    public PCInputController(ServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
        _cam = Camera.main;
        Time.timeScale = 1;
        _serviceManager = ServiceManager.Instance;
        _serviceManager.UpdateHandler += OnUpdate;
        
        _serviceManager.FixedUpdateHandler += OnFixedUpdate;
        _serviceManager.DestroyHandler += OnDestroy;
    }
    private void OnUpdate()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _leftPointerClicked = Input.GetButton("Fire1");
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            CharacterWindowClicked();
        }
            
    }
    private void OnFixedUpdate()
    {
        if (_leftPointerClicked)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 100))
            {
                LeftPointerClickHandler(hitInfo.point, hitInfo.collider);
            }
        }
    }

    private void OnDestroy()
    {
        _serviceManager.UpdateHandler -= OnUpdate;
        _serviceManager.FixedUpdateHandler -= OnFixedUpdate;
        _serviceManager.DestroyHandler -= OnDestroy;
    }


}
