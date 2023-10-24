using InfiniteValue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceManager : MonoBehaviour
{
    private static object _lock = new object();
    private static EnhanceManager _instance = null;
    public static EnhanceManager instance
    {
        get
        {
            if (applicationQuitting)
            {
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject("EnhanceManager ");
                    obj.AddComponent<EnhanceManager>();
                    _instance = obj.GetComponent<EnhanceManager>();
                }
                return _instance;
            }
        }
        set
        {
            _instance = value;
        }
    }
    private static bool applicationQuitting = false;
    // �̱���

    private void Awake()
    {
        _instance = this;
        // �̱��� �ν��Ͻ�
    }

    private void OnDestroy()
    {
        applicationQuitting = true;
        // �ν��Ͻ� ����
    }
    public InfVal powerResourceAmount = 100; //���ҽ�ų �ڿ��� �� (�Ŀ�)
    public InfVal cooldownResourceAmount = 100; //���ҽ�ų �ڿ��� �� (���ݼӵ�)
    public int upgradeCount = 1; //���׷��̵�Ƚ��  
    public void EnhancePower()
    {
        //���ݷ� ���� �Լ�
        ResourceManager.instance.CheckResourceAmount(ResourceManager.ResourceType.Stone, powerResourceAmount);
        if (ResourceManager.instance.consumeAble == true)
        {
            ResourceManager.instance.RemoveResource(ResourceManager.ResourceType.Stone, powerResourceAmount);
            PlayerStatManager.instance.AddPower(upgradeCount);
            ResourceIncrease(powerResourceAmount);
        }
    }
    public void Enhancecooldown()
    {
        //���ݼӵ� ���� �Լ�
        ResourceManager.instance.CheckResourceAmount(ResourceManager.ResourceType.Stone, cooldownResourceAmount);
        if(ResourceManager.instance.consumeAble == true)
        {
            ResourceManager.instance.RemoveResource(ResourceManager.ResourceType.Stone, cooldownResourceAmount);
            PlayerStatManager.instance.AddCoolDown(upgradeCount);
            ResourceIncrease(cooldownResourceAmount);
        }
    }
    public void ResourceIncrease(InfVal resourceAmount)
    {
        resourceAmount = resourceAmount + (resourceAmount*0.1*upgradeCount);
    }
    public void SetUpgradeCount1()
    {
        //���׷��̵�Ƚ���� 1�� ����
        upgradeCount = 1;
    }
    public void SetUpgradeCount10() 
    {
        //���׷��̵�Ƚ���� 10���� ����
        upgradeCount = 10;
    }
    public void SetUpgradeCount100()     
    {
        //���׷��̵�Ƚ���� 100���� ����
        upgradeCount = 100;
    }

}
