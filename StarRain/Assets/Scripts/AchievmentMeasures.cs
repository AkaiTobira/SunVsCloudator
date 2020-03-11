using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentMeasures : MonoBehaviour
{
    private static void set_measure( string measure, float value ){
        if( measure == "points"){
            int total_points = PlayerPrefs.GetInt("Highscore");
            if( value  > total_points ) PlayerPrefs.SetInt("Highscore", (int)value);
        }
    }
    
    private static void add_to_measure( string measure, float value ){
        if( measure == "points"){
            int total_points = PlayerPrefs.GetInt("TotalPoints");
            PlayerPrefs.SetInt("TotalPoints", total_points + (int)value);
        }
        if( measure == "byWall"){
            int total_points = PlayerPrefs.GetInt("Flipped");
            PlayerPrefs.SetInt("Flipped", total_points + (int)value);
        }
    }

    private static float get_value( string measure ){
        if( measure == "points")      return PlayerPrefs.GetInt("Highscore");
        if( measure == "totalPoints") return PlayerPrefs.GetInt("TotalPoints");
        if( measure == "byWall")      return PlayerPrefs.GetInt("Flipped");
        return 0;
    }

    public static string get_requirement_status( string measure ){
        if( measure == "unlockBG2")     return get_value("points").ToString();
        if( measure == "unlockPlayer2") return get_value("totalPoints").ToString();
        if( measure == "unlockPlayer3") return get_value("byWall").ToString();
        return "0";
    }

    public static bool is_requirement_meet( string measure, float target){
        if( measure == "unlockBG3")     return true;
        if( measure == "unlockBG2")     return get_value("points")      > target; 
        if( measure == "unlockPlayer2") return get_value("totalPoints") > target;
        if( measure == "unlockPlayer3") return get_value("byWall")      > target;
        return true;
    }

    public static void update_measure(string measure, float value ){
        set_measure(measure, value);
        add_to_measure(measure, value);
        PlayerPrefs.Save();
    }

    public static string[] get_unlocked(){
        return new string[0];
    }

}
