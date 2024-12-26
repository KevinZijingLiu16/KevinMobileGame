using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardDecorator : Decorator
{
    public enum RunCondition
    { 
      KeyExists,
        KeyNotExists
    }

    public enum NotifyRule
    {
        RunConditionChange,
        KeyValueChange
    }

    public enum NotifyAbort
    { 
       none,
       self,
       lower,
       both
    
     }

    BehaviourTree tree;
    string key;
    object value;

    RunCondition runCondition;
    NotifyRule notifyRule;
    NotifyAbort notifyAbort;




    public BlackboardDecorator(BehaviourTree tree, 
        BTNode child, 
        string key, 
        RunCondition runCondition, 
        NotifyRule notifyRule, 
        NotifyAbort notifyAbort) : base(child)
    {
        this.tree = tree;
        this.key = key;
        this.runCondition = runCondition;
        this.notifyRule = notifyRule;
        this.notifyAbort = notifyAbort;

    }

    protected override NodeResult Execute()
    {
        Blackboard blackboard = tree.Blackboard;
        if (blackboard == null)
        {
            return NodeResult.Failure;
        }

       blackboard.onBlackboardValueChanged -= CheckNotify;
        blackboard.onBlackboardValueChanged += CheckNotify;

        if (CheckRunCondition())
        {
            return NodeResult.InProgress;
        }

        else
        {
            return NodeResult.Failure;


        }
    }

    private bool CheckRunCondition()
    {
        bool exists = tree.Blackboard.GetBlackBoardData(key, out value);

        switch(runCondition)
        {
            case RunCondition.KeyExists:
                return exists;
            case RunCondition.KeyNotExists:
                return !exists;
          
        }

        return false;
    }

    private void CheckNotify(string key, object val)
    {
        if(this.key != key)
        {
            return;
        }

        if(notifyRule == NotifyRule.RunConditionChange)
        {
            bool prevExists = value != null;
            bool currentExists = val != null;

            if (prevExists != currentExists)
            {

                Notify();
            
            }
             
            else if(notifyRule == NotifyRule.KeyValueChange)
            {
               if(value != val)
                {
                    Notify();
                }
            }
        }

       
    }

    private void Notify()
    {
        switch(notifyAbort)
        {
            case NotifyAbort.none:
                break;
            case NotifyAbort.self:
              AbortSelf();
                break;
            case NotifyAbort.lower:
               AbortLower();
                break;
            case NotifyAbort.both:
               AbortBoth();
                
                break;
        }
    }

    

    private void AbortSelf()
    {
        Abort();
    }

    private void AbortLower()
    {
       
    }

    private void AbortBoth()
    {
       Abort();
        AbortLower();
    }

    protected override NodeResult Update()
    {
        return GetChild().UpdateNode();
    }

    protected override void End()
    {
       
        GetChild().Abort();
        base.End();
    }

    
}
