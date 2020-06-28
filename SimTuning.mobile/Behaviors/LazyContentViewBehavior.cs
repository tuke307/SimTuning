using Xamarin.Forms;

namespace SimTuning.mobile.Behaviors
{
    internal class LazyContentViewBehavior : LoadContentOnActivateBehavior<ContentView>
    {
        protected override void SetContent(ContentView element, View contentView)
        {
            element.Content = contentView;
        }
    }
}