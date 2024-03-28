#nullable enable
using UnityEngine.Events;

namespace DungeonCrawler.PlayerAssembly.Classes
{
    internal interface IInteractable
    {
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        public void Interact(Interactor interactor, out bool successful);

        public void EndInteraction();
    }
}
