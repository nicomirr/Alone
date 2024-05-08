using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence       //Una interface define los métodos que debe utilizar el script que la implementa.
{
    void LoadData (GameData data);
    void SaveData (ref GameData data);  //ref permite la modificación directa de valores.
}
