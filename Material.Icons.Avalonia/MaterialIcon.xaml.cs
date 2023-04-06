using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Material.Icons.Avalonia {
    public class MaterialIcon : TemplatedControl {
        private static readonly Lazy<IDictionary<MaterialIconKind, string>> _dataIndex = new(MaterialIconDataFactory.DataSetCreate);

        static MaterialIcon() {
           KindProperty.Changed.Subscribe(args => (args.Sender as MaterialIcon)?.UpdateData());
        }

        MaterialIconKind _kind;

        public static readonly DirectProperty<MaterialIcon, MaterialIconKind> KindProperty = AvaloniaProperty.RegisterDirect<MaterialIcon, MaterialIconKind>(nameof(Kind),
            c => c.Kind, (c, v) => c.Kind = v);

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MaterialIconKind Kind {
            get => _kind;
            set => SetAndRaise(KindProperty, ref _kind, value);
        }

        private static readonly DirectProperty<MaterialIcon, string?>
            DataProperty = AvaloniaProperty.RegisterDirect<MaterialIcon, string?>(nameof(Data), icon => icon.Data);

        private string? _data;

        /// <summary>
        /// Gets the icon path data for the current <see cref="Kind"/>.
        /// </summary>
        public string? Data {
            get => _data;
            private set => SetAndRaise(DataProperty, ref _data, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            base.OnApplyTemplate(e);
            UpdateData();
        }

        private void UpdateData() {
            string? data = null;
            _dataIndex.Value?.TryGetValue(Kind, out data);
            Data = data;
        }
    }
}