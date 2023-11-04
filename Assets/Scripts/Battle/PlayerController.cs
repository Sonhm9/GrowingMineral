using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private MonsterSettings monsterSettings;
    [HideInInspector]
    public int targerPosition = 0;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        BattleModeStart();
    }
    private void AttackTarget()
    {
        GameObject monster = StageManager.instance.monsterGeneratePosition[targerPosition].GetChild(1).GetChild(targerPosition).gameObject;
        MonsterSettings targetInfo = monster.GetComponentInChildren<MonsterSettings>();
        if (targetInfo != null)
        {
            targetInfo.TakeDamage(PlayerStatManager.instance.playerPower);
        }
    }
    public void BattleModeStart()
    {
        StartCoroutine("PerformAttack");
    }
    public void BattleModeEnd()
    {
        StopCoroutine("PerformAttack");
    }
    IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (targerPosition > 4)
            {
                targerPosition = 0;
                yield return new WaitForSeconds(0.5f);
                StageManager.instance.SetStage();
                yield return new WaitForSeconds(2f);
            }
            // ���� �ִϸ��̼� ���
            animator.SetTrigger("AttackState");
            yield return new WaitForSeconds(0.01f);
            animator.SetTrigger("IdleState");

            // ���Ϳ��� ������ ����
            AttackTarget();


            yield return new WaitForSeconds(PlayerStatManager.instance.playerCoolDown); // ���� �ӵ��� ���� ���

        }

    }
}
