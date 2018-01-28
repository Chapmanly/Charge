﻿using System;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class DataMananger : MonoBehaviour {

    public static DataMananger instance;

    private int m_currLevel;
    public int CurrentLevel { get { return m_currLevel; } set { m_currLevel = value; } }
    private float m_time;
    public float Time { get { return m_time; } set { m_time = value; } }
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
        data.Time = m_time;
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
            m_time = data.Time;
            m_totalScore = data.TotalScore;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Time: " + m_time );
    }
}

[Serializable]
class PlayerData
{
    public int CurrentLevel;
    public float Time;
    public float TotalScore;
}
