using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {

    public void Quit()
    {


#if UNITY_Editor
        UnityEditor.EditorApplication.isPlaying = false;    
#else
        Application.Quit();
#endif
    }
}
