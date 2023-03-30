using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class WaveEditor : EditorWindow
{
    PolygonCreator polygonCreator;
    int polygonRayCount;
    float innerPolygonSize;
    float outerPolygonSize;
    float ennemyPolygonHitAreaOffset;
    float polygonRefreshRate;
    Vector3 innerPolygonPosition;
    Vector3 outerPolygonPosition;

    WavePattern wavePattern;
    float rectangleWidthPattern;
    float rectangleLengthPattern;
    bool elipseShapePattern;
    bool rectangleShapePattern;
    bool circlesOnSidesShapePattern;
    bool circlesOnDiagonalsShapePattern;
    bool isInnerShapeARectanglePattern;
    float innerPolygonLengthRatioPattern;

    MonsterSpawn monsterSpawn;
    int triesToSpawnMonster;
    int waveTimeLength;
    int numberOfEnnemiesNeededToSpawn;
    bool groupOfEnnemies;
    int numberOfEnnemiesPerGroup;
    float ennemyInGroupSpawnRange;

    MonsterPulled monsterPulled;
    bool usingMonsterPool1;
    bool usingMonsterPool2;
    bool usingMonsterPool3;

    GameObject waveManager;
    
    public void Awake()
    {
        waveManager = GameObject.Find("WaveManager").gameObject;
    }

    [MenuItem("Window/WaveEditor")]
    public static void ShowWindow()
    {
        GetWindow<WaveEditor>("WaveEditor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Welcome to the Wave Editor", EditorStyles.boldLabel);

        polygonRayCount = EditorGUILayout.IntField(nameof(polygonRayCount), polygonRayCount);
        innerPolygonSize = EditorGUILayout.FloatField(nameof(innerPolygonSize), innerPolygonSize);
        outerPolygonSize = EditorGUILayout.FloatField(nameof(outerPolygonSize), outerPolygonSize);
        ennemyPolygonHitAreaOffset = EditorGUILayout.FloatField(nameof(ennemyPolygonHitAreaOffset), ennemyPolygonHitAreaOffset);
        polygonRefreshRate = EditorGUILayout.FloatField(nameof(polygonRefreshRate), polygonRefreshRate);
        innerPolygonPosition = EditorGUILayout.Vector3Field(nameof(innerPolygonPosition), innerPolygonPosition);
        outerPolygonPosition = EditorGUILayout.Vector3Field(nameof(outerPolygonPosition), outerPolygonPosition);

        rectangleWidthPattern = EditorGUILayout.FloatField(nameof(rectangleWidthPattern), rectangleWidthPattern);
        rectangleLengthPattern = EditorGUILayout.FloatField(nameof(rectangleLengthPattern), rectangleLengthPattern);
        elipseShapePattern = EditorGUILayout.Toggle(nameof(elipseShapePattern), elipseShapePattern);
        rectangleShapePattern = EditorGUILayout.Toggle(nameof(rectangleShapePattern), rectangleShapePattern);
        circlesOnSidesShapePattern = EditorGUILayout.Toggle(nameof(circlesOnSidesShapePattern), circlesOnSidesShapePattern);
        circlesOnDiagonalsShapePattern = EditorGUILayout.Toggle(nameof(circlesOnDiagonalsShapePattern), circlesOnDiagonalsShapePattern);
        isInnerShapeARectanglePattern = EditorGUILayout.Toggle(nameof(isInnerShapeARectanglePattern), isInnerShapeARectanglePattern);
        innerPolygonLengthRatioPattern = EditorGUILayout.FloatField(nameof(innerPolygonLengthRatioPattern), innerPolygonLengthRatioPattern);

        triesToSpawnMonster = EditorGUILayout.IntField(nameof(triesToSpawnMonster), triesToSpawnMonster);
        waveTimeLength = EditorGUILayout.IntField(nameof(waveTimeLength), waveTimeLength);
        numberOfEnnemiesNeededToSpawn = EditorGUILayout.IntField(nameof(numberOfEnnemiesNeededToSpawn), numberOfEnnemiesNeededToSpawn);
        groupOfEnnemies = EditorGUILayout.Toggle(nameof(groupOfEnnemies), groupOfEnnemies);
        numberOfEnnemiesPerGroup = EditorGUILayout.IntField(nameof(numberOfEnnemiesPerGroup), numberOfEnnemiesPerGroup);
        ennemyInGroupSpawnRange = EditorGUILayout.FloatField(nameof(ennemyInGroupSpawnRange), ennemyInGroupSpawnRange);

        usingMonsterPool1 = EditorGUILayout.Toggle(nameof(usingMonsterPool1), usingMonsterPool1);
        usingMonsterPool2 = EditorGUILayout.Toggle(nameof(usingMonsterPool2), usingMonsterPool2);
        usingMonsterPool3 = EditorGUILayout.Toggle(nameof(usingMonsterPool3), usingMonsterPool3);
        
        if (GUILayout.Button("GetWaveValues"))
        {
            foreach (GameObject thisWave in Selection.gameObjects)
            {
                polygonCreator = thisWave.GetComponent<PolygonCreator>();
                wavePattern = thisWave.GetComponent<WavePattern>();
                monsterSpawn = thisWave.GetComponent<MonsterSpawn>();
                monsterPulled = thisWave.GetComponent<MonsterPulled>();

                if (thisWave.transform.parent.gameObject == waveManager)
                {
                    polygonRayCount = polygonCreator.rayCount;
                    innerPolygonSize = polygonCreator.innerDistance;
                    outerPolygonSize = polygonCreator.outerDistance;
                    ennemyPolygonHitAreaOffset = polygonCreator.areaOffset;
                    polygonRefreshRate = polygonCreator.refreshRate;
                    innerPolygonPosition = polygonCreator.innerPosition;
                    outerPolygonPosition = polygonCreator.outerPosition;

                    rectangleWidthPattern = wavePattern.rectangleWidth;
                    rectangleLengthPattern = wavePattern.rectangleLength;
                    elipseShapePattern = wavePattern.elipse;
                    rectangleShapePattern = wavePattern.rectangle;
                    circlesOnSidesShapePattern = wavePattern.star;
                    circlesOnDiagonalsShapePattern = wavePattern.diagonalStar;
                    isInnerShapeARectanglePattern = wavePattern.innerPolygonRectangle;
                    innerPolygonLengthRatioPattern = wavePattern.innerPolygonLengthRatio;

                    triesToSpawnMonster = monsterSpawn.maxTries;
                    waveTimeLength = monsterSpawn.waveTimeLength;
                    numberOfEnnemiesNeededToSpawn = monsterSpawn.numberOfEnnemies;
                    groupOfEnnemies = monsterSpawn.EnnemyGroup;
                    numberOfEnnemiesPerGroup = monsterSpawn.numberOfEnnemiesPerGroup;
                    ennemyInGroupSpawnRange = monsterSpawn.spawnRange;

                    usingMonsterPool1 = monsterPulled.monsterPoolSelected1;
                    usingMonsterPool2 = monsterPulled.monsterPoolSelected2;
                    usingMonsterPool3 = monsterPulled.monsterPoolSelected3;
                }
            }
        }

        if (GUILayout.Button("Apply"))
        {
            foreach (GameObject thisWave in Selection.gameObjects)
            {
                polygonCreator = thisWave.GetComponent<PolygonCreator>();
                wavePattern = thisWave.GetComponent<WavePattern>();
                monsterSpawn = thisWave.GetComponent<MonsterSpawn>();
                monsterPulled = thisWave.GetComponent<MonsterPulled>();

                EditorUtility.SetDirty(polygonCreator);
                EditorUtility.SetDirty(wavePattern);
                EditorUtility.SetDirty(monsterSpawn);
                EditorUtility.SetDirty(monsterPulled);

                if (thisWave.transform.parent.gameObject == waveManager)
                {
                    polygonCreator.rayCount = polygonRayCount;
                    polygonCreator.innerDistance = innerPolygonSize;
                    polygonCreator.outerDistance = outerPolygonSize;
                    polygonCreator.areaOffset = ennemyPolygonHitAreaOffset;
                    polygonCreator.refreshRate = polygonRefreshRate;
                    polygonCreator.innerPosition = innerPolygonPosition;
                    polygonCreator.outerPosition = outerPolygonPosition;

                    wavePattern.rectangleWidth = rectangleWidthPattern;
                    wavePattern.rectangleLength = rectangleLengthPattern;
                    wavePattern.elipse = elipseShapePattern;
                    wavePattern.rectangle = rectangleShapePattern;
                    wavePattern.star = circlesOnSidesShapePattern;
                    wavePattern.diagonalStar = circlesOnDiagonalsShapePattern;
                    wavePattern.innerPolygonRectangle = isInnerShapeARectanglePattern;
                    wavePattern.innerPolygonLengthRatio = innerPolygonLengthRatioPattern;

                    monsterSpawn.maxTries = triesToSpawnMonster;
                    monsterSpawn.waveTimeLength = waveTimeLength;
                    monsterSpawn.numberOfEnnemies = numberOfEnnemiesNeededToSpawn;
                    monsterSpawn.EnnemyGroup = groupOfEnnemies;
                    monsterSpawn.numberOfEnnemiesPerGroup = numberOfEnnemiesPerGroup;
                    monsterSpawn.spawnRange = ennemyInGroupSpawnRange;

                    monsterPulled.monsterPoolSelected1 = usingMonsterPool1;
                    monsterPulled.monsterPoolSelected2 = usingMonsterPool2;
                    monsterPulled.monsterPoolSelected3 = usingMonsterPool3;
                }
            }
        }
    }
}
#endif