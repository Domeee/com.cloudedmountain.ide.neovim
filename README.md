# Unity Neovim Code Editor

Make Neovim a Unity first-class citizen!

## Goals of this project

- Integrate neovim with unity as external script editor
  - Neovim can be selected as external script editor
  - Files of appropriate type will be opened in neovim
    - Opening a file without existing neovim instance opens a new neovim instance
    - Opening a file with existing neovim instance opens the file in the existing neovim instance
  - .NET based unity solution files will be synced if necessary (new file added, file deleted, file renamed)
  - Manually trigger unity solution files synchronization via assets menu

### Out of scope

- Unity integration in neovim
  - Syntax highlighting, code completion and further C# specific features are provided via [neovim's language server capabilities](https://neovim.io/doc/user/lsp.html).
  - [omnisharp-roslyn](https://github.com/OmniSharp/omnisharp-roslyn) is the way to go

## Current state of the project

The project currently is in early stage development. Expect errors and breaking changes.
The current version `0.0.1` was tested on Arch Linux only. The project currently does not support operating systems other than linux.

## Prerequisites

Neovim currently does not provide client/server functionality to act as a command server. To open files in an existing instance of neovim, [neovim-remote](https://github.com/mhinz/neovim-remote) is required. There is an active neovim PR in the works, see [GitHub PR](https://github.com/neovim/neovim/pull/17439).

```
yay -S neovim-remote
```

## Installation

1. Download and prepare the neovim unity package

```
git clone git@github.com:Domeee/com.cloudedmountain.ide.neovim.git
sudo chmod +x ./com.cloudedmountain.ide.neovim.sh
```

2. Add the neovim unity package to unity

```
Window > Package Manager > Add package from disk > select package.json
```

1. Set neovim as external script edtior

```
Edit > Preferences > External Tools > External Script Editor > select Neovim
```

## Usage

The neovim unity package automatically synchronizes the unity solution files if a new file was added, an existing file was deleted or renamed.
The unity solution file synchronization can be manually triggered via `Assets > Sync Unity Solution`

## Todo list

- [] Streamline installation process
- [] Replace `neovim-remote` dependency asap, see [GitHub PR](https://github.com/neovim/neovim/pull/17439)

## Troubleshooting

> Imports not detected on newly created file

See [open issue @ omnisharp-roslyn](https://github.com/OmniSharp/omnisharp-roslyn/issues/2250), [open issue @ neovim](https://github.com/neovim/neovim/issues/14042)

## References

- [Unity Editor Reference Source Code](https://github.com/Unity-Technologies/UnityCsReference/tree/master)
- [JetBrains Unity Resharper Package](https://github.com/JetBrains/resharper-unity)
- [Unity VSCode Package](https://github.com/Unity-Technologies/com.unity.ide.vscode/tree/master/Packages/com.unity.ide.vscode)
