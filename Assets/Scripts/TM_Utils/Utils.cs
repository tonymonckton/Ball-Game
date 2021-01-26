using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TM.Utils {

    class TM_Utils {
        public static GameObject GetGameObject(string tag) {
            GameObject obj = null;
            GameObject[] objects;
            objects = GameObject.FindGameObjectsWithTag(tag);
            if ( objects.Length > 0)
                obj = objects[0];

            return obj;
        }

/*
    t = current time
    b = start value
    c = change in value
    d = duration
*/
        public static float easeInOutQuad(float t, float b, float c, float d) {
            t /= d/2;
            if (t < 1) return c/2*t*t + b;
            t--;
	    return -c/2 * (t*(t-2) - 1) + b;
        }

        public static float InOutQuadBlend(float t)
        {
            if(t <= 0.5f)
                return 2.0f * t * t;
            t -= 0.5f;
            return 2.0f * t * (1.0f - t) + 0.5f;
        }
    }
}