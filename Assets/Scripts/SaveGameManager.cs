using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveGameManager {

    public static void SaveData(string jsonData, string fileName) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName, FileMode.Create );
        bf.Serialize(stream, jsonData);
        stream.Close();
    }

    public static string LoadData(string fileName) {
        if (File.Exists(Application.persistentDataPath + "/" + fileName)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName, FileMode.Open );
            string jsonData = (string) bf.Deserialize(stream);
            stream.Close();
            return jsonData;
        } else {
            return null;
        }
    }
}