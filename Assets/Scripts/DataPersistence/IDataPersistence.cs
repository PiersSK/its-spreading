using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData (GameData data);

    void SaveData (ref GameData data); //only ref needed as we want implementing scripts to modify data.
}
