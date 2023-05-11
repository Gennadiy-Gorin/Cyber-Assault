using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterDescription : MonoBehaviour
{
    private MenuController menu;

    [SerializeField]
    private Text weaponText;

    [SerializeField]
    private Text characterName;

    [SerializeField]
    private Text damageResistText;

    [SerializeField]
    private Text maxHpText;


    [SerializeField]
    private Text descriptionText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private Image gunImage;

    [SerializeField]
    private Image characterImage;

    [SerializeField]
    private List<CharacterData> characters;

    private int currentId;

    private void Start()
    {
        menu = GetComponentInParent<MenuController>();
    }

    private void OnEnable()
    {
        FillDescription(0);
        currentId = 0;
    }


    public void FillDescription(int characterId) {

        if (characterId >= characters.Count) {
            Debug.LogWarning("Character Index to large");
            return;
        }
        currentId = characterId;
        CharacterData player = characters[characterId];

        characterName.text = player.CharacterName;
        characterImage.sprite = player.CharacterImage;
        damageResistText.text = "Damage resist: " + player.DamageResist * 100 + "%";
        maxHpText.text = "Hp: " + player.MaxHealth;
        descriptionText.text = player.CharacterDescription;
        weaponText.text = "Starter weapon:\n" + player.DefaultGun.GunName;
        gunImage.sprite = player.DefaultGun.Icon;
        characterImage.sprite = player.CharacterImage;
        if (player.Speed < 3) speedText.text = "Speed: slow";
        else if(player.Speed>4) speedText.text = "Speed: fast";
        else speedText.text = "Speed: medium";
    }

    public void ChooseCharacter() {

        if (currentId ==-1) return;

        menu.StartGame(characters[currentId]);
    
    }

}
