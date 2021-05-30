using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterectable
{
    InterectionController InterectionController { get; }

    float InteractionDistance { get; }
    float StopingDistance { get; }

    Transform Body { get; }
}

public abstract class InterectionController
{
    private bool _isFocused;
    private bool _hasInteracted;
    private PlayerCreature _player;

    private IInterectable _thisInterectable;

    public InterectionController(IInterectable interectable)
    {
        _thisInterectable = interectable;
        ServiceManager.Instance.UpdateHandler += OnUpdate;
    }


    public void OnFocus(PlayerCreature player)
    {
        _isFocused = true;
        _player = player;
    }

    public void OnUnFocus()
    {
        _isFocused = false;
        _hasInteracted = false;
    }

    // Update is called once per frame
    private void OnUpdate()
    {
        if (_isFocused && _player != null)
        {
            if (Vector3.Distance(_thisInterectable.Body.position, _player.transform.position) < _thisInterectable.InteractionDistance && !_hasInteracted)
            {
                Interact();
            }
        }
    }

    private void Interact()
    {
        _hasInteracted = true;
        Debug.Log("Interacted " + _thisInterectable.Body);
    }

    public void Destroy()
    {
        ServiceManager.Instance.UpdateHandler -= OnUpdate;
    }
   
}

public class NPCInterectionController : InterectionController
{
    public NPCInterectionController(IInterectable interectable) : base(interectable)
    {

    }
}