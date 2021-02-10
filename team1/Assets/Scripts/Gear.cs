using UnityEngine;

public abstract class Gear : MonoBehaviour
{
    [SerializeField]
    protected float cooldown;
    protected float currentCooldown = 0;

    public bool CanActivate()
    {
        return currentCooldown <= 0 && ExtraConditions();
    }

    public void Use() {
        if (CanActivate())
            Activate();
    }

    protected virtual void Activate() { }

    protected virtual bool ExtraConditions() { return true; }
}
