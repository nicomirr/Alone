using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //LanguageManager
    public string language;

    //Player
    public Vector3 playerPos;
    public Vector3 playerScale;
    public bool lookingFront;
    public bool lookingBack;
    public bool sideMove;
    public bool mustHide;
    public bool mustEscape;

    //Lights
    public SerializableDictionary<string, bool> lightsOn;
    public SerializableDictionary<string, bool> firstLightsStatusCheck;

    //Inventory
    public bool isUsingFlashlight;
    public bool isUsingGlass;
    public bool isUsingParentsKey;
    public bool glassFilled;
    public bool fillSoundPlayed;    
    public int itemSelected;
    public ClickableObject inventoryItem;
    public bool hasFlashlight;
    public bool hasGlass;
    public bool hasKeyParents;
    public bool hasDadsNote;

    //ClickableObject
    public SerializableDictionary<string, bool> textFirstSetup;
    public SerializableDictionary<string, string> lookAtText;
    public SerializableDictionary<string, string> turnOnText;
    public SerializableDictionary<string, string> turnOffText;
    public SerializableDictionary<string, string> pickupText;
    public SerializableDictionary<string, string> searchText;
    public SerializableDictionary<string, string> readText;
    public SerializableDictionary<string, string> openText;
    public SerializableDictionary<string, string> closeText;
    public SerializableDictionary<string, string> useText;
    public SerializableDictionary<string, string> hasObjectText;
    public SerializableDictionary<string, string> hideText;
    public SerializableDictionary<string, bool> hasObject;
    public SerializableDictionary<string, bool> firstHasObjectStatusCheck;
    public SerializableDictionary<string, bool> canBeSearched;
    public SerializableDictionary<string, bool> canBeUsed;
    public SerializableDictionary<string, bool> firstCanBeSearchedStatusCheck;


    //CanBeOpenObjects
    public SerializableDictionary<string, bool> isOpen;

    //CanBeTurnOnOffObjects
    public SerializableDictionary<string, bool> isTurnedOn;
    public SerializableDictionary<string, bool> canBeTurnedOnOrOff;

    //CanBePickedUpObjects
    public SerializableDictionary<string, bool> hasBeenPickedUp;

    //ScenesInGame
    public bool firstPlayerRoomScenePlayed;
    public bool firstDinningRoomScenePlayed;
    public bool firstDinningRoomScene;
    public bool firstKitchenScenePlayed;
    public bool firstKitchenScene;
    public bool firstParentsRoomScenePlayed;
    public bool firstParentsRoomScene;
    public bool secondParentsRoomScenePlayed;
    public bool secondParentsRoomSceneFix;
    public bool firstEntranceScenePlayed;
    public bool firstLivingroomScenePlayed;
    
    //MouseAppearance
    public SerializableDictionary<string, bool> appearanceChanged;
    public SerializableDictionary<string, bool> cursorBlackened;

    //Doors
    public SerializableDictionary<string, bool> isLocked;
    public SerializableDictionary<string, bool> firstIsLockedStatusCheck;

    //Shower
    public bool showerCoroutineRunning;

    //Plant
    public SerializableDictionary<string, bool> firstPlantCanBeSearchedStatusCheck;
    public SerializableDictionary<string, bool> hasBeenWatered;

    //Plant zoomed 
    public SerializableDictionary<string, int> imageNum;
    public SerializableDictionary<string, bool> plantHasKey;
    public SerializableDictionary<string, bool> firstPlantHasKeyStatusCheck;
    public SerializableDictionary<string, bool> firstImageUpdated;
    public SerializableDictionary<string, bool> secondImageUpdated;

    //Computer
    public bool correctPasswordEntered;    

    //Notepad
    public bool notepadOpened;

    //Mouse
    public bool notClickable;
    public bool hasJustusedObject;

    //ObjectsInMouseBehaviour
    public SerializableDictionary<string, bool> canBeWatered;
    public SerializableDictionary<string, bool> firstCanBeWateredStatusCheck;

    //StartingScene
    public string sceneToLoad;

    //AudioSource
    public bool horrorSoundIsPlaying;
    public bool horrorSoundStarted;
    public bool horrorSoundStopped;

    //FlashingLights
    public bool flashingLightsCoroutineStarted;

    //Respiration
    public bool respirationTutorialShown;

    public GameData() 
    {
        //LanguageManager
        language = "en";

        //Player
        playerPos = new Vector3(1.85f, -1.27f);
        playerScale = new Vector3(1, 1, 1);
        lookingFront = false;
        lookingBack = false;
        sideMove = false;

        //Lights
        lightsOn = new SerializableDictionary<string, bool>();
        firstLightsStatusCheck = new SerializableDictionary<string, bool>();
                
        //ClickableObject
        hasObject = new SerializableDictionary<string, bool>();
        firstHasObjectStatusCheck = new SerializableDictionary<string, bool>();
        canBeSearched = new SerializableDictionary<string, bool>();
        firstCanBeSearchedStatusCheck = new SerializableDictionary<string, bool>();

        //CanBeOpenObjects
        isOpen = new SerializableDictionary<string, bool>();

        //CanBeTurnOnOffObjects
        isTurnedOn = new SerializableDictionary<string, bool>();
        canBeTurnedOnOrOff = new SerializableDictionary<string, bool>();

        //CanBePickedUpObjects
        hasBeenPickedUp = new SerializableDictionary<string, bool> ();

        //ScenesInGame
        firstPlayerRoomScenePlayed = false;
        firstDinningRoomScene = false;
        firstDinningRoomScenePlayed = false;
        firstKitchenScenePlayed = false;
        firstKitchenScene = false;

        //MouseAppearance
        appearanceChanged = new SerializableDictionary<string, bool>();
        cursorBlackened = new SerializableDictionary<string, bool>();
               
        //Doors
        firstIsLockedStatusCheck = new SerializableDictionary<string, bool>();
        isLocked = new SerializableDictionary<string, bool>();

        //Shower
        showerCoroutineRunning = false;

        //Plant
        hasBeenWatered = new SerializableDictionary<string, bool>();

        //Plant zoomed
        imageNum = new SerializableDictionary<string, int> ();
                                
        //Mouse
        notClickable = false;
        hasJustusedObject = false;
                
        //StartingScene
        sceneToLoad = "PlayersRoom";

    }


}
