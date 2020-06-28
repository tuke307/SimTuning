using Xamarin.Forms;

namespace SimTuning.mobile.Behaviors
{
    internal class LazyContentPageBehavior : LoadContentOnActivateBehavior<ContentPage>
    {
        protected override void SetContent(ContentPage page, View contentView)
        {
            page.Content = contentView;
        }
    }
}