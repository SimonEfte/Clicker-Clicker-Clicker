# Example Scenes and Code

These example scenes demonstrate usage of the assets.

Minigame controls: Arrows (Move), SPACE (Run), ESC (Pause/Reset)

If you want, you can assign your own audio clips on the game objects `_controller` and `Player`.

Check the code in the `Class` folder to learn how to use the assets, and use whatever you need on your projects.

You can simply delete this `_example` folder once you're done with it.


## Troubleshooting

Use the appropriate folders, or else you'll see "pink" materials, not rendered.  
The assets under folders ending with `-URP` are to be used only when `Universal RP` package is installed and enabled.  
Otherwise, use the standard variation. Other render pipelines are not supported out-of-the-box.

If you get errors or warnings when playing the example, it can be due to a missing dependency:

- Unity UI: Make sure the package `com.unity.ugui` is installed, either directly or indirectly.
- Legacy Input Manager: Make sure it's not disabled on `ProjectSettings/Player/ActiveInputHandling`.
