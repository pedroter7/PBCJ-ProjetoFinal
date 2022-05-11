using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Script responsavel por suavizar e corrigir a posi��o da camera.
/// </summary>
public class ArredondaPosCamera : CinemachineExtension
{

    public float PixelsPerUnit = 32; //Quantidade de pixels que cada tile tem no jogo.

    //Corrige a posi��o da camera p�s movimento
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body) //Corrige a posi��o da camera quando o movimento � finalizado.
        {
            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);
            state.PositionCorrection += pos2 - pos;
        }
    }

    //Respons�vel por arredondar os valores de posi��o de acordo com os PixelsPerUnit
    float Round(float value)
    {
        return Mathf.Round(value * PixelsPerUnit) / PixelsPerUnit;
    }

}
