using System;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class DataMananger : MonoBehaviour {

    public static DataMananger instance;

    private string m_currLevel;
    public string CurrentLevel { get { return m_currLevel; } set { m_currLevel = value; } }
    private float m_totalScore;
    public float TotalScore { get { return m_totalScore; } set { m_totalScore = value; } }

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.CurrentLevel = m_currLevel;
        data.TotalScore = m_totalScore;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            m_currLevel = data.CurrentLevel;
            m_totalScore = data.TotalScore;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Score: " + m_totalScore );
    }
}

[Serializable]
class PlayerData
{
    public string CurrentLevel;
    public float TotalScore;
}
