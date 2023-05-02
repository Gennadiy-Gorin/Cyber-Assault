using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType { Enhance, Attack, Utility,Filler }
public abstract class Bonus : MonoBehaviour
{
    public string bonusName;
    public BonusType type;
    public Sprite icon;
    public int cost;
    public string description;
    [SerializeField]
    protected int bonusLevel;
    protected int maxlevel;

    public int Maxlevel { get=>maxlevel; }
    public int BonusLevel { get => bonusLevel; }

    void Start()
    {
        //bonusLevel = 0;
        maxlevel = 4;
    }

    public abstract void Activate();


    public abstract void Deactivate();

    public abstract void Upgrade();


   /* public override bool Equals(object other)
    {
        if (other == null) return false;
        Bonus obj = other as Bonus;
        if (obj == null) return false;
        if (obj.bonusName == this.bonusName) return true;
        return false;

    }*/

   /* public override int GetHashCode()
    {
        int hashCode = -112476320;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + hideFlags.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Transform>.Default.GetHashCode(transform);
        hashCode = hashCode * -1521134295 + EqualityComparer<GameObject>.Default.GetHashCode(gameObject);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(tag);
        hashCode = hashCode * -1521134295 + enabled.GetHashCode();
        hashCode = hashCode * -1521134295 + isActiveAndEnabled.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(bonusName);
        hashCode = hashCode * -1521134295 + type.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Sprite>.Default.GetHashCode(icon);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
        hashCode = hashCode * -1521134295 + bonusLevel.GetHashCode();
        hashCode = hashCode * -1521134295 + maxlevel.GetHashCode();
        hashCode = hashCode * -1521134295 + Maxlevel.GetHashCode();
        hashCode = hashCode * -1521134295 + BonusLevel.GetHashCode();
        return hashCode;
    }*/
}
