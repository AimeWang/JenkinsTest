using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyMesh : MonoBehaviour
{
    // Mesh : 网格, 由三角形面组成
    // Mesh Filter : 设置网格信息
    // Mesh Render : 渲染显示网格
    [SerializeField] Material shadowMaterial;
    //网格信息
    SkinnedMeshRenderer[] skinMeshs;
    void Start()
    {
        skinMeshs = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowFollowShadows(2, 0.3f, 5);
        }
        transform.Translate(0, 0, 0.1f);
    }
    // -----
    //展示多个残影
    public void ShowFollowShadows(float duration, float spaceTime, int count)
    {
        if (skinMeshs != null && duration > 0 && spaceTime > 0 && count > 0)
            StartCoroutine(ShowShadows(duration, spaceTime, count));
    }
    //展示一个残影
    public void ShowFollowShadow(float duration)
    {
        if (skinMeshs != null && duration > 0)
            StartCoroutine(ShowShadow(duration));
    }
    //按照时间间隔依次展示残影
    IEnumerator ShowShadows(float duration, float spaceTime, int count)
    {
        int num = 0;
        while (num < count)
        {
            StartCoroutine(ShowShadow(duration));
            num++;
            yield return new WaitForSeconds(spaceTime);
        }
    }
    //展示并在一段时间后销毁残影
    IEnumerator ShowShadow(float duration)
    {
        //展示
        //将要显示残影的物体(多个, 与网格一一对应)
        Transform shadowObjs = new GameObject("ShadowObjs").transform;
        //遍历
        for (int i = 0; i < skinMeshs.Length; i++)
        {
            //获取当前的网格数据
            Mesh newMesh = new Mesh();
            skinMeshs[i].BakeMesh(newMesh);
            //创建残影物体
            GameObject newObj = GetEmptyShadowObj();
            //网格赋值到新物体, 显示残影网格
            MeshFilter objMesh = newObj.GetComponent<MeshFilter>();
            objMesh.mesh = newMesh;
            //位置旋转缩放
            newObj.transform.position = skinMeshs[i].transform.position;
            newObj.transform.rotation = skinMeshs[i].transform.rotation;
            newObj.transform.localScale = skinMeshs[i].transform.localScale;
            //记录物体
            newObj.transform.SetParent(shadowObjs);
        }
        //设置透明度
        MeshRenderer[] meshes = shadowObjs.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            //shader中的 "_Color" 值
            Color color = meshes[i].material.GetColor("_Color");
            color.a = 1;
            meshes[i].material.SetColor("_Color", color);
        }
        //等待一段时间
        yield return new WaitForSeconds(duration);
        //销毁
        //渐变透明度
        float alfa = 1;
        while (alfa > 0)
        {
            alfa -= 0.02f;
            for (int i = 0; i < meshes.Length; i++)
            {
                Color color = meshes[i].material.GetColor("_Color");
                color.a = alfa;
                meshes[i].material.SetColor("_Color", color);
            }
            yield return new WaitForSeconds(0.02f);
        }
        //隐藏, 回收
        for (int i = 0; i < shadowObjs.childCount; i++)
        {
            GameObject child = shadowObjs.GetChild(i).gameObject;
            child.SetActive(false);
            shadowObjPool.Push(child);
        }
        //销毁父节点
        shadowObjs.DetachChildren();
        Destroy(shadowObjs.gameObject);
    }
    Stack<GameObject> shadowObjPool = new Stack<GameObject>();
    GameObject GetEmptyShadowObj()
    {
        GameObject newObj;
        if (shadowObjPool.Count > 0)
        {
            newObj = shadowObjPool.Pop();
            newObj.SetActive(true);
        }
        else
        {
            newObj = new GameObject("ShadowObj");
            newObj.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = newObj.AddComponent<MeshRenderer>();
            meshRenderer.material = shadowMaterial;
        }
        return newObj;
    }
}
