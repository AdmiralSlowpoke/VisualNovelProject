using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSOContainer : MonoBehaviour
{
    public CharacterSO characterContainer;

    public void SetCharacterExpression(string mood)
    {
        CharacterMood charMood = characterContainer.characterMoods.Find(x => x.characterReactionName == mood);
        if (charMood != null)
        {
            gameObject.GetComponent<Image>().sprite = charMood.characterReactionImage;
        }
    }
}
