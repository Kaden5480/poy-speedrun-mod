using BepInEx.Configuration;
using UILib;
using UILib.Components;
using UILib.Layouts;

using UIButton = UILib.Components.Button;

namespace SpeedrunMod {
    /**
     * <summary>
     * The main UI for Speedrun Mod which lists
     * all available modules and provides a way
     * of interacting with them.
     * </summary>
     */
    internal class UI {
        private Window window;

        /**
         * <summary>
         * Initializes the shortcut to toggle Speedrun Mod's
         * main UI.
         *
         * This shortcut causes the window to toggle on/off,
         * but the window itself is only built when this
         * shortcut is triggered for the first time.
         * </summary>
         */
        internal UI() {
            Shortcut shortcut = new Shortcut(new[] { Config.toggleKeybind });
            shortcut.onTrigger.AddListener(Toggle);
            UIRoot.AddShortcut(shortcut);
        }

        /**
         * <summary>
         * Toggles the visibility of Speedrun Mod's main UI.
         * </summary>
         */
        private void Toggle() {
            // If the window hasn't been built yet, build it
            if (window == null) {
                BuildUI();
            }

            // Toggle the visibility
            window.ToggleVisibility();
        }

        /**
         * <summary>
         * Builds Speedrun Mod's main UI.
         * </summary>
         */
        private void BuildUI() {
            window = new Window("Speedrun Mod", 380f, 450f);
            window.SetMinSize(320f, 370f);

            // Make sure the content starts at the top
            Area scrollArea = new Area();
            scrollArea.SetAnchor(AnchorType.TopMiddle);
            scrollArea.SetContentLayout(LayoutType.Vertical);
            scrollArea.SetContentPadding(20);
            scrollArea.SetElementSpacing(20f);

            window.Add(scrollArea);
            window.scrollView.SetContent(scrollArea);

            // Add sections
            Label simpleTitle = new Label("Modules", 35);
            simpleTitle.SetSize(200f, 40f);
            window.Add(simpleTitle);

            // Simple modules
            AddSimple(Modules.NoBoulders.Module.name, Modules.NoBoulders.Config.enabled);
            AddSimple(Modules.NoKnockouts.Module.name, Modules.NoKnockouts.Config.enabled);

            // Modules with their own UI
            AddDetailed(Modules.PeakSweeper.Module.name);
        }

        /**
         * <summary>
         * Adds a simple module which only has an option to
         * turn it on/off.
         * </summary>
         * <param name="name">The name of the module</param>
         * <param name="enabled">The ConfigEntry to toggle the module</param>
         */
        private void AddSimple(string name, ConfigEntry<bool> enabled) {
            Area area = new Area(230f);
            area.SetFill(FillType.Horizontal);
            area.SetSize(0f, 30f);
            area.SetContentLayout(LayoutType.Horizontal);
            area.SetElementSpacing(30f);

            // Title area
            Area titleArea = new Area();
            titleArea.SetSize(150f, 30f);
            area.Add(titleArea);

            Label title = new Label(name, 22);
            title.SetAnchor(AnchorType.MiddleLeft);
            title.SetAlignment(AnchorType.MiddleLeft);
            title.SetFill(FillType.All);
            title.SetTooltip(enabled.Description.Description);
            titleArea.Add(title);

            // Toggle area
            Area toggleArea = new Area();
            toggleArea.SetSize(30f, 30f);
            area.Add(toggleArea);

            Toggle toggle = new Toggle(enabled.Value);
            toggle.SetAnchor(AnchorType.MiddleRight);
            toggle.SetSize(30f, 30f);
            toggle.onValueChanged.AddListener((bool value) => {
                enabled.Value = value;
            });
            toggleArea.Add(toggle);

            window.Add(area);
        }

        // TODO: This also needs to be provided some kind of UI
        // class which can be toggled. Perhaps it could be
        // provided with an arbitrary callback which invokes the
        // corresponding `Toggle` for the module's UI.
        /**
         * <summary>
         * Adds a button for a more detailed module which
         * has its own UI.
         * </summary>
         * <param name="name">The name of the module</param>
         */
        private void AddDetailed(string name) {
            UIButton button = new UIButton(name, 22);
            button.SetSize(200f, 40f);
            window.Add(button);
        }
    }
}
