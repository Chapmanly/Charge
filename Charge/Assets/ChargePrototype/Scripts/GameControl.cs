using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    private float m_timePassed;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(DataMananger.instance == null)
        {
            Instantiate(Resources.Load("DataManager"));
            DataMananger.instance.Load();
        }
    }

    // Called after each level ends
    public void EndOfLevelTally(float passedTime)
    {
        m_timePassed = passedTime;
    }

    // Called in Austin's SceneManager and when the application quits
    public void SaveDataToDataManager()
    {
        DataMananger.instance.CurrentLevel = SceneManager.GetActiveScene().name;
        DataMananger.instance.TotalScore += m_timePassed;
        DataMananger.instance.Save();
    }

    void OnApplicationQuit()
    {
        SaveDataToDataManager();
    }
}