using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";
    
    public void OpenKeyboard()
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        if (overlayKeyboard != null)
            inputText = overlayKeyboard.text;
    }
}
