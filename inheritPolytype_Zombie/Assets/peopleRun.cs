using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



/// <summary>
/// 會竄逃的人類
/// </summary>
public class peopleRun : People

{
    [Header ("半徑範圍"), Range (1,30)]
    public float radius = 5;
    private Vector3 final;

    private void Update()
    {
        if(agent.remainingDistance < 1f)
        {
            Flee();
        }
        
    }

    /// <summary>
    /// 竄逃
    /// </summary>
    private void Flee()
    {
        // 隨機座標 = 隨機.球內隨機點 * 半徑 + 中心點
        Vector3 pointRun = Random.insideUnitSphere * 5 + transform.position;
        // 導覽網格碰撞 碰撞點
        NavMeshHit hit;

        // 導覽網格. 樣本座標 (座標,碰撞點,半徑,圖層)
        // out 執行方法會將結果直接儲存到傳入的參數內
        // 執行後會將取得的隨機點儲存在 hit 的參數內
        NavMesh.SamplePosition(pointRun, out hit, radius, 1);

        final = hit.position;

        agent.SetDestination(final);
    }
    

    
}
