using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *----------------------------------------------------------------------------
 * <T> : C#のジェネリックスという機能
 *       あえてデータ型を決めない(仮にTと置く)ことで様々なデータに対応できる
 * 
 * 今回の場合は<T>に弾のクラス(Bullet)を割り当てる
 *----------------------------------------------------------------------------
 */
public abstract class PoolObj<T> : MonoBehaviour {
    /*
     * 元となるプレハブを代入する変数
     */ 
    private static GameObject mOriginal;
    /*
     * オブジェクトプール用のスタック
     */ 
    private static Stack<T> mObjPool = new Stack<T>();

    /*
     * 元となるプレハブを設定
     */
    public static void SetOriginal(GameObject origin) {
        mOriginal = origin;
    }

    /*
     * オブジェクトプールからオブジェクトを生成
     */ 
    public static T Create() {
        T obj;
        /*
         * オブジェクトプールにオブジェクトが
         *      ある : オブジェクトを取り出し、objに代入
         *      ない : プレハブをオブジェクトプールに登録
         */
        if(mObjPool.Count > 0) {
            obj = Pop();
        } else {
            var go = Instantiate<GameObject>(mOriginal);
            obj = go.GetComponent<T>();
        }
        /*
         * オブジェクトの初期化
         */ 
        (obj as PoolObj<T>).Init();
        return obj;
    }

    private static T Pop() {
        var ret = mObjPool.Pop();
        return ret;
    }

    public static void Pool(T obj) {
        (obj as PoolObj<T>).Sleep();
        mObjPool.Push(obj);
    }

    public static void Clear() {
        mObjPool.Clear();
    }

    /*
     * 初期化時の処理(オブジェクトを生成するとき)
     */
    public abstract void Init();
    /*
     * 休眠時の処理(オブジェクトを一時的に消すとき)
     */
    public abstract void Sleep();
}