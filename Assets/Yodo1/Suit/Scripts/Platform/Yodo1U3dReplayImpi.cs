using UnityEngine;

public class Yodo1U3dReplayImpi
{
    public virtual void Initialize(Yodo1U3dReplay.Yodo1ReplayConfig replayConfig, string gameObjectName, string callbackName) { }

    public virtual bool IsSupport() { return false; }

    public virtual bool IsRecording() { return false; }

    public virtual void SetType(Yodo1U3dReplay.Yodo1ReplayType replayType) { }

    public virtual void StartRecord() { }

    public virtual void StopRecord() { }

    public virtual void ShowRecorder() { }
}
