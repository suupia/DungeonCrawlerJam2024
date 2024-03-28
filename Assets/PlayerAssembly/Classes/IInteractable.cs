using UnityEngine.Events;
using DungeonCrawler.Runtime.Player;

namespace DungeonCrawler.Runtime.Interactable
{
    internal interface IInteractable
    {
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        public void Interact(Interactor interactor, out bool successful);

        public void EndInteraction();
    }
}
