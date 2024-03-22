
namespace DungeonCrawler.Core.FSM
{
    internal interface ITransitionCallback
    {
        public abstract void BeforeTransition();
        public abstract void AfterTransition();
    }
}
