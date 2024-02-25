using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyDied;

    public static void EnemyDied(Enemy enemy)
    {
        OnEnemyDied?.Invoke(enemy);
    }

    void Update()
    {
        // ����Ƿ��� ESC ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �����˳�Ӧ�ó���ķ���
            QuitGame();
        }
    }

    // �˳�Ӧ�ó���ķ���
    void QuitGame()
    {
        // �ڱ༭��������ʱ������䲻����Ч��ֻ���ڹ�����Ŀ�ִ���ļ��вŻ�������
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
          #else
        Application.Quit();
         #endif
    }

}
