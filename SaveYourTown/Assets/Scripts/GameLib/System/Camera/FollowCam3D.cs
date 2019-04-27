using UnityEngine;

public class FollowCam3D : MonoBehaviour
{
    [SerializeField]
    protected GameObject player;

    [SerializeField]
    protected float posXManipulator;

    [SerializeField]
    protected float posYManipulator;

    protected float posX;
    protected float posY;
    protected float posZ;

    protected float shakeTimer = 0.0f;

    [SerializeField]
    protected float time;

    public float _Time { get { return time; } set { time = value; } }

    private void Start()
    {
        UnityEngine.Camera.main.backgroundColor = Color.black;
        posZ = player.transform.position.z - 10.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        posY = player.transform.position.y + posYManipulator;
        posX = player.transform.position.x + posXManipulator;

        if (shakeTimer > 0.0f)
        {
            posX += Random.Range(-0.1f, 0.1f);
            posY += Random.Range(-0.1f, 0.1f);

            shakeTimer -= Time.fixedDeltaTime;
        }

        transform.position = new Vector3(posX, posY, player.transform.position.z - 10.0f);
    }

    public void shakeIt()
    {
        shakeTimer = time;
    }
}