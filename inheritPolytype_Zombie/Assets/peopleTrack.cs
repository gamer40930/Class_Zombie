using UnityEngine;
using System.Linq; // 引用 系統.查詢語言 Lin Query


/// <summary>
/// 追蹤人類
/// </summary>
public class peopleTrack : People
{

    /// <summary>
    /// 目標
    /// </summary>
    public Transform target;
    /// <summary>
    /// 人類陣列
    /// </summary>
    public People[] people;
    /// <summary>
    /// 距離
    /// </summary>
    public float[] distance;


    protected virtual void Start()
    {
        // 人類陣列 = 透過類型搜尋物件<泛型>
        people = FindObjectsOfType<People>();
        distance = new float[people.Length];
        // 設定目的地，避免一開始導覽器錯亂
        agent.SetDestination(Vector3.zero);
    }

    /// <summary>
    /// 追蹤方法
    /// </summary>
    protected virtual void track()
    {
        // 儲存所有人和殭屍的距離
        for (int i = 0; i < people.Length; i++)
        {
            if (people[i] == null || people[i].transform.name == "殭屍" || people[i].transform.name == "警察")
            {
                if (people[i] = null) distance[i] = 1000; // 如果人類死亡距離改為1000 
                distance[i] = 999;                        // 與殭屍和警察的距離改為999 (不要追殭屍和警察)
                continue;                                 // 繼續 - 跳過並執行下一趟迴圈    
            }
            distance[i] = Vector3.Distance(transform.position, people[i].transform.position);
        }


        // 判斷最近 
        float min = distance.Min();                     // 最小值 = 距離.最小值
        int index = distance.ToList().IndexOf(min);     // 索引值 = 距離.轉成清單.取得索引值(最小值)

        target = people[index].transform;               // 目標 = 人類[索引值].變形

        // 追蹤最近
        agent.SetDestination(target.position);

        Debug.DrawLine(transform.position, target.position, Color.yellow);

        if (agent.remainingDistance <= 1f && min != 999) HitPeople(); // 距離小於 1 且 距離不是999時候攻擊人類

    }


    protected virtual void Update()
    {
        track();
    }

    private float TimerHit;  // Ctrl + R+R 可改名字

    /// <summary>
    /// 攻擊人類
    /// </summary>
    private void HitPeople()
    {
        if (TimerHit >= 1f)                               // 如果時間 >= 1 秒
        {
            TimerHit = 0;                                 // 計時器歸零  
            agent.isStopped = true;                       // 代理器 停止
            ani.SetTrigger("攻擊");                       // 攻擊
            target.GetComponent<People>().Dead();         // 人類死亡
        }
        else
        {
            agent.isStopped = false;                    // 否則 - 代理器開始      
            TimerHit += Time.deltaTime;                 // 計時器 累加
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            Dead();
        }
    }
}
