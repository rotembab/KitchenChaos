using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyThrowToTrash;
    public override void  Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnAnyThrowToTrash?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestorySelf();
        }
    }
}
