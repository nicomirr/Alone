using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    string _dataDirPath = "";
    string _dataFileName = "";
    bool _useEncryption = false;
    readonly string _encryptionCodeWord = "word";
    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        _useEncryption = useEncryption;
    }   

    public GameData Load()
    {
        //Path.Combine se utiliza para evitar dificultades, ya que cada sistema operativo tiene distintos separadores (windows por ejemplo tiene el '/').
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //Cargar la información serializada desde el archivo.
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //desencriptado opcional de la información.
                if(_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //deserealizar información en formato Json a formato C#.
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e) 
            { 
                Debug.LogError("Error ocurred when trying to load data from file: " +  fullPath + "\n" + e); 
            }
        }
        return loadedData;
    }

    public void Save(GameData data) 
    {
        //Path.Combine se utiliza para evitar dificultades, ya que cada sistema operativo tiene distintos separadores (windows por ejemplo tiene el '/').
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);  

        try
        {
            //crear el directorio donde el archivo será escrito en caso de no existir.
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serializar el objeto GameData de C# a un archivo Json.
            string dataToStore = JsonUtility.ToJson(data, true);

            //encriptado opcional de la información
            if(_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //Escribir en un archivo toda la información serializada.
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                { 
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)     //la "e" contiene datos acerca del error (exepción) acontecido.
        {
            Debug.LogError("Error ocurred when trying to save data to file: " + fullPath + "\n" + e);
        }        
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++) 
        {
            modifiedData += (char)(data[i] ^ _encryptionCodeWord[i % _encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
