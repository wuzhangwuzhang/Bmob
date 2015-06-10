using UnityEngine;
using System.Collections;
using cn.bmob.io;

public class MyGameTable : BmobTable
{
    /// <summary>
    /// Bmob服务器端我们定义的表名
    /// </summary>
    public const string TABLENAME = "MyGameTable";

    /// <summary>
    /// 玩家姓名
    /// </summary>
    public string playername { get; set; }
    /// <summary>
    /// 玩家得分
    /// </summary>
    public BmobInt score { get; set; }

    /// <summary>
    /// 成员函数
    /// </summary>
    /// <param name="input"></param>
    public override void readFields(BmobInput input)
    {
        base.readFields(input);
        //读取相应的字段
        this.score = input.getInt("score");
        this.playername = input.getString("playername");

    }  
    public override void write(BmobOutput output, bool all)
    {
        base.write(output, all);
        //写入到数据库对应的字段
        output.Put("score", this.score);
        output.Put("playername", this.playername);
    }
}