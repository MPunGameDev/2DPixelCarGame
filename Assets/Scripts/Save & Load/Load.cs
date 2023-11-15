using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Load : MonoBehaviour
{
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData instance = PlayerData.Instance;
            instance = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return instance;
        }
        else
        {
            PlayerData pd = PlayerData.Instance;

            return pd;
        }
    }

}
