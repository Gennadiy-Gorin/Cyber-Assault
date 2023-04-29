using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterData", menuName = "Character Data", order = 53)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private string characterName;


    [SerializeField]
    private RuntimeAnimatorController animationController;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float gunDamageBonus;

    [SerializeField]
    private float abilityDamageBonus;

    [SerializeField]
    private float damageResist;
    [SerializeField]
    private GunData defaultGun;
    [SerializeField]
    private float xpBonus;



    public string CharacterName { get => characterName; }
    public RuntimeAnimatorController AnimationController { get => animationController; }
    public float MaxHealth { get => maxHealth; }
    public float Speed { get => speed;  }
    public float GunDamageBonus { get => gunDamageBonus;  }
    public float AbilityDamageBonus { get => abilityDamageBonus;  }
    public float DamageResist { get => damageResist;  }
    public GunData DefaultGun { get => defaultGun;  }
    public float XpBonus { get => xpBonus;  }
}
