/**
 * @file NyDebug.cs
 * @brief 実機でデバッグログを描画するための機能
 * @author にゃくお
 */
using UnityEngine;
using System.Collections;



/**
 * @class NyDebug
 * @brief 実機デバッグログ出力クラス
 */
public class NyDebug : MonoBehaviour {
    static private Queue logQueue;      // ログを格納するキュー
    static private uint iNumLog = 20;   // 格納するログの数



	/**
     * @brief 初期化関数：変数の初期化
     * @note 他の機能を使う前に1度だけ呼び出す
     */

    static public void Init()
    {
        logQueue = new Queue();
        iNumLog = 20;
    }



    /**
     * @brief ログのプッシュ(エンキュー)
     * @param str プッシュするログ
     * @param console trueならばUnityのコンソール上にも表示する
     */

    static public void PushLog(string str, bool console = true){
        if (logQueue.Count >= iNumLog) logQueue.Dequeue();

        logQueue.Enqueue(str);
        if(console) Debug.Log(str);
    }



    /**
     * @brief ログを描画する
     * @param rect 描画開始位置、および1行のサイズ
     * @param color 文字の色
     * @note Debug.Logより実行速度が早いので、細かい値の変化が見ることができる
     */
    static public void RenderLog(Rect rect, Color color)
    {
        Rect curRect = rect;
        curRect.y += rect.height * (logQueue.Count-1);
        Color prevColor = GUI.color;
        GUI.color = color;

        System.Collections.IEnumerator ienum = logQueue.GetEnumerator();
        while (ienum.MoveNext())
        {
            GUI.Label(curRect, (string)ienum.Current);
            curRect.y -= rect.height;
        }

        GUI.color = prevColor;
    }
}