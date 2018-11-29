using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HumanEnemyState
{
    Wait,
    Wander,
    Pursuit,
    Destroy,
    Attack,
};

public class HumanEnemy : StatefulObjectBase<HumanEnemy, HumanEnemyState>
{
    private int life;
    private int maxLife = 2;

    //    private float speed = 0.00125f;
    //    private float speed = 0.1f;
    private float speed = 1.6f;
    private Vector3 centerPosition;
    private float wanderRange = 1f * 3f;
    private Animator animator = null;
    private int eX = Animator.StringToHash("x"), eY = Animator.StringToHash("y");
    private float pursuitLevel = 1.5f * 3f;
    //    private float pursuitSpeed = 0.008f;
    private float pursuitSpeed = 1.8f;
    private float attackLevel = 0.6f * 3f;
    private bool isAttack;

    private Transform player;
    private int direction;
    private int x, y;
    [SerializeField]private GameObject AttackEffect;

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
        isAttack = false;

        stateList.Add(new StateWait(this));
        stateList.Add(new StateWander(this));
        stateList.Add(new StatePursuit(this));
        stateList.Add(new StateDestroy(this));
        stateList.Add(new StateAttack(this));

        stateMachine = new StateMachine<HumanEnemy>();

        ChangeState(HumanEnemyState.Wander);
    }

    public void TakeDamage()
    {
        if (life <= 0)
        {
            ChangeState(HumanEnemyState.Destroy);
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
    private class StateWait : State<HumanEnemy>
    {
        public StateWait(HumanEnemy owner) : base(owner) { }
        public override void Enter()
        {
            owner.animator.speed = 0.0f;
            owner.StartCoroutine("Wait");
        }
        public override void Execute()
        {
            if (owner.AreaJudge(owner.pursuitLevel) != 0)
            {
                owner.ChangeState(HumanEnemyState.Pursuit);
            }
        }
        public override void Exit()
        {
            owner.StopCoroutine("Wait");
        }
    }

    //StateWander
    private class StateWander : State<HumanEnemy>
    {
        private Vector3 distance;
        public StateWander(HumanEnemy owner) : base(owner) { }
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
                    owner.ChangeState(HumanEnemyState.Wander);
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
                owner.ChangeState(HumanEnemyState.Pursuit);
            }
            owner.transform.position += distance * owner.speed * Time.deltaTime;
        }
        public override void Exit()
        {
            owner.StopCoroutine("Wander");
        }
    }

    //StatePursuit
    private class StatePursuit : State<HumanEnemy>
    {
        private int buf;
        public StatePursuit(HumanEnemy owner) : base(owner) { }
        public override void Enter()
        {
            owner.animator.speed = 1f;
        }
        public override void Execute()
        {
            buf = owner.direction;
            owner.direction = owner.AreaJudge(owner.pursuitLevel);
            if (owner.direction == 0)
            {
                owner.direction = buf;
                owner.ChangeState(HumanEnemyState.Wait);
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
                        owner.ChangeState(HumanEnemyState.Wait);
                        break;
                }
                owner.animator.SetFloat(owner.eX, owner.x);
                owner.animator.SetFloat(owner.eY, owner.y);
                owner.transform.position += new Vector3(owner.x, owner.y, 0) * owner.pursuitSpeed * Time.deltaTime;
                if (owner.AreaJudge(owner.attackLevel) != 0 && owner.isAttack == false)
                {
                    owner.ChangeState(HumanEnemyState.Attack);
                }
            }
        }
        public override void Exit() { }
    }

    //StateDestroy
    private class StateDestroy : State<HumanEnemy>
    {
        public StateDestroy(HumanEnemy owner) : base(owner) { }
        public override void Enter()
        {
            Destroy(owner);
        }
        public override void Execute() { }
        public override void Exit() { }
    }

    //StateAttack
    private class StateAttack : State<HumanEnemy>
    {
        public StateAttack(HumanEnemy owner) : base(owner) { }
        public override void Enter()
        {
            owner.animator.speed = 0f;
            owner.StartCoroutine("SetEnemyAttackEffect");
            owner.ChangeState(HumanEnemyState.Pursuit);
        }
        public override void Execute() { }
        public override void Exit() { }
    }

    private IEnumerator Wait()
    {
        float second;
        second = Random.Range(1, 4);
        yield return new WaitForSeconds(second);
        ChangeState(HumanEnemyState.Wander);
    }

    private IEnumerator Wander()
    {
        float second;
        second = Random.Range(1f, 2f);
        yield return new WaitForSeconds(second);
        ChangeState(HumanEnemyState.Wait);
    }

    private IEnumerator SetEnemyAttackEffect()
    {
        isAttack = true;

        AttackEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        AttackEffect.transform.rotation = Quaternion.Euler(0, 0, 45f);
        yield return new WaitForSeconds(0.2f);
        AttackEffect.transform.rotation = Quaternion.Euler(0, 0, -45f);
        yield return new WaitForSeconds(0.2f);
        AttackEffect.transform.rotation = Quaternion.Euler(0, 0, 45f);
        yield return new WaitForSeconds(0.2f);
        AttackEffect.transform.rotation = Quaternion.Euler(0, 0, -45f);
        AttackEffect.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
    }
    #endregion
}
