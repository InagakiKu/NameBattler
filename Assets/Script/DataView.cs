using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataView : MonoBehaviour
{
    public int selectOid = 1;
    void Start()
    {
        SqliteDatabase sqlDB = new SqliteDatabase("namebattler.db");
        string query = string.Format("select characters.name as cName,characters.hp as cHP,characters.mp as cMP,characters.str as cSTR,characters.def as cDEF,characters.agi as cAGI,characters.luck as cLUCK,characters.create_at as cCreate_at from characters where oid = {0}", selectOid.ToString());
        DataTable dataTable = sqlDB.ExecuteQuery(query);

    }

}
