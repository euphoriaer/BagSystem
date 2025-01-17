﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 知识点 1.单例模式 2.委托 3.协程 4.字典
/// </summary>
public class ABManager : SingletnoAutoMono<ABManager>
{
    //AB包管理器 目的是 让外部更方便的进行资源加载
    //主包
    private AssetBundle mainAB = null;

    //依赖包获取用的配置文件
    private AssetBundleManifest mainfest = null;

    //AB包不能重复加载 重读加载会报错
    //用字典来存储 加载过的AB包

    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 存放AB包路径 方便修改
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "IOS"
#elif UNITY_ANDROID
            return "Android"
#else
            return "PC";
#endif
        }
    }/// <summary>

     /// 加载AB包 </summary> <param name="abname"></param>
    private void LoadAB(string abName)
    { //加载AB包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            mainfest = mainAB.LoadAsset<AssetBundleManifest>(nameof(AssetBundleManifest));
        }
        //获取包的依赖信息
        AssetBundle ab = null;
        string[] strs = mainfest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //判断包是否加载过
            if (abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源包
        //如果没有加载过 加载
        if (abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    /// <summary>
    /// 同步加载，不指定类型
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public object LoadRes(string abName, string resName)
    {//加载AB包
        LoadAB(abName);
        //在加载资源时，判断资源是不是Gameobjtct
        //如果是 直接实例化 再返还给外部
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }

        //加载资源
        //return abDic[abName].LoadAsset(resName);
    }

    /// <summary>
    /// 同步加载，根据type 指定类型 lua不支持泛型，因此写重载方法
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public object LoadRes(string abName, string resName, System.Type type)
    {
        LoadAB(abName);
        //在加载资源时，判断资源是不是Gameobjtct
        //如果是 直接实例化 再返还给外部
        Object obj = abDic[abName].LoadAsset(resName, type);
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }

    /// <summary>
    /// 同步加载 根据泛型指定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadRes<T>(string abName, string resName) where T : Object
    {
        LoadAB(abName);
        //在加载资源时，判断资源是不是Gameobjtct
        //如果是 直接实例化 再返还给外部
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }

    //异步加载
    //AB包没有使用异步加载
    //只是从AB包加载资源时，使用异步
    /// <summary>
    /// 根据名字异步加载
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callback">使用delegate或者lamada表达式直接对传出的参数object进行操作</param>
    public void LoadResAsync(string abName, string resName, UnityAction<object> callback)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callback));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<object> callback)
    {//加载AB包
        LoadAB(abName);
        //在加载资源时，判断资源是不是Gameobjtct
        //如果是 直接实例化 再返还给外部
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);

        yield return abr;
        //异步加载结束后 通过委托，传递给外部，外部来使用。 
        if (abr.asset is GameObject)
        {
            callback(Instantiate(abr.asset));
        }
        else
        {
            callback(abr.asset);//将参数传出去
        }
    }

    /// <summary>
    /// 根据Type 异步加载资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<object> callback)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, type, callback));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<object> callback)
    {//加载AB包
        LoadAB(abName);
        //在加载资源时，判断资源是不是Gameobjtct
        //如果是 直接实例化 再返还给外部
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);

        yield return abr;
        //异步加载结束后 通过委托，传递给外部，外部来使用。
        if (abr.asset is GameObject)
        {
            callback(Instantiate(abr.asset));
        }
        else
        {
            callback(abr.asset);//将参数传出去
        }
    }

    /// <summary>
    /// 根据泛型 异步加载资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callback"></param>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callback) where T : Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callback));
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callback)where T:Object
    {//加载AB包
        LoadAB(abName);
        //在加载资源时，判断资源是不是Gameobjtct
        //如果是 直接实例化 再返还给外部
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);

        yield return abr;
        //异步加载结束后 通过委托，传递给外部，外部来使用。
        if (abr.asset is GameObject)
        {
            callback(Instantiate(abr.asset) as T);
        }
        else
        {
            callback(abr.asset as T);//将参数传出去
        }
    }

    //单个包卸载
    public void UnLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    //所有包的卸载
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        mainfest = null;
    }
}