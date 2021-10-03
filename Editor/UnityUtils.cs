using System;
using System.Collections.Generic;
using System.Reflection;

public class UnityUtils
{
  public static void SyncSolution()
  {
    // https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/VisualStudioIntegration/SolutionSynchronizer.cs#L214

    var sync_vs_type = Type.GetType("UnityEditor.SyncVS,UnityEditor");
    var synchronizer_field = sync_vs_type.GetField("Synchronizer", BindingFlags.NonPublic | BindingFlags.Static);
    var synchronizer_object = synchronizer_field.GetValue(sync_vs_type);
    var synchronizer_type = synchronizer_object.GetType();
    var synchronizer_sync_fn = synchronizer_type.GetMethod("Sync", BindingFlags.Public | BindingFlags.Instance);

    synchronizer_sync_fn.Invoke(synchronizer_object, null);
  }

  public static void SyncSolutionIfNeeded(IEnumerable<string> affectedFiles, IEnumerable<string> reimportedFiles)
  {
    // https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/VisualStudioIntegration/SolutionSynchronizer.cs#L195

    var sync_vs_type = Type.GetType("UnityEditor.SyncVS,UnityEditor");
    var synchronizer_field = sync_vs_type.GetField("Synchronizer", BindingFlags.NonPublic | BindingFlags.Static);
    var synchronizer_object = synchronizer_field.GetValue(sync_vs_type);
    var synchronizer_type = synchronizer_object.GetType();
    var synchronizer_sync_fn = synchronizer_type.GetMethod("SyncIfNeeded", BindingFlags.Public | BindingFlags.Instance);

    synchronizer_sync_fn.Invoke(synchronizer_object, new object[] { affectedFiles, reimportedFiles });
  }
}
