using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Save and load system that convert useful variables into binary files, storing them into a proper file,
//and the other way around
public static class SaveSystem
{  
    public static void SaveGame(GameMaster gameMaster, int slotNumber)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data" + slotNumber + ".gd";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameMaster);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame(int slotNumber)
    {
        string path = Application.persistentDataPath + "/data" + slotNumber + ".gd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("The save file was not found in " + path);
            return null;
        }
    }

    //Save and load data used in the main menú
    public static void SaveStartData(GameMaster gameMaster)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/startData.gd";
        FileStream stream = new FileStream(path, FileMode.Create);

        StartData data = new StartData(gameMaster);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static StartData LoadStartData()
    {
        string path = Application.persistentDataPath + "/startData.gd";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Debug.Log(Application.persistentDataPath + "/startData.gd");
            StartData data = formatter.Deserialize(stream) as StartData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("The save file was not found in " + path);
            return null;
        }
    }
}
