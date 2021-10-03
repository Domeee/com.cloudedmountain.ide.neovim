using System.Diagnostics;
using System.IO;
using System.Linq;
using Unity.CodeEditor;
using UnityEditor;

[InitializeOnLoad]
public class NeovimCodeEditor : IExternalCodeEditor
{
  private const string PackageName = "com.cloudedmountain.ide.neovim";
  private const string NeovimLauncher = "com.cloudedmountain.ide.neovim.sh";

  static NeovimCodeEditor()
  {
    launcherPath = Path.GetFullPath(Path.Combine("Packages", PackageName, NeovimLauncher));
    installations = new CodeEditor.Installation[] {
    new CodeEditor.Installation { Name = "Neovim", Path = launcherPath }
  };
  }

  private static CodeEditor.Installation[] installations;
  private static string launcherPath;

  // Provide the editor with a list of known and supported editors that this instance supports. This gets called multiple times through execution and is not expected to be persistent. For example, after calling TryGetInstallationForPath this class may realize that you have added a custom code editor.
  public CodeEditor.Installation[] Installations => installations;

  // Callback to the IExternalCodeEditor when it has been chosen from the PreferenceWindow.
  public void Initialize(string editorInstallationPath) { }

  // 	Unity calls this methodf when it populates "Preferences/External Tools" in order to allow the code editor to generate necessary GUI. For example, when creating an an argument field for modifying the arguments sent to the code editor.
  public void OnGUI() { }

  // The external code editor needs to handle the request to open a file.
  public bool OpenProject(string filePath = "", int line = -1, int column = -1)
  {
    ProcessStartInfo startInfo = new ProcessStartInfo
    {
      Arguments = $"+{System.Math.Max(line, 0)} {filePath}",
      FileName = launcherPath,
      UseShellExecute = false,
      RedirectStandardOutput = true,
    };

    Process.Start(startInfo);

    return true;
  }

  // Unity calls this function during initialization in order to sync the Project. This is different from SyncIfNeeded in that it does not get a list of changes.
  public void SyncAll()
  {
    AssetDatabase.Refresh();
    UnityUtils.SyncSolution();
  }

  // When you change Assets in Unity, this method for the current chosen instance of IExternalCodeEditor parses the new and changed Assets.
  public void SyncIfNeeded(string[] addedFiles, string[] deletedFiles, string[] movedFiles, string[] movedFromFiles, string[] importedFiles)
  {
    UnityUtils.SyncSolutionIfNeeded(addedFiles.Union(deletedFiles).Union(movedFiles).Union(movedFromFiles).ToList(), importedFiles);
  }

  // Unity stores the path of the chosen editor. An instance of IExternalCodeEditor can take responsibility for this path, by returning true when this method is being called. The out variable installation need to be constructed with the path and the name that should be shown in the "External Tools" code editor list.
  public bool TryGetInstallationForPath(string editorPath, out CodeEditor.Installation installation)
  {
    installation = Installations.FirstOrDefault(install => install.Path == editorPath);
    return !string.IsNullOrEmpty(installation.Name);
  }
}
