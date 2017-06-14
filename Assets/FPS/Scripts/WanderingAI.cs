using UnityEngine;

/// <summary>
/// 漫游 AI
/// </summary>
public class WanderingAI : MonoBehaviour
{
    public const float baseSpeed = 2.0f;
    public float speed = 2.0f;
    public float obstacleRange = 3.0f;

    private bool _isAlive;

    [SerializeField]
    private GameObject fireballPrefab;
    private GameObject _fireball;

    public void SetIsAlive(bool isAlive)
    {
        this._isAlive = isAlive;
    }

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
        _isAlive = true;
    }

    void Update()
    {
        // 如果该组件未被激活，停止漫游
        if (!_isAlive)
        {
            return;
        }

        // 向前走一小步
        this.transform.Translate(0, 0, speed * Time.deltaTime);

        // 向正前方发射射线
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hitInfo;

        // 检测是否有足够的空间通过
        bool isHit = Physics.SphereCast(ray, 0.75f, out hitInfo);
        if (isHit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>() && null == _fireball)
            {
                // 如果正前方是玩家对象，且场景中没有火球，则发射一颗火球飞向玩家
                _fireball = Instantiate(fireballPrefab) as GameObject;
                _fireball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                _fireball.transform.position = this.transform.TransformPoint(Vector3.forward * 1.5f);
                _fireball.transform.rotation = this.transform.rotation;
            }
            else if (hitInfo.distance < obstacleRange)
            {
                // 如果前方有障碍物，则随机转向
                float angle = Random.Range(-110, 110);
                this.transform.Rotate(0, angle, 0);
            }
        }
    }   

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }
}
