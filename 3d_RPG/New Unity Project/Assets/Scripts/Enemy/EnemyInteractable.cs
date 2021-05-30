using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractable : Interectable
{
    protected override void Interact()
    {
        base.Interact();
        _player.CreatureAnimator.SetBool("Attack", true);
        
    }
    public override void OnUnFocus()
    {
        base.OnUnFocus();
        _player.CreatureAnimator.SetBool("Attack", false);
    }
}
