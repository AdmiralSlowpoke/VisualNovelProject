using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadScript : MonoBehaviour
{
    public InkScenary ink;
    
}

/*
 * Saving and loading
To save the state of your story within your game, call:

string savedJson = _inkStory.state.ToJson();

...and then to load it again:

_inkStory.state.LoadJson(savedJson);
*/