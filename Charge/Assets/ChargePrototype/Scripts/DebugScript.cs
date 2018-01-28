using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour {

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 100, 100, 30), "Time Up"))
        {
            DataMananger.instance.Time += 10;
        }

        if (GUI.Button(new Rect(10, 140, 100, 30), "Time Down"))
        {
            DataMananger.instance.Time -= 10;
        }

        if (GUI.Button(new Rect(10, 180, 100, 30), "Save"))
        {
            DataMananger.instance.Save();
        }

        if (GUI.Button(new Rect(10, 220, 100, 30), "Load"))
        {
            DataMananger.instance.Load();
        }

        if (GUI.Button(new Rect(10, 260, 100, 30), "NextLevel"))
        {
            if (SceneManager.GetActiveScene().name == SceneName.TestSaveScene.ToString())
            {           
                SceneManager.LoadScene(SceneName.TestSaveScene1.ToString());   
            }
            else
            {
                Debug.LogError(SceneManager.GetActiveScene().ToString());
                SceneManager.LoadScene(SceneName.TestSaveScene.ToString());
            }
           
        }
    }

    enum SceneName
    {
        TestSaveScene, TestSaveScene1
    }
}
