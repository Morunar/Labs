using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : Interectable
{
    [SerializeField] private GameObject _chat;
    protected override void Interact()
    {
        base.Interact();
        _chat.SetActive(true);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _chat.SetActive(false);
        }
    }
}
