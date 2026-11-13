// Recreated from
// https://github.com/mastodon/mastodon-android/blob/1f4152b588f05ffafa93bc4e9a422ad9653195af/mastodon/src/main/java/org/joinmastodon/android/ui/views/MediaGridLayout.java

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections;
using System.Collections.Specialized;
using Windows.Foundation;

namespace View.Controls.Components;

internal sealed partial class MediaGrid: Grid {
    private const double TileCornerRadius = 4;

    private TiledLayout? layout = null;
    private double[] layoutRatios = [];

    public object? ItemsSource {
        get;
        set {
            field = value;
            OnItemsChanged();
        }
    } = null;

    public DataTemplate? ItemTemplate {
        get;
        set {
            field = value;
            OnItemsChanged();
        }
    } = null;

    private INotifyCollectionChanged? observedCollection = null;
    private bool hasGeneratedChildren = false;

    private void OnItemsChanged() {
        if (IsLoaded) {
            ObserveCollection(ItemsSource as INotifyCollectionChanged);
        }
        GenerateChildren();
    }

    private void ObserveCollection(INotifyCollectionChanged? collection) {
        observedCollection?.CollectionChanged -= ItemsSource_CollectionChanged;
        observedCollection = collection;
        observedCollection?.CollectionChanged += ItemsSource_CollectionChanged;
    }

    private void ItemsSource_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        GenerateChildren();
    }

    private void GenerateChildren() {
        // Leave children declared directly in XAML alone.
        if ((ItemsSource is null) && !hasGeneratedChildren) {
            return;
        }

        Children.Clear();
        hasGeneratedChildren = false;
        if ((ItemsSource is not IEnumerable items) || (ItemTemplate is null)) {
            return;
        }

        foreach (var item in items) {
            if (ItemTemplate.LoadContent() is not FrameworkElement element) {
                throw new InvalidOperationException($"{nameof(MediaGrid)}.{nameof(ItemTemplate)} root must be a {nameof(FrameworkElement)}.");
            }
            element.DataContext = item;
            Children.Add(element);
        }
        hasGeneratedChildren = true;
    }

    private void UpdateTiles(List<MediaPreview> tiles) {
        var ratios = tiles.Select(x => x.Aspect).ToArray();
        if (!ratios.SequenceEqual(layoutRatios)) {
            layoutRatios = ratios;
            layout = (ratios.Length > 0) ? TiledLayout.Create(ratios) : null;

            ColumnDefinitions.Clear();
            RowDefinitions.Clear();
            foreach (var size in layout?.ColumnSizes ?? []) {
                ColumnDefinitions.Add(new() { Width = new(size, GridUnitType.Star) });
            }
            foreach (var size in layout?.RowSizes ?? []) {
                RowDefinitions.Add(new() { Height = new(size, GridUnitType.Star) });
            }
        }

        // Children may be replaced without changing ratios, so positions are always reapplied.
        for (var i = 0; i < tiles.Count; i++) {
            var item = tiles[i];
            var tile = layout!.Tiles[i];
            item.CornerRadius = tile.GetCornerRadius(TileCornerRadius);
            SetColumn(item, tile.StartColumn);
            SetColumnSpan(item, Math.Max(1, tile.ColumnSpan));
            SetRow(item, tile.StartRow);
            SetRowSpan(item, Math.Max(1, tile.RowSpan));
        }
    }

    // Height is derived from the offered width only; the offered height is ignored.
    protected override Size MeasureOverride(Size availableSize) {
        var tiles = new List<MediaPreview>(Children.Count);
        foreach (var child in Children) {
            if (child is not MediaPreview item) {
                throw new InvalidOperationException($"{nameof(MediaGrid)} children must be {nameof(MediaPreview)}, got {child.GetType().FullName}.");
            }
            // Collapsed children are excluded from the layout.
            if (child.Visibility == Visibility.Visible) {
                tiles.Add(item);
            }
        }
        UpdateTiles(tiles);

        // Unbounded width would produce an infinite height, which layout rejects.
        if ((layout is null) || !Double.IsFinite(availableSize.Width)) {
            return new(0, 0);
        }

        var width = availableSize.Width;
        // Layouts narrower than the virtual canvas report a narrower desired width.
        var contentWidth = width * Math.Min(layout.Width, TiledLayout.MaxWidth) / TiledLayout.MaxWidth;
        // Rows use the same pixels-per-unit as columns, so spacing doesn't distort tile aspect ratios.
        var columnSpacing = ColumnSpacing * (layout.ColumnSizes.Length - 1);
        var scale = Math.Max(0, contentWidth - columnSpacing) / layout.ColumnSizes.Sum();
        var height = (layout.RowSizes.Sum() * scale) + (RowSpacing * (layout.RowSizes.Length - 1));
        base.MeasureOverride(new(contentWidth, height));
        return new(contentWidth, height);
    }

    private sealed class Tile(int columnSpan, int rowSpan, int startColumn, int startRow) {
        public int ColumnSpan { get; set; } = columnSpan;
        public int RowSpan { get; } = rowSpan;
        public int StartColumn { get; set; } = startColumn;
        public int StartRow { get; } = startRow;
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        public int Width { get; init; }

        public CornerRadius GetCornerRadius(double radius) {
            bool tl = false, tr = false, bl = false, br = false;
            if ((ColumnCount == 1) && (RowCount == 1)) {
                tl = tr = bl = br = true;
            } else if ((ColumnSpan == ColumnCount) && (StartRow == 0)) {
                tl = tr = true;
            } else if ((ColumnSpan == ColumnCount) && (StartRow == RowCount - 1)) {
                bl = br = true;
            } else if ((StartColumn == 0) && (StartRow == 0) && (RowSpan == RowCount)) {
                tl = bl = true;
            } else if ((StartColumn == ColumnCount - 1) && (StartRow == 0) && (RowSpan == RowCount)) {
                tr = br = true;
            } else if ((StartColumn == 0) && (StartRow == 0)) {
                tl = true;
            } else if ((StartColumn == ColumnCount - ColumnSpan) && (StartRow == 0)) {
                tr = true;
            } else if ((StartColumn == 0) && (StartRow == RowCount - RowSpan)) {
                bl = true;
            } else if ((StartColumn == ColumnCount - ColumnSpan) && (StartRow == RowCount - RowSpan)) {
                br = true;
            }
            return new(tl ? radius : 0, tr ? radius : 0, br ? radius : 0, bl ? radius : 0);
        }
    }

    // Sizes are in units of a MaxWidth-wide virtual canvas.
    private sealed class TiledLayout {
        public const int MaxWidth = 1000;
        private const int MaxHeight = 1777;
        private const int MinHeight = 563;
        private const float Gap = 1.5f;

        public int[] ColumnSizes { get; private set; } = [];
        public int[] RowSizes { get; private set; } = [];
        public Tile[] Tiles { get; private set; } = [];
        public int Width { get; private set; }
        public int Height { get; private set; }

        // Matches Java's Math.round(float).
        private static int Round(float value) => (int)MathF.Floor(value + 0.5f);

        private static float GetMultiThumbsHeight(List<float> ratios, float width, float margin) {
            return (width - ((ratios.Count - 1) * margin)) / ratios.Sum();
        }

        public static TiledLayout Create(IReadOnlyList<double> aspectRatios) {
            const float maxRatio = MaxWidth / (float)MaxHeight;
            var result = new TiledLayout();
            var sourceRatios = aspectRatios
                .Select(x => (Double.IsFinite(x) && (x > 0)) ? (float)x : 1f)
                .ToArray();

            if (sourceRatios.Length == 1) {
                var ratio = sourceRatios[0];
                result.Width = MaxWidth;
                result.Height = (ratio > maxRatio) ? Math.Max(MinHeight, Round(1 / ratio * MaxWidth)) : MaxHeight;
                result.ColumnSizes = [result.Width];
                result.RowSizes = [result.Height];
                result.Tiles = [new(1, 1, 0, 0)];
            } else {
                var ratios = new List<float>();
                bool allAreWide = true, allAreSquare = true;
                foreach (var sourceRatio in sourceRatios) {
                    var ratio = Math.Max(0.45f, sourceRatio);
                    if (ratio <= 1.2f) {
                        allAreWide = false;
                        if (ratio < 0.8f) {
                            allAreSquare = false;
                        }
                    } else {
                        allAreSquare = false;
                    }
                    ratios.Add(ratio);
                }
                var avgRatio = ratios.Sum() / ratios.Count;

                switch (ratios.Count) {
                case 2:
                    result.LayoutTwo(ratios, maxRatio, avgRatio, allAreWide, allAreSquare);
                    break;
                case 3:
                    result.LayoutThree(ratios, maxRatio, avgRatio, allAreWide);
                    break;
                case 4:
                    result.LayoutFour(ratios, maxRatio, avgRatio, allAreWide);
                    break;
                default:
                    result.LayoutMany(ratios, avgRatio);
                    break;
                }
            }

            foreach (var tile in result.Tiles) {
                tile.ColumnCount = result.ColumnSizes.Length;
                tile.RowCount = result.RowSizes.Length;
            }
            return result;
        }

        private void LayoutTwo(List<float> ratios, float maxRatio, float avgRatio, bool allAreWide, bool allAreSquare) {
            if (allAreWide && (avgRatio > 1.4 * maxRatio) && (Math.Abs(ratios[1] - ratios[0]) < 0.2)) {
                // Two wide photos, one above the other
                var h = Math.Max(Math.Min(MaxWidth / ratios[0], Math.Min(MaxWidth / ratios[1], (MaxHeight - Gap) / 2f)), MinHeight / 2f);
                Width = MaxWidth;
                Height = Round((h * 2) + Gap);
                ColumnSizes = [Width];
                RowSizes = [Round(h), Round(h)];
                Tiles = [new(1, 1, 0, 0), new(1, 1, 0, 1)];
            } else if (allAreWide) {
                // Two wide photos, one above the other, different ratios
                Width = MaxWidth;
                var h0 = MaxWidth / ratios[0];
                var h1 = MaxWidth / ratios[1];
                if (h0 + h1 < MinHeight) {
                    var prevTotalHeight = h0 + h1;
                    h0 = MinHeight * (h0 / prevTotalHeight);
                    h1 = MinHeight * (h1 / prevTotalHeight);
                }
                Height = Round(h0 + h1 + Gap);
                RowSizes = [Round(h0), Round(h1)];
                ColumnSizes = [MaxWidth];
                Tiles = [new(1, 1, 0, 0), new(1, 1, 0, 1)];
            } else if (allAreSquare) {
                // Next to each other, same ratio
                var w = (MaxWidth - Gap) / 2;
                var h = Math.Max(Math.Min(w / ratios[0], Math.Min(w / ratios[1], MaxHeight)), MinHeight);
                Width = MaxWidth;
                Height = Round(h);
                ColumnSizes = [Round(w), MaxWidth - Round(w)];
                RowSizes = [Round(h)];
                Tiles = [new(1, 1, 0, 0), new(1, 1, 1, 0)];
            } else {
                // Next to each other, different ratios
                var w0 = (MaxWidth - Gap) / ratios[1] / ((1 / ratios[0]) + (1 / ratios[1]));
                var w1 = MaxWidth - w0 - Gap;
                var h = Math.Max(Math.Min(MaxHeight, Math.Min(w0 / ratios[0], w1 / ratios[1])), MinHeight);
                ColumnSizes = [Round(w0), Round(w1)];
                RowSizes = [Round(h)];
                Width = Round(w0 + w1 + Gap);
                Height = Round(h);
                Tiles = [new(1, 1, 0, 0), new(1, 1, 1, 0)];
            }
        }

        private void LayoutThree(List<float> ratios, float maxRatio, float avgRatio, bool allAreWide) {
            if ((ratios[0] > 1.2 * maxRatio) || (avgRatio > 1.5 * maxRatio) || allAreWide) {
                // 2nd and 3rd photos are on the next line
                var hCover = Math.Min(MaxWidth / ratios[0], (MaxHeight - Gap) * 0.66f);
                var w2 = (MaxWidth - Gap) / 2;
                var h = Math.Min(MaxHeight - hCover - Gap, Math.Min(w2 / ratios[1], w2 / ratios[2]));
                if (hCover + h < MinHeight) {
                    var prevTotalHeight = hCover + h;
                    hCover = MinHeight * (hCover / prevTotalHeight);
                    h = MinHeight * (h / prevTotalHeight);
                }
                Width = MaxWidth;
                Height = Round(hCover + h + Gap);
                ColumnSizes = [Round(w2), MaxWidth - Round(w2)];
                RowSizes = [Round(hCover), Round(h)];
                Tiles = [new(2, 1, 0, 0), new(1, 1, 0, 1), new(1, 1, 1, 1)];
            } else {
                // 2nd and 3rd photos are on the right part
                var height = Math.Min(MaxHeight, MaxWidth * 0.66f / avgRatio);
                var wCover = Math.Min(height * ratios[0], (MaxWidth - Gap) * 0.66f);
                var h1 = ratios[1] * (height - Gap) / (ratios[2] + ratios[1]);
                var h0 = height - h1 - Gap;
                var w = Math.Min(MaxWidth - wCover - Gap, Math.Min(h1 * ratios[2], h0 * ratios[1]));
                Width = Round(wCover + w + Gap);
                Height = Round(height);
                ColumnSizes = [Round(wCover), Round(w)];
                RowSizes = [Round(h0), Round(h1)];
                Tiles = [new(1, 2, 0, 0), new(1, 1, 1, 0), new(1, 1, 1, 1)];
            }
        }

        private void LayoutFour(List<float> ratios, float maxRatio, float avgRatio, bool allAreWide) {
            if ((ratios[0] > 1.2 * maxRatio) || (avgRatio > 1.5 * maxRatio) || allAreWide) {
                // 2nd, 3rd and 4th photos are on the next line
                var hCover = Math.Min(MaxWidth / ratios[0], (MaxHeight - Gap) * 0.66f);
                var h = (MaxWidth - (2 * Gap)) / (ratios[1] + ratios[2] + ratios[3]);
                var w0 = h * ratios[1];
                var w1 = h * ratios[2];
                h = Math.Min(MaxHeight - hCover - Gap, h);
                if (hCover + h < MinHeight) {
                    var prevTotalHeight = hCover + h;
                    hCover = MinHeight * (hCover / prevTotalHeight);
                    h = MinHeight * (h / prevTotalHeight);
                }
                Width = MaxWidth;
                Height = Round(hCover + h + Gap);
                ColumnSizes = [Round(w0), Round(w1), MaxWidth - Round(w0) - Round(w1)];
                RowSizes = [Round(hCover), Round(h)];
                Tiles = [new(3, 1, 0, 0), new(1, 1, 0, 1), new(1, 1, 1, 1), new(1, 1, 2, 1)];
            } else {
                // 2nd, 3rd and 4th photos are on the right part
                var height = Math.Min(MaxHeight, MaxWidth * 0.66f / avgRatio);
                var wCover = Math.Min(height * ratios[0], (MaxWidth - Gap) * 0.66f);
                var w = (height - (2 * Gap)) / ((1 / ratios[1]) + (1 / ratios[2]) + (1 / ratios[3]));
                var h0 = w / ratios[1];
                var h1 = w / ratios[2];
                var h2 = (w / ratios[3]) + Gap;
                w = Math.Min(MaxWidth - wCover - Gap, w);
                Width = Round(wCover + Gap + w);
                Height = Round(height);
                ColumnSizes = [Round(wCover), Round(w)];
                RowSizes = [Round(h0), Round(h1), Round(h2)];
                Tiles = [new(1, 3, 0, 0), new(1, 1, 1, 0), new(1, 1, 1, 1), new(1, 1, 1, 2)];
            }
        }

        private void LayoutMany(List<float> ratios, float avgRatio) {
            var count = ratios.Count;
            var ratiosCropped = ratios.Select(x => (avgRatio > 1.1) ? Math.Max(1f, x) : Math.Min(1f, x)).ToList();

            var tries = new List<(int[] Configuration, float[] Heights)> {
                // One line
                ([count], [GetMultiThumbsHeight(ratiosCropped, MaxWidth, Gap)]),
            };
            // Two lines
            for (var firstLine = 1; firstLine <= count - 1; firstLine++) {
                tries.Add(([firstLine, count - firstLine], [
                    GetMultiThumbsHeight(ratiosCropped.GetRange(0, firstLine), MaxWidth, Gap),
                    GetMultiThumbsHeight(ratiosCropped.GetRange(firstLine, count - firstLine), MaxWidth, Gap),
                ]));
            }
            // Three lines
            for (var firstLine = 1; firstLine <= count - 2; firstLine++) {
                for (var secondLine = 1; secondLine <= count - firstLine - 1; secondLine++) {
                    tries.Add(([firstLine, secondLine, count - firstLine - secondLine], [
                        GetMultiThumbsHeight(ratiosCropped.GetRange(0, firstLine), MaxWidth, Gap),
                        GetMultiThumbsHeight(ratiosCropped.GetRange(firstLine, secondLine), MaxWidth, Gap),
                        GetMultiThumbsHeight(ratiosCropped.GetRange(firstLine + secondLine, count - firstLine - secondLine), MaxWidth, Gap),
                    ]));
                }
            }

            // Looking for minimum difference between thumbs block height and maxHeight (may probably be little over)
            var realMaxHeight = Math.Min(MaxHeight, MaxWidth);
            int[]? optConf = null;
            float[] optHeights = [];
            float optDiff = 0;
            foreach (var (conf, heights) in tries) {
                var confH = (Gap * (heights.Length - 1)) + heights.Sum();
                var confDiff = Math.Abs(confH - realMaxHeight);
                if ((conf.Length > 1) && ((conf[0] > conf[1]) || ((conf.Length > 2) && (conf[1] > conf[2])))) {
                    confDiff *= 1.1f;
                }
                if ((optConf is null) || (confDiff < optDiff)) {
                    optConf = conf;
                    optHeights = heights;
                    optDiff = confDiff;
                }
            }

            Width = MaxWidth;
            RowSizes = new int[optHeights.Length];
            Tiles = new Tile[count];
            float totalHeight = 0;
            var gridLineOffsets = new List<int>();
            var rowTiles = new List<List<Tile>>(optHeights.Length);

            var k = 0;
            for (var i = 0; i < optConf!.Length; i++) {
                var lineChunksNum = optConf[i];
                var lineHeight = optHeights[i];
                totalHeight += lineHeight;
                RowSizes[i] = Round(lineHeight);
                var totalWidth = 0;
                var row = new List<Tile>();
                for (var j = 0; j < lineChunksNum; j++) {
                    var thumbRatio = ratiosCropped[k];
                    var w = (j == lineChunksNum - 1) ? (MaxWidth - totalWidth) : (thumbRatio * lineHeight);
                    totalWidth += Round(w);
                    if ((j < lineChunksNum - 1) && !gridLineOffsets.Contains(totalWidth)) {
                        gridLineOffsets.Add(totalWidth);
                    }
                    var tile = new Tile(1, 1, 0, i) { Width = Round(w) };
                    Tiles[k] = tile;
                    row.Add(tile);
                    k++;
                }
                rowTiles.Add(row);
            }
            gridLineOffsets.Sort();
            gridLineOffsets.Add(MaxWidth);
            ColumnSizes = new int[gridLineOffsets.Count];
            ColumnSizes[0] = gridLineOffsets[0];
            for (var i = gridLineOffsets.Count - 1; i > 0; i--) {
                ColumnSizes[i] = gridLineOffsets[i] - gridLineOffsets[i - 1];
            }

            foreach (var row in rowTiles) {
                var columnOffset = 0;
                foreach (var tile in row) {
                    tile.StartColumn = columnOffset;
                    var width = 0;
                    tile.ColumnSpan = 0;
                    for (var i = columnOffset; i < ColumnSizes.Length; i++) {
                        width += ColumnSizes[i];
                        tile.ColumnSpan++;
                        if (width == tile.Width) {
                            break;
                        }
                    }
                    columnOffset += tile.ColumnSpan;
                }
            }
            Height = Round(totalHeight + (Gap * (optHeights.Length - 1)));
        }
    }

    private void Grid_Loaded(object sender, RoutedEventArgs e) {
        // Only observe while loaded so a long-lived collection doesn't keep this grid alive.
        ObserveCollection(ItemsSource as INotifyCollectionChanged);
        GenerateChildren();
    }

    private void Grid_Unloaded(object sender, RoutedEventArgs e) {
        ObserveCollection(null);
    }

    public MediaGrid() {
        InitializeComponent();
    }
}
