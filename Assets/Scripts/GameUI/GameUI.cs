using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider EnemyHPBar; // �Ϲ� �� ü�� ��
    [SerializeField] UnityEngine.UI.Slider BossHPBar; // ���� ü�� ��
    [SerializeField] UnityEngine.UI.Slider TimerBar; // Ÿ�̸� ��
    [SerializeField] UnityEngine.UI.Button BossTryButton; // ���� �õ� ��ư
    [SerializeField] GameObject StoneUpgradeUI; // ���� ��ư
    [SerializeField] GameObject LevelUpgradeUI; // ��ȭ ��ư

    public static float time = 30; // ���� ���� �ð� 30��
    bool TimerStart = false;

    // Start is called before the first frame update
    void Awake()
    {
        NormalEnemyHunting();
        LevelUpgrade();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerStart == true)
            Timer();
    }

    public void NormalEnemyHunting()
    {
        BossTryButton.gameObject.SetActive(true);
        EnemyHPBar.gameObject.SetActive(true);
        BossHPBar.gameObject.SetActive(false);
        TimerBar.gameObject.SetActive(false);
    }

    public void BossTry()
    {
        BossTryButton.gameObject.SetActive(false);
        EnemyHPBar.gameObject.SetActive(false);
        BossHPBar.gameObject.SetActive(true);
        TimerBar.gameObject.SetActive(true);
        TimerStart = true;
    }

    public void Timer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            TimerBar.value = time;
        }
    }
    public void StoneUpgrade()
    {
        LevelUpgradeUI.SetActive(false);
        StoneUpgradeUI.SetActive(true);
    }
    public void LevelUpgrade()
    {
        StoneUpgradeUI.SetActive(false);
        LevelUpgradeUI.SetActive(true);
    }
}
