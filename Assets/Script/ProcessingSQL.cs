using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingSQL
{
    // DB名を指定して接続
    SqliteDatabase sqlDB = new SqliteDatabase("namebattler.db");

    /// <summary>
    /// SELECT文
    /// </summary>
    /// <returns>DB取得できたかどうか</returns>
    public bool SelectSQL()
    {
        return false;
    }

    /// <summary>
    /// INSERT文
    /// </summary>
    /// <returns>追加できたかどうか</returns>
    public bool InsertSQL()
    {
        return false;
    }

    /// <summary>
    /// DELETE文
    /// </summary>
    /// <returns></returns>
    public void DeleteSQL(string name)
    {
        // SQL文の作成
        string query = string.Format("delete from characters where name = '{0}'", name);
        // SQL文実行
        DataTable dataTable = sqlDB.ExecuteQuery(query);

        // 削除に成功したかどうか
        Debug.Log(name + "の削除に成功");

    }
}
