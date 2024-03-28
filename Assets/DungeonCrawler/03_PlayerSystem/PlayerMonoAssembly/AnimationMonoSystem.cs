using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonCrawler.PlayerMonoAssembly
{
    sealed class AnimationMonoSystem
    {
        Dictionary<MonoBehaviour, Queue<AnimationRequest>> _animationQueue = new();

        /// <summary>
        /// Ensure an animation is completely stoped.
        /// </summary>
        /// <param name="request">An animation request.</param>
        private void EnsureCoroutineHasStopped(AnimationRequest request)
        {
            if (request.coroutine != null)
            {
                request.value.StopCoroutine(request.coroutine);
                request.coroutine = null;
            }
        }

        /// <summary>
        /// Generic animation coroutine.
        /// </summary>
        /// <param name="request">An animation request.</param>
        /// <returns></returns>
        private IEnumerator GenrticAnimationCoroutine(AnimationRequest request)
        {
            float elapsedTime = 0.0f;
            float progress = 0.0f;

            while (progress <= 1.0f)
            {
                request.animationFunction(progress);
                elapsedTime += Time.unscaledDeltaTime;
                progress = elapsedTime / request.duration;
                yield return null;
            }

            request.animationFunction(1.0f);
            request.onComplete?.Invoke();

            request.hasCompleted = true;
        }

        /// <summary>
        /// Start an animations.
        /// </summary>
        /// <param name="request">An animation request.</param>
        private void StartAnimationCoroutine(AnimationRequest request)
        {
            request.hasStarted = true;
            request.coroutine = request.value.StartCoroutine(GenrticAnimationCoroutine(request));
        }

        /// <summary>
        /// Request an animation to be played.
        /// </summary>
        /// <param name="request">An animation request.</param>
        /// <param name="force">If the animation should be ran right away or queued.</param>
        public void RequestAnimation(AnimationRequest request, bool force = false)
        {
            if (force)
            {
                StartAnimationCoroutine(request);
                return;
            }

            if (!_animationQueue.ContainsKey(request.value))
            {
                _animationQueue.Add(request.value, new Queue<AnimationRequest>());
                _animationQueue[request.value].Enqueue(request);
            }
            else
            {
                _animationQueue[request.value].Enqueue(request);
            }
        }

        /// <summary>
        /// Request an animation to be played.
        /// </summary>
        /// <param name="value">MonoBehaviour to attached coroutine to.</param>
        /// <param name="duration">The duration of the animation</param>
        /// <param name="animationFunction">Callback to run on each interation of the animation.</param>
        /// <param name="onComplete">Callback to be called when the animation is complete</param>
        /// <param name="force">If the animation should be ran right away or queued.</param>
        public void RequestAnimation(MonoBehaviour value, float duration, Action<float> animationFunction, Action onComplete = null, bool force = false)
        {
            RequestAnimation(new AnimationRequest(value, duration, animationFunction, onComplete), force);
        }

        /// <summary>
        /// Stops all animations attached to a MonoBehaviour object.
        /// </summary>
        /// <param name="value">MonoBehaviour to attached coroutine to.</param>
        public void StopAllAnimations(MonoBehaviour value)
        {
            if (value == null || !_animationQueue.ContainsKey(value)) return;

            foreach (AnimationRequest request in _animationQueue[value])
            {
                if (request.coroutine != null) value.StopCoroutine(request.coroutine);
            }

            _animationQueue[value].Clear();
        }
        
        public void Update()
        {
            if (_animationQueue == null || _animationQueue.Count == 0) return;

            List<MonoBehaviour> keysToRemove = new List<MonoBehaviour>();

            foreach (KeyValuePair<MonoBehaviour, Queue<AnimationRequest>> animator in _animationQueue)
            {
                if (animator.Value != null && animator.Value.Any())
                {
                    if (!animator.Value.Peek().hasStarted) StartAnimationCoroutine(animator.Value.Peek());
                    else if (animator.Value.Peek().hasCompleted) EnsureCoroutineHasStopped(animator.Value.Dequeue());
                }
                else keysToRemove.Add(animator.Key);
            }

            foreach (MonoBehaviour key in keysToRemove)
            {
                _animationQueue.Remove(key);
            }
        }
    }
}
