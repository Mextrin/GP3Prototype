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
            GUILayout.Label("Spawnpoint selected (" + selected + ")");
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newSpawnpointPos = EditorGUILayout.Vector3Field("Position", spawner.spawnPoints[selected]);
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(spawner);
                    Undo.RecordObject(spawner, "Moved SpawnPoint");

                    spawner.spawnPoints[selected] = newSpawnpointPos;
                }
            }

            {
                EditorGUI.BeginChangeCheck();
                int newSpawnChance = EditorGUILayout.IntField("Spawn Chance", spawner.spawnChance[selected]);
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(spawner);
                    Undo.RecordObject(spawner, "Changed Spawn Chance");

                    spawner.spawnChance[selected] = newSpawnChance;
                }
            }

            if (GUILayout.Button("Remove Selected"))
            {
                EditorUtility.SetDirty(spawner);
                Undo.RecordObject(spawner, "Removed SpawnPoint");

                spawner.spawnPoints.RemoveAt(selected);
                spawner.spawnChance.RemoveAt(selected);
                selected = -1;
                SceneView.RepaintAll();
            }

            GUILayout.Space(10);
        }

        if (GUILayout.Button("Add Spawnpoint"))
        {
            EditorUtility.SetDirty(spawner);
            Undo.RecordObject(spawner, "Added SpawnPoint");

            spawner.spawnPoints.Add(Vector3.zero);
            spawner.spawnChance.Add(1);
            SceneView.RepaintAll();
        }
    }

    private void OnSceneGUI()
    {
        Spawner spawner = target as Spawner;

        Tools.current = Tool.None;

        for (int i = 0; i < spawner.spawnPoints.Count; i++)
        {
            Handles.color = Color.red;
            float buttonSize = HandleUtility.GetHandleSize(spawner.spawnPoints[i]) * buttonScale;
            Handles.Label(spawner.spawnPoints[i] + new Vector3(0, buttonSize, 0), "SpawnPoint: " + i);

            if (Handles.Button(spawner.spawnPoints[i], Quaternion.identity, buttonSize, buttonSize, Handles.SphereHandleCap))
            {
                selected = i;
                Repaint();
            }

            if (i == selected)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newSpawnpointPos = Handles.DoPositionHandle(spawner.spawnPoints[i], Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(spawner);
                    Undo.RecordObject(spawner, "Moved SpawnPoint");
                    spawner.spawnPoints[i] = newSpawnpointPos;
                }
            }
        }
    }
}
