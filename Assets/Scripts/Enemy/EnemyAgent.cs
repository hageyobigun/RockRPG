﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class EnemyAgent : Agent, Enemy.IAttackable
{
    [SerializeField] private int hpValue = 1;
    [SerializeField] private int _enemyPos = 4;
    [SerializeField] private GameObject player = null;

    private int[] moveList = { -3, -1, 1, 3 };

    private EnemyMove _enemyMove ;
    private EnemyAttacks _enemyAttacks;
    private PlayerAgent _playerAgent;

    protected bool IsDead() => --hpValue <= 0;

    //プロパティー
    public int GetHpValue
    {
        get { return this.hpValue; }  //取得用
        private set { this.hpValue = value; } //値入力用
    }

    public void Attacked(float damage)
    {
        IsDead();
    }

    public override void Initialize()
    {
        _enemyMove = new EnemyMove(_enemyPos, gameObject);
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _playerAgent = player.GetComponent<PlayerAgent>();
    }

    //エピソード開始時
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        hpValue = 5;
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(player.transform.position);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        //StartCoroutine(move(vectorAction));

        int move = (int)vectorAction[0];
        int attack = (int)vectorAction[1];

        bool isMove = _enemyMove.IsStage(moveList[move]);
        if (attack == 1) _enemyAttacks.BulletAttack();

        if (_playerAgent != null)
        {
            if (_playerAgent.GetHpValue <= 0)
            {
                AddReward(1.0f);
                EndEpisode();
            }
        }
        if (isMove)
        {
            _enemyMove.Move();
        }
        if (hpValue <= 0)
        {
            EndEpisode();
        }
    }

    //IEnumerator move(float[] vectorAction)
    //{
    //    int move = (int)vectorAction[0];
    //    int attack = (int)vectorAction[1];

    //    yield return new WaitForSeconds(1.0f);

    //    bool isMove = _enemyMove.IsStage(moveList[move]);
    //    if (attack == 1) _enemyAttacks.BulletAttack();

    //    if (isMove)
    //    {
    //        _enemyMove.Move();
    //        AddReward(0.5f);
    //    }
    //    else
    //    {
    //        AddReward(-0.1f);
    //    }

    //    if (hpValue < 0)
    //    {
    //        AddReward(-0.5f);
    //        EndEpisode();
    //    }
    //}

}