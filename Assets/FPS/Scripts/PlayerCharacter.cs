using UnityEngine;

/// <summary>
/// 玩家角色
/// </summary>
public class PlayerCharacter : MonoBehaviour
{
    // 玩家初始血量
    [SerializeField]
    private int _health;

    /// <summary>
    /// 玩家受伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void Hurt(int damage)
    {
        _health = _health - damage;
        Debug.Log("Player Health: " + _health);
    }
}
