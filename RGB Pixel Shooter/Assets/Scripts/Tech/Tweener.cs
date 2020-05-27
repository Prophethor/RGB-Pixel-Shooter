// Use for effects such as a fade

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate T Getter<T> ();
public delegate void Setter<T> (T val);
public delegate T TweenMethod<T> (T val);
public delegate void Callback ();

public enum TweenMethodEnum { Linear, Quadratic, Ease, SoftEase, SoftLog, HardLog }

public static class TweenMethods {
    public static TweenMethod<float> Linear {
        get {
            return (x) => (x);
        }
    }
    public static TweenMethod<float> Quadratic {
        get {
            return (x) => (x * x);
        }
    }
    public static TweenMethod<float> Jump {
        get {
            return (x) => (1.1f * x / (x + 0.1f));
        }
    }
    public static TweenMethod<float> Kick {
        get {
            return (x) => (x * x * x * x * x * x * x * x);
        }
    }
    public static TweenMethod<float> Ease {
        get {
            return (x) => ((x * x) / (x * x + (1 - x) * (1 - x)));
        }
    }
    public static TweenMethod<float> SoftEase {
        get {
            return (x) => (Mathf.Pow(x, 1.5f) / (Mathf.Pow(x, 1.5f) + Mathf.Pow(1f - x, 1.5f)));
        }
    }
    public static TweenMethod<float> SoftLog {
        get {
            return (x) => (Mathf.Log(15 * x + 1, 2f) / 4);
        }
    }
    public static TweenMethod<float> HardLog {
        get {
            return (x) => (Mathf.Log(255 * x + 1, 2f) / 8);
        }
    }

    public static TweenMethod<float> GetMethod (TweenMethodEnum method) {
        TweenMethod<float> tweenMethod;

        switch (method) {
            case (TweenMethodEnum.Linear):
                tweenMethod = Linear;
                break;
            case (TweenMethodEnum.Quadratic):
                tweenMethod = Quadratic;
                break;
            case (TweenMethodEnum.Ease):
                tweenMethod = Ease;
                break;
            case (TweenMethodEnum.SoftEase):
                tweenMethod = SoftEase;
                break;
            case (TweenMethodEnum.SoftLog):
                tweenMethod = SoftLog;
                break;
            case (TweenMethodEnum.HardLog):
                tweenMethod = HardLog;
                break;
            default:
                tweenMethod = Linear;
                break;
        }

        return tweenMethod;
    }
}

public class Tween {

    private Getter<float> getter;
    private Setter<float> setter;
    private TweenMethod<float> tweenMethod;
    private Callback callback = (() => { });

    private float startValue;
    private float targetValue;
    private float elapsedTime = 0f;
    private float tweenDuration;

    public bool active = true;
    public bool useUnscaledTime = false;

    private float Current {
        get {
            return getter();
        }
        set {
            setter(value);
        }
    }

    public Tween (Getter<float> valueGetter, Setter<float> valueSetter, float targetValue, float tweenTime) {
        getter = valueGetter;
        setter = valueSetter;

        startValue = Current;
        this.targetValue = targetValue;

        tweenDuration = tweenTime;
        tweenMethod = TweenMethods.Linear;
    }

    public Tween (Getter<float> valueGetter, Setter<float> valueSetter, float targetValue, float tweenTime, TweenMethod<float> method) {
        getter = valueGetter;
        setter = valueSetter;

        startValue = Current;
        this.targetValue = targetValue;

        tweenDuration = tweenTime;
        tweenMethod = method;
    }

    public void Update (float deltaTime) {
        elapsedTime += deltaTime;
        if (elapsedTime >= tweenDuration) {
            Finish();
            return;
        }
        Current = startValue + tweenMethod(Mathf.Min(elapsedTime, tweenDuration) / tweenDuration) * (targetValue - startValue);

    }

    public void Finish () {
        Current = targetValue;
        active = false;
        callback();
    }

    public void SetCallback (Callback newCallback) {
        callback = newCallback;
    }

    public void ExecuteCallback () {
        callback();
    }
}

public class Tweener : MonoBehaviour {

    private static Tweener instance;

    public static Tweener getInstance () {
        return instance;
    }

    private List<Tween> activeTweens = new List<Tween>();

    private void Start () {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }

        instance = this;
    }

    private void Update () {
        int i = 0;
        while (i < activeTweens.Count) {
            if (activeTweens[i].useUnscaledTime) {
                activeTweens[i].Update(Time.unscaledDeltaTime);
            }
            else {
                activeTweens[i].Update(Time.deltaTime);
            }

            if (!activeTweens[i].active) {
                activeTweens.RemoveAt(i);
            }
            else {
                i++;
            }
        }
    }

    public static Tween AddTween (Getter<float> valueGetter, Setter<float> valueSetter, float targetValue, float tweenTime, bool useUnscaledTime = false) {
        return AddTween(valueGetter, valueSetter, targetValue, tweenTime, TweenMethods.Quadratic, () => { }, useUnscaledTime);
    }

    public static Tween AddTween (Getter<float> valueGetter, Setter<float> valueSetter, float targetValue, float tweenTime, TweenMethod<float> method, bool useUnscaledTime = false) {
        return AddTween(valueGetter, valueSetter, targetValue, tweenTime, method, () => { }, useUnscaledTime);
    }

    public static Tween AddTween (Getter<float> valueGetter, Setter<float> valueSetter, float targetValue, float tweenTime, Callback callback, bool useUnscaledTime = false) {
        return AddTween(valueGetter, valueSetter, targetValue, tweenTime, TweenMethods.Quadratic, callback, useUnscaledTime);
    }

    public static Tween AddTween (Getter<float> valueGetter, Setter<float> valueSetter, float targetValue, float tweenTime, TweenMethod<float> method, Callback callback, bool useUnscaledTime = false) {
        if (instance == null) {
            GameObject gameObj = new GameObject("Tweener");
            instance = gameObj.AddComponent<Tweener>();
        }

        Tween tween = new Tween(valueGetter, valueSetter, targetValue, tweenTime, method);
        tween.useUnscaledTime = useUnscaledTime;
        if (instance != null && tween != null) {
            tween.SetCallback(callback);
            instance.activeTweens.Add(tween);
        }
        else {
            Debug.LogError("Tweener: Something is missing.");
        }

        //Debug.Log ("Active tweens: " + instance.activeTweens.Count);

        return tween;
    }

    // Safe method invocation
    public static Tween Invoke (float delay, Callback callback, bool useUnscaledTime = false) {
        float t = 0f;
        return AddTween(() => t, (x) => t = x, 1f, delay, callback, useUnscaledTime);
    }

    public static void RemoveTween (Tween tween) {
        if (getInstance().activeTweens.Contains(tween)) {
            tween.active = false;
        }
    }
}