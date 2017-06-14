using System.Collections;
using UnityEngine;

/// <summary>
/// 有反应的对象
/// </summary>
public class ReactiveTarget : MonoBehaviour
{
    /// <summary>
    /// 对击中作出反应
    /// </summary>
    public void ReactToHit()
    {
        WanderingAI behaviour = this.GetComponent<WanderingAI>();
        if (null != behaviour)
        {
            // 停止被击中对象的漫游行为
            behaviour.SetIsAlive(false);
        }

        // 播放死亡效果
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
