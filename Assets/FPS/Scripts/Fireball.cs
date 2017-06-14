using UnityEngine;

/// <summary>
/// 火球
/// </summary>
public class Fireball : MonoBehaviour
{
    // 移动速度
    [SerializeField]
    float speed = 8.0f;

    [SerializeField]
    // 被火球击中的伤害值
    int damage = 1;

    void Update()
    {
        // 持续向前移动
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        ReactiveTarget enemy = other.GetComponent<ReactiveTarget>();

        if (null != player)
        {
            // 如果击中了玩家，则对玩家造成 damage 点伤害
            Debug.Log("Player hit!");
            player.Hurt(damage);
        }
        else if (null != enemy)
        {
            // 如果击中了敌人，则使敌人死亡
            Debug.Log("Enemy die!");
            enemy.ReactToHit();

            // 广播「击中敌人」消息
            Messenger.Broadcast(GameEvent.ENEMY_HIT);
        }

        Destroy(this.gameObject);
    }
}
