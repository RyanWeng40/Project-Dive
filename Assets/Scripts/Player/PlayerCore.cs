using UnityEngine;

using MyBox;

namespace Player
{
    [RequireComponent(typeof(PlayerActor))]
    [RequireComponent(typeof(PlayerSpawnManager))]
    public class PlayerCore : Singleton<PlayerCore>
    {
        #region Player Properties
        [Foldout("Move", true)]
        [SerializeField] private int moveSpeed;
        public static int MoveSpeed => Instance.moveSpeed;
        [SerializeField] private int maxAcceleration;
        public static int MaxAcceleration => Instance.maxAcceleration;  
        [SerializeField] private int maxAirAcceleration;
        public static int MaxAirAcceleration => Instance.maxAirAcceleration;
        [SerializeField] private int maxDeceleration;
        public static int MaxDeceleration =>Instance. maxDeceleration;
        [Tooltip("Timer between the player crashing into a wall and them getting their speed back (called a cornerboost)")]
        [SerializeField] private float cornerboostTimer;
        public static float CornerboostTimer => Instance.cornerboostTimer;
        [Tooltip("Cornerboost speed multiplier")]
        [SerializeField] private float cornerboostMultiplier;
        public static float CornerboostMultiplier => Instance.cornerboostMultiplier;

        [Foldout("Jump", true)]
        [SerializeField] private int jumpHeight;
        public static int JumpHeight => Instance.jumpHeight;
        [SerializeField] private int doubleJumpHeight;
        public static int DoubleJumpHeight => Instance.doubleJumpHeight;
        [SerializeField] private float jumpCoyoteTime;
        public static float JumpCoyoteTime => Instance.jumpCoyoteTime;
        [SerializeField] private float jumpBufferTime;
        public static float JumpBufferTime => Instance.jumpBufferTime;
        [SerializeField, Range(0f, 1f)] private float jumpCutMultiplier;
        public static float JumpCutMultiplier => Instance.jumpCutMultiplier;

        [Foldout("Dive", true)]
        [SerializeField] private int diveVelocity;
        public static int DiveVelocity => Instance.diveVelocity;
        [SerializeField] private int diveDeceleration;
        public static int DiveDeceleration => Instance.diveDeceleration;

        [Foldout("Dogo", true)]
        [SerializeField] private float dogoJumpHeight;
        public static float DogoJumpHeight => Instance.dogoJumpHeight;
        [SerializeField] private float dogoJumpXV;
        public static float DogoJumpXV => Instance.dogoJumpXV;
        [SerializeField] private int dogoJumpAcceleration;
        public static int DogoJumpAcceleration => Instance.dogoJumpAcceleration;
        [Tooltip("Time where acceleration/decelartion is 0")]
        [SerializeField] private float dogoJumpTime;
        public static float DogoJumpTime => Instance.dogoJumpTime;
        [SerializeField] private float dogoConserveXVTime;
        public static float DogoConserveXVTime => Instance.dogoConserveXVTime;
        [Tooltip("Time to let players input a direction change")]
        [SerializeField] private float dogoJumpGraceTime;
        public static float DogoJumpGraceTime => Instance.dogoJumpGraceTime;

        [Foldout("RoomTransitions", true)]
        [SerializeField, Range(0f, 1f)] private float roomTransitionVCutX = 0.5f;
        public static float RoomTransitionVCutX => Instance.roomTransitionVCutX;
        [SerializeField, Range(0f, 1f)] private float roomTransitionVCutY = 0.5f;
        public static float RoomTransistionVCutY => Instance.roomTransitionVCutY;
        #endregion

        public static PlayerStateMachine StateMachine { get; private set; }
        public static IInputController Input { get; private set; }
        public static PlayerActor Actor { get; private set; }
        public static PlayerSpawnManager SpawnManager { get; private set; }

        private void Awake()
        {
            StateMachine = GetComponent<PlayerStateMachine>();
            Input = GetComponent<PlayerInputController>();
            Actor = GetComponent<PlayerActor>();
            SpawnManager = GetComponent<PlayerSpawnManager>();
        }
    }
}