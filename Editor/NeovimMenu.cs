using UnityEditor;

public static class NeovimMenu
{
  [MenuItem("Assets/Sync Unity Solution", false, 1000)]
  public static void SyncSolution()
  {
    UnityUtils.SyncSolution();
  }
}
