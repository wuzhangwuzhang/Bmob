using UnityEngine;
using System.Collections;
using cn.bmob.api;
using cn.bmob.tools;
using cn.bmob.io;
using System.Collections.Generic;

public class TestBmob1 : MonoBehaviour
{
    private BmobUnity Bmob;

    // Use this for initialization
    void Start()
    {
        BmobDebug.Register(print);
        Bmob = gameObject.GetComponent<BmobUnity>();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Insert"))
        {
            InsertData();
        }
        if (GUILayout.Button("GetData"))
        {
            getRecoard();
        }
        if (GUILayout.Button("Update"))
        {
            updateData();
        }
        if (GUILayout.Button("AllData"))
        {
            getAllInfo();
        }
        if (GUILayout.Button("删除数据"))
        {
            deleteData();
        }
    }

    #region
    /// <summary>
    /// 插入数据
    /// </summary>
    public void InsertData()
    {
        MyGameTable mg = new MyGameTable();
        mg.score = 100;
        mg.playername = "testBmob";
        

        Bmob.Create(MyGameTable.TABLENAME, mg, (resp, exception) =>
        {
            if (exception != null)
            {
                Debug.Log("保存失败，原因： " + exception.Message);
            }
            else
            {
                Debug.Log("保存成功" + resp.createdAt);
            }
        });
    }
    /// <summary>
    /// 获取表所以信息
    /// </summary>
    public void getAllInfo()
    {
        	BmobQuery query = new BmobQuery();
            query.WhereEqualTo("playername", "testBmob");
            Bmob.Find<MyGameTable>(MyGameTable.TABLENAME, query, (resp, exception) =>
            {
                if (exception != null)
                {
                    print("查询失败, 失败原因为： " + exception.Message);
                    return;
                }

                //对返回结果进行处理
                List<MyGameTable> list = resp.results;
                for (int i = 0; i < list.Count; i++)
                {
                    print(list[i].playername.ToString() + "\t" + list[i].score.ToString() + "\t" + list[i].updatedAt.ToString());
                }
            });
    }   
    /// <summary>
    /// 查询数据
    /// </summary>
    public void getRecoard()
    {
        MyGameTable mg = new MyGameTable();

        Bmob.Get<MyGameTable>(MyGameTable.TABLENAME, "674dcc697d", (resp, exception) =>
        {
            if (exception != null)
            {
                Debug.Log("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            MyGameTable game = resp;
            Debug.Log("playerName:"+game.playername + "\n"+"Score:" + game.score );
        });
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    public void updateData()
    {
        MyGameTable game = new MyGameTable();
        game.playername = "wuzhang";
        Bmob.Update(MyGameTable.TABLENAME, "674dcc697d", game, (resp, exception) =>
        {
            if (exception != null)
            {
                Debug.Log("保存失败, 失败原因为： " + exception.Message);
                return;
            }

            Debug.Log("保存成功, @" + resp.updatedAt);
        });
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    public void deleteData()
    {
        Bmob.Delete(MyGameTable.TABLENAME, "674dcc697d", (resp, exception) =>
        {
            if (exception != null)
            {
                Debug.Log("删除失败, 失败原因为： " + exception.Message);
                return;
            }
            else
            {
                Debug.Log("删除成功, @" + resp.msg);
            }
        });
    }
    #endregion
}
