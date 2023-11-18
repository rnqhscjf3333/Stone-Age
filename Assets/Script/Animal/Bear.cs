using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Bear : MonoBehaviour
{
    [SerializeField] private string animalName; // ������ �̸�
    [SerializeField] private int hp;  // ������ ü��

    [SerializeField] private float walkSpeed;  // �ȱ� �ӷ�
    [SerializeField] private float runSpeed;  // �ȱ� �ӷ�

    private Vector3 direction;  // ����

    // ���� ����
    private bool isAction;  // �ൿ ������ �ƴ��� �Ǻ�
    private bool isWalking; // �ȴ���, �� �ȴ��� �Ǻ�
    private bool isRunning;

    [SerializeField] private float walkTime;  // �ȱ� �ð�
    [SerializeField] private float waitTime;  // ��� �ð�
    [SerializeField] private float runTime;  // �ٴ� �ð�
    private float currentTime;

    // �ʿ��� ������Ʈ
    private Animator anim;
    private Rigidbody rigidl;
    private BoxCollider boxCol;

    protected Vector3 destination;  // ������
    protected NavMeshAgent nav;

    public LayerMask PlayerLayer;

    public float viewDistance;
    public float viewAngle;

    private float hpcul = 0;

    public Transform Attackpos;
    public float AttackSize;

    private float Attackcul;
    public float maxAttackcul;

    public Transform StonePos;
    public GameObject Stone;

    public float StonePower;
    public float Stonemaxcul;

    public float Stonecul = 10;

    public AudioClip WalkSound;
    public AudioClip AngrySound;
    public AudioClip DieSound;


    void Start()
    {
        currentTime = 0;   // ��� ����
        isAction = true;   // ��⵵ �ൿ
        anim = GetComponent<Animator>();
        rigidl = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (hp > 0)
        {
            if (Attackcul <= 0)
            {
                Move();
                ElapseTime();
                View();
                Attack();
                if(animalName == "Bear")
                    StoneAttack();
            }
        }
        if (hpcul > 0)
        {
            hpcul -= Time.deltaTime;
        }
        if (Attackcul >= 0.0f)
        {
            Attackcul -= Time.deltaTime;
        }
        Stonecul -= Time.deltaTime;
    }

    private void Move()
    {
        if (isWalking || isRunning)
            nav.SetDestination(transform.position + destination * 5f);
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)  // �����ϰ� ���� �ൿ�� ����
                ReSet();
        }
    }

    private void ReSet()  // ���� �ൿ �غ�
    {
        isAction = true;

        nav.ResetPath();

        isWalking = false;
        anim.SetBool("isWalk", isWalking);
        isRunning = false;
        anim.SetBool("isRun", isRunning);
        nav.speed = walkSpeed;

        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 3); // ���, Ǯ���, �θ���, �ȱ�
        anim.SetBool("isEat", false);

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Eat();
        else if (_random == 2)
            TryWalk();
    }

    private void Wait()  // ���
    {
        currentTime = waitTime;
    }

    private void Eat()  // Ǯ ���
    {
        currentTime = waitTime;
        anim.SetBool("isEat", true);
    }

    private void TryWalk()  // �ȱ�
    {
        currentTime = walkTime;
        isWalking = true;
        anim.SetBool("isWalk", isWalking);
        nav.speed = walkSpeed;
    }

    public void Run(Vector3 _targetPos)
    {
        destination = -new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        nav.speed = runSpeed;

        anim.SetBool("isRun", isRunning);
    }

    public bool View()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, PlayerLayer);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if (_targetTf.name == "Player")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < viewAngle)
                {
                    Debug.DrawRay(transform.position + transform.up, _direction, Color.blue);
                    Run(_targetTf.transform.position);
                }
            }
        }
        return false;
    }
    public void Dead()
    {
        CancelInvoke();
        nav.ResetPath();
        anim.SetTrigger("isDie");
        gameObject.layer = LayerMask.NameToLayer("Item");
    }

    public void hit()
    {
        if (hpcul <= 0)
        {
            hpcul = 1;
            hp -= 1;
            Run(GameObject.FindGameObjectWithTag("Player").transform.position);
            if (animalName == "Bear" && hp <= 5)
            {
                GetComponent<TrailsFX.TrailEffect>().enabled = true;
                runSpeed = 4.5f;
                AudioSource.PlayClipAtPoint(AngrySound, transform.position, 1f);
            }
            if (hp == 0)
            {
                if(DieSound)
                AudioSource.PlayClipAtPoint(DieSound, transform.position, 1f);
                Dead();
            }
        }
    }

    public void Attack()
    {
        Collider[] _target = Physics.OverlapSphere(Attackpos.position, AttackSize, PlayerLayer);

        for (int i = 0; i < _target.Length; i++)
        {
            if (_target[i].GetComponent<ThirdPersonController>())
                if (Attackcul <= 0.0f)
                {
                    Attackcul = maxAttackcul;
                    Invoke("AttackBoxon", 0.7f);
                    if (animalName == "Bear")
                    {
                            Invoke("AttackBoxon", 1.5f);

                    }
                    anim.SetTrigger("isAttack");

                    nav.ResetPath();

                    isWalking = false;
                    anim.SetBool("isWalk", isWalking);
                    isRunning = false;
                    anim.SetBool("isRun", isRunning);
                    nav.speed = 0;
                    currentTime = maxAttackcul;
                }
        }
    }

    public void AttackBoxon()
    {
        Collider[] _target = Physics.OverlapSphere(Attackpos.position, AttackSize, PlayerLayer);

        for (int i = 0; i < _target.Length; i++)
        {
            if (_target[i].GetComponent<ThirdPersonController>())
                _target[i].GetComponent<ThirdPersonController>().hit();
        }
    }
    public void StoneAttack()
    {
        if(Attackcul<=0 && Stonecul <= 0)
        {
            Stonecul = Stonemaxcul;
            Attackcul = 5f;

            anim.SetTrigger("isAttack2");

            nav.ResetPath();

            isWalking = false;
            anim.SetBool("isWalk", isWalking);
            isRunning = false;
            anim.SetBool("isRun", isRunning);
            nav.speed = walkSpeed;

            Invoke("ThrowStone", 3f);

        }
    }
    public void ThrowStone()
    {
        Vector3 aimDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position);
        GameObject bearstone = Instantiate(Stone, StonePos.position, Quaternion.identity);
        bearstone.GetComponent<Rigidbody>().AddForce(aimDirection*StonePower);
        bearstone.GetComponent<Rigidbody>().AddForce(Vector3.up * 200f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(Attackpos.position, AttackSize);

        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, viewDistance);
    }

    public void WalkPlay()
    {
        AudioSource.PlayClipAtPoint(WalkSound, transform.position, 1f);
    }
}