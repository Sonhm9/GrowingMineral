using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    private static object _lock = new object();
    private static PlayerStatManager _instance = null;
    public static PlayerStatManager instance
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
                    GameObject obj = new GameObject("PlayerStatManager ");
                    obj.AddComponent<PlayerStatManager>();
                    _instance = obj.GetComponent<PlayerStatManager>();
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

    private int playerLevel { get; set; }
    private float playerPower { get; set; }
    private float playerCoolDown { get; set; }

    public void AddLevel(int amount)
    {
        playerLevel += amount;
        AddPower(amount);
    }

    public void AddPower(float amount)
    {
        playerPower += amount/100;
    }

    public void AddCoolDown(float amount)
    {
        playerCoolDown -= amount;
    }
}
