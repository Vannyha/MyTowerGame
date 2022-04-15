using UnityEngine;

namespace Helpers.SlowUpdate
{
    public class SlowUpdateProc
    {
        private readonly SlowUpdateDelegate slowUpdate;
        private float updateTime;
        private float updateCurrentTime;

        public SlowUpdateProc(SlowUpdateDelegate slowUpdate, float updateTime)
        {
            this.slowUpdate = slowUpdate;
            this.updateTime = updateTime;
        }

        public delegate void SlowUpdateDelegate();
        
        public void ProceedOnFixedUpdate(float deltaTime = -1)
        {
            float dt = deltaTime < 0 ? Time.deltaTime : deltaTime;
            if (updateCurrentTime <= 0)
            {
                slowUpdate();
                updateCurrentTime += updateTime;
            }
            updateCurrentTime -= dt;
        }

        public void CallTick(bool resetTimer = true)
        {
            slowUpdate();
            
            if (resetTimer)
            {
                Reset();
            }
        }

        public void Reset()
        {
            updateCurrentTime = updateTime;
        }

        public float DeltaTime
        {
            get { return updateTime - updateCurrentTime; }
        }

        public float UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
    }
}