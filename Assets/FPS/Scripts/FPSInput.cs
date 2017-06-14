using UnityEngine;

/// <summary>
/// 响应键盘输入来移动
/// </summary>
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    [SerializeField]
    const float baseSpeed = 6.0f;
    [SerializeField]
    float speed = 6.0f;

    // 游戏对象所受的重力
    [SerializeField]
    float gravity = -9.8f;

    private CharacterController _charactorController;

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void Start()
    {
        _charactorController = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        // 给予游戏对象一个竖直向下的重力
        movement.y = gravity;

        // 将方向向量乘以 Time.deltaTime 以消除帧率依赖
        movement = movement * Time.deltaTime;
        // 将方向向量从本地坐标系转换为全局坐标系
        movement = this.transform.TransformDirection(movement);

        _charactorController.Move(movement);
    }

    /// <summary>
    /// 处理速度变化方法
    /// </summary>
    /// <param name="value">速度调节系数</param>
    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }
}
