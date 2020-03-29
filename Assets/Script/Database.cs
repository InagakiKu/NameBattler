using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Database : MonoBehaviour
{
    // 初期化の際に使用
    void Start()
    {
        // Insert
        SqliteDatabase sqlDB = new SqliteDatabase("namebattler.db");
        string query = "insert into characters values('hoge',0,0,0,0,0,0,0,null)";
        sqlDB.ExecuteNonQuery(query);

        //Select
        string selectQuery = "select * from characters";
        DataTable dataTable = sqlDB.ExecuteQuery(selectQuery);

        string name = "";
        foreach (DataRow dr in dataTable.Rows)
        {
            name = (string)dr["name"];
            Debug.Log("name:" + name);
        }
    }


}
