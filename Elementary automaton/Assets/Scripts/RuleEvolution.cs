using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleEvolution
{
    private int _rule;

    public RuleEvolution(int rule)
    {
        _rule = rule;
    }
    
    private Dictionary<int, char> _rules = new(8)
    {
        [7] = '0',
        [6] = '0',
        [5] = '0',
        [4] = '0',
        [3] = '0',
        [2] = '0',
        [1] = '0',
        [0] = '0',
    };

    private string RuleToBinary(int ruleValue)
    {
        return Convert.ToString(ruleValue, 2);
    }

    private void UpdateDictWithNewRule(string ruleBinary)
    {
        for (int i = 0; i < ruleBinary.Length; i++)
        {
            _rules[i] = ruleBinary[ruleBinary.Length - i - 1];
        }
    }

    public void CreateRule()
    {
        string ruleBinary = RuleToBinary(_rule);
        UpdateDictWithNewRule(ruleBinary);
    }

    public string[] DoEvolution(string[] statePoints)
    {
        string[] newStatePoints = new string[statePoints.Length];
        string ruleBase = "";
        int ruleBaseKey = 0;
        for (int i = 0; i < statePoints.Length; i++)
        {
            if (i == 0)
            {
                ruleBase += statePoints[^1];
                ruleBase += statePoints[i];
                ruleBase += statePoints[i + 1];
            }
            
            else if (i == statePoints.Length - 1)
            {
                ruleBase += statePoints[i - 1];
                ruleBase += statePoints[i];
                ruleBase += statePoints[0];
            }

            else
            {
                ruleBase += statePoints[i - 1];
                ruleBase += statePoints[i];
                ruleBase += statePoints[i + 1];
            }

            ruleBaseKey = Convert.ToInt32(ruleBase, 2);

            newStatePoints[i] = Convert.ToString(_rules[ruleBaseKey]);

            ruleBase = "";
        }

        return newStatePoints;
    }
}
