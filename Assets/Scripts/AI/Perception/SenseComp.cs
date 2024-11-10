using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class SenseComp : MonoBehaviour
{
    static List<PerceptionStimuli> registeredStimulis = new List<PerceptionStimuli>();
    List<PerceptionStimuli> PerceivableStimuli = new List<PerceptionStimuli>();
    [SerializeField] float forgettingTime = 3f; 

    Dictionary<PerceptionStimuli, Coroutine> ForgettingRotines = new Dictionary<PerceptionStimuli, Coroutine>();

    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool successfulySensed);
    public event OnPerceptionUpdated onPerceptionUpdated;

    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
      if(registeredStimulis.Contains(stimuli))
        
            return;
        

        registeredStimulis.Add(stimuli);

    }

    static public void UnRigisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var stimuli in registeredStimulis)
        {
            if (IsStimuliSensable(stimuli))
            {
                if(!PerceivableStimuli.Contains(stimuli))
                {
                    PerceivableStimuli.Add(stimuli);
                    if (ForgettingRotines.TryGetValue(stimuli, out Coroutine routine))
                    {
                        StopCoroutine(routine);
                        ForgettingRotines.Remove(stimuli);
                    }
                    else 
                    {
                        onPerceptionUpdated?.Invoke(stimuli, true);
                        //Debug.Log($"I just sensed {stimuli.gameObject}");
                    }
                  
                }

            }
            else
            {
                if (PerceivableStimuli.Contains(stimuli))
                {
                    PerceivableStimuli.Remove(stimuli);
                    //Debug.Log($"I just lost sense of {stimuli.gameObject}");

                    ForgettingRotines.Add (stimuli, StartCoroutine(ForgetStimuli(stimuli)));
                }
            }
        }
    }

    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgettingTime);
        ForgettingRotines.Remove(stimuli);
        onPerceptionUpdated?.Invoke(stimuli, false);
       // Debug.Log($"I just forgot about {stimuli.gameObject}");
    }

    protected virtual void DrawDebug()
    {

    }

    private void OnDrawGizmos()
    {
       DrawDebug();
    }
}
