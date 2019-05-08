using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    int selected = -1;
    float buttonScale = 0.1f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Spawner spawner = target as Spawner;
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Amount of spawnpoints");
            GUILayout.Label(spawner.spawnPoints.Count.ToString());
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(10);

        if (selected >= 0 && selected < spawner.spawnPoints.Count)
        {
            SpawnPoint spawnPoint = spawner.spawnPoints[selected];
            EditorGUILayout.BeginVertical("Box");
            {
                GUILayout.Label("Spawnpoint selected (" + selected + ")");
                EditorGUILayout.BeginVertical("Box");
                {
                    EditorGUI.BeginChangeCheck();
                    {
                        Vector3 newSpawnpointPos = EditorGUILayout.Vector3Field("Position", spawnPoint.position);

                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(spawner);
                            Undo.RecordObject(spawner, "Moved SpawnPoint");

                            spawnPoint.position = newSpawnpointPos;
                        }
                    }
                }

                {
                    EditorGUI.BeginChangeCheck();
                    {
                        int newSpawnChance = EditorGUILayout.IntField("Spawn Chance", spawnPoint.pointChoiceChance);

                        if (EditorGUI.EndChangeCheck())
                        {
                            EditorUtility.SetDirty(spawner);
                            Undo.RecordObject(spawner, "Changed Spawn Chance");

                            spawnPoint.pointChoiceChance = newSpawnChance;
                        }
                    }
                }
                EditorGUILayout.EndVertical();

                GUILayout.Space(5);

                EditorGUILayout.BeginVertical("Box");
                {
                    if (spawnPoint.enemiesToSpawn.Count > 0)
                    {

                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Enemy prefab");
                            GUILayout.Label("Enemy spawn chance");
                        }
                        EditorGUILayout.EndHorizontal();
                        for (int i = 0; i < spawnPoint.enemiesToSpawn.Count; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUI.BeginChangeCheck();
                                {
                                    Object pickedEnemy = spawnPoint.enemiesToSpawn[i] as Object;
                                    Object selectedEnemy = EditorGUILayout.ObjectField(spawnPoint.enemiesToSpawn[i], typeof(Enemy), false);

                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        EditorUtility.SetDirty(spawner);
                                        Undo.RecordObject(spawner, "Changed enemy prefab in spawn list");

                                        spawnPoint.enemiesToSpawn[i] = selectedEnemy as Enemy;
                                    }
                                }

                                EditorGUI.BeginChangeCheck();
                                {
                                    int newEnemySpawnChance = EditorGUILayout.IntField(spawnPoint.enemySpawnChance[i]);

                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        EditorUtility.SetDirty(spawner);
                                        Undo.RecordObject(spawner, "Changed enemy spawn chance in spawn list");

                                        spawnPoint.enemySpawnChance[i] = newEnemySpawnChance;
                                    }
                                }

                                if (GUILayout.Button("-"))
                                {
                                    EditorUtility.SetDirty(spawner);
                                    Undo.RecordObject(spawner, "Removed Enemy from SpawnPoint List");

                                    spawnPoint.enemiesToSpawn.RemoveAt(i);
                                    spawnPoint.enemySpawnChance.RemoveAt(i);
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                    }

                    if (GUILayout.Button("Add Enemy"))
                    {
                        EditorUtility.SetDirty(spawner);
                        Undo.RecordObject(spawner, "Added Enemy to SpawnPoint List");

                        spawnPoint.enemiesToSpawn.Add(null);
                        spawnPoint.enemySpawnChance.Add(1);
                    }

                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        {

            if (GUILayout.Button("Add Spawnpoint", EditorStyles.miniButtonLeft))
            {
                EditorUtility.SetDirty(spawner);
                Undo.RecordObject(spawner, "Added SpawnPoint");

                spawner.spawnPoints.Add(new SpawnPoint(Vector3.zero));
                selected = spawner.spawnPoints.Count - 1;
                SceneView.RepaintAll();
            }

            EditorGUI.BeginDisabledGroup(selected < 0);
            if (GUILayout.Button("Remove Selected", EditorStyles.miniButtonRight))
            {
                EditorUtility.SetDirty(spawner);
                Undo.RecordObject(spawner, "Removed SpawnPoint");

                spawner.spawnPoints.RemoveAt(selected);
                selected = -1;
                SceneView.RepaintAll();
            }
            EditorGUI.EndDisabledGroup();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OnSceneGUI()
    {
        Spawner spawner = target as Spawner;

        Tools.current = Tool.None;

        for (int i = 0; i < spawner.spawnPoints.Count; i++)
        {
            Handles.color = Color.red;
            float buttonSize = HandleUtility.GetHandleSize(spawner.spawnPoints[i].position) * buttonScale;
            Handles.Label(spawner.spawnPoints[i].position + new Vector3(0, buttonSize, 0), "SpawnPoint: " + i);

            if (Handles.Button(spawner.spawnPoints[i].position, Quaternion.identity, buttonSize, buttonSize, Handles.SphereHandleCap))
            {
                selected = i;
                Repaint();
            }

            if (i == selected)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newSpawnpointPos = Handles.DoPositionHandle(spawner.spawnPoints[i].position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(spawner);
                    Undo.RecordObject(spawner, "Moved SpawnPoint");
                    spawner.spawnPoints[i].position = newSpawnpointPos;
                }
            }
        }
    }
}
