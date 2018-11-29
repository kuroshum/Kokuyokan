using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WingSnakeEnemyState
{
    Wait,
    Wander,
    Pursuit,
    Destroy,
    Attack,
    Leave,
};

public class WingSnakeEnemy : StatefulObjectBase<WingSnakeEnemy, WingSnakeEnemyState>
{
    private int life;
    private int maxLife = 1;

    //    private float speed = 0.00125f;
//    private float speed = 0.1f;
    private float speed = 0.2f*2f;
    private Vector3 centerPosition;
    private float wanderRange = 0.8f * 3f;
    private Animator animator = null;
    private int eX = Animator.StringToHash("x"), eY = Animator.StringToHash("y");
    private float pursuitLevel = 1.5f * 3f;
//    private float pursuitSpeed = 0.008f;
    private float pursuitSpeed = 0.3f*2f;
    private float attackLevel = 0.8f * 3f;
    private float leaveLevel = 1.0f * 3f;
    private float leaveSpeed = 0.1f;
    private bool isLeave;
    private bool isAttack;

    private Transform player;
    private int direction;
    private int x, y;
    [SerializeField]
    private GameObject[] AttackEffect = new GameObject[4];
    private Vector3[] AttackEffectLocalPosition = new Vector3[4];


    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        life = maxLife;
        centerPosition = transform.position;
        isLeave = false;
        isAttack = false;
        AttackEffectLocalPosition[0] = AttackEffect[0].transform.localPosition;
        AttackEffectLocalPosition[1] = AttackEffect[1].transform.localPosition;
        AttackEffectLocalPosition[2] = AttackEffect[2].transform.localPosition;
        AttackEffectLocalPosition[3] = AttackEffect[3].transform.localPosition;

        stateList.Add(new StateWait(this));
        stateList.Add(new StateWander(this));
        stateList.Add(new StatePursuit(this));
        stateList.Add(new StateDestroy(this));
        stateList.Add(new StateAttack(this));
        stateList.Add(new StateLeave(this));

        stateMachine = new StateMachine<WingSnakeEnemy>();

        ChangeState(WingSnakeEnemyState.Wander);
    }

    public void TakeDamage()
    {
        if (life <= 0)
        {
            ChangeState(WingSnakeEnemyState.Destroy);
        }
    }

    public int AreaJudge(float level)
    {
        int area;
        float x = player.position.x - this.gameObject.transform.position.x;
        float y = player.position.y - this.gameObject.transform.position.y;
        if (Mathf.Abs(x) < level)
        {
            if (Mathf.Abs(y) < level)
            {
                if (x > 0)
                {
                    if (y > 0)
                    {
                        if (x > y)
                        {
                            area = 2;
                        }
                        else
                        {
                            area = 1;
                        }
                    }
                    else
                    {
                        if (x + y > 0)
                        {
                            area = 3;
                        }
                        else
                        {
                            area = 4;
                        }
                    }
                }
                else
                {
                    if (y > 0)
                    {
                        if (x + y < 0)
                        {
                            area = 7;
                        }
                        else
                        {
                            area = 8;
                        }
                    }
                    else
                    {
                        if (x > y)
                        {
                            area = 5;
                        }
                        else
                        {
                            area = 6;
                        }
                    }
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
        if (direction == 1)
        {
            if (area == 2 || area == 3)
            {
                return direction;
            }
            else if (area == 4 || area == 5)
            {
                return 3;
            }
            else if (area == 8 || area == 1)
            {
                return 4;
            }
            else
            {
                return 0;
            }
        }
        else if (direction == 2)
        {
            if (area == 6 || area == 7)
            {
                return direction;
            }
            else if (area == 8 || area == 1)
            {
                return 4;
            }
            else if (area == 4 || area == 5)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
        else if (direction == 3)
        {
            if (area == 4 || area == 5)
            {
                return direction;
            }
            else if (area == 6 || area == 7)
            {
                return 2;
            }
            else if (area == 2 || area == 3)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else if (direction == 4)
        {
            if (area == 8 || area == 1)
            {
                return direction;
            }
            else if (area == 2 || area == 3)
            {
                return 1;
            }
            else if (area == 6 || area == 7)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        return 0;
    }


    #region States
    //StateWait
    private class StateWait : State<WingSnakeEnemy>
    {
        public StateWait(WingSnakeEnemy owner) : base(owner) { }
        public override void Enter()
        {
            owner.animator.speed = 0.0f;
            owner.StartCoroutine("Wait");
        }
        public override void Execute()
        {
            if (owner.AreaJudge(owner.pursuitLevel) != 0)
            {
                owner.ChangeState(WingSnakeEnemyState.Pursuit);
            }
        }
        public override void Exit()
        {
            owner.StopCoroutine("Wait");
        }
    }

    //StateWander
    private class StateWander : State<WingSnakeEnemy>
    {
        private Vector3 distance;
        public StateWander(WingSnakeEnemy owner) : base(owner) { }
        public override void Enter()
        {
            owner.direction = Random.Range(1, 5);
            switch (owner.direction)
            {
                case 1:
                    owner.x = 1;
                    owner.y = 0;
                    break;
                case 2:
                    owner.x = -1;
                    owner.y = 0;
                    break;
                case 3:
                    owner.x = 0;
                    owner.y = -1;
                    break;
                case 4:
                    owner.x = 0;
                    owner.y = 1;
                    break;
                default:
                    owner.x = 0;
                    owner.y = 0;
                    owner.animator.speed = 0.0f;
                    owner.ChangeState(WingSnakeEnemyState.Wander);
                    break;
            }
            if (owner.transform.position.x - owner.centerPosition.x > owner.wanderRange)
            {
                owner.x = -1;
                owner.y = 0;
            }
            if (owner.centerPosition.x - owner.transform.position.x > owner.wanderRange)
            {
                owner.x = 1;
                owner.y = 0;
            }
            if (owner.transform.position.y - owner.centerPosition.y > owner.wanderRange)
            {
                owner.x = 0;
                owner.y = -1;
            }
            if (owner.centerPosition.y - owner.transform.position.y > owner.wanderRange)
            {
                owner.x = 0;
                owner.y = 1;
            }
            distance = new Vector3(owner.x, owner.y);
            owner.animator.speed = 1f;
            owner.animator.SetFloat(owner.eX, owner.x);
            owner.animator.SetFloat(owner.eY, owner.y);
            owner.StartCoroutine("Wander");
        }
        public override void Execute()
        {
            if (owner.AreaJudge(owner.pursuitLevel) != 0)
            {
                owner.ChangeState(WingSnakeEnemyState.Pursuit);
            }
            owner.transform.position += distance * owner.speed * Time.deltaTime;
        }
        public override void Exit()
        {
            owner.StopCoroutine("Wander");
        }
    }

    //StatePursuit
    private class StatePursuit : State<WingSnakeEnemy>
    {
        private int buf;
        public StatePursuit(WingSnakeEnemy owner) : base(owner) { }
        public override void Enter()
        {
            owner.animator.speed = 1f;
        }
        public override void Execute()
        {
            if(owner.isLeave == false)
            {
                if (owner.AreaJudge(owner.leaveLevel) != 0)
                {
                    owner.ChangeState(WingSnakeEnemyState.Leave);
                }
            }
            buf = owner.direction;
            owner.direction = owner.AreaJudge(owner.pursuitLevel);
            if (owner.direction == 0)
            {
                owner.direction = buf;
                owner.ChangeState(WingSnakeEnemyState.Wait);
            }
            else
            {
                switch (owner.direction)
                {
                    case 1:
                        owner.x = 1;
                        owner.y = 0;
                        break;
                    case 2:
                        owner.x = -1;
                        owner.y = 0;
                        break;
                    case 3:
                        owner.x = 0;
                        owner.y = -1;
                        break;
                    case 4:
                        owner.x = 0;
                        owner.y = 1;
                        break;
                    default:
                        owner.x = 0;
                        owner.y = 0;
                        owner.animator.speed = 0.0f;
                        owner.ChangeState(WingSnakeEnemyState.Wait);
                        break;
                }
                owner.animator.SetFloat(owner.eX, owner.x);
                owner.animator.SetFloat(owner.eY, owner.y);
                owner.transform.position += new Vector3(owner.x, owner.y, 0) * owner.pursuitSpeed * Time.deltaTime;
                if (owner.AreaJudge(owner.attackLevel) != 0)
                {
                    owner.ChangeState(WingSnakeEnemyState.Attack);
                }
            }
        }
        public override void Exit() { }
    }

    //StateDestroy
    private class StateDestroy : State<WingSnakeEnemy>
    {
        public StateDestroy(WingSnakeEnemy owner) : base(owner) { }
        public override void Enter()
        {
            Destroy(owner);
        }
        public override void Execute() { }
        public override void Exit() { }
    }

    //StateAttack
    private class StateAttack : State<WingSnakeEnemy>
    {
        private int buf;
        public StateAttack(WingSnakeEnemy owner) : base(owner) { }
        public override void Enter() { }
        public override void Execute()
        {
            buf = owner.direction;
            owner.direction = owner.AreaJudge(owner.attackLevel);
            if (owner.direction == 0)
            {
                owner.direction = buf;
                owner.ChangeState(WingSnakeEnemyState.Pursuit);
            }
            else
            {
                switch (owner.direction)
                {
                    case 1:
                        owner.x = 1;
                        owner.y = 0;
                        break;
                    case 2:
                        owner.x = -1;
                        owner.y = 0;
                        break;
                    case 3:
                        owner.x = 0;
                        owner.y = -1;
                        break;
                    case 4:
                        owner.x = 0;
                        owner.y = 1;
                        break;
                    default:
                        owner.x = 0;
                        owner.y = 0;
                        owner.animator.speed = 0.0f;
                        owner.ChangeState(WingSnakeEnemyState.Wait);
                        break;
                }
                owner.animator.SetFloat(owner.eX, owner.x);
                owner.animator.SetFloat(owner.eY, owner.y);
                if (!owner.isAttack)
                {
                    if (owner.direction == 4)
                    {
                        owner.StartCoroutine("SetEnemyAttackEffect", 0);
                    }
                    else if (owner.direction == 3)
                    {
                        owner.StartCoroutine("SetEnemyAttackEffect", 1);
                    }
                    else if (owner.direction == 2)
                    {
                        owner.StartCoroutine("SetEnemyAttackEffect", 2);
                    }
                    else if (owner.direction == 1)
                    {
                        owner.StartCoroutine("SetEnemyAttackEffect", 3);
                    }
                }
            }
            owner.animator.speed = 0f;
        }
        public override void Exit() { }
    }

    //StateLeave
    private class StateLeave : State<WingSnakeEnemy>
    {
        private int x, y, i;
        public StateLeave(WingSnakeEnemy owner) : base(owner) { }
        public override void Enter()
        {
            x = (owner.direction % 3) - 1;
            x = -x;
            y = owner.direction / 3;
            y = (y % 3) - 1;
            y = -y;
            i = 0;
        }
        public override void Execute()
        {
            owner.transform.position += new Vector3(5*x, 5*y) * owner.leaveSpeed * Time.deltaTime;
            i++;
            if (i >= 50)
            {
                owner.ChangeState(WingSnakeEnemyState.Pursuit);
            }
        }
        public override void Exit()
        {
            owner.isLeave = true;
        }
    }


    private IEnumerator Wait()
    {
        float second;
        second = Random.Range(1, 4);
        yield return new WaitForSeconds(second);
        ChangeState(WingSnakeEnemyState.Wander);
    }

    private IEnumerator Wander()
    {
        float second;
        second = Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(second);
        ChangeState(WingSnakeEnemyState.Wait);
    }

    private IEnumerator SetEnemyAttackEffect(int num)
    {
        isAttack = true;
        AttackEffect[num].transform.parent = null;
        AttackEffect[num].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        AttackEffect[num].SetActive(false);
        AttackEffect[num].transform.parent = transform;
        AttackEffect[num].transform.localPosition = AttackEffectLocalPosition[num];
        yield return new WaitForSeconds(1.2f);
        isAttack = false;
    }
    #endregion
}
