using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 射线发射器
/// </summary>
public class RayShooter : MonoBehaviour
{
    [SerializeField]
    GameObject fireballPrefab;
    [SerializeField]
    AudioSource soundSource;
    [SerializeField]
    AudioClip hitWallSound;
    [SerializeField]
    AudioClip hitEnemySound;

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();

        // 隐藏屏幕中心的光标
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    void OnGUI()
    {
        // 利用 Unity 的即时 GUI 系统，在屏幕中心绘制一个星号 * 代表射击的准星
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;

        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // 屏幕中心点坐标
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

            // 从屏幕中心发出射线
            Ray ray = _camera.ScreenPointToRay(point);

            RaycastHit hitInfo;
            bool isHit = Physics.Raycast(ray, out hitInfo);
            if (isHit)
            {
                GameObject hitObject = hitInfo.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (null != target)
                {
                    // 如果射线击中敌人，则生成一个火球飞向敌人
                    GameObject _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    _fireball.transform.position = this.transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = this.transform.rotation;

                    soundSource.PlayOneShot(hitEnemySound);
                }
                else
                {
                    // 如果射线没有击中敌人，则在射线击中的物体上显示一个球体
                    StartCoroutine(SphereIndicator(hitInfo.point));

                    soundSource.PlayOneShot(hitWallSound);
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.position = position;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
