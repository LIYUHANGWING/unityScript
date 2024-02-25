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
        // 检测是否按下 ESC 键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 调用退出应用程序的方法
            QuitGame();
        }
    }

    // 退出应用程序的方法
    void QuitGame()
    {
        // 在编辑器中运行时，该语句不会生效，只有在构建后的可执行文件中才会起作用
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
          #else
        Application.Quit();
         #endif
    }

}
