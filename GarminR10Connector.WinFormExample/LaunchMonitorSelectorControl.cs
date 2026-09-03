/*
 * Copyright (c) 2026 Jason Landreth
 *
 * Licensed under the Apache License, Version 2.0 (the ""License"");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an ""AS IS"" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using GarminR10Connector.WinFormExample.EventArgs;
using System.ComponentModel;

namespace GarminR10Connector.WinFormExample
{
  public partial class LaunchMonitorSelectorControl : UserControl
  {
    private LaunchMonitorType _selectedLaunchMonitorType;

    public LaunchMonitorSelectorControl()
    {
      InitializeComponent();
    }

    [Category("Appearance")]
    [Description("Background image shown in the launch monitor panel.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Localizable(true)]
    public Image? SelectorBackgroundImage
    {
      get => panelLaunchMonitor.BackgroundImage;
      set => panelLaunchMonitor.BackgroundImage = value;
    }

    [Category("Appearance")]
    [Description("Background image layout for the launch monitor panel.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ImageLayout SelectorBackgroundImageLayout
    {
      get => panelLaunchMonitor.BackgroundImageLayout;
      set => panelLaunchMonitor.BackgroundImageLayout = value;
    }

    [Category("Data")]
    [Description("Currently selected launch monitor type.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public LaunchMonitorType SelectedLaunchMonitorType
    {
      get => _selectedLaunchMonitorType;
      set
      {
        if (_selectedLaunchMonitorType == value) return;
        _selectedLaunchMonitorType = value;
        RaiseOnSelectionChanged(value);
      }
    }

    [Category("Behavior")]
    [Description("Raised when the selected launch monitor type changes.")]
    public event EventHandler<LaunchMonitorSelectionEventArgs>? OnSelectionChanged;

    protected virtual void RaiseOnSelectionChanged(LaunchMonitorType selectedType)
    {
      OnSelectionChanged?.Invoke(this, new LaunchMonitorSelectionEventArgs(selectedType));
    }
  }
}
