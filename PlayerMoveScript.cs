using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject MainPlayer;
    
    
    public float playerMoveSpeed = 5f;
    private int playOnce = 1;
    public bool startPlayerMove = false;

    private Animator _anim;

    private HealthBarHero _healthBarB;
    private ManagerGameMain _gameManager;
    private MoveHint _moveHint;
    private RotatePlayer _rotatePlayer;
    [SerializeField] private PlayerController _playerController;
    private DestroyerKit _destroyerKit;
    private Image _levelUpEffectBanknote;
    
    [SerializeField] private GameObject _finishUI;
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private EndGameAnim EndGameAnimationScript;
    [SerializeField] private LoseAnim LoseGameAnimationScript;
    
    
    [SerializeField] private List<GameObject> deactivatedObjects = new List<GameObject>();
    
    private void Start()
    {
        GameObject managerObject = GameObject.Find("ManagerOfGAme");
        _gameManager = managerObject.GetComponent<ManagerGameMain>();
        
        GameObject healthBar = GameObject.Find("Health BAR");
        _healthBarB = healthBar.GetComponent<HealthBarHero>();
        
        GameObject animator = GameObject.Find("player");
        _anim = animator.GetComponent<Animator>();
        
        GameObject playerController = GameObject.Find("player");
        _playerController = playerController.GetComponent<PlayerController>();
        
        GameObject moveHint = GameObject.Find("StartHint");
        _moveHint = moveHint.GetComponent<MoveHint>();
        

        GameObject destroyerKit = GameObject.Find("PartySchoolKit");
        _destroyerKit = destroyerKit.GetComponent<DestroyerKit>();
        
        GameObject levelUpBanknote = GameObject.Find("LevelUp");
        _levelUpEffectBanknote = levelUpBanknote.GetComponent<Image>();
        
        GameObject finishUI = GameObject.Find("Win UI Show");
        _finishUI = finishUI.GetComponent<GameObject>();
        
        GameObject loseUI = GameObject.Find("Lose UI Show");
        _loseUI = loseUI.GetComponent<GameObject>();
        
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (playerMoveSpeed * Time.deltaTime));
        if (startPlayerMove)
        {
            _anim.SetFloat("IterationChange", 1f);
            startPlayerMove = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RotatePlane"))
        {
            
            RotatePlayer _rotatePlayer = other.GetComponent<RotatePlayer>();
            
            if (_rotatePlayer.rotateDirection == RotatePlayer.RotateSide.Right)
            {   //ПОВОРОТ НА ПРАВО + Отключения Rotate Plane изза бага 
                StartCoroutine(DisableAndEnable(other.gameObject, 3f));
                MainPlayer.transform.DOLocalRotate(new Vector3(0, 90, 0), 1f); 
            }
            else if (_rotatePlayer.rotateDirection == RotatePlayer.RotateSide.Left)
            {   //ПОВОРОТ НА ЛЕВО
                StartCoroutine(DisableAndEnable(other.gameObject, 3f));
                MainPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0), 1f); 
            }
        }
        
        
        if (other.CompareTag($"banknote"))
        {
            _levelUpEffectBanknote.DOFade(1f, 0.2f).OnComplete(() => _levelUpEffectBanknote.DOFade(0f, 0.2f));
            _healthBarB.ChangeBanknote();
            _gameManager.MoneyBanknote(); 
            _gameManager.Checker();
            other.gameObject.SetActive(false);
            deactivatedObjects.Add(other.gameObject);
        }
        else if (other.CompareTag($"bottle"))
        {
            if (_gameManager.money <= 0)
            {
                _playerController.canMove = false;
                _loseUI.SetActive(true);
                LoseGameAnimationScript.LoseGameAnimation();
                finishDefault();
            }
            _healthBarB.ChangeHealthBarMinusBottle(); //ссылка на шкалу статуса игрока ВАЖНО
            _gameManager.MoneyBottle(); //при подборе от общем суммы
            _gameManager.Checker();
            other.gameObject.SetActive(false);
            deactivatedObjects.Add(other.gameObject);
        }
        else if (other.CompareTag($"party"))
        {
            if (_gameManager.money <= 0)
            {
                _playerController.canMove = false;
                _loseUI.SetActive(true);
                LoseGameAnimationScript.LoseGameAnimation();
                finishDefault();
            }
            _destroyerKit.DisableKit();
            _healthBarB.ChangeHealthBarMinusParty();
            _gameManager.MoneyParty(); //дополнительные
            _gameManager.Checker();
            other.gameObject.SetActive(false);
            deactivatedObjects.Add(other.gameObject);
        }
        else if (other.CompareTag($"school"))
        {
            _healthBarB.ChangeBanknoteSchool();
            _destroyerKit.DisableKit();
            _gameManager.MoneySchool();
            _gameManager.Checker();
            other.gameObject.SetActive(false);
            deactivatedObjects.Add(other.gameObject);
        }
        else if (other.CompareTag($"robber"))
        {
            _gameManager.MoneyRobber();
            _gameManager.Checker();
            other.gameObject.SetActive(false);
            deactivatedObjects.Add(other.gameObject);
        }
        
        if (other.CompareTag($"Finish")) //Показ финала уровня игры
        {
            _playerController.canMove = false;
            finishDefault();
            _finishUI.SetActive(true);
            EndGameAnimationScript.EndGameAnimation();
        }
    }
    
    
    private IEnumerator DisableAndEnable(GameObject target, float delay)
    {
        target.SetActive(false);
        yield return new WaitForSeconds(delay);
        target.SetActive(true);
    }

    public void TurnOnHint()
    {
        _moveHint.showHint = true;
        _moveHint.ShowHintToStartVoid();
        _moveHint.gameObject.SetActive(true);
    }

    public void TurnOffHint()
    {
        _moveHint.showHint = false;
        _moveHint.BreakYoyoLoop();
        _moveHint.gameObject.SetActive(false);
        if (playOnce == 1)
        {
            StartCoroutine(SlowlyIncreaseSpeed());
            playOnce++;
        }
       
    }
    IEnumerator SlowlyIncreaseSpeed()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.1f);
            playerMoveSpeed += 1;
        }
    }

    private void finishDefault()
    {
        playerMoveSpeed = 0f;
        startPlayerMove = false;
        _anim.SetFloat("IterationChange", 0f);
        transform.DORotate(new Vector3(0, 0, 0), 0.75f); 
    }

    public void StartAgainGame()
    {
        _destroyerKit.ActivateKit();
        _gameManager.SetMoneyZero();
        playOnce = 1;
        MainPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f); 
        _playerController.canMove = true;
        _loseUI.SetActive(false);
        _finishUI.SetActive(false);
        MainPlayer.transform.DOMove(startPoint.transform.position, 0.1f).OnComplete(() => MainPlayer.transform.DOMove(startPoint.transform.position, 0.1f));
        TurnOnHint();
        ReactivateDeactivatedObjects();
    }
    
    public void ReactivateDeactivatedObjects() // Активация после рестарта
    {
        foreach (GameObject obj in deactivatedObjects)
        {
            obj.SetActive(true);
        }
        deactivatedObjects.Clear(); 
    }
}
