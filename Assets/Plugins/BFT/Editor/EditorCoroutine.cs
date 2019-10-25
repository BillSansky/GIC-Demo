using System.Collections;
using UnityEditor;

public class EditorCoroutine
{
    readonly IEnumerator routine;

    EditorCoroutine(IEnumerator routine)
    {
        this.routine = routine;
    }

    public static EditorCoroutine Start(IEnumerator routine)
    {
        EditorCoroutine coroutine = new EditorCoroutine(routine);
        coroutine.Start();
        return coroutine;
    }

    void Start()
    {
        //Debug.Log("start");
        EditorApplication.update += Update;
    }

    public void Stop()
    {
        //Debug.Log("stop");
        EditorApplication.update -= Update;
    }

    void Update()
    {
        /* NOTE: no need to try/catch MoveNext,
         * if an IEnumerator throws its next iteration returns false.
         * Also, Unity probably catches when calling EditorApplication.update.
         */

        //Debug.Log("update");
        if (!routine.MoveNext())
        {
            Stop();
        }
    }
}