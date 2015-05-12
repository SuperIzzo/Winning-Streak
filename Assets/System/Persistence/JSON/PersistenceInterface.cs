using UnityEngine;
using System.Collections;

static public class PersistenceInterface {

    static private JSON_Persistence persistence = new JSON_Persistence();

    public static void LoadSave()
    {
        persistence.Load();
    }

    public static void Save()
    {
        persistence.Save();
    }
}

