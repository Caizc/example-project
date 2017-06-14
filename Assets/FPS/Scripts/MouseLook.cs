using UnityEngine;

/// <summary>
/// 鼠标观察
/// </summary>
public class MouseLook : MonoBehaviour
{
    /// <summary>
    /// 可旋转方向枚举
    /// </summary>
    public enum RotationAxes
    {
        // 水平和垂直旋转
        MouseXAndY = 0,
        // 仅水平旋转
        MouseX = 1,
        // 仅垂直旋转
        MouseY = 2
    }

    [SerializeField]
    RotationAxes axes = RotationAxes.MouseXAndY;

    // 水平旋转速度
    [SerializeField]
    float sensitivityHor = 2.0f;
    // 垂直旋转速度
    [SerializeField]
    float sensitivityVert = 2.0f;

    // 垂直角度的最小值
    [SerializeField]
    float minimumVert = -45.0f;
    // 垂直角度的最大值
    [SerializeField]
    float maximumVert = 45.0f;

    // 垂直角度
    private float _rotationX = 0;
    // 水平角度
    private float _rotationY = 0;

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (null != body)
        {
            // 如果 Player 上存在 Rigidbody 组件，则设置其 freezeRotation 属性为 true，可以使 Player 单独由鼠标控制而不受物理仿真的作用
            body.freezeRotation = true;
        }
    }

    void Update()
    {
        switch (axes)
        {
            // 水平和垂直旋转
            case RotationAxes.MouseXAndY:
                _rotationX = _rotationX - Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                _rotationY = this.transform.localEulerAngles.y + delta;

                this.transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
                break;

            // 仅水平旋转
            case RotationAxes.MouseX:
                this.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
                break;

            // 仅垂直旋转
            case RotationAxes.MouseY:
                _rotationX = _rotationX - Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                _rotationY = this.transform.localEulerAngles.y;

                this.transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
                break;

            default:
                break;
        }
    }
}
