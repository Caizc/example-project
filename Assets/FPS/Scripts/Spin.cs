using UnityEngine;

/// <summary>
/// 旋转游戏对象
/// </summary>
public class Spin : MonoBehaviour
{
    [HideInInspector]
    float speed = 0.5f;

    void Start()
    {
        SayHello();
    }

    void Update()
    {
        this.transform.Rotate(0, speed, 0, Space.Self);
    }

    private void SayHello()
    {
        Debug.Log("Hello World!");
    }
}
