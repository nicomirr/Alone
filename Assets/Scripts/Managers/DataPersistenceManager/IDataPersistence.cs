using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence       //Una interface define los m�todos que debe utilizar el script que la implementa.
{
    void LoadData (GameData data);
    void SaveData (ref GameData data);  //ref permite la modificaci�n directa de valores.
}
