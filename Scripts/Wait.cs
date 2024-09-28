using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wait
{
    public static IEnumerator waitForSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Debug.Log("");
    }
}
