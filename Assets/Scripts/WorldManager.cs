using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    List<GameObject> sortedByX = new List<GameObject>();
    XPosComparer xPosComparer = new XPosComparer();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sortedByX.Sort(xPosComparer);

    }


    class XPosComparer : IComparer<GameObject>
    {
        public int Compare(GameObject a, GameObject b)
        {
            var aPos = a.transform.localPosition;
            var bPos = b.transform.localPosition;
            if (aPos.x < bPos.x)
            {
                return -1;
            }
            else if (aPos.x < bPos.x)
            {
                return +1;
            }
            return 0;
        }
    }
}
