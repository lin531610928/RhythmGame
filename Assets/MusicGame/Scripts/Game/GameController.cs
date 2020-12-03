using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class GameController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<GameObject> gameObjects;
    // Start is called before the first frame update

    void Start()
    {
        print(gameObjects);
        gameObjects
            .ObserveEveryValueChanged(e => e.Select(x => x.transform.localPosition).ToArray())
            .Subscribe(vectors =>
            {
                foreach (Vector3 i in vectors)
                {
                    print(i.x + "," + i.y + "," + i.z);
                }
                lineRenderer.positionCount = vectors.Count();
                lineRenderer.SetPositions(vectors);
                //lineRenderer.SetPositions(vectors);
            });
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// 生成游戏路径
    /// </summary>
    private void PathCreator()
    {

    }
}
