using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievmentMeasures : MonoBehaviour
{

    private static Dictionary< string, List<float> > Acievments = new Dictionary<string, List<float>>();
    private const int IS_VALID_INDEX = 0;
    private const int MAX_VALUE_INDEX = 1;

    private static List<string> unlockedQueue = new List<string>();

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
        if( measure == "rainbow"){
            int total_points = PlayerPrefs.GetInt("RainbowBeam");
            PlayerPrefs.SetInt("RainbowBeam", total_points + (int)value);
        }
    }

    private static float get_value( string measure ){
        if( measure == "points")      return PlayerPrefs.GetInt("Highscore");
        if( measure == "totalPoints") return PlayerPrefs.GetInt("TotalPoints");
        if( measure == "byWall")      return PlayerPrefs.GetInt("Flipped");
        if( measure == "rainbow")     return PlayerPrefs.GetInt("RainbowBeam");
        return 0;
    }

    public static string get_requirement_status( string measure ){
        if( measure == "unlockBG2")     return get_value("rainbow").ToString();
        if( measure == "unlockPlayer2") return get_value("totalPoints").ToString();
        if( measure == "unlockPlayer3") return get_value("byWall").ToString();
        return "0";
    }

    public static bool is_requirement_meet( string measure, float target){
        if( measure == "unlockBG3")     return true;
        if( measure == "unlockBG2")     return get_value("rainbow")     >= target; 
        if( measure == "unlockPlayer1") return true;
        if( measure == "unlockPlayer2") return get_value("totalPoints") >= target;
        if( measure == "unlockPlayer3") return get_value("byWall")      >= target;
        return true;
    }

    public static void update_measure(string measure, float value ){
        set_measure(measure, value);
        add_to_measure(measure, value);
        PlayerPrefs.Save();
        update_archive();
    }

    public static void register_achievment( string name, float value){
        if( Acievments.ContainsKey(name) ) return;
        List< float > tempList = new List<float>();
        tempList.Add( (is_requirement_meet(name, value)) ? 1.0f : 0.0f );
        tempList.Add( value );
        Acievments[ name ] = tempList;
    }

    private static void update_archive(){
        foreach( KeyValuePair< string, List<float>> kvp in Acievments )
        {
            if( kvp.Value[IS_VALID_INDEX] == 1.0f) continue;
            if( is_requirement_meet(kvp.Key, kvp.Value[MAX_VALUE_INDEX])){
                Acievments[kvp.Key][IS_VALID_INDEX] = 1.0f;
                unlockedQueue.Add( kvp.Key );
            }
        }
    }

    public static bool unlocked_new_background(){
        string result = unlockedQueue.Find(x => x.Contains("BG"));
        unlockedQueue.RemoveAll(x => x.Contains("BG"));
        return result != null;
    }

    public static bool unlocked_new_player(){
        string result = unlockedQueue.Find(x => x.Contains("Player"));
        unlockedQueue.RemoveAll(x => x.Contains("Player"));
        return result != null;
    }
}

//TO DO:
// 1.AChievment player sie nie wyświetla
// 2.Podmiana  tła 