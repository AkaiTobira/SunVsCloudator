using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAchivement : MonoBehaviour
{
    
    [SerializeField] private GameObject[] previews = null;
    [SerializeField] private GameObject age_name = null;
    [SerializeField] private GameObject requirement = null;

    [SerializeField] private GameObject requirementDetails = null;
    [SerializeField] private GameObject requirementProgress = null;
    [SerializeField] private GameObject requirementMax = null;
    [SerializeField] private GameObject commingSoon = null;
    

    [SerializeField] private string[]  age_names = null;

    [SerializeField] private float[]  requirements_targets = null;
    [SerializeField] private string[] requirements_description = null;
    [HideInInspector] public int backgorund_index;

    void Start()
    {
        backgorund_index = PlayerPrefs.GetInt("Background");
        updateWindow();
        clearRequirements();
    }

    private void updateWindow(){
        for( int i = 0; i < previews.Length; i++){
                previews[i].SetActive(( i == backgorund_index ));
        }
        age_name.GetComponent<Text>().text = age_names[backgorund_index];
        checkCommingSoon();
        fillRequirements();
    }

    private void checkCommingSoon(){
        commingSoon.SetActive(requirements_description[backgorund_index] == "Comming Soon");
    }

    private void clearRequirements(){
        requirement.SetActive(false);
    }

    private void fillRequirements(){
        clearRequirements();
        if( check_requirements() ) return;
        requirementDetails.GetComponent<Text>().text  = requirements_description[backgorund_index];
        requirementMax.GetComponent<Text>().text      = ((int)requirements_targets[backgorund_index]).ToString();
        requirementProgress.GetComponent<Text>().text = AchievmentMeasures.get_requirement_status("unlockBG" + (backgorund_index+1).ToString()) + " /";
        requirement.SetActive(true);
        return;
    }
    private bool check_requirements(){
        if( requirements_targets[backgorund_index] == 0.0f ) return true;
        return AchievmentMeasures.is_requirement_meet( "unlockBG" + (backgorund_index+1).ToString(), requirements_targets[backgorund_index] );
    }

    public void IncreaseIndex(){
        backgorund_index += 1;
        if( backgorund_index >= age_names.Length){ backgorund_index = 0;}
        updateWindow();
    }

    public void DecreaseIndex(){
        backgorund_index -= 1;
        if( backgorund_index < 0){ backgorund_index = age_names.Length-1;}
        updateWindow();
    }

    public bool is_current_backgorund_valid(){
        if(requirements_description[backgorund_index] == "Comming Soon") return false;
        if( check_requirements() ) return true;
        return false;
    }

}
