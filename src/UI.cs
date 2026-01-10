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
            window = new Window("Speedrun Mod", 450f, 600f);
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
    }
}
