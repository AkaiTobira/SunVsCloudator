using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HolderController : MonoBehaviour
{

    [SerializeField] int containType = 0;
    [SerializeField] int index = 0;

    float MaxSwipe = 100.0f;
    float currentSwipe = 0;
    float increase     = 1;
    float decrease     = -1;

    float basePositionX = 0;

    const float DISTANCE = 275.0f;
    [SerializeField] bool shiftToRight ;


    [SerializeField] float requirementMax = 0;
    [SerializeField] string meansureName = "";
    [SerializeField] string description   = "";
    [SerializeField] string objectName    = "";

    private void SetToActiveValue(){
        if( containType == 0 ){
            if(PlayerPrefs.GetInt("PlayerID") == index){
                transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, index*DISTANCE);
            }
        }else if( containType == 1 ) {
            if(PlayerPrefs.GetInt("Background") == index){
                transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, index*DISTANCE);
            }
        }
    }

    private void SetName(){
        transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = objectName;
    }

    private void DisableRequiremenetField(){
        var requirementsPart = transform.GetChild(0).GetChild(1);
        requirementsPart.GetComponent<Image>().enabled  = false;
        for( int i = 0; i < requirementsPart.childCount; i++){
            requirementsPart.GetChild(i).GetComponent<Text>().enabled  = false;
        }
    }

    private void FillRequirementField(){
        var requirementsPart = transform.GetChild(0).GetChild(1);
        requirementsPart.GetChild(1).GetComponent<Text>().text = description;
        requirementsPart.GetChild(2).GetComponent<Text>().text = AchievmentMeasures.get_requirement_status(meansureName) + " of";
        requirementsPart.GetChild(3).GetComponent<Text>().text = requirementMax.ToString();
    }

    void Awake() {
        basePositionX = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x;
        SetToActiveValue();
        SetName();

        AchievmentMeasures.register_achievment( meansureName, requirementMax );

        if( AchievmentMeasures.is_requirement_meet(meansureName, requirementMax ) ){
            DisableRequiremenetField();
        }else{
            FillRequirementField();
        }
    }

    void shiftToSide( ){
        Vector2 parentPosiiton = transform.parent.GetComponent<RectTransform>().anchoredPosition;
        Vector2 posiiton       = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        Vector2 newPostion     = new Vector2(basePositionX, 0 ); 
        if( shiftToRight ){
            newPostion.x  +=  50.0f-Mathf.Abs(parentPosiiton.y - index * DISTANCE);
        }else{
            newPostion.x  += -50.0f + Mathf.Abs(parentPosiiton.y - index * DISTANCE) ;
        }
        transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = newPostion;
        SetVisibility(parentPosiiton);

    }

    void SetVisibility( Vector2 pPosition){

        Vector2 posiitonVector = new Vector2( 0, index * DISTANCE );
        float distance = Vector2.Distance(pPosition, posiitonVector);

        if( distance > 130 ){
            float newAlpha = 1.0f - ( distance - 130 )/250.0f;
            changeVisibility(newAlpha);
        }else{ changeVisibility(1.0f); }
    }

    private void changeVisibilityBackground( Transform buttonBackgound, float alphaRate ){
        buttonBackgound.GetComponent<Image>().color = new Color(1,1,1,alphaRate);
    //    buttonBackgound.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,alphaRate);
    }

    private void changeVisibilityNameField( Transform buttonBackgound, float alphaRate ){
        var namePart = buttonBackgound.GetChild(2);
        namePart.GetComponent<Image>().color = new Color(1,1,1,alphaRate);
        namePart.GetChild(0).GetComponent<Text>().color  = new Color(1,1,1,alphaRate);
    }

    private void changeVisibilityRequirementField( Transform buttonBackgound, float alphaRate ){
        var requirementsPart = buttonBackgound.GetChild(1);
        requirementsPart.GetComponent<Image>().color = new Color(1,1,1, alphaRate);
        for( int i = 0; i < requirementsPart.childCount; i++){
            var texComponent = requirementsPart.GetChild(i).GetComponent<Text>();
            texComponent.color  = new Color(texComponent.color.r, texComponent.color.g, texComponent.color.b, alphaRate);
        }
    }

    void changeVisibility( float alphaRate ){
        var buttonBackgound = transform.GetChild(0);
        changeVisibilityBackground( buttonBackgound, alphaRate);
        changeVisibilityNameField( buttonBackgound, alphaRate);
        changeVisibilityRequirementField( buttonBackgound, alphaRate);
    }

    public void onButtonBackgroundPress( ){
        if(AchievmentMeasures.is_requirement_meet(meansureName, requirementMax )){
            PlayerPrefs.SetInt("Background", index);
            PlayerPrefs.Save();
        }
    }

    public void onButtonPlayerPress( ){
        if(AchievmentMeasures.is_requirement_meet(meansureName, requirementMax )){
            PlayerPrefs.SetInt("PlayerID", index);
            PlayerPrefs.Save();
        }
    }

    public Vector2 GetPosition(){
        if( AchievmentMeasures.is_requirement_meet(meansureName, requirementMax ) ) return new Vector2( 0, index * DISTANCE);
        else return new Vector2( -1000, -1000);
    }
    void Update()
    {
        shiftToSide();
    }
}
