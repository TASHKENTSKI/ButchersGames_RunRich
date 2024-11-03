using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Touch touch;
    [SerializeField] private float rotateSpeedModifier = 0.0035f;

    private ManagerGameMain _gameManager1;
    private PlayerMoveScript _playerMove;
    public float playerMoveSpeed = 3f;
    public float targetSpeed = 5f;
    public float speedIncreaseRate = 0.05f;

    public bool canMove = true;

    private void Start()

    {
        GameObject managerObject1 = GameObject.Find("ManagerOfGAme");
        _gameManager1 = managerObject1.GetComponent<ManagerGameMain>();

        GameObject playerMoveScript = GameObject.Find("player");
        _playerMove = playerMoveScript.GetComponent<PlayerMoveScript>();

        canMove = true;
    }

    public void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            if (canMove)
            {
                _playerMove.TurnOffHint();
                _playerMove.startPlayerMove = true;
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position += transform.right * (touch.deltaPosition.x * rotateSpeedModifier);
                }
            }
        }
    }
}