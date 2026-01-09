using BepInEx.Configuration;
using UILib;
using UILib.Components;
using UILib.Layouts;

using UIButton = UILib.Components.Button;

namespace SpeedrunMod {
    internal class UI {
        private Window window;

        internal UI() {
            Shortcut shortcut = new Shortcut(new[] { Config.toggleKeybind });
            shortcut.onTrigger.AddListener(Toggle);
            UIRoot.AddShortcut(shortcut);
        }

        private void Toggle() {
            if (window == null) {
                BuildUI();
            }

            window.ToggleVisibility();
        }

        private void BuildUI() {
            window = new Window("Speedrun Mod", 600f, 700f);

            // Make sure the content starts at the top
            Area scrollArea = new Area();
            scrollArea.SetAnchor(AnchorType.TopMiddle);
            scrollArea.SetContentLayout(LayoutType.Vertical);
            scrollArea.SetContentPadding(20);
            scrollArea.SetElementSpacing(20f);

            window.Add(scrollArea);
            window.scrollView.SetContent(scrollArea);

            // Add sections
            Label simpleTitle = new Label("Simple Modules", 30);
            simpleTitle.SetSize(200f, 40f);
            window.Add(simpleTitle);

            AddSimple(Modules.NoBoulders.Module.name, Modules.NoBoulders.Config.enabled);
            AddSimple(Modules.NoKnockouts.Module.name, Modules.NoKnockouts.Config.enabled);

            window.Add(new Area(height: 10f));

            Label detailTitle = new Label("Detailed Modules", 30);
            detailTitle.SetSize(200f, 50f);
            window.Add(detailTitle);

            AddDetailed(Modules.PeakSweeper.Module.name);
        }

        private void AddDetailed(string name) {
            UIButton button = new UIButton(name, 22);
            button.SetSize(200f, 40f);
            window.Add(button);
        }

        private void AddSimple(string name, ConfigEntry<bool> enabled) {
            float totalWidth  = 500f;
            float totalHeight = 30f;
            float spacing     = 70f;

            Area area = new Area(totalWidth);
            area.SetFill(FillType.Horizontal);
            area.SetSize(0f, totalHeight);
            area.SetContentLayout(LayoutType.Horizontal);
            area.SetElementSpacing(spacing);

            // Title area
            Area titleArea = new Area();
            titleArea.SetSize((totalWidth - spacing)/2f, totalHeight);
            area.Add(titleArea);

            Label title = new Label($"Enable {name}", 22);
            title.SetAnchor(AnchorType.MiddleRight);
            title.SetAlignment(AnchorType.MiddleRight);
            title.SetFill(FillType.All);
            titleArea.Add(title);

            // Toggle area
            Area toggleArea = new Area();
            toggleArea.SetSize((totalWidth - spacing)/2f, totalHeight);
            area.Add(toggleArea);

            Toggle toggle = new Toggle(enabled.Value);
            toggle.SetAnchor(AnchorType.MiddleLeft);
            toggle.SetOffset(0f, 0f);
            toggle.SetSize(30f, 30f);
            toggle.onValueChanged.AddListener((bool value) => {
                enabled.Value = value;
            });
            toggleArea.Add(toggle);

            window.Add(area);
        }
    }
}
