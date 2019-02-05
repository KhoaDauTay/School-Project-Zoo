using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Animals;
using CagedItems;
using Utilities;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for CageWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class CageWindow : Window
    {
        /// <summary>
        /// The window's cage.
        /// </summary>
        private Cage cage;

        /// <summary>
        /// Initializes a new instance of the CageWindow class.
        /// </summary>
        /// <param name="cage">The window's cage.</param>
        public CageWindow(Cage cage)
        {
            this.InitializeComponent();
            this.cage = cage;
            this.cage.OnImageUpdate = item =>
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(delegate()
                    {
                        int zIndex = 0;

                        // Loop through view box.
                        foreach (Viewbox v in this.cageGrid.Children)
                        {
                            if (v.Tag == item)
                            {
                                cageGrid.Children.Remove(v);

                                break;
                            }

                            zIndex++;
                        }

                        if (item.IsActive)
                        {
                            this.DrawItem(item, zIndex);
                        }
                    }));
                }
                catch (TaskCanceledException)
                {
                }
            };
        }

        /// <summary>
        /// Gets a view box with set positioning.
        /// </summary>
        /// <param name="maxX">The max x location.</param>
        /// <param name="maxY">The max y location.</param>
        /// <param name="xPos">The current x position.</param>
        /// <param name="yPos">The current y position.</param>
        /// <param name="resourceKey">The key that defines the animal to be drawn.</param>
        /// <param name="displayScale">Changes the default size based on it's scale.</param>
        /// <returns>The finished view box.</returns>
        private Viewbox GetViewBox(double maxX, double maxY, int xPos, int yPos, string resourceKey, double displayScale)
        {
            // Create a canvas of the item to draw
            Canvas canvas = Application.Current.Resources[resourceKey] as Canvas;

            // Finished viewbox.
            Viewbox finishedViewBox = new Viewbox();

            // Gets image ratio.
            double imageRatio = canvas.Width / canvas.Height;

            // Sets width to a percent of the window size based on it's scale.
            double itemWidth = this.cageGrid.ActualWidth * 0.2 * displayScale;

            // Sets the height to the ratio of the width.
            double itemHeight = itemWidth / imageRatio;

            // Sets the width of the viewbox to the size of the canvas.
            finishedViewBox.Width = itemWidth;
            finishedViewBox.Height = itemHeight;

            // Sets the animals location on the screen.
            double xPercent = (this.cageGrid.ActualWidth - itemWidth) / maxX;
            double yPercent = (this.cageGrid.ActualHeight - itemHeight) / maxY;

            int posX = Convert.ToInt32(xPos * xPercent);
            int posY = Convert.ToInt32(yPos * yPercent);

            finishedViewBox.Margin = new Thickness(posX, posY, 0, 0);

            // Adds the canvas to the view box.
            finishedViewBox.Child = canvas;

            // Returns the finished viewbox.
            return finishedViewBox;
        }

        /// <summary>
        /// Draws the item to the screen.
        /// </summary>
        /// <param name="item">The item to draw.</param>
        /// <param name="zIndex">The index of the item.</param>
        private void DrawItem(ICageable item, int zIndex)
        {
            // Resource key.
            string resourceKey = item.ResourceKey;

            // Gets the view box.
            Viewbox animalViewbox = this.GetViewBox(800, 400, item.XPosition, item.YPosition, resourceKey, item.DisplaySize);

            // Aligns the view box to the top left of the grid.
            animalViewbox.HorizontalAlignment = HorizontalAlignment.Left;
            animalViewbox.VerticalAlignment = VerticalAlignment.Top;

            TransformGroup unconsciousTransformGroup = new TransformGroup();

            // Label
            if (item is Animal)
            {
                Label animalLabel = new Label();
                animalLabel.Content = (item as Animal).Name;
                animalLabel.Height = 30;
                animalLabel.FontSize = 16;
                animalLabel.Width = animalViewbox.Width;
                animalLabel.HorizontalContentAlignment = HorizontalAlignment.Center;

                animalLabel.Margin = new Thickness(animalViewbox.Margin.Left, animalViewbox.Margin.Top - animalLabel.Height, 0, 0);
                animalLabel.HorizontalAlignment = HorizontalAlignment.Left;
                animalLabel.VerticalAlignment = VerticalAlignment.Top;         
            }

            // If the animal is moving to the left
            if (item.XDirection == HorizontalDirection.Left)
            {
                // Set the origin point of the transformation to the middle of the viewbox
                animalViewbox.RenderTransformOrigin = new Point(0.5, 0.5);

                // Initialize a ScaleTransform instance
                ScaleTransform flipTransform = new ScaleTransform();

                // Flip the viewbox horizontally so the animal faces to the left.
                flipTransform.ScaleX = -1;

                // Adds flip transform to group.
                unconsciousTransformGroup.Children.Add(flipTransform);

                // Apply the ScaleTransform to the viewbox
                animalViewbox.RenderTransform = flipTransform;
            }

            // Create a new SkewTransform and set its Angle to 30 degrees in the direction the cageable is facing.
            SkewTransform unconsciousSkew = new SkewTransform();
            unconsciousSkew.AngleX = item.XDirection == HorizontalDirection.Left ? 30.0 : -30.0;

            // Creates a new ScaleTransform and set it to half the height and three fourths of the width.
            ScaleTransform unconsciousScale = new ScaleTransform();
            unconsciousScale.ScaleY = unconsciousScale.ScaleY * .5;
            unconsciousScale.ScaleX = unconsciousScale.ScaleX * .75;

            // Adds transforms to the group.
            unconsciousTransformGroup.Children.Add(unconsciousSkew);
            unconsciousTransformGroup.Children.Add(unconsciousScale);

            if (item.HungerState == HungerState.Unconscious)
            {
                // Adds the group to the render transform.
                animalViewbox.RenderTransform = unconsciousTransformGroup;
            }

            // Stores the object in the vivewbox for future reference.
            animalViewbox.Tag = item;

            // Add the viewbox to the grid.
            this.cageGrid.Children.Insert(zIndex, animalViewbox);

            // Increment index.
            zIndex++;
        }

        /// <summary>
        /// Draws all animals in the cage window.
        /// </summary>
        private void DrawAllItems()
        {
            this.cageGrid.Children.Clear();

            int zIndex = 0;

            this.cage.CagedItems.ToList().ForEach(c => this.DrawItem(c, zIndex));
        }

        /// <summary>
        /// Handles the redrawing of items.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void RedrawHandler(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(delegate()
                {
                    this.DrawAllItems();
                }));
            }
            catch (TaskCanceledException)
            {
            }
        }

        /// <summary>
        /// Loads the window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments of the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawAllItems();
        }

        /// <summary>
        /// When the window closes, remove on image update.
        /// </summary>
        /// <param name="sender">The object of the sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.cage.OnImageUpdate = null;
        }
    }
}